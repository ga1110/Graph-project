﻿using System;
using System.Collections.Generic;
using Structures;

namespace Handlers
{
    public static class GraphPrinter
    {
        public static void DisplayAdjacencyList(Graph graph)
        {
            Console.WriteLine("\nСписок смежности графа:");

            // Проходим по каждому элементу списка смежности графа
            foreach (var adjacencyListElement in graph.adjacencyList)
            {
                // Получаем имя вершины
                string vertexName = adjacencyListElement.Key.Name;
                // Получаем список ребер, исходящих из данной вершины
                var edges = adjacencyListElement.Value;

                // Выводим имя вершины
                Console.Write($"{vertexName}: ");

                // Если список ребер пустой или равен null
                if (edges == null || edges.Count == 0)
                {
                    // Выводим сообщение об отсутствии ребер и переходим к следующей итерации
                    Console.WriteLine("нет рёбер.");
                    continue;
                }

                // Создаем список строк для представления ребер
                List<string> edgeStrings = new List<string>();

                // Проходим по каждому ребру в списке ребер
                foreach (var edge in edges)
                {
                    // Инициализируем строку представления ребра с именем вершины назначения
                    string edgeStr = "(" + edge.Destination.Name;

                    // Если у ребра задан вес
                    if (edge.Weight.HasValue)
                    {
                        // Добавляем информацию о весе к строке представления ребра
                        edgeStr += $", Вес: {edge.Weight.Value}";
                    }

                    // Закрываем скобку в строке представления ребра
                    edgeStr += ")";

                    // Добавляем строку ребра в список строк ребер
                    edgeStrings.Add(edgeStr);
                }

                // Выводим список ребер, объединенных пробелом
                Console.WriteLine(string.Join(" ", edgeStrings));
            }

            // Вывод пустой строки для разделения выводов
            Console.WriteLine();
        }
        public static void DisplayVerticesWithGreaterOutDegree(string vertexName, GraphManager graphManager)
        {

            List<Vertex> verticesWithGreaterOutDegree = graphManager.FindVerticesWithGreaterOutDegree(vertexName);
            if (verticesWithGreaterOutDegree == null)
            {
                Console.WriteLine($"Вершина '{vertexName}' не найдена в графе.");
                return;
            }
            int givenVertexOutDegree = graphManager.GetOutDegree(graphManager.GetVertexByName(vertexName));
            if (verticesWithGreaterOutDegree.Count > 0)
            {
                Console.WriteLine($"Вершины, полустепень исхода которых больше, чем у вершины '{vertexName}' (исходящая степень {givenVertexOutDegree}):");
                foreach (var vertex in verticesWithGreaterOutDegree)
                {
                    int outDegree = graphManager.GetOutDegree(vertex);
                    Console.WriteLine($"- {vertex.Name} (исходящая степень {outDegree})");
                }
            }
            else
            {
                Console.WriteLine($"Нет вершин с полустепенью исхода, большей чем у вершины '{vertexName}' (исходящая степень {givenVertexOutDegree}).");
            }
        }
        public static void DisplayNonAdjacentVertices(string vertexName, GraphManager graphManager)
        {
            List<Vertex> nonAdjacentVertices = graphManager.FindNonAdjacentVertices(vertexName);
            if (nonAdjacentVertices == null)
            {
                Console.WriteLine($"Вершина '{vertexName}' не найдена в графе.");
                return;
            }
            // Вывод результата
            if (nonAdjacentVertices.Count > 0)
            {
                Console.WriteLine($"Вершины, не смежные с вершиной '{vertexName}':");
                foreach (var vertex in nonAdjacentVertices)
                {
                    Console.WriteLine($"- {vertex.Name}");
                }
            }
            else
            {
                Console.WriteLine($"Все вершины смежны с вершиной '{vertexName}'.");
            }
        }
    }
}
