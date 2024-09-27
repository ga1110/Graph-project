using System;
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
        public static void DisplayVerticesWithGreaterOutDegree(string vertexName, GraphManager graphManager, Graph graph)
        {

            // Получаем вершину по ее имени
            Vertex givenVertex = graphManager.GetVertexByName(vertexName);

            // Проверяем, что вершина существует
            if (givenVertex == null)
            {
                Console.WriteLine($"Вершина '{vertexName}' не найдена в графе.");
                return;
            }

            // Получаем полустепень исхода заданной вершины
            int givenVertexOutDegree = GetOutDegree(givenVertex, graph);

            // Список вершин с большей полустепенью исхода
            List<Vertex> verticesWithGreaterOutDegree = new List<Vertex>();

            // Проходим по всем вершинам в графе
            foreach (var vertex in graph.adjacencyList.Keys)
            {
                // Пропускаем заданную вершину
                if (vertex.Equals(givenVertex))
                {
                    continue;
                }

                // Получаем полустепень исхода текущей вершины
                int currentVertexOutDegree = GetOutDegree(vertex, graph);

                // Если полустепень исхода больше, добавляем в список
                if (currentVertexOutDegree > givenVertexOutDegree)
                {
                    verticesWithGreaterOutDegree.Add(vertex);
                }
            }

            // Вывод результата
            if (verticesWithGreaterOutDegree.Count > 0)
            {
                Console.WriteLine($"Вершины, полустепень исхода которых больше, чем у вершины '{vertexName}' (исходящая степень {givenVertexOutDegree}):");
                foreach (var vertex in verticesWithGreaterOutDegree)
                {
                    int outDegree = GetOutDegree(vertex, graph);
                    Console.WriteLine($"- {vertex.Name} (исходящая степень {outDegree})");
                }
            }
            else
            {
                Console.WriteLine($"Нет вершин с полустепенью исхода, большей чем у вершины '{vertexName}' (исходящая степень {givenVertexOutDegree}).");
            }
        }

        // Вспомогательный метод для получения полустепени исхода вершины
        private static int GetOutDegree(Vertex vertex, Graph graph)
        {
            // Проверяем, содержит ли граф данную вершину
            if (graph.adjacencyList.ContainsKey(vertex))
            {
                // Возвращаем количество исходящих ребер
                return graph.adjacencyList[vertex].Count;
            }
            else
            {
                return 0;
            }
        }
    }
}
