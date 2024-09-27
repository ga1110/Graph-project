using System;
using System.IO;
using Structures;
using Handlers;

namespace Handlers
{
    // Класс MenuHandler для взаимодействия с пользователем через меню
    public class MenuHandler
    {
        // Приватное поле graph, которое будет использоваться для хранения текущего графа
        private Graph graph;

        // Приватное поле graphManager для управления графом
        private GraphManager graphManager;

        // Метод Start, запускающий основной цикл программы
        public void Start()
        {
            while (true)
            {
                // Вызов метода, отображающего главное меню
                ShowMainMenu();
                // Считывание выбора пользователя из консоли
                string userChoice = Console.ReadLine();

                // Обработка выбора пользователя с помощью конструкции switch
                switch (userChoice)
                {
                    case "1":
                        // Создание нового графа
                        graph = CreateNewGraph();
                        // Инициализация GraphManager с новым графом
                        graphManager = new GraphManager(graph);
                        break;
                    case "2":
                        // Загрузка графа из файла
                        graph = LoadGraphFromFile();
                        // Инициализация GraphManager с загруженным графом
                        if (graph != null)
                            graphManager = new GraphManager(graph);
                        break;
                    case "3":
                        // Завершение программы
                        return;
                    default:
                        // Вывод сообщения об ошибке при неверном вводе
                        Console.WriteLine("Неверная опция. Попробуйте снова.");
                        break;
                }

                // Если граф успешно создан или загружен
                if (graph != null && graphManager != null)
                {
                    // Вызов метода для обработки операций с графом
                    HandleGraphOperations();
                }

                // Вывод пустой строки для разделения выводов в консоли
                Console.WriteLine();
            }
        }

        // Приватный метод, отображающий главное меню
        private void ShowMainMenu()
        {
            Console.WriteLine("Главное меню:");
            Console.WriteLine("1. Создать новый граф");
            Console.WriteLine("2. Загрузить граф из файла");
            Console.WriteLine("3. Выход");
            Console.Write("Выберите опцию: ");
        }

        // Приватный метод для создания нового графа
        private Graph CreateNewGraph()
        {
            while (true)
            {
                Console.Write("Создать ориентированный граф? (Y/N): ");
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "y")
                {
                    Console.WriteLine("Создание ориентированного графа.");
                    return new Graph(isDirected: true);
                }
                else if (input == "n")
                {
                    Console.WriteLine("Создание неориентированного графа.");
                    return new Graph(isDirected: false);
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите 'y' или 'n'.");
                }
            }
        }

        // Приватный метод для загрузки графа из файла
        private Graph LoadGraphFromFile()
        {
            while (true)
            {
                Console.Write("Введите имя файла для загрузки графа: ");
                string loadFilename = Console.ReadLine();

                try
                {
                    Graph loadedGraph = GraphFileHandler.LoadFromFile(loadFilename);
                    return loadedGraph;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке графа: {ex.Message}");
                    return null;
                }
            }
        }

        // Приватный метод для обработки операций с графом после его создания или загрузки
        private void HandleGraphOperations()
        {
            while (true)
            {
                ShowGraphManagementMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddVertex();
                        break;
                    case "2":
                        AddEdge();
                        break;
                    case "3":
                        RemoveVertex();
                        break;
                    case "4":
                        RemoveEdge();
                        break;
                    case "5":
                        DisplayGraph();
                        break;
                    case "6":
                        SaveGraphToFile();
                        break;
                    case "7":
                        DisplayVerticesWithGreaterOutDegree();
                        break;
                    case "8":
                        Console.WriteLine("Возвращение в главное меню.");
                        return;
                    default:
                        Console.WriteLine("Неверная опция. Попробуйте снова.");
                        break;
                }
            }
        }

        // Приватный метод, отображающий меню управления графом
        private void ShowGraphManagementMenu()
        {
            Console.WriteLine("\nМеню управления графом:");
            Console.WriteLine("1. Добавить вершину");
            Console.WriteLine("2. Добавить ребро");
            Console.WriteLine("3. Удалить вершину");
            Console.WriteLine("4. Удалить ребро");
            Console.WriteLine("5. Показать список смежности");
            Console.WriteLine("6. Сохранить граф в файл");
            Console.WriteLine("7. Вывести вершины с большей полустепенью исхода");
            Console.WriteLine("8. Вернуться в главное меню");
            Console.Write("Выберите опцию: ");
        }

        // Приватный метод для добавления новой вершины в граф
        private void AddVertex()
        {
            Console.Write("Введите название вершины: ");
            string vertexName = Console.ReadLine();
            try
            {
                var vertex = new Vertex(vertexName);
                graphManager.AddVertex(vertex);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Приватный метод для добавления нового ребра в граф
        private void AddEdge()
        {
            Console.Write("Введите исходную вершину: ");
            string source = Console.ReadLine();
            Console.Write("Введите конечную вершину: ");
            string destination = Console.ReadLine();
            Console.Write("Введите вес (опционально): ");
            string weightInput = Console.ReadLine();

            double? weight = null;
            if (double.TryParse(weightInput, out double parsedWeight))
            {
                weight = parsedWeight;
            }

            graphManager.AddEdge(source, destination, weight);
        }

        // Приватный метод для удаления вершины из графа
        private void RemoveVertex()
        {
            Console.Write("Введите название вершины для удаления: ");
            string vertexToRemove = Console.ReadLine();
            graphManager.RemoveVertex(vertexToRemove);
        }

        // Приватный метод для удаления ребра из графа
        private void RemoveEdge()
        {
            Console.Write("Введите исходную вершину: ");
            string edgeSource = Console.ReadLine();
            Console.Write("Введите конечную вершину: ");
            string edgeDestination = Console.ReadLine();
            graphManager.RemoveEdge(edgeSource, edgeDestination);
        }

        // Приватный метод для отображения списка смежности графа
        private void DisplayGraph()
        {
            graphManager.DisplayAdjacencyList();
        }

        // Приватный метод для сохранения графа в файл
        private void SaveGraphToFile()
        {
            Console.Write("Введите имя файла для сохранения графа: ");
            string saveFilename = Console.ReadLine();
            GraphFileHandler.SaveToFile(graph, saveFilename);
        }

        // Приватный метод для вывода вершин с большей полустепенью исхода
        private void DisplayVerticesWithGreaterOutDegree()
        {
            Console.Write("Введите название вершины для сравнения полустепени исхода: ");
            string vertexName = Console.ReadLine();
            graphManager.DisplayVerticesWithGreaterOutDegree(vertexName);
        }
    }
}
