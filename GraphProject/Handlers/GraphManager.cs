using GraphProject.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GraphProject.Handlers
{
    // Публичный класс GraphManager для управления графом
    public static class GraphManager
    {
        // Метод для добавления новой вершины в граф
        public static void AddVertex(Vertex vertex, Graph graph)
        {
            // Проверяем, что переданная вершина не равна null
            if (vertex == null)
                throw new Exception("Вершина не существует");

            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            // Проверяем, содержит ли список смежности уже такую вершину
            if (!graph.adjacencyList.ContainsKey(vertex))
            {
                // Добавляем новую вершину в список смежности с пустым списком ребер
                graph.adjacencyList[vertex] = new List<Edge>();
            }
            else
            {
                throw new Exception($"Вершина {vertex.Name} уже существует");
            }
        }

        // Метод для добавления нового ребра в граф
        public static void AddEdge(string sourceName, string destinationName, Graph graph, double? weight = null)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            if (string.IsNullOrEmpty(sourceName))
                throw new Exception("Имя вершины не может быть null");

            if (string.IsNullOrEmpty(destinationName))
                throw new Exception("Имя вершины не может быть null");

            // Получаем объекты вершин по их именам
            var source = GraphSearcher.FindVertexByName(sourceName, graph);
            var destination = GraphSearcher.FindVertexByName(destinationName, graph);

            // Проверяем, существует ли исходная вершина
            if (source == null)
            {
                source = new Vertex(sourceName.ToUpper().Trim());
                AddVertex(source, graph);
            }

            // Проверяем, существует ли конечная вершина
            if (destination == null)
            {
                destination = new Vertex(destinationName.ToUpper().Trim());
                AddVertex(destination, graph);
            }

            // Проверяем, существует ли уже ребро от source к destination
            var existingEdge = graph.adjacencyList[source].FirstOrDefault(e => e.Destination == destination);
            if (existingEdge != null)
            {
                throw new ArgumentException($"Ребро от '{source.Name}' к '{destination.Name}' уже существует");
            }
            else
            {
                // Добавляем новое ребро в список ребер исходной вершины
                graph.adjacencyList[source].Add(new Edge(source, destination, weight));
            }

            // Если граф неориентированный, добавляем обратное ребро
            if (!graph.IsDirected)
            {
                // Проверяем, существует ли уже обратное ребро
                var existingReverseEdge = graph.adjacencyList[destination].FirstOrDefault(e => e.Destination == source);
                if (existingReverseEdge != null)
                {
                    throw new ArgumentException($"Ребро от '{source.Name}' к '{destination.Name}' уже существует");
                }
                else
                {
                    graph.adjacencyList[destination].Add(new Edge(destination, source, weight));
                }
            }
        }

        // Метод для удаления вершины из графа
        public static void RemoveVertex(string vertexName, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");
            // Получаем объект вершины по имени
            var vertex = GraphSearcher.FindVertexByName(vertexName, graph);

            // Проверяем, существует ли такая вершина
            if (vertex == null)
            {
                throw new ArgumentNullException($"Вершина '{vertexName}' не существует.");
            }

            // Удаляем вершину из списка смежности
            graph.adjacencyList.Remove(vertex);

            // Проходим по всем спискам ребер и удаляем ребра, ведущие к удаленной вершине
            foreach (var edges in graph.adjacencyList.Values)
            {
                edges.RemoveAll(e => e.Destination.Equals(vertex));
            }
        }

        // Метод для удаления ребра из графа
        public static void RemoveEdge(string sourceName, string destinationName, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            // Получаем объекты вершин по их именам
            var source = GraphSearcher.FindVertexByName(sourceName, graph);
            var destination = GraphSearcher.FindVertexByName(destinationName, graph);

            // Проверяем, существует ли исходная вершина
            if (source == null)
            {
                throw new ArgumentException($"Вершина '{sourceName}' не существует");
            }

            // Проверяем, существует ли конечная вершина
            if (destination == null)
            {
                throw new ArgumentException($"Вершина '{destinationName}' не существует");
            }

            // Удаляем ребра из списка исходной вершины, ведущие к конечной вершине
            bool removed = graph.adjacencyList[source].RemoveAll(e => e.Destination.Equals(destination)) > 0;

            // Выводим сообщение об успешном удалении или отсутствии ребра
            if (!removed)
                throw new ArgumentNullException($"Ребро от '{source.Name}' к '{destination.Name}' не найдено");

            // Если граф неориентированный, удаляем обратное ребро
            if (!graph.IsDirected)
            {
                bool removedReverse = graph.adjacencyList[destination].RemoveAll(e => e.Destination.Equals(source)) > 0;
                if (!removedReverse)
                    throw new ArgumentNullException($"Ребро от '{destination.Name}' к '{source.Name}' не найдено");
            }
        }

        // Метод для удаления ребра из графа
        public static void RemoveEdge(Vertex source, Vertex destination, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            if (source == null || destination == null)
                throw new ArgumentNullException("Вершина не найдена");

            // Удаляем ребра из списка исходной вершины, ведущие к конечной вершине
            bool removed = graph.adjacencyList[source].RemoveAll(e => e.Destination.Equals(destination)) > 0;

            // Выводим сообщение об успешном удалении или отсутствии ребра
            if (!removed)
                throw new ArgumentNullException($"Ребро от '{destination.Name}' к '{source.Name}' не найдено");

            // Если граф неориентированный, удаляем обратное ребро
            if (!graph.IsDirected)
            {
                bool removedReverse = graph.adjacencyList[destination].RemoveAll(e => e.Destination.Equals(source)) > 0;
                if (!removedReverse)
                    throw new ArgumentNullException($"Ребро от '{destination.Name}' к '{source.Name}' не найдено");
            }
        }

        public static Dictionary<Vertex, List<Edge>> GetAdj (Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            return graph.adjacencyList;
        }

        public static void SetAllVertexID(Graph graph)
        {
            var index = 0;
            foreach (var vertex in GraphManager.GetAdj(graph).Keys)
            {
                vertex.SetId(index++);
            }
        }

        public static void DeleteAllVertexID(Graph graph)
        {
            foreach (var vertex in GraphManager.GetAdj(graph).Keys)
            {
                vertex.SetId(-1);
            }
        }
    }
}
