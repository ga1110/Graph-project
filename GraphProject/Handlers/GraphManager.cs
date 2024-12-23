using GraphProject.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphProject.Handlers
{
    // Публичный класс GraphManager для управления графом
    public static class GraphManager
    {
        /// <summary>
        /// Добавляет новую вершину в граф.
        /// </summary>
        /// <param name="vertex">Вершина, которую необходимо добавить.</param>
        /// <param name="graph">Граф, в который необходимо добавить вершину.</param>
        /// <exception cref="Exception">Если переданная вершина равна null или вершина уже существует в графе.</exception>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        public static void AddVertex(Vertex vertex, Graph graph)
        {
            if (vertex == null)
                throw new Exception("Вершина не существует");

            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            if (!graph.adjacencyList.ContainsKey(vertex))
            {
                graph.adjacencyList[vertex] = new List<Edge>();
            }
            else
            {
                throw new Exception($"Вершина {vertex.Name} уже существует");
            }
        }

        /// <summary>
        /// Добавляет новое ребро в граф.
        /// </summary>
        /// <param name="sourceName">Имя исходной вершины.</param>
        /// <param name="destinationName">Имя конечной вершины.</param>
        /// <param name="graph">Граф, в который необходимо добавить ребро.</param>
        /// <param name="weight">Вес ребра (опционально).</param>
        /// <param name="capacity">Емкость ребра (опционально).</param>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        /// <exception cref="Exception">Если имя исходной или конечной вершины пустое или равно null.</exception>
        /// <exception cref="ArgumentException">Если ребро от исходной вершины к конечной вершине уже существует.</exception>
        public static void AddEdge(string sourceName, string destinationName, Graph graph, double? weight = null, double? capacity = null)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            if (string.IsNullOrEmpty(sourceName))
                throw new Exception("Имя вершины не может быть null");

            if (string.IsNullOrEmpty(destinationName))
                throw new Exception("Имя вершины не может быть null");

            var source = GraphSearcher.FindVertexByName(sourceName, graph);
            var destination = GraphSearcher.FindVertexByName(destinationName, graph);

            if (source == null)
            {
                source = new Vertex(sourceName.ToUpper().Trim());
                AddVertex(source, graph);
            }

            if (destination == null)
            {
                destination = new Vertex(destinationName.ToUpper().Trim());
                AddVertex(destination, graph);
            }

            var existingEdge = graph.adjacencyList[source].FirstOrDefault(e => e.Destination == destination);
            if (existingEdge != null)
            {
                throw new ArgumentException($"Ребро от '{source.Name}' к '{destination.Name}' уже существует");
            }
            else
            {
                graph.adjacencyList[source].Add(new Edge(source, destination, weight, capacity));
            }

            if (!graph.IsDirected)
            {
                var existingReverseEdge = graph.adjacencyList[destination].FirstOrDefault(e => e.Destination == source);
                if (existingReverseEdge != null)
                {
                    throw new ArgumentException($"Ребро от '{source.Name}' к '{destination.Name}' уже существует");
                }
                else
                {
                    graph.adjacencyList[destination].Add(new Edge(destination, source, weight, capacity));
                }
            }
        }

        /// <summary>
        /// Добавляет новое ребро в граф.
        /// </summary>
        /// <param name="sourceName">Имя исходной вершины.</param>
        /// <param name="destinationName">Имя конечной вершины.</param>
        /// <param name="graph">Граф, в который необходимо добавить ребро.</param>
        /// <param name="weight">Вес ребра (опционально).</param>
        /// <param name="capacity">Емкость ребра (опционально).</param>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        /// <exception cref="Exception">Если имя исходной или конечной вершины пустое или равно null.</exception>
        /// <exception cref="ArgumentException">Если ребро от исходной вершины к конечной вершине уже существует.</exception>
        public static void AddEdge(Edge edge, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            if (edge == null)
                throw new Exception("Вершина не может быть null");

            var source = edge.Source;
            var destination = edge.Destination;

            var existingEdge = graph.adjacencyList[source].FirstOrDefault(e => e.Destination == destination);

            if (GraphSearcher.FindEdge(graph, source, destination) != null)
            {
                throw new ArgumentException($"Ребро от '{source.Name}' к '{destination.Name}' уже существует");
            }
            else
            {
                graph.adjacencyList[source].Add(edge);
            }

            if (!graph.IsDirected)
            {
                var existingReverseEdge = graph.adjacencyList[destination].FirstOrDefault(e => e.Destination == source);
                if (existingReverseEdge != null)
                {
                    throw new ArgumentException($"Ребро от '{source.Name}' к '{destination.Name}' уже существует");
                }
                else
                {
                    graph.adjacencyList[destination].Add(edge);
                }
            }
        }

        /// <summary>
        /// Удаляет вершину из графа.
        /// </summary>
        /// <param name="vertexName">Имя вершины, которую необходимо удалить.</param>
        /// <param name="graph">Граф, из которого необходимо удалить вершину.</param>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        /// <exception cref="ArgumentNullException">Если вершина не найдена.</exception>
        public static void RemoveVertex(string vertexName, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            var vertex = GraphSearcher.FindVertexByName(vertexName, graph);

            if (vertex == null)
            {
                throw new ArgumentNullException($"Вершина '{vertexName}' не существует.");
            }

            graph.adjacencyList.Remove(vertex);

            foreach (var edges in graph.adjacencyList.Values)
            {
                edges.RemoveAll(e => e.Destination.Equals(vertex));
            }
        }

        /// <summary>
        /// Удаляет ребро из графа.
        /// </summary>
        /// <param name="sourceName">Имя исходной вершины.</param>
        /// <param name="destinationName">Имя конечной вершины.</param>
        /// <param name="graph">Граф, из которого необходимо удалить ребро.</param>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        /// <exception cref="ArgumentException">Если исходная или конечная вершина не найдена.</exception>
        /// <exception cref="ArgumentNullException">Если ребро не найдено.</exception>
        public static void RemoveEdge(string sourceName, string destinationName, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            var source = GraphSearcher.FindVertexByName(sourceName, graph);
            var destination = GraphSearcher.FindVertexByName(destinationName, graph);

            if (source == null)
            {
                throw new ArgumentException($"Вершина '{sourceName}' не существует");
            }

            if (destination == null)
            {
                throw new ArgumentException($"Вершина '{destinationName}' не существует");
            }

            bool removed = graph.adjacencyList[source].RemoveAll(e => e.Destination.Equals(destination)) > 0;

            if (!removed)
                throw new ArgumentNullException($"Ребро от '{source.Name}' к '{destination.Name}' не найдено");

            if (!graph.IsDirected)
            {
                bool removedReverse = graph.adjacencyList[destination].RemoveAll(e => e.Destination.Equals(source)) > 0;
                if (!removedReverse)
                    throw new ArgumentNullException($"Ребро от '{destination.Name}' к '{source.Name}' не найдено");
            }
        }

        /// <summary>
        /// Удаляет ребро из графа.
        /// </summary>
        /// <param name="source">Исходная вершина.</param>
        /// <param name="destination">Конечная вершина.</param>
        /// <param name="graph">Граф, из которого необходимо удалить ребро.</param>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        /// <exception cref="ArgumentNullException">Если исходная или конечная вершина равна null.</exception>
        /// <exception cref="ArgumentNullException">Если ребро не найдено.</exception>
        public static void RemoveEdge(Vertex source, Vertex destination, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            if (source == null || destination == null)
                throw new ArgumentNullException("Вершина не найдена");

            bool removed = graph.adjacencyList[source].RemoveAll(e => e.Destination.Equals(destination)) > 0;

            if (!removed)
                throw new ArgumentNullException($"Ребро от '{destination.Name}' к '{source.Name}' не найдено");

            if (!graph.IsDirected)
            {
                bool removedReverse = graph.adjacencyList[destination].RemoveAll(e => e.Destination.Equals(source)) > 0;
                if (!removedReverse)
                    throw new ArgumentNullException($"Ребро от '{destination.Name}' к '{source.Name}' не найдено");
            }
        }
    
        /// <summary>
        /// Возвращает список смежности графа.
        /// </summary>
        /// <param name="graph">Граф, для которого необходимо получить список смежности.</param>
        /// <returns>Список смежности графа.</returns>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        public static Dictionary<Vertex, List<Edge>> GetAdj(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            return graph.adjacencyList;
        }

        /// <summary>
        /// Добавление ID вершинам.
        /// </summary>
        /// <param name="graph">Граф, который содержит вершины.</param>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        public static void SetAllVertexID(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");
            
            var index = 0;
            foreach (var vertex in GetAdj(graph).Keys)
            {
                vertex.SetId(index++);
            }
        }

        /// <summary>
        /// Удаляет (утсанавлеливает как -1) ID у всех вершин.
        /// </summary>
        /// <param name="graph">Граф, который содержит вершины.</param>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        public static void DeleteAllVertexID(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            foreach (var vertex in GetAdj(graph).Keys)
            {
                vertex.SetId(-1);
            }
        }
    }
}
