using System;
using System.Collections.Generic;

namespace Structures
{
    // Публичный класс Graph, представляющий граф
    public class Graph
    {
        // Внутреннее поле adjacencyList, хранящее список смежности графа
        internal Dictionary<Vertex, List<Edge>> adjacencyList;

        // Публичное свойство IsDirected, определяющее, является ли граф ориентированным
        public bool IsDirected { get; private set; }

        // Инициализирует новый пустой граф с указанным направлением (ориентированный или неориентированный)
        public Graph(bool isDirected = false)
        {
            IsDirected = isDirected;
            adjacencyList = new Dictionary<Vertex, List<Edge>>();
        }

        // Конструктор копирования, принимающий другой объект Graph
        public Graph(Graph other)
        {
            // Проверяем, что переданный граф не равен null
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            // Копируем значение свойства IsDirected из другого графа
            IsDirected = other.IsDirected;

            // Инициализируем новый пустой список смежности
            adjacencyList = new Dictionary<Vertex, List<Edge>>();

            // Создаем словарь для отображения вершин из оригинального графа в новые вершины копии
            var vertexMapping = new Dictionary<Vertex, Vertex>();

            // Копируем вершины
            foreach (var vertex in other.adjacencyList.Keys)
            {
                var vertexCopy = new Vertex(vertex.Name);
                adjacencyList[vertexCopy] = new List<Edge>();
                vertexMapping[vertex] = vertexCopy;
            }

            // Копируем ребра
            foreach (var kvp in other.adjacencyList)
            {
                var sourceCopy = vertexMapping[kvp.Key];
                foreach (var edge in kvp.Value)
                {
                    var destinationCopy = vertexMapping[edge.Destination];
                    var edgeCopy = new Edge(destinationCopy, edge.Weight);
                    adjacencyList[sourceCopy].Add(edgeCopy);
                }
            }
        }
    }
}
