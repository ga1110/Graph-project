using System.Windows;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.WpfGraphControl;
using Microsoft.Msagl.Layout.MDS;
using GraphProject.Algorithms;
using System.Xml.Linq;
using Microsoft.Msagl.Core.Geometry.Curves;
using System.Windows.Controls;
using System.Windows.Media;
using GraphVisualization.Algorithms;
using System;
using System.Linq;
using GraphProject.Structures;
using GraphProject.Handlers;
using System.Collections.Generic;
using System.Reflection;

namespace GraphVisualization
{

    public partial class MainWindow : Window
    {
        private Microsoft.Msagl.Drawing.Graph _msaglGraph;
        private GraphProject.Structures.Graph _graph;
        private GraphVoult _graphVoult = new();
        private const bool NeedToInputN = true;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void DisplayGraph()
        {
            _msaglGraph = GraphConverter.Execute(_graph);
            graphControl.Graph = _msaglGraph;
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            InfoTextBox.Text = "Информация о графе:\n";
            if (_graph == null)
            {
                return;
            }
            InfoTextBox.Text += $"\nИмя графа: {_graph.GraphName}";
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
                    DisplayGraph();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    DisplayGraph();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddEdgeButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            AddRemoveEdgeWindow addRemoveEdgeWindow = new AddRemoveEdgeWindow(true);
            addRemoveEdgeWindow.Owner = this;

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
                    DisplayGraph();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    DisplayGraph();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CreateGraphButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            CreateGraphWindow createGraphWindow = new(!_graphVoult.isVoultEmpty())
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
                    DisplayGraph();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoadGraphButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            LoadGraphWindow loadGraphWindow = new(!_graphVoult.isVoultEmpty())
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
                    if (_graphVoult.isVoultEmpty())
                    {
                        _graphVoult.AddNewGraph(_graph);
                    }
                    else
                    {
                        if (isAddToVoult)
                            _graphVoult.ReplaceCurrentGraph(_graph);
                        else
                            _graphVoult.AddNewGraph(_graph);
                    }
                    DisplayGraph();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CopyGraphButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_graphVoult.isVoultEmpty() || _graph == null)
                {
                    throw new ArgumentNullException("Нельзя скопировать null-граф");
                }

                _graphVoult.CopyCurrentGrahp();
                DisplayGraph();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindVertex_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            FindVertexWindow findVertexWindow = new(_graph)
            {
                Owner = this
            };

            findVertexWindow.ShowDialog();
        }

        private void FindMST_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!KruskalAlgorithm.IsGraphCorrectForMST(_graph))
                {
                    return;
                }
                GraphProject.Structures.Graph tmpGraph = KruskalAlgorithm.KruskalMST(_graph);
                _graphVoult.AddNewGraph(tmpGraph);
                _graph = tmpGraph;
                DisplayGraph();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show(_graphVoult.GetGrahpsList().Count().ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        throw new Exception("Вершина не найдена");
                    }
                    // Вывод результата
                    if (nonAdjacentVertices.Count > 0)
                    {
                        output += ($"Вершины, не смежные с вершиной {vertexName}:") + "\n";
                        foreach (var vertex in nonAdjacentVertices)
                        {
                            output += ($"- {vertex.Name}") + "\n";
                        }
                    }
                    else
                    {
                        output += ($"Все вершины смежны с вершиной '{vertexName}'.");
                    }
                    MessageBox.Show(output, $"Вершины не смежные с {vertexName}", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        throw new ArgumentNullException(nameof(_graph), "Граф не может быть null.");

                    List<Vertex>? verticesWithGreaterOutDegree = GraphSearcher.FindVerticesWithGreaterOutDegree(vertexName, _graph);

                    if (verticesWithGreaterOutDegree == null)
                        throw new ArgumentNullException($"Вершина '{vertexName}' не найдена в графе.");

                    Vertex currVertex = GraphSearcher.FindVertexByName(vertexName, _graph);

                    if (verticesWithGreaterOutDegree == null)
                        throw new ArgumentNullException($"Вершина '{vertexName}' не найдена в графе.");

                    int givenVertexOutDegree = VertexAnalyzer.GetOutDegree(currVertex, _graph);
                    if (verticesWithGreaterOutDegree.Count > 0)
                    {
                        output += $"Вершины, полустепень исхода которых больше, чем у вершины '{vertexName}' (исходящая степень {givenVertexOutDegree}):" + "\n";
                        foreach (var vertex in verticesWithGreaterOutDegree)
                        {
                            int outDegree = VertexAnalyzer.GetOutDegree(vertex, _graph);
                            output += ($"- {vertex.Name} (исходящая степень {outDegree})") + "\n";
                        }
                    }
                    else
                    {
                        output += $"Нет вершин с полустепенью исхода, большей чем у вершины '{vertexName}' (исходящая степень {givenVertexOutDegree})." + "\n";
                    }
                    MessageBox.Show(output, $"Вершины с большей полустепенью исхода {vertexName}", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        output += $"Cписок вершин не достижимых из {vertexName}:" + "\n";
                        foreach (var vertex in unreachableVertices)
                        {
                            output += $"{index}. {vertex.Name}" + "\n";
                            index++;
                        }
                    }
                    else
                    {
                        output += $"Нет вершин не достижимых из {vertexName}." + "\n";
                    }
                    MessageBox.Show(output, $"Вершины с большей полустепенью исхода {vertexName}", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        output += ($"Вершин, расстояние которых от вершины {vertexName}, меньше {parsedN} - нет \n");
                    }
                    else
                    {
                        int index = 1;
                        vertexList = vertexList.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                        foreach (var currentElement in vertexList)
                        {
                            output += ($"{index++}. {currentElement.Key.Name}, расстояние: {currentElement.Value} \n");
                            
                        }
                    }

                    MessageBox.Show(output, $"Вершины с большей полустепенью исхода {vertexName}", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show(output, $"Длинны кратчайших путей от {vertexName}", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        Console.WriteLine($"Вершины с именем {vertexName} - не существует");
                        return;
                    }

                    var periphery = GraphSearcher.FindNPeriphery(_graph, vertex, parsedN);
                    if (periphery == null || periphery.Count() == 0)
                    {
                        output += "N-переферия не существует \n";
                    }
                    else
                    {
                        int index = 0;
                        foreach (var element in periphery)
                        {
                            output += $"{index++}. {element}" + "\n";
                        }

                    }
                    MessageBox.Show(output, $"N-переферия для {vertexName}", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

                try
                {
                    Vertex? sourceVertex = GraphSearcher.FindVertexByName(source, _graph);
                    if (sourceVertex == null)
                    {
                        Console.WriteLine($"Вершины с именем {source} - не существует");
                        return;
                    }

                    Vertex? sinkVertex = GraphSearcher.FindVertexByName(sink, _graph);
                    if (sinkVertex == null)
                    {
                        Console.WriteLine($"Вершины с именем {source} - не существует");
                        return;
                    }

                    var maxflow = GraphSearcher.FindMaxFlow(_graph, source, sink);
                    DisplayGraph();
                    InfoTextBox.Text += $"\nМаксимальный поток - {maxflow}";
                    MessageBox.Show(maxflow.ToString(), $"Максимальный поток", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
