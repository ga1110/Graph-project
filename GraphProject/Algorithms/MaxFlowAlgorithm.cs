using GraphProject.Handlers;
using GraphProject.Structures;
using System.Collections.Generic;
using System;

public static class MaxFlowSolver
{
    public static double FindMaxFlow(Graph graph, Vertex source, Vertex sink)
    {
        if (graph == null)
            throw new ArgumentNullException(nameof(graph));
        if (source == null || sink == null)
            throw new ArgumentNullException("Источник или сток не могут быть null");

        double maxFlow = 0;
        var adjacencyList = GraphManager.GetAdj(graph);

        // Инициализируем потоки по всем ребрам
        foreach (var edges in adjacencyList.Values)
        {
            foreach (var edge in edges)
            {
                edge.Flow = 0;
            }
        }

        var parentMap = new Dictionary<Vertex, Edge>();

        while (BFS(graph, source, sink, parentMap))
        {
            // Находим минимальную остаточную емкость по пути
            double pathFlow = double.MaxValue;

            for (Vertex v = sink ; v != source ;)
            {
                Edge edge = parentMap[v];
                var edgeFlow = edge.Flow;
                var edgeCapacity = edge.Capacity ?? throw new Exception($"{edge.ToString()} - не имеет ёмкости");
                pathFlow = Math.Min(pathFlow, edgeCapacity - edgeFlow);
                v = edge.Source;
            }

            // Обновляем потоки и остаточные емкости по пути
            for (Vertex v = sink ; v != source ;)
            {
                Edge edge = parentMap[v];
                edge.Flow += pathFlow;

                // Обработка обратного ребра
                Edge reverseEdge = FindEdge(adjacencyList, edge.Destination, edge.Source);
                if (reverseEdge == null)
                {
                    // Создаем обратное ребро с нулевым потоком и емкостью, равной потоку прямого ребра
                    reverseEdge = new Edge(edge.Destination, edge.Source, 0);
                    reverseEdge.Flow = 0;
                    adjacencyList[edge.Destination].Add(reverseEdge);
                }
                reverseEdge.Flow -= pathFlow;

                v = edge.Source;
            }

            maxFlow += pathFlow;
            parentMap.Clear();
        }

        return maxFlow;
    }

    private static bool BFS(Graph graph, Vertex source, Vertex sink, Dictionary<Vertex, Edge> parentMap)
    {
        var adjacencyList = GraphManager.GetAdj(graph);
        var visited = new HashSet<Vertex>();
        var queue = new Queue<Vertex>();

        queue.Enqueue(source);
        visited.Add(source);

        while (queue.Count > 0)
        {
            Vertex u = queue.Dequeue();

            if (adjacencyList.TryGetValue(u, out var edges))
            {
                foreach (Edge edge in edges)
                {
                    Vertex v = edge.Destination;

                    // Проверяем остаточную емкость
                    if (!visited.Contains(v) && edge.Capacity - edge.Flow > 0)
                    {
                        visited.Add(v);
                        parentMap[v] = edge;

                        if (v.Equals(sink))
                        {
                            return true;
                        }

                        queue.Enqueue(v);
                    }
                }
            }
        }

        return false;
    }

    private static Edge FindEdge(Dictionary<Vertex, List<Edge>> adjacencyList, Vertex source, Vertex destination)
    {
        if (adjacencyList.TryGetValue(source, out var edges))
        {
            return edges.Find(e => e.Destination.Equals(destination));
        }
        return null;
    }
}
