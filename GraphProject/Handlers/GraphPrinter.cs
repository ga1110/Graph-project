﻿using GraphProject.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphProject.Handlers
{
    public static class GraphPrinter
    {
        public static void DisplayAdjacencyList(Graph graph)
        {
            if (graph == null)
                throw new Exception("Граф - null");
            Console.WriteLine("\nСписок смежности графа:");

            // Проходим по каждому элементу списка смежности графа
            foreach (var vertex in graph.adjacencyList)
            {
                // Получаем имя вершины
                string vertexName = vertex.Key.Name;
                // Получаем список ребер, исходящих из данной вершины
                var edges = vertex.Value;

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
        public static void DisplayVerticesWithGreaterOutDegree(string vertexName, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф не может быть null.");

            List<Vertex>? verticesWithGreaterOutDegree = GraphSearcher.FindVerticesWithGreaterOutDegree(vertexName, graph);

            if (verticesWithGreaterOutDegree == null)
                throw new ArgumentNullException($"Вершина '{vertexName}' не найдена в графе.");

            Vertex currVertex = GraphSearcher.FindVertexByName(vertexName, graph);

            if (verticesWithGreaterOutDegree == null)
                throw new ArgumentNullException($"Вершина '{vertexName}' не найдена в графе.");

            int givenVertexOutDegree = VertexAnalyzer.GetOutDegree(currVertex, graph);
            if (verticesWithGreaterOutDegree.Count > 0)
            {
                Console.WriteLine($"Вершины, полустепень исхода которых больше, чем у вершины '{vertexName}' (исходящая степень {givenVertexOutDegree}):");
                foreach (var vertex in verticesWithGreaterOutDegree)
                {
                    int outDegree = VertexAnalyzer.GetOutDegree(vertex, graph);
                    Console.WriteLine($"- {vertex.Name} (исходящая степень {outDegree})");
                }
            }
            else
            {
                Console.WriteLine($"Нет вершин с полустепенью исхода, большей чем у вершины '{vertexName}' (исходящая степень {givenVertexOutDegree}).");
            }
        }
        public static void DisplayNonAdjacentVertices(string vertexName, Graph graph)
        {
            List<Vertex> nonAdjacentVertices = GraphSearcher.FindNonAdjacentVertices(vertexName, graph);
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
        public static void DisplayGraphList(GraphVoult GraphVoult)
        {
            Console.WriteLine("\nСписок графов:");
            var currentGraphVoult = GraphVoult.GetGrahpsList();
            var index = 1;
            foreach (var graphInList in currentGraphVoult)
            {
                Console.WriteLine($"{index}. {graphInList.GraphName}");
                index++;
            }
            Console.WriteLine();
            var currentGraphIndex = GraphVoult.GetCurrentGraphIndex();
            var currentGraphName = GraphVoult.GetCurrentGraph().GraphName;
            Console.WriteLine($"Выбранный граф: {currentGraphIndex}. {currentGraphName}");
            Console.WriteLine();
        }
        public static void DisplayCurrentGraph (GraphVoult GraphVoult)
        {
            var currentGraphName = GraphVoult.GetCurrentGraph().GraphName;
            var currentGraphIndex = GraphVoult.GetCurrentGraphIndex();
            Console.WriteLine($"Текущий граф: {currentGraphIndex}. {currentGraphName}");
        }
        public static void DisplayGraphIndexError (GraphVoult GraphVoult)
        {
            var currentGraphIndex = GraphVoult.GetCurrentGraphIndex();
            Console.WriteLine($"Графа под номером {currentGraphIndex} не существует.");
        }
        public static void DisplayUnreachableVertices(string vertexName, Graph graph)
        {
            List<Vertex> unreachableVertices = GraphSearcher.FindUnreachableVertices(vertexName, graph);
            int index = 1;
            if (unreachableVertices != null && unreachableVertices.Count() != 0)
            {
                Console.WriteLine($"Cписок вершин не достижимых из {vertexName}:");
                foreach (var vertex in unreachableVertices)
                {
                    Console.WriteLine($"{index}. {vertex.Name}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine($"Нет вершин не достижимых из {vertexName}.");
            }
        }
        public static void DisplayVertexDistanceLessN (Dictionary<Vertex,double> vertexList, string vertexName, double n)
        {
            if (vertexList == null)
            {
                Console.WriteLine($"Вершин, расстояние которых от вершины {vertexName}, меньше {n} - нет");
                return;
            }
            int index = 1;
            Console.WriteLine("Список найденных вершин:");
            vertexList = vertexList.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var currentElement in vertexList)
            {
                Console.WriteLine($"{index++}. {currentElement.Key.Name}, расстояние: {currentElement.Value}");
            }
        }
    }

}
