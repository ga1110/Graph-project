using GraphProject.Handlers;
using GraphProject.Structures;
using System.Collections.Generic;

namespace GraphVisualization.Algorithms
{
    public static class AdjacencyListToStr
    {
        /// <summary>
        /// Метод для преобразования списка смежности графа в строку
        /// </summary>
        /// <param name="graph">Граф</param>
        /// <returns>Строка, представляющая список смежности графа</returns>
        public static string ToStr(Graph graph)
        {
            var adjacencyList = GraphManager.GetAdj(graph);
            string adjacencyStr = "";

            // Проходим по каждому элементу списка смежности графа
            foreach (var vertex in adjacencyList)
            {
                // Получаем имя вершины
                string vertexName = vertex.Key.Name;
                // Получаем список ребер, исходящих из данной вершины
                var edges = vertex.Value;

                // Выводим имя вершины
                adjacencyStr += $"{vertexName}-> ";

                // Если список ребер пустой или равен null
                if (edges == null || edges.Count == 0)
                {
                    // Выводим сообщение об отсутствии ребер и переходим к следующей итерации
                    adjacencyStr += "нет рёбер.\n";
                    continue;
                }

                // Создаем список строк для представления ребер
                List<string> edgeStrings = new();

                int index = 0;
                // Проходим по каждому ребру в списке ребер
                foreach (var edge in edges)
                {
                    // Инициализируем строку представления ребра с именем вершины назначения
                    string edgeStr = index == 0 ? "" : " | ";
                    edgeStr += edge.Destination.Name;

                    // Если у ребра задан вес
                    if (edge.Weight.HasValue)
                    {
                        // Добавляем информацию о весе к строке представления ребра
                        edgeStr += $", Вес: {edge.Weight.Value}";
                    }
                    // Если у ребра задан вес
                    if (edge.Capacity.HasValue)
                    {
                        // Добавляем информацию о весе к строке представления ребра
                        edgeStr += $", Поток: {edge.Capacity}";
                    }
                    // Добавляем строку ребра в список строк ребер
                    edgeStrings.Add(edgeStr);
                    index++;
                }

                // Выводим список ребер, объединенных пробелом
                adjacencyStr += string.Join(" ", edgeStrings) + "\n\n";
            }

            return adjacencyStr;
        }
    }
}
