using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace GraphVisualization.Algorithms
{
    public class Logger
    {
        public Queue<string> OutputQueue {  get; private set; }
        public TextBox TextBox { get; private set; }

        public Logger ()
        {
            OutputQueue = new Queue<string>();
        }

        public void SetTextBox(TextBox textBox)
        {
            this.TextBox = textBox;
        } 

        public void Add(string message)
        {
            string outputText = NormalizeMessage(message);
            if (OutputQueue.Count() >= 5)
            {
                OutputQueue.Dequeue();
                OutputQueue.Enqueue(outputText);
            }
            else
            {
                OutputQueue.Enqueue(outputText);
            }

            Log();
        }

        public void Add(Exception message)
        {
            string outputText = NormalizeMessage($"Ошибка!\n{message.Message}");
            if (OutputQueue.Count() >= 5)
            {
                OutputQueue.Dequeue();
                OutputQueue.Enqueue(outputText);
            }
            else
            {
                OutputQueue.Enqueue(outputText);
            }
            SystemSounds.Exclamation.Play();
            Log();
        }

        private string NormalizeMessage(string message)
        {
            string dividingLine = "\n======================\n";
            return dividingLine + message + dividingLine;
        }

        private void Log()
        {
            TextBox.Text = "";
            foreach (var item in OutputQueue) {
                TextBox.Text += item;
            }
        }
    }

    public class ErrorLogger : Logger
    {

    }
}
