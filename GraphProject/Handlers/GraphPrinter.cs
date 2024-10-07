using Structures;

namespace Handlers
{
    public static class GraphPrinter
    {
        public static void DisplayAdjacencyList(Graph graph)
        {
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

            List<Vertex> verticesWithGreaterOutDegree = GraphSearcher.FindVerticesWithGreaterOutDegree(vertexName, graph);
            if (verticesWithGreaterOutDegree == null)
            {
                Console.WriteLine($"Вершина '{vertexName}' не найдена в графе.");
                return;
            }
            Vertex currVertex = GraphSearcher.FindVertexByName(vertexName, graph);
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
        public static void DisplayGraphList(GraphVault GraphVault)
        {
            Console.WriteLine("\nСписок графов:");
            var currentGraphVault = GraphVault.GetGrahpsList();
            var index = 1;
            foreach (var graphInList in currentGraphVault)
            {
                Console.WriteLine($"{index}. {graphInList.GraphName}");
                index++;
            }
            Console.WriteLine();
            var currentGraphIndex = GraphVault.GetCurrentGraphIndex();
            var currentGraphName = GraphVault.GetCurrentGraph().GraphName;
            Console.WriteLine($"Выбранный граф: {currentGraphIndex}. {currentGraphName}");
            Console.WriteLine();
        }
        public static void DisplayCurrentGraph (GraphVault GraphVault)
        {
            var currentGraphName = GraphVault.GetCurrentGraph().GraphName;
            var currentGraphIndex = GraphVault.GetCurrentGraphIndex();
            Console.WriteLine($"Текущий граф: {currentGraphIndex}. {currentGraphName}");
        }
        public static void DisplayGraphIndexError (GraphVault GraphVault)
        {
            var currentGraphIndex = GraphVault.GetCurrentGraphIndex();
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
    }

}
