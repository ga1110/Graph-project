using System.Windows;

namespace GraphVisualization
{
    public partial class AddEdgeWindow : Window
    {
        public string StartVertex { get; private set; }
        public string EndVertex { get; private set; }
        public double? Weight { get; private set; }

        public AddEdgeWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            StartVertex = StartVertexTextBox.Text.Trim();
            EndVertex = EndVertexTextBox.Text.Trim();

            // Проверяем, введен ли вес, и пытаемся преобразовать его в число
            if (double.TryParse(WeightTextBox.Text.Trim(), out double weight))
            {
                Weight = weight;
            }
            else
            {
                Weight = null;
            }

            // Проверяем, что начальная и конечная вершины указаны
            if (string.IsNullOrEmpty(StartVertex) || string.IsNullOrEmpty(EndVertex))
            {
                MessageBox.Show("Пожалуйста, укажите начальную и конечную вершины.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.DialogResult = true;
            this.Close();
        }
    }
}
