using GraphProject.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphProject.Algorithms
{
    public static class KruskalAlgorithm
    {
        /// <summary>
        /// Метод для поиска корня сжатия путей.
        /// </summary>
        /// <param name="parent">Словарь, в котором ключами являются вершины, а значениями - их родительские вершины.</param>
        /// <param name="vertex">Вершина, для которой необходимо найти корень.</param>
        /// <returns>Корень вершины.</returns>
        private static Vertex Find(Dictionary<Vertex, Vertex> parent, Vertex vertex)
        {
            if (parent[vertex] == vertex)
                return vertex;
            return parent[vertex] = Find(parent, parent[vertex]);
        }

        /// <summary>
        /// Метод для объединения подмножеств.
        /// </summary>
        /// <param name="parent">Словарь, в котором ключами являются вершины, а значениями - их родительские вершины.</param>
        /// <param name="rank">Словарь, в котором ключами являются вершины, а значениями - ранги вершин.</param>
        /// <param name="root1">Первая вершина.</param>
        /// <param name="root2">Вторая вершина.</param>
        private static void Union(Dictionary<Vertex, Vertex> parent, Dictionary<Vertex, int> rank, Vertex root1, Vertex root2)
        {
            if (rank[root1] < rank[root2])
                parent[root1] = root2;
            else if (rank[root1] > rank[root2])
                parent[root2] = root1;
            else
            {
                parent[root2] = root1;
                rank[root1]++;
            }
        }

        /// <summary>
        /// Основной метод для выполнения алгоритма Краскала.
        /// </summary>
        /// <param name="graph">Граф, в котором необходимо выполнить алгоритм.</param>
        /// <returns>Граф, представляющий минимальное остовное дерево (MST).</returns>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        public static Graph KruskalMST(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - null");

            List<Edge> result = new List<Edge>();
            List<Edge> edges = new List<Edge>();

            foreach (var edgeList in graph.adjacencyList.Values)
            {
                edges.AddRange(edgeList);
            }

            edges = edges.Where(e => e.Weight.HasValue).OrderBy(e => e.Weight.Value).ToList();

            Dictionary<Vertex, Vertex> parent = new Dictionary<Vertex, Vertex>();
            Dictionary<Vertex, int> rank = new Dictionary<Vertex, int>();

            foreach (var vertex in graph.adjacencyList.Keys)
            {
                parent[vertex] = vertex;
                rank[vertex] = 0;
            }

            foreach (Edge edge in edges)
            {
                Vertex rootSource = Find(parent, edge.Source);
                Vertex rootDestination = Find(parent, edge.Destination);

                if (rootSource != rootDestination)
                {
                    result.Add(edge);
                    Union(parent, rank, rootSource, rootDestination);
                }
            }

            return new Graph(result, graph.GraphName, graph.IsDirected);
        }

        /// <summary>
        /// Проверяет, является ли граф корректным для выполнения алгоритма построения MST.
        /// </summary>
        /// <param name="graph">Граф, который необходимо проверить.</param>
        /// <returns>True, если граф корректен, иначе - false.</returns>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null или содержит вершины без веса.</exception>
        /// <exception cref="ArgumentNullException">Если граф является ориентированным.</exception>
        public static bool IsGraphCorrectForMST(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            foreach (var vertex in graph.adjacencyList)
            {
                foreach (var edge in vertex.Value)
                {
                    if (edge.Weight == null)
                        throw new ArgumentNullException($"В графе есть вершина без веса ({edge.ToString()})");
                }
            }

            if (graph.IsDirected)
                throw new ArgumentNullException($"Граф - ориентированный");

            return true;
        }
    }
}
