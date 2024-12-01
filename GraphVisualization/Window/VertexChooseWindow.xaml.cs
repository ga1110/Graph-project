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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace GraphVisualization
{
    /// <summary>
    /// Логика взаимодействия для VertexChoose.xaml
    /// </summary>
    public partial class VertexChooseWindow : Window
    {
        public string Vertex { get; private set; }

        public double N { get; private set; }

        private bool _withN {  get; set; }

        public VertexChooseWindow(bool withN)
        {
            _withN = withN;
            InitializeComponent();
            if (withN)
                InitNButton();
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Vertex = VertexTextBox.Text.ToUpper().Trim();

            // Проверяем, введен ли вес, и пытаемся преобразовать его в число
            if(_withN)
            {
                if (double.TryParse(NTextBox.Text.Trim(), out double n))
                {
                    N = n;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, укажите N.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            // Проверяем, что начальная и конечная вершины указаны
            if (string.IsNullOrEmpty(Vertex))
            {
                MessageBox.Show("Пожалуйста, укажите вершину.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.DialogResult = true;
            this.Close();
        }

        private void InitNButton()
        {
            N_label.Visibility = Visibility.Visible;
            N_label.IsEnabled = true;

            NTextBox.Visibility = Visibility.Visible;
            NTextBox.IsEnabled = true;
        }
    }
}
