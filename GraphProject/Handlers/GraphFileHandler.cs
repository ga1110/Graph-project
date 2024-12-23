using GraphProject.Structures;
using System;
using System.IO;
using System.Linq;

namespace GraphProject.Handlers
{
    // Класс-обработчик для работы с файлами графов (загрузка и сохранение)
    public static class GraphFileHandler
    {
        /// <summary>
        /// Сохраняет граф в файл.
        /// </summary>
        /// <param name="graph">Граф, который необходимо сохранить.</param>
        /// <param name="filename">Имя файла для сохранения графа.</param>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        public static void SaveToFile(Graph graph, string filename)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            if (!filename.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                filename += ".txt";
            }

            string projectDirectory = GetProjectDirectory();
            string saveDirectory = Path.Combine(projectDirectory, "SavedGraphs");

            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            string filePath = Path.Combine(saveDirectory, filename);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(graph.IsDirected ? "Directed" : "Undirected");

                foreach (var adjacencyListElement in graph.adjacencyList)
                {
                    string sourceVertexName = adjacencyListElement.Key.Name;
                    var edges = adjacencyListElement.Value;

                    if (edges.Count == 0)
                    {
                        writer.WriteLine($"{sourceVertexName}");
                    }
                    else
                    {
                        int index = 0;
                        string line = $"{sourceVertexName} => ";

                        foreach (var edge in edges)
                        {
                            line = $"{edge.Destination.Name}";

                            if (edge.Weight.HasValue)
                                line += $", w: {edge.Weight.Value}";

                            if (edge.Capacity.HasValue)
                                line += $", c: {edge.Capacity.Value}";

                            if (index < edges.Count - 1)
                                line += "|";

                            index++;
                        }
                        writer.WriteLine(line);
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает путь к папке проекта.
        /// </summary>
        /// <returns>Путь к папке проекта.</returns>
        private static string GetProjectDirectory()
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo directory = new(exePath);

            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }

            if (directory == null)
            {
                throw new DirectoryNotFoundException("Не удалось определить путь к папке проекта.");
            }

            return directory.FullName;
        }

        /// <summary>
        /// Создает полный путь к файлу внутри папки SavedGraphs.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Полный путь к файлу.</returns>
        /// <exception cref="ArgumentNullException">Если имя файла пустое и/или равно null.</exception>
        /// <exception cref="FileNotFoundException">Если указанный файл не найден.</exception>
        public static string CreateFilePath(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("Имя файла пустое и/или null");

            if (!fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                fileName += ".txt";
            }

            string projectDirectory = GetProjectDirectory();
            string filePath = Path.Combine(projectDirectory, "SavedGraphs", fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл '{filePath}' не найден.");
            }

            return filePath;
        }
    }
}
