using System;
using System.Collections.Generic;
using Structures;

namespace Handlers
{
    // Публичный класс GraphManager для управления графом
    public class GraphManager
    {
        // Приватное поле graph типа Graph, с которым будет работать менеджер
        private Graph graph;

        // Конструктор класса GraphManager, принимающий экземпляр графа
        public GraphManager(Graph graph)
        {
            // Проверяем, что переданный граф не равен null
            this.graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        // Метод для добавления новой вершины в граф
        public void AddVertex(Vertex vertex)
        {
            // Проверяем, что переданная вершина не равна null
            if (vertex == null)
            {
                Console.WriteLine("Вершина не может быть null.");
                return;
            }

            // Проверяем, содержит ли список смежности уже такую вершину
            if (!graph.adjacencyList.ContainsKey(vertex))
            {
                // Добавляем новую вершину в список смежности с пустым списком ребер
                graph.adjacencyList[vertex] = new List<Edge>();

                // Выводим сообщение об успешном добавлении вершины
                Console.WriteLine($"Вершина '{vertex.Name}' добавлена.");
            }
            else
            {
                // Выводим сообщение о том, что такая вершина уже существует
                Console.WriteLine($"Вершина '{vertex.Name}' уже существует.");
            }
        }

        // Метод для добавления нового ребра в граф
        public void AddEdge(string sourceName, string destinationName, double? weight = null)
        {
            // Получаем объекты вершин по их именам
            var source = GetVertexByName(sourceName);
            var destination = GetVertexByName(destinationName);

            // Проверяем, существует ли исходная вершина
            if (source == null)
            {
                Console.WriteLine($"Вершина '{sourceName}' не существует.");
                return;
            }

            // Проверяем, существует ли конечная вершина
            if (destination == null)
            {
                Console.WriteLine($"Вершина '{destinationName}' не существует.");
                return;
            }

            // Добавляем новое ребро в список ребер исходной вершины
            graph.adjacencyList[source].Add(new Edge(destination, weight));

            // Выводим сообщение об успешном добавлении ребра
            Console.WriteLine($"Ребро от '{source.Name}' к '{destination.Name}' добавлено.");

            // Если граф неориентированный, добавляем обратное ребро
            if (!graph.IsDirected)
            {
                graph.adjacencyList[destination].Add(new Edge(source, weight));
                Console.WriteLine($"Ребро от '{destination.Name}' к '{source.Name}' добавлено (неориентированный граф).");
            }
        }

        // Метод для удаления вершины из графа
        public void RemoveVertex(string vertexName)
        {
            // Получаем объект вершины по имени
            var vertex = GetVertexByName(vertexName);

            // Проверяем, существует ли такая вершина
            if (vertex == null)
            {
                Console.WriteLine($"Вершина '{vertexName}' не существует.");
                return;
            }

            // Удаляем вершину из списка смежности
            graph.adjacencyList.Remove(vertex);

            // Проходим по всем спискам ребер и удаляем ребра, ведущие к удаленной вершине
            foreach (var edges in graph.adjacencyList.Values)
            {
                edges.RemoveAll(e => e.Destination.Equals(vertex));
            }

            // Выводим сообщение об успешном удалении вершины и связанных ребер
            Console.WriteLine($"Вершина '{vertexName}' и все связанные с ней рёбра удалены.");
        }

        // Метод для удаления ребра из графа
        public void RemoveEdge(string sourceName, string destinationName)
        {
            // Получаем объекты вершин по их именам
            var source = GetVertexByName(sourceName);
            var destination = GetVertexByName(destinationName);

            // Проверяем, существует ли исходная вершина
            if (source == null)
            {
                Console.WriteLine($"Вершина '{sourceName}' не существует.");
                return;
            }

            // Проверяем, существует ли конечная вершина
            if (destination == null)
            {
                Console.WriteLine($"Вершина '{destinationName}' не существует.");
                return;
            }

            // Удаляем ребра из списка исходной вершины, ведущие к конечной вершине
            bool removed = graph.adjacencyList[source].RemoveAll(e => e.Destination.Equals(destination)) > 0;

            // Выводим сообщение об успешном удалении или отсутствии ребра
            if (removed)
                Console.WriteLine($"Ребро от '{source.Name}' к '{destination.Name}' удалено.");
            else
                Console.WriteLine($"Ребро от '{source.Name}' к '{destination.Name}' не найдено.");

            // Если граф неориентированный, удаляем обратное ребро
            if (!graph.IsDirected)
            {
                bool removedReverse = graph.adjacencyList[destination].RemoveAll(e => e.Destination.Equals(source)) > 0;
                if (removedReverse)
                    Console.WriteLine($"Ребро от '{destination.Name}' к '{source.Name}' удалено (неориентированный граф).");
                else
                    Console.WriteLine($"Ребро от '{destination.Name}' к '{source.Name}' не найдено (неориентированный граф).");
            }
        }
        // Метод для получения вершины по ее имени
        public Vertex GetVertexByName(string name)
        {
            // Приводим имя к нижнему регистру для поиска без учета регистра
            string lowerName = name.ToLower();

            // Проходим по всем вершинам в списке смежности
            foreach (var vertex in graph.adjacencyList.Keys)
            {
                // Если имя вершины совпадает с заданным (без учета регистра)
                if (vertex.Name.ToLower() == lowerName)
                    return vertex; // Возвращаем найденную вершину
            }

            // Если вершина не найдена, возвращаем null
            return null;
        }
        

    }
}
