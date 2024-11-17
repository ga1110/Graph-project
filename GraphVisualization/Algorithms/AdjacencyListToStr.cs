using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers;
using Structures;
namespace GraphVisualization.Algorithms
{
    public static class AdjacencyListToStr
    {
        public static string ToStr(Graph graph)
        {
            var adjacencyList = GraphManager.GetAdj(graph);
            string adjacencyStr = "Список смежности графа:\n\n";

            // Проходим по каждому элементу списка смежности графа
            foreach (var vertex in adjacencyList)
            {
                // Получаем имя вершины
                string vertexName = vertex.Key.Name;
                // Получаем список ребер, исходящих из данной вершины
                var edges = vertex.Value;

                // Выводим имя вершины
                adjacencyStr += $"{vertexName}: ";

                // Если список ребер пустой или равен null
                if (edges == null || edges.Count == 0)
                {
                    // Выводим сообщение об отсутствии ребер и переходим к следующей итерации
                    adjacencyStr += "нет рёбер.\n";
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
                adjacencyStr += string.Join(" ", edgeStrings) + "\n";
            }

            return adjacencyStr;
        }
    }
}
