using System;
using System.Collections.Generic;
using Structures;

namespace Algorithms
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
            foreach (var vertex in graph.adjacencyList.Keys)
            {
                distances[vertex] = double.MaxValue;
            }
            distances[startVertex] = 0;

            var priorityQueue = new PriorityQueue<Vertex, double>();
            priorityQueue.Enqueue(startVertex, 0);

            while (priorityQueue.Count != 0)
            {
                var currentVertex = priorityQueue.Dequeue();
                if (visited.Contains(currentVertex))
                {
                    continue;
                }

                visited.Add(currentVertex);
                foreach (var edge in graph.adjacencyList[currentVertex])
                {
                    var neighbor = edge.Destination;
                    var distance = distances[currentVertex] + (edge.Weight ?? 0);

                    if (distance < distances[neighbor])
                    {
                        distances[neighbor] = distance;
                        priorityQueue.Enqueue(neighbor, distance);
                    }
                }
            }

            return distances;
        }
    }
}
