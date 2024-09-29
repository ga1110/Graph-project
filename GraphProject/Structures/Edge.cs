using System.Xml.Linq;

namespace Structures
{
    public class Edge
    {
        public Vertex Destination { get; set; } // Представляет вершину назначения, к которой ведет данное ребро

        public double? Weight { get; set; } // Представляет вес ребра; если вес не задан, свойство будет равно null

        public Edge(Vertex destination, double? weight = null)
        {
            // Проверка: если параметр destination равен null, исключение ArgumentNullException
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            Weight = weight;
        }

        // Переопределение метода ToString для представления объекта Edge в виде строки
        public override string ToString()
        {
            string result = Destination.Name;

            // Проверка, имеет ли Weight значение (не равно null)
            if (Weight != null)
                // Если вес задан, добавляем информацию о весе к строке result
                result += $", Вес: {Weight.Value}";

            // Возвращаем итоговую строку, представляющую ребро
            return result;
        }

    }
}
