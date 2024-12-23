using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GraphProject.Handlers;
using GraphProject.Structures;

namespace GraphVisualization
{
    /// <summary>
    /// Логика взаимодействия для FindVertexWindow.xaml
    /// </summary>
    public partial class FindVertexWindow : Window
    {
        private GraphProject.Structures.Graph _graph;
        private const bool NeedToInputN = true;

        public FindVertexWindow(GraphProject.Structures.Graph graph)
        {
            _graph = graph;
            InitializeComponent();
        }

        private void DisplayNonAdjacentVertex_Click(object sender, RoutedEventArgs e)
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

        private void DisplayVerticesWithGreaterOutDegree_Click(object sender, RoutedEventArgs e)
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
                        Console.WriteLine($"Вершины с именем {vertexName} - не существует");
                        return;
                    }

                    var vertexList = GraphSearcher.FindVerticesDistanceLessOrEqualN(vertexName, _graph, parsedN);
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

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }   
    }
}
                
