using GraphProject.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphProject.Algorithms
{
    public static class GraphTraversal
    {
        public static Dictionary<Vertex, int> RecursiveGraphTraversal(Graph graph, Vertex currentVertex)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - null");

            if (currentVertex == null)
                throw new ArgumentNullException($"Вершина {currentVertex} - не существует");

            Dictionary<Vertex, int> visitedDictionary = new Dictionary<Vertex, int>();

            recursiveGraphTraversalHelp(graph, currentVertex, visitedDictionary);

            return visitedDictionary;
        }

        private static void recursiveGraphTraversalHelp(Graph graph, Vertex currentVertex, Dictionary<Vertex, int> visitedDictionary, int n = 0)
        {
            // Проверяем, была ли вершина уже посещена
            if (visitedDictionary.ContainsKey(currentVertex))
                return;

            // Добавляем текущую вершину в список посещенных
            visitedDictionary.Add(currentVertex, n);

            // Проверяем, существуют ли смежные вершины для текущей вершины
            if (graph.adjacencyList.ContainsKey(currentVertex))
            {
                var edges = graph.adjacencyList[currentVertex];
                foreach (var edge in edges)
                {
                    recursiveGraphTraversalHelp(graph, edge.Destination, visitedDictionary, n + 1);
                }
            }
        }
    }
}
