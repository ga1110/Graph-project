using GraphProject.Handlers;
using GraphProject.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject.Algorithms
{
    public static class Floyd_Warshall
    {
        public static Dictionary<(Vertex, Vertex), double> Execute(Graph graph)
        {
            var adjacencyList = GraphManager.GetAdj(graph);
            GraphManager.SetAllVertexID(graph);

            var vertices = new List<Vertex>(adjacencyList.Keys);
            int n = vertices.Count;
            var indexMap = new Dictionary<Vertex, int>();
            for (int i = 0 ; i < n ; i++)
            {
                indexMap[vertices[i]] = i;
            }

            // Инициализация матрицы расстояний
            double[,] dist = new double[n, n];
            for (int i = 0 ; i < n ; i++)
            {
                for (int j = 0 ; j < n ; j++)
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

            // Заполнение начальных расстояний из списка смежности
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

            // Алгоритм Флойда-Уоршелла
            for (int k = 0 ; k < n ; k++)
            {
                for (int i = 0 ; i < n ; i++)
                {
                    for (int j = 0 ; j < n ; j++)
                    {
                        if (dist[i, k] + dist[k, j] < dist[i, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                        }
                    }
                }
            }

            // Формирование результата
            var result = new Dictionary<(Vertex, Vertex), double>();
            for (int i = 0 ; i < n ; i++)
            {
                for (int j = 0 ; j < n ; j++)
                {
                    result[(vertices[i], vertices[j])] = dist[i, j];
                }
            }

            return result;
        }


    }
}
