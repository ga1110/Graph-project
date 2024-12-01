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

namespace GraphVisualization
{

    public partial class MainWindow : Window
    {
        private Microsoft.Msagl.Drawing.Graph _msaglGraph;
        private GraphProject.Structures.Graph _graph;
        private GraphVoult _graphVoult = new();

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
                try
                {
                    GraphManager.AddEdge(startVertex, endVertex, _graph, weight);
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
    }
}

// TODO

// 1. ShowList
// 2. RemoveAtNth (GraphVoult)
// 3. ChangeCurrentGraph

// 1. SaveGraphToFile

