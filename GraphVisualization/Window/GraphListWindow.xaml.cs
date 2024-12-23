using GraphProject.Structures;
using GraphVisualization;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace GraphVisualization
{
    public partial class GraphListWindow : Window
    {
        private GraphVoult GraphVoult { get; set; }
        public int CurrentNumber { get; private set; }
        public ObservableCollection<GraphItem> GraphItems { get; set; }

        public GraphListWindow(GraphVoult graphVoult)
        {
            InitializeComponent();
            GraphVoult = graphVoult;
            GraphItems = new ObservableCollection<GraphItem>();
            DataContext = this; // Устанавливаем DataContext окна
            LoadGraphItems();
        }

        private void LoadGraphItems()
        {
            try
            {
                var graphs = GraphVoult.GetGrahpsList();
                for (int i = 0 ; i < graphs.Count ; i++)
                {
                    GraphItems.Add(new GraphItem
                    {
                        Number = i,
                        GraphName = graphs[i].Name
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = listBox.SelectedItem as GraphItem;
            if (selectedItem != null)
            {
                CurrentNumber = selectedItem.Number;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не выбрали ни одной опции.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = listBox.SelectedItem as GraphItem;
            if (selectedItem != null)
            {
                try
                {
                    // Удаляем граф из GraphVoult
                    GraphVoult.RemoveGraph(selectedItem.Number);

                    // Удаляем элемент из коллекции GraphItems
                    GraphItems.Remove(selectedItem);

                    // Переиндексация оставшихся элементов
                    ReindexGraphItems();

                    // Обновляем выбранный элемент
                    listBox.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали ни одной опции.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ReindexGraphItems()
        {
            for (int i = 0 ; i < GraphItems.Count ; i++)
            {
                GraphItems[i].Number = i;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }

    public class GraphItem : INotifyPropertyChanged
    {
        private int number;
        public int Number
        {
            get => number;
            set
            {
                if (number != value)
                {
                    number = value;
                    OnPropertyChanged(nameof(Number));
                }
            }
        }

        public string GraphName { get; set; }

        public string DisplayName
        {
            get
            {
                return GraphName.Length > 15 ? $"{GraphName.Substring(0, 13)}..." : GraphName;
            }
        }

        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
