using Handlers;
using Structures;
using System;
using Algorithms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Handlers
{
    // Класс MenuHandler для взаимодействия с пользователем через меню
    public class MenuHandler
    {
        // Приватное поле graph, которое будет использоваться для хранения текущего графа
        private Graph? graph;

        private GraphVault GraphVault = new();

        // Приватное поле для выбора опций в главном меню
        private enum GraphLoadCreateMenuOption
        {
            CreateNewGraph,
            LoadGraphFromFile,
            Exit
        }

        // Приватное поле для выбора опций в меню управления графом
        private enum MainMenuOption
        {
            OpenGraphManager,
            OpenVertexManager,
            OpenGraphVaultManager,
            Exit
        }

        private enum GraphManagerMenyOption
        {
            SaveGraphToFile,
            RemoveLeafsEdges,
            AddVertex,
            AddEdge,
            RemoveVertex,
            RemoveEdge,
            DisplayGraph,
            IsGraphConnected,
            FindMst,
            Exit
        }

        // Приватное поле для выбора опций в меню списка графов
        private enum GraphVaultMenuOption
        {
            ShowList,
            RemoveAtNth,
            CopyCurrentGrpah,
            ChangeCurrentGraph,
            Exit
        }

        private enum VertexMenuOption
        {
            DisplayVerticesWithGreaterOutDegree,
            DisplayNonAdjacentVertices,
            FindUnreachableVertices,
            FindVerticesDistanceLessN,
            Exit
        }
        // Метод Start, запускающий основной цикл программы
        public void Start()
        {
            while (true)
            {
                // Вызов метода, отображающего меню загрузки графа
                ShowGraphLoadCreateMenu();
                // Считывание выбора пользователя из консоли
                string userChoice = Console.ReadLine();

                if (int.TryParse(userChoice, out int option))
                {
                    // Обработка выбора пользователя с помощью конструкции switch
                    switch ((GraphLoadCreateMenuOption)option)
                    {
                        case GraphLoadCreateMenuOption.CreateNewGraph:
                            // Создание нового графа
                            graph = CreateNewGraph();
                            break;
                        case GraphLoadCreateMenuOption.LoadGraphFromFile:
                            // Загрузка графа из файла
                            graph = LoadGraphFromFile();
                            break;
                        case GraphLoadCreateMenuOption.Exit:
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
                    HandleMainMenu();
                }

                // Вывод пустой строки для разделения выводов в консоли
                Console.WriteLine();
            }
        }

        // Приватный метод для обработки операций с графом после его создания или загрузки
        private void HandleMainMenu()
        {
            while (true)
            {
                ShowMainMenu();
                string? userChoice = Console.ReadLine();
                if (int.TryParse(userChoice, out int option))
                {
                    switch ((MainMenuOption)option)
                    {
                        case MainMenuOption.OpenGraphManager:
                            HandleGraphManagerOperations();
                            break;
                        case MainMenuOption.OpenVertexManager:
                            HandleVertexManagerOperations();
                            break;
                        case MainMenuOption.OpenGraphVaultManager:
                            HandleGraphVaultOperations();
                            break;
                        case MainMenuOption.Exit:
                            Console.WriteLine("Возвращение в главное меню.");
                            return;
                        default:
                            Console.WriteLine("Неверная опция. Попробуйте снова.");
                            break;
                    }
                }
            }
        }

        // Приватный метод для обработки операций с списком графов
        private void HandleGraphVaultOperations()
        {
            while (true)
            {
                ShowGraphVaultManagementMenu();
                string? userChoice = Console.ReadLine();
                if (int.TryParse(userChoice, out int option))
                {
                    switch ((GraphVaultMenuOption)option)
                    {
                        case GraphVaultMenuOption.ShowList:
                            ShowGraphList();
                            break;
                        case GraphVaultMenuOption.RemoveAtNth:
                            RemoveNthGraph();
                            break;
                        case GraphVaultMenuOption.CopyCurrentGrpah:
                            CopyCurrentGraph();
                            break;
                        case GraphVaultMenuOption.ChangeCurrentGraph:
                            ChangeCurrentGraph();
                            break;
                        case GraphVaultMenuOption.Exit:
                            Console.WriteLine("Возвращение в меню управления графом.");
                            return;
                        default:
                            Console.WriteLine("Неверная опция. Попробуйте снова.");
                            break;
                    }
                }
            }
        }

        // Приватный метод для обработки операций с графом
        private void HandleGraphManagerOperations()
        {
            while (true)
            {
                ShowGraphManagerMenu();
                string userChoice = Console.ReadLine();
                if (int.TryParse(userChoice, out int option))
                {
                    switch ((GraphManagerMenyOption)option)
                    {
                        case GraphManagerMenyOption.SaveGraphToFile:
                            SaveGraphToFile();
                            break;
                        case GraphManagerMenyOption.RemoveLeafsEdges:
                            RemoveLeafsEdges();
                            break;
                        case GraphManagerMenyOption.AddVertex:
                            AddVertex();
                            break;
                        case GraphManagerMenyOption.AddEdge:
                            AddEdge();
                            break;
                        case GraphManagerMenyOption.RemoveVertex:
                            RemoveVertex();
                            break;
                        case GraphManagerMenyOption.RemoveEdge:
                            RemoveEdge();
                            break;
                        case GraphManagerMenyOption.DisplayGraph:
                            DisplayGraph();
                            break;
                        case GraphManagerMenyOption.IsGraphConnected:
                            IsGraphConnected();
                            break;
                        case GraphManagerMenyOption.FindMst:
                            MST();
                            break;
                        case GraphManagerMenyOption.Exit:
                            Console.WriteLine("Возвращение в главное меню.");
                            return;
                        default:
                            Console.WriteLine("Неверная опция. Попробуйте снова.");
                            break;
                    }
                }
            }
        }

        // Приватный метод для обработки операций с вершинами
        private void HandleVertexManagerOperations()
        {
            while (true)
            {
                ShowVertexManagerMenu();
                string? userChoice = Console.ReadLine();
                if (int.TryParse(userChoice, out int option))
                {
                    switch ((VertexMenuOption)option)
                    {
                        case VertexMenuOption.DisplayVerticesWithGreaterOutDegree:
                            DisplayVerticesWithGreaterOutDegree();
                            break;
                        case VertexMenuOption.DisplayNonAdjacentVertices:
                            DisplayNonAdjacentVertices();
                            break;
                        case VertexMenuOption.FindUnreachableVertices:
                            FindUnreachableVertices();
                            break;
                        case VertexMenuOption.FindVerticesDistanceLessN:
                            FindVerticesDistanceLessN();
                            break;
                        case VertexMenuOption.Exit:
                            Console.WriteLine("Возвращение в главное меню.");
                            return;
                        default:
                            Console.WriteLine("Неверная опция. Попробуйте снова.");
                            break;
                    }
                }
            }
        }

        // Приватный метод для обработки операций с графом 
        private void ShowGraphVaultManagementMenu()
        {
            int index = 0;
            Console.WriteLine("\nМеню управления списком графов:");
            Console.WriteLine($"{index++}. Показать список");
            Console.WriteLine($"{index++}. Удалить n-й граф");
            Console.WriteLine($"{index++}. Дублировать текущий граф");
            Console.WriteLine($"{index++}. Поменять текущий граф");
            Console.WriteLine($"{index++}. Вернуться в главное меню");
            Console.Write("Выберите опцию: ");
        }

        // Приватный метод, отображающий главное меню
        private void ShowMainMenu()
        {
            int index = 0;
            Console.WriteLine("\n==Главное меню==");
            Console.WriteLine($"{index++}. Открыть меню управления графом.");
            Console.WriteLine($"{index++}. Открыть меню управления вершинами.");
            Console.WriteLine($"{index++}. Открыть меню управления списком графов");
            Console.WriteLine($"{index++}. Открыть меню создания и загрузки графа.");
            Console.Write("Выберите опцию: ");
        }

        // Приватный метод, отображающий меню управления графом
        private void ShowGraphManagerMenu()
        {
            int index = 0;
            Console.WriteLine("\n==Меню управления графом==");
            Console.WriteLine($"{index++}. Сохранить граф в файл.");
            Console.WriteLine($"{index++}. Построить граф, полученный удалением рёбер, ведущих в листья.");
            Console.WriteLine($"{index++}. Добавить вершину.");
            Console.WriteLine($"{index++}. Добавить ребро.");
            Console.WriteLine($"{index++}. Удалить вершину.");
            Console.WriteLine($"{index++}. Удалить ребро.");
            Console.WriteLine($"{index++}. Показать список смежности.");
            Console.WriteLine($"{index++}. Проверить является ли граф связным");
            Console.WriteLine($"{index++}. Найти минимальное остовное дерево");
            Console.WriteLine($"{index++}. Вернуться в главное меню.");
            Console.Write("Выберите опцию: ");
        }

        // Приватный метод, отображающий меню загруски и сошздания графа
        private void ShowGraphLoadCreateMenu()
        {
            int index = 0;
            Console.WriteLine("\n==Меню создания и загрузки графа==");
            Console.WriteLine($"{index++}. Создать новый граф");
            Console.WriteLine($"{index++}. Загрузить граф из файла");
            Console.WriteLine($"{index++}. Выход");
            Console.Write("Выберите опцию: ");
        }

        // Приватный метод, отображающий меню для работы с вершинами
        private void ShowVertexManagerMenu()
        {
            int index = 0;
            Console.WriteLine("\n==Меню управления вершинами==");
            Console.WriteLine($"{index++}. Вывести вершины с большей полустепенью исхода.");
            Console.WriteLine($"{index++}. Вывести вершины с несмежные с данной.");
            Console.WriteLine($"{index++}. Найти вершины недостижимые из данной.");
            Console.WriteLine($"{index++}. Найти множество вершины, расстояние от которых до заданной вершины не более N.");
            Console.WriteLine($"{index++}. Выход");
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
            GraphVault.CopyCurrentGrahp();
            Console.WriteLine("Текущий граф скопирован в конец списка");
        }

        // Приватный метод для загрузки графа из файла
        private Graph? LoadGraphFromFile()
        {
            while (true)
            {
                Console.Write("Введите имя файла для загрузки графа: ");
                string? loadFilename = Console.ReadLine();
                try
                {
                    var filePath = GraphFileHandler.CreateFilePath(loadFilename);
                    Console.Write("Введите имя графа: ");
                    string? input = Console.ReadLine();
                    string name = string.IsNullOrEmpty(input) ? loadFilename : input;
                    return new Graph(filePath, name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке графа: {ex.Message}");
                    return null;
                }
            }
        }

        // Приватный метод для добавления новой вершины в граф
        private void AddVertex()
        {
            Console.Write("Введите название вершины: ");
            string vertexName = Console.ReadLine();
            try
            {
                var vertex = new Vertex(vertexName.ToUpper());
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
            if (GraphVault.isVoultEmpty())
            {
                GraphVault.AddNewGraph(graph);
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
                        GraphVault.ReplaceCurrentGraph(graph);
                        return;
                    }
                    else if (input == "n")
                    {
                        Console.WriteLine("В список добавлен новый граф.");
                        GraphVault.AddNewGraph(graph);
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
            GraphPrinter.DisplayGraphList(GraphVault);
        }

        // Приватный метод для удаления графа по номеру
        private void RemoveNthGraph()
        {
            Console.WriteLine("Введите номер графа, который нужно удалить");
            string userChoice = Console.ReadLine();
            int index;
            if (int.TryParse(userChoice, out index))
            {
                bool isDeleted = GraphVault.RemoveGraph(index);
                if (isDeleted)
                {
                    Console.WriteLine($"Граф под номером {index} успешно удален");
                }
                else
                {
                    GraphPrinter.DisplayGraphIndexError(GraphVault);
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

                if (GraphVault.ChangeCurrentGraph(index))
                {
                    graph = GraphVault.GetCurrentGraph();
                    var newCurrentGraphIndex = GraphVault.GetCurrentGraphIndex();
                    var newCurrentGraphName = graph.GraphName;
                    Console.WriteLine("Успешно задан новый граф");
                    GraphPrinter.DisplayCurrentGraph(GraphVault);
                }
                GraphPrinter.DisplayGraphIndexError(GraphVault);
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
            if (vertexName == null || vertexName == "")
            {
                return;
            }
            GraphPrinter.DisplayUnreachableVertices(vertexName, graph);
        }

        // Приватный метод для проверки является ли граф связным
        private void IsGraphConnected()
        {
            if (GraphAnalyzer.IsGraphConnected(graph))
            {
                Console.WriteLine($"Граф {GraphVault.GetCurrentGraphIndex()} - связный");
            }
            else
            {
                Console.WriteLine($"Граф {GraphVault.GetCurrentGraphIndex()} - не связный");
            }
        }

        // Метод получения минимального остовного дерева
        private void MST()
        {
            if(!KruskalAlgorithm.IsGraphCorrectForMST(graph))
            {
                return;
            }
            Graph tmpGraph = KruskalAlgorithm.KruskalMST(graph);
            AddGraphToList(tmpGraph);
            GraphPrinter.DisplayAdjacencyList(tmpGraph);
            graph = tmpGraph;
        }

        // Метод нахождения вершин расстояние от заданной меньше N
        private void FindVerticesDistanceLessN()
        {
            Console.Write("Введите имя вершины: ");
            var vertexName = Console.ReadLine();
            if (vertexName == null || vertexName == "")
            {
                Console.WriteLine("Введите имя вершины.");
                return;
            }

            Console.Write("Введите N: ");
            var n = Console.ReadLine();
            double parsedN;
            if (n == null || n == "" || !double.TryParse(n, out parsedN))
            {
                return;
            }

            Vertex? vertex = GraphSearcher.FindVertexByName(vertexName, graph);
            if (vertex == null) 
            {
                Console.WriteLine($"Вершины с именем {vertexName} - не существует");
                return;
            }

            var vertexList = GraphSearcher.FindVerticesDistanceLessOrEqualN(vertexName, graph, parsedN);
            GraphPrinter.DisplayVertexDistanceLessN(vertexList, vertexName, parsedN);
        }
    }
}
