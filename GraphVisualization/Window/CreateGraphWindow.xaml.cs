using System.Windows;
using GraphVisualization;
namespace GraphVisualization
{
    public partial class CreateGraphWindow : Window
    {
        public string GraphName { get; private set; }
        public bool IsDirected { get; private set; }
        public bool IsAddToVoult {get; private set;}

        public CreateGraphWindow(bool IsVoultEmpty)
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
            IsDirected = IsDirectedCheckBox.IsChecked ?? false;
            IsAddToVoult = AddToVoult.IsChecked ?? false;
            this.DialogResult = true;
            this.Close();
        }
    }
}
