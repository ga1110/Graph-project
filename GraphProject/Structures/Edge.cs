using System;

namespace GraphProject.Structures
{
    /// <summary>
    /// Представляет ребро в графе.
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// Представляет вершину источника, от которой начинается данное ребро.
        /// </summary>
        public Vertex Source { get; set; }

        /// <summary>
        /// Представляет вершину назначения, к которой ведет данное ребро.
        /// </summary>
        public Vertex Destination { get; set; }

        /// <summary>
        /// Представляет емкость ребра (максимальную пропускную способность).
        /// </summary>
        public double? Capacity { get; set; }

        /// <summary>
        /// Представляет текущий поток по ребру.
        /// </summary>
        public double Flow { get; set; }

        /// <summary>
        /// Представляет вес ребра; если вес не задан, свойство будет равно null.
        /// </summary>
        public double? Weight { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса Edge.
        /// </summary>
        /// <param name="source">Вершина источника.</param>
        /// <param name="destination">Вершина назначения.</param>
        /// <param name="weight">Вес ребра (по умолчанию null).</param>
        /// <param name="capacity">Емкость ребра (по умолчанию null).</param>
        public Edge(Vertex source, Vertex destination, double? weight = null, double? capacity = null)
        {
            Source = source;
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            Weight = weight;
            Flow = 0;
            Capacity = capacity;
        }

        /// <summary>
        /// Переопределение метода ToString для представления объекта Edge в виде строки.
        /// </summary>
        /// <returns>Строковое представление ребра.</returns>
        public override string ToString()
        {
            string result = $"{Source.Name},  {Destination.Name}";

            if (Weight != null)
                result += $", Вес: {Weight.Value}";

            if (Capacity != null)
                result += $", Поток: {this.Flow}/{Capacity.Value}";

            return result;
        }

        /// <summary>
        /// Оператор сравнения "больше" для ребер.
        /// </summary>
        /// <param name="firstEdge">Первое ребро.</param>
        /// <param name="secondEdge">Второе ребро.</param>
        /// <returns>True, если вес первого ребра больше веса второго ребра, иначе false.</returns>
        public static bool operator >(Edge firstEdge, Edge secondEdge)
        {
            return firstEdge.Weight > secondEdge.Weight;
        }

        /// <summary>
        /// Оператор сравнения "меньше" для ребер.
        /// </summary>
        /// <param name="firstEdge">Первое ребро.</param>
        /// <param name="secondEdge">Второе ребро.</param>
        /// <returns>True, если вес первого ребра меньше веса второго ребра, иначе false.</returns>
        public static bool operator <(Edge firstEdge, Edge secondEdge)
        {
            return firstEdge.Weight < secondEdge.Weight;
        }

        public override bool Equals(object obj)
        {
            if (obj is Edge other)
            {
                // Сравниваем имена, приводя их к нижнему регистру
                return (Source == other.Source) && (Destination == other.Destination);
            }
            return false;
        }
    }
}
