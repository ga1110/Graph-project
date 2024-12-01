using System;
using System.Collections.Generic;
using GraphProject.Structures;

namespace GraphProject.Algorithms
{
    public static class DijkstraAlgorithm
    {
        public static Dictionary<Vertex, double> Execute(Graph graph, Vertex startVertex)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - null");

            if (!graph.adjacencyList.ContainsKey(startVertex))
                throw new ArgumentException("Исходная вершина не существует в графе.");

            var distances = new Dictionary<Vertex, double>();
            var visited = new HashSet<Vertex>();
            var unvisited = new HashSet<Vertex>();

            // Инициализация расстояний
            foreach (var vertex in graph.adjacencyList.Keys)
            {
                distances[vertex] = double.MaxValue;
                unvisited.Add(vertex);
            }
            distances[startVertex] = 0;

            while (unvisited.Count > 0)
            {
                // Найти вершину с минимальным расстоянием
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
