﻿using System;
using System.Xml.Linq;

namespace GraphProject.Structures
{
    public class Edge
    {
        public Vertex Source { get; set; }
        public Vertex Destination { get; set; } // Представляет вершину назначения, к которой ведет данное ребро

        public double? Capacity { get; set; } // Емкость ребра (максимальная пропускная способность)
        public double Flow { get; set; } // Текущий поток по ребру

        public double? Weight { get; set; } // Представляет вес ребра; если вес не задан, свойство будет равно null
        public Edge(Vertex source, Vertex destination, double? weight = null, double? capacity = null)
        {
            Source = source;
            // Проверка: если параметр destination равен null, исключение ArgumentNullException
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            Weight = weight;
            Flow = 0;
            Capacity = capacity;

        }

        public double? ResidualCapacity => Capacity - Flow;

        // Переопределение метода ToString для представления объекта Edge в виде строки
        public override string ToString()
        {
            string result = $"{Source.Name},  {Destination.Name}";

            // Проверка, имеет ли Weight значение (не равно null)
            if (Weight != null)
                // Если вес задан, добавляем информацию о весе к строке result
                result += $", Вес: {Weight.Value}";
            if (Capacity != null)
                result += $", Поток: {this.Flow}/{Capacity.Value}";
            // Возвращаем итоговую строку, представляющую ребро
            return result;
        }

        public static bool operator >(Edge firstEdge, Edge secondEdge)
        {
            return firstEdge.Weight > secondEdge.Weight;
        }
        public static bool operator <(Edge firstEdge, Edge secondEdge)
        {
            return firstEdge.Weight < secondEdge.Weight;
        }
    }
}
