using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GraphVisualization
{
    /// <summary>
    /// Логика взаимодействия для MaxFlowVertexChoose.xaml
    /// </summary>
    public partial class MaxFlowVertexChoose : Window
    {
        public string SourceVertex { get; private set; }
        public string SinkVertex { get; private set; }
        public bool View { get; private set; }
        public MaxFlowVertexChoose()
        {
            InitializeComponent();

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            SourceVertex = StartVertexTextBox.Text.Trim();
            SinkVertex = EndVertexTextBox.Text.Trim();
            View = ViewBox.IsChecked ?? false;
            // Проверяем, что начальная и конечная вершины указаны
            if (string.IsNullOrEmpty(SourceVertex) || string.IsNullOrEmpty(SinkVertex))
            {
                MessageBox.Show("Пожалуйста, укажите начальную и конечную вершины.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.DialogResult = true;
            this.Close();
        }
    }
}