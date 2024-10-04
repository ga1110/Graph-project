using Handlers;
using Structures;

namespace Handlers
{
    // Класс MenuHandler для взаимодействия с пользователем через меню
    public class MenuHandler
    {
        // Приватное поле graph, которое будет использоваться для хранения текущего графа
        private Graph graph;

        private GraphVoult graphVoult = new GraphVoult();

        // Приватное поле для выбора опций в главном меню
        private enum MainMenuOption
        {
            CreateNewGraph,
            LoadGraphFromFile,
            Exit
        }

        // Приватное поле для выбора опций в меню управления графом
        private enum GraphMenuOption
        {
            AddVertex,
            AddEdge,
            RemoveVertex,
            RemoveEdge,
            DisplayGraph,
            SaveGraphToFile,
            DisplayVerticesWithGreaterOutDegree,
            DisplayNonAdjacentVertices,
            RemoveLeafsEdges,
            FindUnreachableVertices,
            OpenGraphVoultManager,
            Exit
        }

        private enum GraphVoultMenuOption
        {
            ShowList,
            RemoveAtNth,
            CopyCurrentGrpah,
            ChangeCurrentGraph,
            Exit
        }

        // Метод Start, запускающий основной цикл программы
        public void Start()
        {
            while (true)
            {
                // Вызов метода, отображающего главное меню
                ShowMainMenu();
                // Считывание выбора пользователя из консоли
                string userChoice = Console.ReadLine();

                if (int.TryParse(userChoice, out int option))
                {
                    // Обработка выбора пользователя с помощью конструкции switch
                    switch ((MainMenuOption)option)
                    {
                        case MainMenuOption.CreateNewGraph:
                            // Создание нового графа
                            graph = CreateNewGraph();
                            break;
                        case MainMenuOption.LoadGraphFromFile:
                            // Загрузка графа из файла
                            graph = LoadGraphFromFile();
                            break;
                        case MainMenuOption.Exit:
                            // Завершение программы
                            return;
                        default:
                            Console.WriteLine("Неверная опция. Попробуйте снова.");
                            break;
                    }
                }
                // Если граф успешно создан или загружен
                if (graph != null)
                {
                    // Вызов метода для обработки операций с графом
                    AddGraphToList(graph);
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
            Console.WriteLine("0. Создать новый граф");
            Console.WriteLine("1. Загрузить граф из файла");
            Console.WriteLine("2. Выход");
            Console.Write("Выберите опцию: ");
        }

        // Приватный метод для обработки операций с графом после его создания или загрузки
        private void HandleGraphOperations()
        {
            while (true)
            {
                ShowGraphManagementMenu();
                string userChoice = Console.ReadLine();
                if (int.TryParse(userChoice, out int option))
                {
                    switch ((GraphMenuOption)option)
                    {
                        case GraphMenuOption.AddVertex:
                            AddVertex();
                            break;
                        case GraphMenuOption.AddEdge:
                            AddEdge();
                            break;
                        case GraphMenuOption.RemoveVertex:
                            RemoveVertex();
                            break;
                        case GraphMenuOption.RemoveEdge:
                            RemoveEdge();
                            break;
                        case GraphMenuOption.DisplayGraph:
                            DisplayGraph();
                            break;
                        case GraphMenuOption.SaveGraphToFile:
                            SaveGraphToFile();
                            break;
                        case GraphMenuOption.DisplayVerticesWithGreaterOutDegree:
                            DisplayVerticesWithGreaterOutDegree();
                            break;
                        case GraphMenuOption.DisplayNonAdjacentVertices:
                            DisplayNonAdjacentVertices();
                            break;
                        case GraphMenuOption.RemoveLeafsEdges:
                            RemoveLeafsEdges();
                            break;
                        case GraphMenuOption.FindUnreachableVertices:
                            FindUnreachableVertices();
                            break;
                        case GraphMenuOption.OpenGraphVoultManager:
                            HandleGraphVoultOperations();
                            break;
                        case GraphMenuOption.Exit:
                            Console.WriteLine("Возвращение в главное меню.");
                            return;
                        default:
                            Console.WriteLine("Неверная опция. Попробуйте снова.");
                            break;
                    }
                }
            }
        }

        // Приватный метод для обработки операций с графом после его создания или загрузки
        private void HandleGraphVoultOperations()
        {
            while (true)
            {
                ShowGraphVoultManagementMenu();
                string userChoice = Console.ReadLine();
                if (int.TryParse(userChoice, out int option))
                {
                    switch ((GraphVoultMenuOption)option)
                    {
                        case GraphVoultMenuOption.ShowList:
                            ShowGraphList();
                            break;
                        case GraphVoultMenuOption.RemoveAtNth:
                            RemoveNthGraph();
                            break;
                        case GraphVoultMenuOption.CopyCurrentGrpah:
                            CopyCurrentGraph();
                            break;
                        case GraphVoultMenuOption.ChangeCurrentGraph:
                            ChangeCurrentGraph();
                            break;
                        case GraphVoultMenuOption.Exit:
                            Console.WriteLine("Возвращение в меню управления графом.");
                            return;
                        default:
                            Console.WriteLine("Неверная опция. Попробуйте снова.");
                            break;
                    }
                }
            }
        }

        private void ShowGraphVoultManagementMenu()
        {
            Console.WriteLine("\nМеню управления списком графов:");
            Console.WriteLine("0. Показать список");
            Console.WriteLine("1. Удалить n-й граф");
            Console.WriteLine("2. Дублировать текущий граф");
            Console.WriteLine("3. Поменять текущий граф");
            Console.WriteLine("4. Вернуться в меню управления графом");
            Console.Write("Выберите опцию: ");
        }

        // Приватный метод для создания нового графа
        private Graph CreateNewGraph()
        {
            while (true)
            {
                Console.Write("Создать ориентированный граф? (Y/N): ");
                string input = Console.ReadLine().Trim().ToLower();

                Console.Write("Введите имя графа: ");
                string name = Console.ReadLine();

                if (input == "y")
                {
                    Console.WriteLine("Создание ориентированного графа.");
                    return new Graph(name, isDirected: true);
                }
                else if (input == "n")
                {
                    Console.WriteLine("Создание неориентированного графа.");
                    return new Graph(name, isDirected: false);
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите 'y' или 'n'.");
                }
            }
        }

        // Приватный метод для копирования текущего графа
        private void CopyCurrentGraph()
        {
            graphVoult.CopyCurrentGrahp();
            Console.WriteLine("Текущий граф скопирован в конец списка");
        }

        // Приватный метод для загрузки графа из файла
        private Graph LoadGraphFromFile()
        {
            while (true)
            {
                Console.Write("Введите имя файла для загрузки графа: ");
                string loadFilename = Console.ReadLine();

                Console.Write("Введите имя графа: ");
                string name = Console.ReadLine();
                try
                {
                    var filePath = GraphFileHandler.CreateFilePath(loadFilename);
                    return new Graph(filePath, name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке графа: {ex.Message}");
                    return null;
                }
            }
        }

        // Приватный метод, отображающий меню управления графом
        private void ShowGraphManagementMenu()
        {
            Console.WriteLine("\nМеню управления графом:");
            Console.WriteLine("0. Добавить вершину.");
            Console.WriteLine("1. Добавить ребро.");
            Console.WriteLine("2. Удалить вершину.");
            Console.WriteLine("3. Удалить ребро.");
            Console.WriteLine("4. Показать список смежности.");
            Console.WriteLine("5. Сохранить граф в файл.");
            Console.WriteLine("6. Вывести вершины с большей полустепенью исхода.");
            Console.WriteLine("7. Вывести вершины с несмежные с данной.");
            Console.WriteLine("8. Построить граф, полученный удалением рёбер, ведущих в листья.");
            Console.WriteLine("9. Найти вершины недостижимые из данной.");
            Console.WriteLine("10. Открыть меню управления списком графов");
            Console.WriteLine("11. Вернуться в главное меню.");
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
                GraphManager.AddVertex(vertex, graph);
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

            GraphManager.AddEdge(source, destination, graph, weight);
        }

        // Приватный метод для удаления вершины из графа
        private void RemoveVertex()
        {
            Console.Write("Введите название вершины для удаления: ");
            string vertexToRemove = Console.ReadLine();
            GraphManager.RemoveVertex(vertexToRemove, graph);
        }

        // Приватный метод для удаления ребра из графа
        private void RemoveEdge()
        {
            Console.Write("Введите исходную вершину: ");
            string edgeSource = Console.ReadLine();
            Console.Write("Введите конечную вершину: ");
            string edgeDestination = Console.ReadLine();
            GraphManager.RemoveEdge(edgeSource, edgeDestination, graph);
        }

        // Приватный метод для отображения списка смежности графа
        private void DisplayGraph()
        {
            GraphPrinter.DisplayAdjacencyList(graph);
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
            GraphPrinter.DisplayVerticesWithGreaterOutDegree(vertexName, graph);
        }

        // Приватный метод для вывода вершин с большей полустепенью исхода
        private void DisplayNonAdjacentVertices()
        {
            Console.Write("Введите название вершины для поиска несмежных вершин: ");
            string vertexName = Console.ReadLine();
            GraphPrinter.DisplayNonAdjacentVertices(vertexName, graph);
        }

        // Приватный метод для удаления ребер ведущих к листьям 
        private void RemoveLeafsEdges()
        {
            CopyCurrentGraph();
            var tmpList = GraphSearcher.FindLeafsEdges(graph);
            foreach (var pair in tmpList)
            {
                GraphManager.RemoveEdge(pair.Item1, pair.Item2, graph);
            }
        }

        // Приватный метод добавления графа в список
        private void AddGraphToList(Graph graph)
        {
            if (graphVoult.isVoultEmpty())
            {
                graphVoult.AddNewGraph(graph);
            }
            else
            {
                while (true)
                {
                    Console.Write("Заменить текущий граф новым? (Y/N): ");
                    string input = Console.ReadLine().Trim().ToLower();
                    if (input == "y")
                    {
                        Console.WriteLine("Текущий граф заменён.");
                        graphVoult.ReplaceCurrentGraph(graph);
                        return;
                    }
                    else if (input == "n")
                    {
                        Console.WriteLine("В список добавлен новый граф.");
                        graphVoult.AddNewGraph(graph);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод. Пожалуйста, введите 'y' или 'n'.");
                    }
                }
            }
        }

        // Приватный метод для вывода списка графов
        private void ShowGraphList()
        {
            GraphPrinter.DisplayGraphList(graphVoult);
        }

        // Приватный метод для удаления графа по номеру
        private void RemoveNthGraph()
        {
            Console.WriteLine("Введите номер графа, который нужно удалить");
            string userChoice = Console.ReadLine();
            int index;
            if (int.TryParse(userChoice, out index))
            {
                bool isDeleted = graphVoult.RemoveGraph(index);
                if (isDeleted)
                {
                    Console.WriteLine($"Граф под номером {index} успешно удален");
                }
                else
                {
                    GraphPrinter.DisplayGraphIndexError(graphVoult);
                }
            }
            else
            {
                Console.WriteLine("Ошибка: введено не число.");
            }

        }

        // Приватный метод для смены текущего графа
        private void ChangeCurrentGraph()
        {
            Console.Write("Введите номер нового текущего графа: ");
            string userChoice = Console.ReadLine();
            int index;
            if (int.TryParse(userChoice, out index))
            {

                if (graphVoult.ChangeCurrentGraph(index))
                {
                    graph = graphVoult.GetCurrentGraph();
                    var newCurrentGraphIndex = graphVoult.GetCurrentGraphIndex();
                    var newCurrentGraphName = graph.GraphName;
                    Console.WriteLine("Успешно задан новый граф");
                    GraphPrinter.DisplayCurrentGraph(graphVoult);
                }
                GraphPrinter.DisplayGraphIndexError(graphVoult);
            }
            else
            {
                Console.WriteLine("Ошибка: введено не число.");
            }
        }

        // Приватный метод для поиска не смежных вершин
        private void FindUnreachableVertices()
        {
            Console.Write("Введите имя вершины: ");
            var vertexName = Console.ReadLine();
            GraphPrinter.DisplayUnreachableVertices(vertexName, graph);
        }
    }
}
