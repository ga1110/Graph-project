using System.Runtime.CompilerServices;
using System.Windows;

namespace GraphVisualization
{
    public partial class AddRemoveVertexWindow : Window
    {
        public string Vertex { get; private set; }
        public AddRemoveVertexWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Vertex = StartVertexTextBox.Text.Trim();

            // Проверяем, что начальная и конечная вершины указаны
            if (string.IsNullOrEmpty(Vertex))
            {
                MessageBox.Show("Пожалуйста, укажите вершину.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.DialogResult = true;
            this.Close();
        }
    }
}