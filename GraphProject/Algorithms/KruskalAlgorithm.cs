using Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class KruskalAlgorithm
    {
        // Метод для поиска корня сжатия путей
        private static Vertex Find(Dictionary<Vertex, Vertex> parent, Vertex vertex)
        {
            if (parent[vertex] == vertex)
                return vertex;
            return parent[vertex] = Find(parent, parent[vertex]);
        }

        // Метод для объединения подмножеств
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

        // Основной метод для выполнения алгоритма Краскала
        public static Graph KruskalMST(Graph graph)
        {
            if (graph == null) 
                throw new ArgumentNullException(nameof(graph), "Граф - null");

            List<Edge> result = new List<Edge>(); // Список для хранения MST
            List<Edge> edges = new List<Edge>(); // Список всех рёбер графа

            // Извлечение всех рёбер из графа
            foreach (var edgeList in graph.adjacencyList.Values)
            {
                edges.AddRange(edgeList);
            }

            // Сортируем рёбра по весу (по возрастанию)
            edges = edges.Where(e => e.Weight.HasValue).OrderBy(e => e.Weight.Value).ToList();

            // Инициализация структуры для хранения корней и рангов вершин
            Dictionary<Vertex, Vertex> parent = new Dictionary<Vertex, Vertex>();
            Dictionary<Vertex, int> rank = new Dictionary<Vertex, int>();

            // Инициализация каждой вершины как своей собственной компоненты
            foreach (var vertex in graph.adjacencyList.Keys)
            {
                parent[vertex] = vertex;
                rank[vertex] = 0;
            }

            // Основная логика алгоритма
            foreach (Edge edge in edges)
            {
                Vertex rootSource = Find(parent, edge.Source);
                Vertex rootDestination = Find(parent, edge.Destination);

                // Если вершины находятся в разных множествах, добавляем ребро к MST
                if (rootSource != rootDestination)
                {
                    result.Add(edge);
                    Union(parent, rank, rootSource, rootDestination);
                }
            }

            // Возвращаем новый граф, представляющий MST
            return new Graph(result, graph.GraphName, graph.IsDirected);
        }

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
