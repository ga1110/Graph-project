using System;
using System.IO;
using System.Linq;
using Structures;

namespace Handlers
{
    // Класс-обработчик для работы с файлами графов (загрузка и сохранение)
    public static class GraphFileHandler
    {
        // Метод для загрузки графа из файла
        public static Graph LoadFromFile(string filename)
        {
            // Проверяем и добавляем расширение .txt, если его нет
            if (!filename.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                filename += ".txt";
            }

            // Получаем путь к папке проекта
            string projectDirectory = GetProjectDirectory();

            // Создаем полный путь к файлу внутри папки SavedGraphs
            string filePath = Path.Combine(projectDirectory, "SavedGraphs", filename);

            // Проверяем, существует ли указанный файл
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл '{filePath}' не найден.");
            }

            Graph graph;
            GraphManager graphManager;

            // Открываем файл для чтения
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Читаем первую строку файла (тип графа: ориентированный или неориентированный)
                string firstLine = reader.ReadLine();

                // Если файл пустой, вызываем исключение
                if (firstLine == null)
                {
                    throw new InvalidDataException("Файл пуст.");
                }

                // Проверяем, является ли граф ориентированным, и создаём объект Graph 
                bool isDirected = firstLine.Trim().ToLower() == "directed";
                graph = new Graph(isDirected);

                // Инициализируем GraphManager с созданным графом
                graphManager = new GraphManager(graph);

                string currentLine;

                // Читаем файл построчно до конца
                while ((currentLine = reader.ReadLine()) != null)
                {
                    // Разбиваем строку на имена вершин и опционально вес 
                    var lineElems = currentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    // Пропускаем пустые строки
                    if (lineElems.Length == 0)
                    {
                        continue;
                    }

                    // Получаем имя исходной вершины
                    string sourceVertexName = lineElems[0];

                    // Ищем вершину по имени или создаём новую, если она не найдена
                    Vertex sourceVertex = graphManager.GetVertexByName(sourceVertexName);

                    if (sourceVertex == null)
                    {
                        sourceVertex = new Vertex(sourceVertexName);
                        graphManager.AddVertex(sourceVertex);
                    }

                    // Если в строке указана вторая вершина (ребро)
                    if (lineElems.Length > 1)
                    {
                        // Получаем имя конечной вершины
                        string destinationVertexName = lineElems[1];
                        Vertex destinationVertex = graphManager.GetVertexByName(destinationVertexName);

                        // Если конечная вершина не найдена, создаём и добавляем её
                        if (destinationVertex == null)
                        {
                            destinationVertex = new Vertex(destinationVertexName);
                            graphManager.AddVertex(destinationVertex);
                        }

                        // Вес ребра (опциональный параметр)
                        double? weight = null;

                        // Если в строке указан вес
                        if (lineElems.Length > 2)
                        {
                            // Пытаемся распарсить третий элемент как вес ребра
                            if (double.TryParse(lineElems[2], out double parsedWeight))
                            {
                                weight = parsedWeight;
                            }
                        }

                        // Добавляем ребро в граф с указанными параметрами через GraphManager
                        graphManager.AddEdge(sourceVertexName, destinationVertexName, weight);
                    }
                }
            }

            // Выводим сообщение об успешной загрузке графа
            Console.WriteLine($"Граф успешно загружен из файла '{filePath}'. Тип графа: {(graph.IsDirected ? "Ориентированный" : "Неориентированный")}.");

            // Возвращаем загруженный граф
            return graph;
        }

        // Метод для сохранения графа в файл
        public static void SaveToFile(Graph graph, string filename)
        {
            try
            {
                // Проверяем и добавляем расширение .txt, если его нет
                if (!filename.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    filename += ".txt";
                }

                // Получаем путь к папке проекта
                string projectDirectory = GetProjectDirectory();

                // Создаем путь к папке SavedGraphs внутри папки проекта
                string saveDirectory = Path.Combine(projectDirectory, "SavedGraphs");

                // Проверяем, существует ли папка SavedGraphs, если нет — создаем ее
                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                // Создаем полный путь к файлу внутри папки SavedGraphs
                string filePath = Path.Combine(saveDirectory, filename);

                // Открываем файл для записи
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Записываем тип графа (ориентированный или неориентированный) на первую строку
                    writer.WriteLine(graph.IsDirected ? "Directed" : "Undirected");

                    // Проходим по каждой вершине и её рёбрам
                    foreach (var adjacencyListElement in graph.adjacencyList)
                    {
                        string sourceVertexName = adjacencyListElement.Key.Name;
                        var edges = adjacencyListElement.Value;

                        // Для каждого ребра записываем исходную и конечную вершины, а также опционально вес
                        foreach (var edge in edges)
                        {
                            string line = $"{sourceVertexName} {edge.Destination.Name}";

                            // Добавляем вес, если он указан
                            if (edge.Weight.HasValue)
                                line += $" {edge.Weight.Value}";

                            // Записываем строку в файл
                            writer.WriteLine(line);
                        }

                        // Если у вершины нет рёбер, записываем только её имя
                        if (edges.Count == 0)
                        {
                            writer.WriteLine($"{sourceVertexName}");
                        }
                    }
                }

                // Выводим сообщение об успешном сохранении графа
                Console.WriteLine($"Граф успешно сохранён в файл '{filePath}'.");
            }
            catch (Exception ex)
            {
                // В случае ошибки выводим сообщение об ошибке
                Console.WriteLine($"Ошибка при сохранении графа: {ex.Message}");
            }
        }

        // Метод для получения пути к папке проекта
        private static string GetProjectDirectory()
        {
            // Получаем путь к директории, где находится исполняемый файл
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo directory = new DirectoryInfo(exePath);

            // Ищем файл проекта (.csproj), поднимаясь вверх по дереву директорий
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            // Если не удалось найти папку проекта, выбрасываем исключение
            if (directory == null)
            {
                throw new DirectoryNotFoundException("Не удалось определить путь к папке проекта.");
            }

            return directory.FullName;
        }
    }
}
