using GraphProject.Handlers;
using GraphProject.Structures;
using System;
using System.Collections.Generic;

namespace GraphProject.Algorithms
{
    public static class Floyd_Warshall
    {
        /// <summary>
        /// Выполняет алгоритм Флойда-Уоршелла для нахождения кратчайших путей между всеми парами вершин в графе.
        /// </summary>
        /// <param name="graph">Граф, в котором необходимо выполнить алгоритм.</param>
        /// <returns>
        /// Словарь, в котором ключами являются пары вершин, а значениями - минимальные расстояния
        /// между каждой парой вершин.
        /// </returns>
        public static Dictionary<(Vertex, Vertex), double> Execute(Graph graph)
        {
            var adjacencyList = GraphManager.GetAdj(graph);
            GraphManager.SetAllVertexID(graph);

            var vertices = new List<Vertex>(adjacencyList.Keys);
            int n = vertices.Count;
            var indexMap = new Dictionary<Vertex, int>();
            for (int i = 0; i < n; i++)
            {
                indexMap[vertices[i]] = i;
            }

            double[,] dist = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        dist[i, j] = 0;
                    }
                    else
                    {
                        dist[i, j] = double.PositiveInfinity;
                    }
                }
            }

            foreach (var from in adjacencyList.Keys)
            {
                int i = indexMap[from];
                foreach (var edge in adjacencyList[from])
                {
                    int j = indexMap[edge.Destination];
                    if (edge.Weight < dist[i, j])
                    {
                        dist[i, j] = edge.Weight ?? throw new Exception($"У вершины {edge} нет веса");
                    }
                }
            }

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (dist[i, k] + dist[k, j] < dist[i, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                        }
                    }
                }
            }

            var result = new Dictionary<(Vertex, Vertex), double>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[(vertices[i], vertices[j])] = dist[i, j];
                }
            }

            return result;
        }
    }
}