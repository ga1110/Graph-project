using GraphProject.Handlers;
using GraphProject.Structures;
using System;
using System.Collections.Generic;

namespace GraphProject.Algorithms
{
    /// <summary>
    /// Класс GraphTraversal содержит методы для обхода графа.
    /// </summary>
    public static class GraphTraversal
    {
        /// <summary>
        /// Метод RecursiveGraphTraversal выполняет рекурсивный обход графа и возвращает словарь посещенных вершин.
        /// </summary>
        /// <param name="graph">Граф для обхода.</param>
        /// <param name="currentVertex">Текущая вершина.</param>
        /// <returns>Словарь посещенных вершин.</returns>
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

        /// <summary>
        /// Вспомогательный метод для рекурсивного обхода графа.
        /// </summary>
        /// <param name="graph">Граф для обхода.</param>
        /// <param name="currentVertex">Текущая вершина.</param>
        /// <param name="visitedDictionary">Словарь посещенных вершин.</param>
        /// <param name="n">Уровень обхода.</param>
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

        /// <summary>
        /// Метод BFS выполняет обход графа в ширину и возвращает список вершин в порядке обхода.
        /// </summary>
        /// <param name="graph">Граф для обхода.</param>
        /// <param name="startVertex">Начальная вершина.</param>
        /// <returns>Список вершин в порядке обхода.</returns>
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

        /// <summary>
        /// Метод FindAdjacentVertices возвращает список смежных вершин для заданной вершины.
        /// </summary>
        /// <param name="graph">Граф.</param>
        /// <param name="currentVertex">Текущая вершина.</param>
        /// <returns>Список смежных вершин.</returns>
        public static List<Vertex> FindAdjacentVertices(Graph graph, Vertex currentVertex)
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
