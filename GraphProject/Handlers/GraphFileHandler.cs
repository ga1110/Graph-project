using Structures;
using System;
using System.IO;
using System.Linq;

namespace Handlers
{
    // Класс-обработчик для работы с файлами графов (загрузка и сохранение)
    public static class GraphFileHandler
    {
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

        public static string CreateFilePath(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("Имя файла пустое и/или null");

            // Проверяем и добавляем расширение .txt, если его нет
            if (!fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                fileName += ".txt";
            }

            string projectDirectory = GraphFileHandler.GetProjectDirectory();

            // Создаем полный путь к файлу внутри папки SavedGraphs
            string filePath = Path.Combine(projectDirectory, "SavedGraphs", fileName);

            // Проверяем, существует ли указанный файл
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл '{filePath}' не найден.");
            }

            return filePath;
        }
    }
}
