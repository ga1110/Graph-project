using System.Runtime.CompilerServices;
using System.Windows;

namespace GraphVisualization
{
    public partial class AddRemoveEdgeWindow : Window
    {
        public string StartVertex { get; private set; }
        public string EndVertex { get; private set; }
        public double? Weight { get; private set; }
        private bool _toAdd {  get; set; }

        public AddRemoveEdgeWindow(bool toAdd)
        {
            InitializeComponent();
            _toAdd = toAdd;
            if(toAdd)
            {
                initButton();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            StartVertex = StartVertexTextBox.Text.Trim();
            EndVertex = EndVertexTextBox.Text.Trim();

            if (_toAdd)
            {
                // Проверяем, введен ли вес, и пытаемся преобразовать его в число
                if (double.TryParse(WeightTextBox.Text.Trim(), out double weight))
                {
                    Weight = weight;
                }
                else
                {
                    Weight = null;
                }
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

        private void initButton()
        {
            WeightTextBox.IsEnabled = true;
            WeightTextBox.Visibility = Visibility.Visible;

            WeightLabel.IsEnabled = true;
            WeightLabel.Visibility = Visibility.Visible;
        }
    }
}