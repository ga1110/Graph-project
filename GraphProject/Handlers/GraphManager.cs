using Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Handlers
{
    // Публичный класс GraphManager для управления графом
    public static class GraphManager
    {
        // Метод для добавления новой вершины в граф
        public static void AddVertex(Vertex vertex, Graph graph)
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
        public static void AddEdge(string sourceName, string destinationName, Graph graph, double? weight = null)
        {
            // Получаем объекты вершин по их именам
            var source = GraphSearcher.FindVertexByName(sourceName, graph);
            var destination = GraphSearcher.FindVertexByName(destinationName, graph);

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

            // Проверяем, существует ли уже ребро от source к destination
            var existingEdge = graph.adjacencyList[source].FirstOrDefault(e => e.Destination == destination);
            if (existingEdge != null)
            {
                Console.WriteLine($"Ребро от '{source.Name}' к '{destination.Name}' уже существует.");
            }
            else
            {
                // Добавляем новое ребро в список ребер исходной вершины
                graph.adjacencyList[source].Add(new Edge(source, destination, weight));

                // Выводим сообщение об успешном добавлении ребра
                Console.WriteLine($"Ребро от '{source.Name}' к '{destination.Name}' добавлено.");
            }

            // Если граф неориентированный, добавляем обратное ребро
            if (!graph.IsDirected)
            {
                // Проверяем, существует ли уже обратное ребро
                var existingReverseEdge = graph.adjacencyList[destination].FirstOrDefault(e => e.Destination == source);
                if (existingReverseEdge != null)
                {
                    Console.WriteLine($"Ребро от '{destination.Name}' к '{source.Name}' уже существует.");
                }
                else
                {
                    graph.adjacencyList[destination].Add(new Edge(destination, source, weight));
                    Console.WriteLine($"Ребро от '{destination.Name}' к '{source.Name}' добавлено.");
                }
            }
        }

        // Метод для удаления вершины из графа
        public static void RemoveVertex(string vertexName, Graph graph)
        {
            // Получаем объект вершины по имени
            var vertex = GraphSearcher.FindVertexByName(vertexName, graph);

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
        public static void RemoveEdge(string sourceName, string destinationName, Graph graph)
        {
            // Получаем объекты вершин по их именам
            var source = GraphSearcher.FindVertexByName(sourceName, graph);
            var destination = GraphSearcher.FindVertexByName(destinationName, graph);

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

        // Метод для удаления ребра из графа
        public static void RemoveEdge(Vertex source, Vertex destination, Graph graph)
        {
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

        public static Dictionary<Vertex, List<Edge>> GetAdj (Graph graph)
        {
            return graph.adjacencyList;
        }
    }
}
