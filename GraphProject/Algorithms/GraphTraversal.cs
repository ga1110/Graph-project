using GraphProject.Handlers;
using GraphProject.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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

        public static List<Vertex> BFS(Graph graph, Vertex startVertex)
        {
            if (graph == null)
                throw new ArgumentNullException("Граф - null");

            List<Vertex> bfs = new();
            Queue<Vertex> queue = new();
            HashSet<Vertex> visited = new();

            queue.Enqueue(startVertex);
            visited.Add(startVertex);

            while (queue.Count > 0)
            {
                Vertex currentVertex = queue.Dequeue();
                bfs.Add(currentVertex);

                foreach (var vertex in FindAdjacentVertices(graph, currentVertex))
                {
                    if (!visited.Contains(vertex))
                    {
                        visited.Add(vertex);
                        queue.Enqueue(vertex);
                    }
                }
            }

            return bfs;
        }

        private static List<Vertex> FindAdjacentVertices(Graph graph, Vertex currentVertex)
        {
            var adjacentList = GraphManager.GetAdj(graph);
            List<Vertex> adjacentVertices = new();
            foreach (var element in adjacentList)
            {
                if (element.Key == currentVertex)
                {
                    foreach (var edge in element.Value)
                    {
                        adjacentVertices.Add(edge.Destination);
                    }
                }
            }
            return adjacentVertices;
        }


    }
}
