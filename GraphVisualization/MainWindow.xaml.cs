using System.Windows;
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
            graphViewer.BindToPanel(graphContainer); // graphContainer — это ваш контейнер в XAML
        }

        private void DisplayGraph()
        {
            // Создаем новый граф
            MsaglGraph = new();

            // Настраиваем алгоритм компоновки
            MsaglGraph.LayoutAlgorithmSettings = new MdsLayoutSettings();

            // Добавляем вершины и ребра с весами
            Microsoft.Msagl.Drawing.Edge edgeAB = MsaglGraph.AddEdge("A", "4", "B");
            MakeUndirected(edgeAB);

            Microsoft.Msagl.Drawing.Edge edgeBC = MsaglGraph.AddEdge("B", "3", "C");
            MakeUndirected(edgeBC);

            Microsoft.Msagl.Drawing.Edge edgeCA = MsaglGraph.AddEdge("C","4", "A");
            MakeUndirected(edgeCA);

            Microsoft.Msagl.Drawing.Edge edgeAD = MsaglGraph.AddEdge("A","5", "D");
            MakeUndirected(edgeAD);

            Microsoft.Msagl.Drawing.Edge loopEdge = MsaglGraph.AddEdge("A", "A");
            MakeUndirected(loopEdge);
            loopEdge.LabelText = "Петля";
            // Назначаем граф для отображения
            graphViewer.Graph = MsaglGraph;

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

        private void ConverGraphToMSAGL()
        {

        }

    }
}
