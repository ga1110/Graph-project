using System;
using System.Collections.Generic;
using GraphProject.Structures;

namespace GraphProject.Algorithms
{
    public static class DijkstraAlgorithm
    {
        /// <summary>
        /// Выполняет алгоритм Дейкстры для нахождения кратчайших путей от начальной вершины
        /// до всех остальных вершин в графе.
        /// </summary>
        /// <param name="graph">Граф, в котором необходимо выполнить алгоритм.</param>
        /// <param name="startVertex">Начальная вершина.</param>
        /// <returns>
        /// Словарь, в котором ключами являются вершины, а значениями - минимальные расстояния
        /// от начальной вершины до каждой из вершин.
        /// </returns>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        /// <exception cref="ArgumentException">Если начальная вершина не существует в графе.</exception>
        public static Dictionary<Vertex, double> Execute(Graph graph, Vertex startVertex)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - null");

            if (!graph.adjacencyList.ContainsKey(startVertex))
                throw new ArgumentException("Исходная вершина не существует в графе.");

            var distances = new Dictionary<Vertex, double>();
            var visited = new HashSet<Vertex>();
            var unvisited = new HashSet<Vertex>();

            foreach (var vertex in graph.adjacencyList.Keys)
            {
                distances[vertex] = double.MaxValue;
                unvisited.Add(vertex);
            }
            distances[startVertex] = 0;

            while (unvisited.Count > 0)
            {
                var currentVertex = GetVertexWithMinDistance(unvisited, distances);
                if (currentVertex == null)
                    break;

                unvisited.Remove(currentVertex);
                visited.Add(currentVertex);

                foreach (var edge in graph.adjacencyList[currentVertex])
                {
                    var neighbor = edge.Destination;
                    if (visited.Contains(neighbor))
                        continue;

                    var newDistance = distances[currentVertex] + (edge.Weight ?? 0);
                    if (newDistance < distances[neighbor])
                    {
                        distances[neighbor] = newDistance;
                    }
                }
            }

            return distances;
        }

        /// <summary>
        /// Находит вершину с минимальным расстоянием среди непосещенных вершин.
        /// </summary>
        /// <param name="unvisited">Непосещенные вершины.</param>
        /// <param name="distances">Словарь расстояний от начальной вершины до всех вершин.</param>
        /// <returns>Вершина с минимальным расстоянием.</returns>
        private static Vertex GetVertexWithMinDistance(HashSet<Vertex> unvisited, Dictionary<Vertex, double> distances)
        {
            Vertex minVertex = null;
            double minDistance = double.MaxValue;

            foreach (var vertex in unvisited)
            {
                if (distances[vertex] < minDistance)
                {
                    minDistance = distances[vertex];
                    minVertex = vertex;
                }
            }

            return minVertex;
        }
    }
}
