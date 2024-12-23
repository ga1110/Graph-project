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
        /// <summary>
        /// Очередь вывода сообщений
        /// </summary>
        public Queue<string> OutputQueue { get; private set; }

        /// <summary>
        /// Текстовое поле для вывода сообщений
        /// </summary>
        public TextBox TextBox { get; private set; }

        /// <summary>
        /// Конструктор класса Logger
        /// </summary>
        public Logger()
        {
            OutputQueue = new Queue<string>();
        }

        /// <summary>
        /// Метод для установки текстового поля для вывода сообщений
        /// </summary>
        /// <param name="textBox">Текстовое поле</param>
        public void SetTextBox(TextBox textBox)
        {
            this.TextBox = textBox;
        }

        /// <summary>
        /// Метод для добавления сообщения в очередь вывода
        /// </summary>
        /// <param name="message">Сообщение</param>
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

        /// <summary>
        /// Метод для добавления сообщения об ошибке в очередь вывода
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
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

        /// <summary>
        /// Метод для нормализации сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Нормализованное сообщение</returns>
        private string NormalizeMessage(string message)
        {
            string dividingLine = "\n======================\n";
            return dividingLine + message + dividingLine;
        }

        /// <summary>
        /// Метод для вывода сообщений из очереди в текстовое поле
        /// </summary>
        private void Log()
        {
            TextBox.Text = "";
            foreach (var item in OutputQueue)
            {
                TextBox.Text += item;
            }
        }
    }


    public class ErrorLogger : Logger
    {

    }
}
