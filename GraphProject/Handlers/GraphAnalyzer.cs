using System;
using System.Collections.Generic;
using System.Linq;
using GraphProject.Algorithms;
using GraphProject.Structures;

namespace GraphProject.Handlers
{
    public static class GraphAnalyzer
    {
        /// <summary>
        /// Проверяет, является ли граф связным.
        /// </summary>
        /// <param name="graph">Граф, который необходимо проверить.</param>
        /// <returns>True, если граф связный, иначе - false.</returns>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        public static bool IsGraphConnected(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            var allVertex = graph.adjacencyList.Keys.ToList();
            var contAllVertex = allVertex.ToList().Count;

            List<List<Vertex>> traversalList = new();

            foreach (var vertex in allVertex)
            {
                traversalList.Add(GraphTraversal.RecursiveGraphTraversal(graph, vertex).Keys.ToList());
            }

            foreach (var vertexList in traversalList)
            {
                if (vertexList.Count != contAllVertex)
                {
                    return false;
                }
            }
            return true;
        }

        
        /// Метод для поиска вершин на периферии графа на расстоянии больше или равном N от заданной вершины
        /// </summary>
        /// <param name="graph">Граф</param>
        /// <param name="source">Исходная вершина</param>
        /// <param name="N">Минимальное расстояние</param>
        /// <returns>Список вершин на периферии графа</returns>
        public static List<Vertex> FindNPeriphery(Graph graph, Vertex source, double N)
        {
            // Получаем все кратчайшие расстояния с помощью алгоритма Флойда-Уоршелла
            var allDistances = Floyd_Warshall.Execute(graph);

            var periphery = new List<Vertex>();

            foreach (var target in GraphManager.GetAdj(graph).Keys) // Предполагается, что Graph имеет свойство Vertices
            {
                if (source.Equals(target))
                    continue;

                if (allDistances.TryGetValue((source, target), out double distance))
                {
                    if (distance > N)
                    {
                        periphery.Add(target);
                    }
                }
                else
                {
                    // Если расстояние не найдено, это означает, что нет пути между source и target
                    // В зависимости от требований, можно решить, как обрабатывать такие случаи
                    // Например, можно считать расстояние бесконечным
                    periphery.Add(target);
                }
            }

            return periphery;
        }

        /// <summary>
        /// Метод для поиска максимального потока в графе от заданной исходной вершины к заданной конечной вершине
        /// </summary>
        /// <param name="graph">Граф</param>
        /// <param name="sourceName">Имя исходной вершины</param>
        /// <param name="sinkName">Имя конечной вершины</param>
        /// <returns>Максимальный поток</returns>
        public static Tuple<double, List<Graph>> FindMaxFlow(Graph graph, string sourceName, string sinkName)
        {
            // Получаем вершину по ее имени
            Vertex source = GraphSearcher.FindVertexByName(sourceName, graph) ?? throw new ArgumentNullException($"Вершины {sourceName} не существует");
            // Получаем вершину по ее имени
            Vertex sink = GraphSearcher.FindVertexByName(sinkName, graph) ?? throw new ArgumentNullException($"Вершины {sinkName} не существует");
            return FordFulkerson.MaxFlow(graph, source, sink);
        }
    }
}
