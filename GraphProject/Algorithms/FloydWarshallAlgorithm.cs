using System;
using System.Collections.Generic;
using Structures; // Импортируем пространство имён, где определены Graph, Vertex, Edge

namespace Algorithms
{
    public static class FloydWarshallAlgorithm
    {
        public static double[,] Execute(Graph graph)
        {
            int V = graph.adjacencyList.Count;
            double[,] dist = new double[V, V];

            // Инициализация расстояний
            for (int i = 0 ; i < V ; i++)
            {
                for (int j = 0 ; j < V ; j++)
                {
                    if (i == j)
                        dist[i, j] = 0;
                    else
                        dist[i, j] = double.PositiveInfinity;
                }
            }

            // Список вершин для индексации
            var vertices = new List<Vertex>(graph.adjacencyList.Keys);

            // Установка начальных расстояний на основе смежности
            for (int i = 0 ; i < vertices.Count ; i++)
            {
                foreach (Edge edge in graph.adjacencyList[vertices[i]])
                {
                    int u = vertices.IndexOf(edge.Source);
                    int v = vertices.IndexOf(edge.Destination);
                    dist[u, v] = edge.Weight ?? double.PositiveInfinity;
                }
            }

            // Алгоритм Флойда-Уоршалла
            for (int k = 0 ; k < V ; k++)
            {
                for (int i = 0 ; i < V ; i++)
                {
                    for (int j = 0 ; j < V ; j++)
                    {
                        if (dist[i, k] + dist[k, j] < dist[i, j])
                            dist[i, j] = dist[i, k] + dist[k, j];
                    }
                }
            }

            return dist;
        }
    }
}
