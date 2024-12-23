using GraphProject.Handlers;
using GraphProject.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GraphProject.Algorithms
{
    public class FordFulkerson
    {
        public static double MaxFlow(Graph graph, Vertex source, Vertex sink)
        {
            if (graph == null || source == null || sink == null)
                throw new ArgumentNullException("Граф, исток или сток не должны быть null");

            if (source.Equals(sink))
                throw new ArgumentException("Исток и сток не должны совпадать");

            IsGraphCorrectToAlgorithm(graph); // проверяем граф на корректность
            GraphManager.SetAllVertexID(graph);
            var adjacencyList = GraphManager.GetAdj(graph);
            int n = 0;
            foreach (var vertex in adjacencyList.Keys)
            {
                n++;
            }
            foreach (var edgeList in adjacencyList.Values)
            {
                foreach (var edge in edgeList)
                {
                    edge.Flow = 0;
                }
            }
            List<List<double>> residualGraph = new();

            for (int i = 0; i < n; i++)
            {
                residualGraph.Add(new List<double>());
                for (int j = 0; j < n; j++)
                {
                    residualGraph[i].Add(0);
                }
            }



            using (StreamWriter writer = new StreamWriter("F:\\Study\\Теория графов\\Graph-project\\Path.txt", true))
            {
                foreach(var vertex in adjacencyList.Keys)
                {
                    writer.WriteLine(vertex.ToString() + " = " + vertex.Id + "\n");
                }
            }
            WriteInFile(residualGraph);



            foreach (var element in adjacencyList)
            {
                var vertex = element.Key;
                foreach (var edge in element.Value)
                {
                    residualGraph[vertex.Id][edge.Destination.Id] = edge.Capacity ?? throw new ArgumentNullException("Ребро не содержит емкость");
                }
            }
            List<int> parent = Enumerable.Repeat(-1, n).ToList();
            double maxFlow = 0;
            List<List<List<double>>> residualGraphList = new();
            while (BFS(residualGraph, source.Id, sink.Id, parent))
            {
                // Находим минимальный поток на найденном пути
                double pathFlow = double.MaxValue;
                for (int v = sink.Id; v != source.Id; v = parent[v])
                {
                    int u = parent[v];
                    pathFlow = Math.Min(pathFlow, residualGraph[u][v]);
                }

                // Обновляем остаточную сеть
                for (int v = sink.Id; v != source.Id; v = parent[v])
                {
                    int u = parent[v];
                    residualGraph[u][v] -= pathFlow;
                    residualGraph[v][u] += pathFlow;
                }

                maxFlow += pathFlow;

                // Создаем глубокую копию residualGraph
                var residualGraphCopy = new List<List<double>>();
                foreach (var row in residualGraph)
                {
                    residualGraphCopy.Add(new List<double>(row)); // Копируем каждую строку
                }
                WriteInFile(residualGraph);
                // Добавляем копию в residualGraphList
                residualGraphList.Add(residualGraphCopy);
            }
            
            return maxFlow;
        }

        private static void IsGraphCorrectToAlgorithm(Graph graph)
        {
            var adjacencyList = GraphManager.GetAdj(graph);
            foreach (var element in adjacencyList)
            {
                foreach (var edge in element.Value)
                {
                    if (edge.Capacity == null)
                    {
                        throw new ArgumentNullException($"Вершина {edge} не имеет свойство Capacity");
                    }
                }
            }
        }

        private static bool BFS(List<List<double>> residualGraph, int source, int sink, List<int> parent)
        {
            int n = residualGraph.Count;
            var visited = new bool[n];

            var queue = new Queue<int>();
            queue.Enqueue(source);
            visited[source] = true;
            parent[source] = -1;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                for (int v = 0; v < n; ++v)
                {
                    if (!visited[v] && residualGraph[u][v] > 0)
                    {
                        queue.Enqueue(v);
                        parent[v] = u;
                        visited[v] = true;

                        if (v == sink)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static void WriteInFile(List<List<double>> residualGraph)
        {
            var strGraph = "";
            foreach (var elementList in residualGraph)
            {
                foreach (var element in elementList)
                {
                    strGraph += element + " ";
                }
                strGraph += "\n";
            }
            strGraph += "\n";
            using (StreamWriter writer = new StreamWriter("F:\\Study\\Теория графов\\Graph-project\\Path.txt", true))
            {
                writer.WriteLine(string.Join(", ", strGraph));
            }
        }
    }
}
// Создать словарь формата Dictionory <Vertex, Vertex> который проверяет существует ли путь из вершины A в B (нужно для вычеркивания лишних путей)