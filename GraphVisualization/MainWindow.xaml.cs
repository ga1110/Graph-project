﻿using System.Windows;
using GraphProject.Algorithms;
using System.Windows.Controls;
using GraphVisualization.Algorithms;
using System;
using System.Linq;
using GraphProject.Structures;
using GraphProject.Handlers;
using System.Collections.Generic;
using System.Windows.Input;
using System.IO;
namespace GraphVisualization
{

    public partial class MainWindow : Window
    {
        private Microsoft.Msagl.Drawing.Graph _msaglGraph;
        private GraphProject.Structures.Graph _graph;
        private GraphProject.Structures.Graph _graphTMP;
        private GraphVoult _graphVoult = new();
        public Logger Logger = new Logger();
        private const bool NeedToInputN = true;
        private bool isFullScreen = false;

        private bool _stepByStep = false;
        private List<Graph> _flowGraphs;
        private int _currentGraphInFlowList = 0;
        public MainWindow()
        {
            InitializeComponent();
            Logger.SetTextBox(HistoryTextBox);
        }

        private void DisplayGraph()
        {
            if (_graph == null)
            {
                return;
            }
            else
            {
                _msaglGraph = GraphConverter.Execute(_graph);
                graphControl.Graph = _msaglGraph;
                UpdateInfo();
            }
        }

        private void UpdateInfo()
        {
            InfoTextBox.Text = "";
            if (_graph == null)
            {
                return;
            }
            InfoTextBox.Text += $"Имя графа: {_graph.Name}";
            InfoTextBox.Text += $"\nНомер графа: {_graphVoult.GetCurrentGraphIndex()}";
            InfoTextBox.Text += $"\nГраф - {(GraphAnalyzer.IsGraphConnected(_graph) ? "связный" : "не связный")}";

            AdjTextBox.Text = AdjacencyListToStr.ToStr(_graph);
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            // Предотвращаем дальнейшее распространение события
            e.Handled = true;

            // Получаем текущий развёрнутый Expander
            Expander expandedExpander = sender as Expander;

            // Получаем родительский контейнер (StackPanel)
            StackPanel parentPanel = expandedExpander.Parent as StackPanel;

            if (parentPanel != null)
            {
                // Проходим по всем дочерним элементам контейнера
                foreach (var child in parentPanel.Children)
                {
                    if (child is Expander expander && expander != expandedExpander)
                    {
                        // Сворачиваем все остальные Expanders
                        expander.IsExpanded = false;
                    }
                }
            }
        }

        private void AddVertexButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            AddRemoveVertexWindow addRemoveVertexWindow = new();
            addRemoveVertexWindow.Owner = this;

            if (addRemoveVertexWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string newVertex = addRemoveVertexWindow.Vertex;

                try
                {
                    GraphManager.AddVertex(new Vertex(newVertex), _graph);
                    Logger.Add($"Вершина {newVertex} успешно добавлена");
                    DisplayGraph();
                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void RemoveVertexButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            AddRemoveVertexWindow addRemoveVertexWindow = new();
            addRemoveVertexWindow.Owner = this;

            if (addRemoveVertexWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string vertexToRemove = addRemoveVertexWindow.Vertex;

                try
                {
                    GraphManager.RemoveVertex(vertexToRemove, _graph);
                    Logger.Add($"Вершина {vertexToRemove} успешно удалена");
                    DisplayGraph();
                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void AddEdgeButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            AddRemoveEdgeWindow addRemoveEdgeWindow = new AddRemoveEdgeWindow(true);
            addRemoveEdgeWindow.Owner = this;
            var test = graphControl;
            if (addRemoveEdgeWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string startVertex = addRemoveEdgeWindow.StartVertex;
                string endVertex = addRemoveEdgeWindow.EndVertex;
                double? weight = addRemoveEdgeWindow.Weight;
                double? capacity = addRemoveEdgeWindow.Capacity;
                try
                {
                    GraphManager.AddEdge(startVertex, endVertex, _graph, weight, capacity);
                    string? weightTolog = weight == null? null : "Вес: " + weight.ToString();
                    string? capacityTolog = weight == null? null : "Ёмкость: " + capacity.ToString();
                    Logger.Add($"Ребро {startVertex} - {endVertex} {weightTolog} {capacityTolog} успешно добавлено");
                    DisplayGraph();
                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void RemoveEdgeButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            AddRemoveEdgeWindow addRemoveEdgeWindow = new AddRemoveEdgeWindow(false);
            addRemoveEdgeWindow.Owner = this;

            if (addRemoveEdgeWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string startVertex = addRemoveEdgeWindow.StartVertex;
                string endVertex = addRemoveEdgeWindow.EndVertex;
                try
                {
                    GraphManager.RemoveEdge(startVertex, endVertex, _graph);
                    Logger.Add($"Ребро {startVertex} - {endVertex} успешно удалено");
                    DisplayGraph();
                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void CreateGraphButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            CreateGraphWindow createGraphWindow = new(!_graphVoult.IsEmpty())
            {
                Owner = this
            };

            if (createGraphWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string graphName = createGraphWindow.GraphName;
                bool isDirected = createGraphWindow.IsDirected;
                bool isAddToVoult = createGraphWindow.IsAddToVoult;

                try
                {
                    _graph = new GraphProject.Structures.Graph(graphName, isDirected);
                    if (isAddToVoult)
                        _graphVoult.AddNewGraph(_graph);

                    Logger.Add($"Граф - {graphName} успешно создан");

                    DisplayGraph();

                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void LoadGraphButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            LoadGraphWindow loadGraphWindow = new(!_graphVoult.IsEmpty())
            {
                Owner = this
            };

            if (loadGraphWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string filePath = loadGraphWindow.FilePath;
                bool isAddToVoult = loadGraphWindow.IsAddToVoult;
                string graphName = loadGraphWindow.GraphName;

                try
                {
                    _graph = new GraphProject.Structures.Graph(filePath, graphName);
                    if (_graphVoult.IsEmpty())
                    {
                        _graphVoult.AddNewGraph(_graph);
                    }
                    else
                    {
                        if (isAddToVoult)
                        {
                            _graphVoult.ReplaceCurrentGraph(_graph);
                        }

                        else
                        {
                            _graphVoult.AddNewGraph(_graph);
                        }
                    }
                    Logger.Add($"Граф - {graphName} успешно загружен");
                    DisplayGraph();

                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void SaveGraphToFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_graph == null)
                    throw new Exception("Нельзя сохранить пустой граф в файл");

                GraphFileHandler.SaveToFile(_graph, _graph.Name);
                Logger.Add($"Граф - {_graph.Name} успешно сохранен в файл ");
            }
            catch (Exception ex)
            {
                Logger.Add(ex);
                return;
            }
        }

        private void CopyGraphButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_graphVoult.IsEmpty() || _graph == null)
                {
                    Logger.Add($"Ошибка!\nНельзя скопировать null-граф");
                    return;
                }

                _graphVoult.CopyCurrentGrahp();

                Logger.Add($"Граф - {_graph.Name} успешно скопирован");

                DisplayGraph();

            }
            catch (Exception ex)
            {
                Logger.Add(ex);
                return;
            }
        }

        private void FindMST_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!KruskalAlgorithm.IsGraphCorrectForMST(_graph))
                {
                    Logger.Add($"Граф - {_graph.Name} не подходит для алгоритма поиска MST");
                    return;
                }
                GraphProject.Structures.Graph tmpGraph = KruskalAlgorithm.KruskalMST(_graph);
                _graphVoult.AddNewGraph(tmpGraph);
                _graph = tmpGraph;
                Logger.Add($"Для графа - {_graph.Name} MST успешно найденно");
                DisplayGraph();
            }
            catch (Exception ex)
            {
                Logger.Add(ex);
                return;
            }
        }

        private void RemoveLeafEdgeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tmpList = GraphSearcher.FindLeafsEdges(_graph);
                foreach (var pair in tmpList)
                {
                    GraphManager.RemoveEdge(pair.Item1, pair.Item2, _graph);
                }
                Logger.Add("Все вершины ведущие в листья успешно удалены");
                DisplayGraph();
            }
            catch (Exception ex)
            {
                Logger.Add(ex);
                return;
            }
        }

        private void OpenGraphVoult_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            GraphListWindow graphListWindow = new(_graphVoult);
            graphListWindow.Owner = this;
            if (graphListWindow.ShowDialog() == true)
            {
                try
                {
                    // Получаем данные от пользователя
                    var currentGraphNumber = graphListWindow.CurrentNumber;
                    _graphVoult.ChangeCurrentGraph(currentGraphNumber);
                    _graph = _graphVoult.GetCurrentGraph();
                    DisplayGraph();
                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void FindNonAdjacentVertex_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            VertexChooseWindow vertexChooseWindow = new(!NeedToInputN)
            {
                Owner = this
            };

            if (vertexChooseWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string vertexName = vertexChooseWindow.Vertex;
                string output = "";
                try
                {
                    List<Vertex>? nonAdjacentVertices = GraphSearcher.FindNonAdjacentVertices(vertexName, _graph);
                    if (nonAdjacentVertices == null)
                    {
                        Logger.Add($"Ошибка!\nВершина не найдена");
                        return;
                    }
                    // Вывод результата
                    if (nonAdjacentVertices.Count > 0)
                    {
                        output += ($"Вершины, не смежные с вершиной {vertexName}:") + "\n";
                        int index = 1;
                        foreach (var vertex in nonAdjacentVertices)
                        {
                            output += ($"{index++}. {vertex.Name}") + "\n";
                        }
                    }
                    else
                    {
                        output += ($"Нет вершин не смежных с '{vertexName}'.");
                    }
                    Logger.Add(output);
                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void FindVerticesWithGreaterOutDegree_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            VertexChooseWindow vertexChooseWindow = new(!NeedToInputN)
            {
                Owner = this
            };

            if (vertexChooseWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string vertexName = vertexChooseWindow.Vertex;
                string output = "";
                try
                {
                    if (_graph == null)
                    {
                        throw new ArgumentNullException("Граф не может быть null.");
                    }

                    List<Vertex>? verticesWithGreaterOutDegree = GraphSearcher.FindVerticesWithGreaterOutDegree(vertexName, _graph);

                    if (verticesWithGreaterOutDegree == null)
                        throw new Exception($"Вершина '{vertexName}' не найдена в графе.");

                    Vertex currVertex = GraphSearcher.FindVertexByName(vertexName, _graph);

                    if (verticesWithGreaterOutDegree == null)
                        throw new ArgumentNullException($"Вершина '{vertexName}' не найдена в графе.");

                    int givenVertexOutDegree = VertexAnalyzer.GetOutDegree(currVertex, _graph);
                    if (verticesWithGreaterOutDegree.Count > 0)
                    {
                        int index = 1;
                        foreach (var vertex in verticesWithGreaterOutDegree)
                        {
                            int outDegree = VertexAnalyzer.GetOutDegree(vertex, _graph);
                            output += ($"{index++}. {vertex.Name} (исходящая степень {outDegree})") + "\n";
                        }
                    }
                    else
                    {
                        output += $"Нет вершин таких вершин" + "\n";
                    }
                    Logger.Add($"Вершины с большей полустепенью исхода {vertexName} (исходящая степень {givenVertexOutDegree}): \n{output}");
                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void FindUnreachableVertices_Click(object sender, EventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            VertexChooseWindow vertexChooseWindow = new(!NeedToInputN)
            {
                Owner = this
            };

            if (vertexChooseWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string vertexName = vertexChooseWindow.Vertex;
                string output = "";
                try
                {
                    List<Vertex> unreachableVertices = GraphSearcher.FindUnreachableVertices(vertexName, _graph);
                    int index = 1;
                    if (unreachableVertices != null && unreachableVertices.Count() != 0)
                    {
                        output += $"Cписок вершин недостижимых из {vertexName}:" + "\n";
                        foreach (var vertex in unreachableVertices)
                        {
                            output += $"{index}. {vertex.Name}" + "\n";
                            index++;
                        }
                    }
                    else
                    {
                        output += $"Нет вершин недостижимых из {vertexName}." + "\n";
                    }
                    Logger.Add(output);
                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void FindVerticesDistanceLessN_Click(object sender, EventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            VertexChooseWindow vertexChooseWindow = new(NeedToInputN)
            {
                Owner = this
            };

            if (vertexChooseWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string vertexName = vertexChooseWindow.Vertex;
                string output = "";
                double parsedN = vertexChooseWindow.N;
                try
                {
                    Vertex? vertex = GraphSearcher.FindVertexByName(vertexName, _graph);
                    if (vertex == null)
                    {
                        throw new Exception($"Вершины с именем {vertexName} - не существует");
                    }

                    var vertexList = GraphSearcher.FindVerticesDistanceLessOrEqualN(vertexName, _graph, parsedN);
                    if (vertexList == null || vertexList.Count() == 0)
                    {
                        output += ($"Вершин, расстояние которых, от вершины {vertexName}, меньше {parsedN} - нет \n");
                    }
                    else
                    {
                        output += $"Вершины, расстояние которых, от вершины {vertexName}, меньше {parsedN}:\n";
                        int index = 1;
                        vertexList = vertexList.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                        foreach (var currentElement in vertexList)
                        {
                            output += ($"{index++}. {currentElement.Key.Name}, расстояние: {currentElement.Value} \n");
                            
                        }
                    }

                    Logger.Add(output);
                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void FindShortestPath_Click(object sender, EventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            VertexChooseWindow vertexChooseWindow = new(!NeedToInputN)
            {
                Owner = this
            };

            if (vertexChooseWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string vertexName = vertexChooseWindow.Vertex;
                string output = "";
                try
                {
                    var distances = GraphSearcher.FindShortestPathsFrom(vertexName, _graph);
                    output += $"Длинны кратчайших путей от {vertexName}:" + "\n";
                    int index = 1;
                    foreach (var vertex in distances)
                    {
                        output += $"{index++}. {vertex.Key} - {vertex.Value}" + "\n";
                    }
                    Logger.Add(output);
                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void FindNPeriphery_Click(object sender, EventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            VertexChooseWindow vertexChooseWindow = new(NeedToInputN)
            {
                Owner = this
            };

            if (vertexChooseWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string vertexName = vertexChooseWindow.Vertex;
                string output = "";
                double parsedN = vertexChooseWindow.N;
                try
                {
                    Vertex? vertex = GraphSearcher.FindVertexByName(vertexName, _graph);
                    if (vertex == null)
                    {
                        throw new Exception($"Вершины с именем {vertexName} - не существует");
                    }

                    var periphery = GraphAnalyzer.FindNPeriphery(_graph, vertex, parsedN);
                    if (periphery == null || periphery.Count() == 0)
                    {
                        output += "N-переферия не существует \n";
                    }
                    else
                    {
                        output += $"N-переферия для вершины {vertexName}, при N = {parsedN} \n";
                        int index = 1;
                        foreach (var element in periphery)
                        {
                            output += $"{index++}. {element}" + "\n";
                        }

                    }
                    Logger.Add(output);
                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void FindMaxFlow_Click(object sender, EventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            MaxFlowVertexChoose maxFlowVertexChoose = new()
            {
                Owner = this
            };

            if (maxFlowVertexChoose.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string source = maxFlowVertexChoose.SourceVertex;
                string sink = maxFlowVertexChoose.SinkVertex;
                bool view = maxFlowVertexChoose.View;
                try
                {
                    Vertex? sourceVertex = GraphSearcher.FindVertexByName(source, _graph);
                    if (_graph == null)
                    {
                        throw new Exception($"Граф - null");
                    }
                    if (sourceVertex == null)
                    {
                        throw new Exception($"Вершины с именем {source} - не существует");
                    }

                    Vertex? sinkVertex = GraphSearcher.FindVertexByName(sink, _graph);
                    if (sinkVertex == null)
                    {
                        throw new Exception($"Вершины с именем {source} - не существует");
                    }
                    var maxflow = GraphAnalyzer.FindMaxFlow(_graph, source, sink);
                    InfoTextBox.Text += $"\nМаксимальный поток - {maxflow.Item1}";
                    Logger.Add($"Максимальный поток - {maxflow.Item1}");

                    if (view)
                    {
                        _stepByStep = true;
                        _graphTMP = new GraphProject.Structures.Graph(_graph);
                        _graph = maxflow.Item2[0];
                        _flowGraphs = maxflow.Item2;
                        DisableExpanders();
                    }

                }
                catch (Exception ex)
                {
                    Logger.Add(ex);
                    return;
                }
            }
        }

        private void ShowHints()
        {
            string hint = "'Q' - Вернуть граф в исходное положение\n" +
                          "'Esc' - Закрыть приложение\n" +
                          "'Tab' - Переключение полноэкранного режима\n";
            if(_stepByStep)
            {
                hint += "'[' - Предыдущий шаг алгоритма\n" +
                        "']' - Следующий шаг алгоритма\n";
            }

            MessageBox.Show(hint);
        }
        private void DisableExpanders()
        {
            FirstExpander.IsEnabled = false;
            SecondExpander.IsEnabled = false;
            ThirdExpander.IsEnabled = false;
            FourthExpander.IsEnabled = false;
            FifthExpander.IsEnabled = false;

            FirstExpander.Visibility = Visibility.Hidden;
            SecondExpander.Visibility = Visibility.Hidden;
            ThirdExpander.Visibility = Visibility.Hidden;
            FourthExpander.Visibility = Visibility.Hidden;
            FifthExpander.Visibility = Visibility.Hidden;
        }

        private void EnableExpanders()
        {
            FirstExpander.IsEnabled = true;
            SecondExpander.IsEnabled = true;
            ThirdExpander.IsEnabled = true;
            FourthExpander.IsEnabled = true;
            FifthExpander.IsEnabled = true;

            FirstExpander.Visibility = Visibility.Visible;
            SecondExpander.Visibility = Visibility.Visible;
            ThirdExpander.Visibility = Visibility.Visible;
            FourthExpander.Visibility = Visibility.Visible;
            FifthExpander.Visibility = Visibility.Visible;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
            {
                DisplayGraph();
                return;
            }
            if (e.Key == Key.Escape)
            {
                Close();
                return;
            }
            if (e.Key == Key.Tab)
            {
                if (isFullScreen)
                {
                    // Возврат в обычный режим
                    this.WindowState = WindowState.Normal;
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.ResizeMode = ResizeMode.CanResize;
                    isFullScreen = false;
                }
                else
                {
                    // Переход в полноэкранный режим
                    this.WindowState = WindowState.Maximized;
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                    this.ResizeMode = ResizeMode.NoResize;
                    isFullScreen = true;
                }
                return;
            }
            if (e.Key == Key.I)
            {
                ShowHints();
                return;
            }
            if (e.Key == Key.OemOpenBrackets)
            {
                if(_stepByStep)
                {
                    if (!(_currentGraphInFlowList - 1 < 0))
                    {
                        _currentGraphInFlowList -= 1;
                        _graph = _flowGraphs[_currentGraphInFlowList];
                        DisplayGraph();
                        return;
                    }
                }    
                return;
            }

            if (e.Key == Key.OemCloseBrackets)
            {
                if (_stepByStep)
                {
                    if (_currentGraphInFlowList + 1 == _flowGraphs.Count - 1)
                    {
                        MessageBox.Show("Последний шаг алгоритма");
                        _currentGraphInFlowList += 1;
                        _graph = _flowGraphs[_currentGraphInFlowList];
                        DisplayGraph();
                        return;
                    }

                    if (_currentGraphInFlowList == _flowGraphs.Count - 1)
                    {
                        _currentGraphInFlowList = 0;
                        _stepByStep = false;
                        _flowGraphs = null;
                        _graph = new Graph(_graphTMP);
                        _graphTMP = null;
                        EnableExpanders();
                        DisplayGraph();
                        return;
                    }

                    _currentGraphInFlowList += 1;
                    _graph = _flowGraphs[_currentGraphInFlowList];
                    DisplayGraph();
                    return;
                }
                return;
            }
        }
    }
}
