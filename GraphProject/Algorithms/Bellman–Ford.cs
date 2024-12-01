﻿using GraphProject.Structures;
using System;
using System.Collections.Generic;

namespace GraphProject.Algorithms
{
    public static class BellmanFord
    {
        // Константа для обозначения "бесконечности"
        const double Inf = double.PositiveInfinity;

        public static Dictionary<Vertex, double> Execute(Graph graph, Vertex startVertex)
        {
            // Проверяем, что граф не равен null
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - null");

            // Проверяем, что начальная вершина существует в графе
            if (!graph.adjacencyList.ContainsKey(startVertex))
                throw new ArgumentException("Исходная вершина не существует в графе.");

            // Словарь для хранения минимальных расстояний от начальной вершины до других вершин
            Dictionary<Vertex, double> distances = new();

            // Инициализируем расстояния для всех вершин графа
            foreach (var vertex in graph.adjacencyList.Keys)
            {
                distances[vertex] = Inf; // Устанавливаем расстояние как бесконечность
            }
            distances[startVertex] = 0; // Для начальной вершины расстояние равно 0

            // Получаем количество вершин в графе
            int vertexCount = graph.adjacencyList.Count;

            // Основной цикл алгоритма: повторяем V-1 раз, где V - количество вершин
            for (int i = 1 ; i < vertexCount ; i++)
            {
                // Проходим по всем вершинам графа
                foreach (var vertex in graph.adjacencyList.Keys)
                {
                    // Проверяем всех соседей текущей вершины
                    foreach (var edge in graph.adjacencyList[vertex])
                    {
                        var neighbor = edge.Destination; // Получаем конечную вершину ребра
                        var weight = edge.Weight ?? 0;   // Вес ребра (если null, заменяем на 0)

                        // Если расстояние до текущей вершины не бесконечность и найден более короткий путь
                        if (distances[vertex] != Inf && distances[vertex] + weight < distances[neighbor])
                        {
                            // Обновляем минимальное расстояние до соседней вершины
                            distances[neighbor] = distances[vertex] + weight;
                        }
                    }
                }
            }

            // Проверка на наличие отрицательных циклов в графе
            foreach (var vertex in graph.adjacencyList.Keys)
            {
                foreach (var edge in graph.adjacencyList[vertex])
                {
                    var neighbor = edge.Destination; // Конечная вершина ребра
                    var weight = edge.Weight ?? 0;   // Вес ребра

                    // Если можно уменьшить расстояние, значит в графе есть отрицательный цикл
                    if (distances[vertex] != Inf && distances[vertex] + weight < distances[neighbor])
                    {
                        throw new InvalidOperationException("Граф содержит отрицательный цикл.");
                    }
                }
            }

            // Возвращаем словарь минимальных расстояний
            return distances;
        }
    }
}
