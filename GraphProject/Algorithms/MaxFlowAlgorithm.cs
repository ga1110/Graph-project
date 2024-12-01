using GraphProject.Structures;
using System;
using System.Collections.Generic;
using GraphProject.Handlers;

public static class MaxFlowSolver
{
    // Метод для нахождения максимального потока в графе от источника (source) к стоку (sink)
    public static double FindMaxFlow(Graph graph, Vertex source, Vertex sink)
    {
        double maxFlow = 0; // Инициализируем максимальный поток значением 0
        var edgeToParent = new Dictionary<Vertex, Edge>(); // Словарь для хранения пути увеличения: вершина -> ребро, по которому мы пришли в эту вершину
        var adjacencyList = GraphManager.GetAdj(graph); // Получаем список смежности графа

        // Пока существует путь увеличения от источника к стоку
        while (BFS(graph, source, sink, edgeToParent))
        {
            // Инициализируем поток вдоль пути максимальным возможным значением
            double pathFlow = double.MaxValue;

            // Проходим по найденному пути от стока к источнику, чтобы найти минимальную остаточную емкость (узкое место)
            for (Vertex v = sink ; v != source ;)
            {
                Edge edge = edgeToParent[v]; // Получаем ребро, по которому пришли в вершину v
                double edgeResidualCapacity = edge.ResidualCapacity ?? throw new Exception($"У ребра {edge} не задана емкость"); // Остаточная емкость ребра
                pathFlow = Math.Min(pathFlow, edgeResidualCapacity); // Обновляем pathFlow до минимальной остаточной емкости на пути
                v = edge.Source; // Переходим к предыдущей вершине в пути
            }

            // Обновляем потоки по ребрам на найденном пути
            for (Vertex v = sink ; v != source ;)
            {
                Edge edge = edgeToParent[v]; // Получаем ребро, по которому пришли в вершину v
                edge.Flow += pathFlow; // Увеличиваем поток по ребру на значение pathFlow

                // Ищем обратное ребро для корректного подсчета остаточных емкостей
                Edge reverseEdge = null;

                // Получаем список ребер, исходящих из вершины edge.Destination (текущей вершины в обратном обходе)
                if (adjacencyList.TryGetValue(edge.Destination, out var edges))
                {
                    // Ищем среди них ребро, ведущее к edge.Source (обратное ребро)
                    reverseEdge = edges.Find(e => e.Destination.Equals(edge.Source));
                }

                if (reverseEdge == null)
                {
                    // Если обратного ребра нет, создаем его с нулевой емкостью
                    reverseEdge = new Edge(edge.Destination, edge.Source, 0);

                    // Добавляем новое ребро в список смежности
                    if (!adjacencyList.ContainsKey(edge.Destination))
                    {
                        adjacencyList[edge.Destination] = new List<Edge>();
                    }
                    adjacencyList[edge.Destination].Add(reverseEdge);
                }

                reverseEdge.Flow -= pathFlow; // Уменьшаем поток по обратному ребру на значение pathFlow
                v = edge.Source; // Переходим к предыдущей вершине в пути
            }

            maxFlow += pathFlow; // Увеличиваем общий максимальный поток на значение pathFlow

            // Очищаем словарь edgeToParent для следующей итерации
            edgeToParent.Clear();
        }

        return maxFlow; // Возвращаем вычисленный максимальный поток
    }

    // Метод для поиска пути увеличения с помощью поиска в ширину (BFS)
    private static bool BFS(Graph graph, Vertex source, Vertex sink, Dictionary<Vertex, Edge> edgeToParent)
    {
        var adjacencyList = GraphManager.GetAdj(graph); // Получаем список смежности графа
        var visited = new HashSet<Vertex>(); // Множество посещенных вершин
        var queue = new Queue<Vertex>(); // Очередь для реализации BFS

        queue.Enqueue(source); // Добавляем источник в очередь
        visited.Add(source); // Помечаем источник как посещенный

        while (queue.Count > 0)
        {
            Vertex u = queue.Dequeue(); // Извлекаем вершину из очереди

            // Получаем список ребер, исходящих из вершины u
            if (adjacencyList.TryGetValue(u, out var edges))
            {
                foreach (Edge edge in edges)
                {
                    Vertex v = edge.Destination; // Получаем вершину назначения ребра

                    // Если вершина v не посещена и остаточная емкость ребра больше 0
                    if (!visited.Contains(v) && edge.ResidualCapacity > 0)
                    {
                        visited.Add(v); // Помечаем вершину v как посещенную
                        edgeToParent[v] = edge; // Сохраняем ребро, по которому мы пришли в v

                        // Если достигли стока, возвращаем true (путь найден)
                        if (v.Equals(sink))
                        {
                            return true;
                        }

                        queue.Enqueue(v); // Добавляем вершину v в очередь для дальнейшего поиска
                    }
                }
            }
        }

        return false; // Если путь до стока не найден, возвращаем false
    }
}
