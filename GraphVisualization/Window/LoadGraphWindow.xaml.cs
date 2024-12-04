using System.Windows;
using GraphVisualization;
using Microsoft.Win32;
using System;
using System.Linq;

namespace GraphVisualization
{
    public partial class LoadGraphWindow : Window
    {
        public string GraphName { get; private set; }  
        public string FilePath { get; private set; }
        public bool IsAddToVoult { get; private set; }

        public LoadGraphWindow(bool IsVoultEmpty)
        {
            InitializeComponent();
            if (!IsVoultEmpty)
            {
                AddToVoult.IsChecked = true;
                AddToVoult.IsEnabled = false;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            GraphName = GraphNameTextBox.Text.Trim();
            if(GraphName == null || GraphName.Length == 0)
            {
                GraphName = (FilePath.Split('\\').Last()).Split('.').First();
            }
            FilePath = FilePathTextBox.Text.Trim();
            IsAddToVoult = AddToVoult.IsChecked ?? false;
            this.DialogResult = true;
            this.Close();
        }
        private void LoadFileButton_Click( object sender, RoutedEventArgs e )
        {
            // Создаем экземпляр OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл";
            openFileDialog.Filter = "Все файлы (*.txt)|*.*"; // Можно настроить фильтр файлов

            // Открываем диалог и проверяем, что файл выбран
            if (openFileDialog.ShowDialog() == true)
            {
                // Получаем путь к файлу и сохраняем его в переменную
                FilePath = openFileDialog.FileName;
                FilePathTextBox.Text = FilePath;
            }
        }
    }
}
