﻿using System.Windows;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.WpfGraphControl;
using Microsoft.Msagl.Layout.MDS;
using Structures;
using Handlers;
using Algorithms;
namespace GraphVisualization
{
    
    public partial class MainWindow : Window
    {
        private GraphViewer graphViewer;
        private Microsoft.Msagl.Drawing.Graph MsaglGraph;
        private Structures.Graph MyGraph;
        public MainWindow()
        {
            InitializeComponent();
            InitializeGraphViewer();
            DisplayGraph();
        }

        private void InitializeGraphViewer()
        {
            // Создаем экземпляр GraphViewer
            graphViewer = new GraphViewer();

            // Связываем GraphViewer с контейнером
            graphViewer.BindToPanel(graphContainer);
        }
            
        private void DisplayGraph()
        {
            // Назначаем граф для отображения
            graphViewer.Graph = MsaglGraph;
            //MsaglGraph = GraphConverter.Execute(MyGraph);
            // Центрируем граф в контейнере
            graphViewer.Invalidate();
        }

        private void MakeUndirected(Microsoft.Msagl.Drawing.Edge edge)
        {
            edge.Attr.ArrowheadAtTarget = ArrowStyle.None;
            edge.Attr.ArrowheadAtSource = ArrowStyle.None;
        }

        private void AddEdgeButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и отображаем диалоговое окно
            AddEdgeWindow addEdgeWindow = new AddEdgeWindow();
            addEdgeWindow.Owner = this;

            if (addEdgeWindow.ShowDialog() == true)
            {
                // Получаем данные от пользователя
                string startVertex = addEdgeWindow.StartVertex;
                string endVertex = addEdgeWindow.EndVertex;
                double? weight = addEdgeWindow.Weight;

                // Добавляем ребро в граф
                Microsoft.Msagl.Drawing.Edge edge = MsaglGraph.AddEdge(startVertex, endVertex);

                // Устанавливаем вес, если он задан
                if (weight.HasValue)
                {
                    edge.LabelText = weight.Value.ToString();
                }

                // Настраиваем ребро как неориентированное
                MakeUndirected(edge);

                // Обновляем отображение графа
                graphViewer.Graph = MsaglGraph;
                graphViewer.Invalidate(); // Обновляем отображение
            }
        }

    }
}
