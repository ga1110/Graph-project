using GraphProject.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphProject.Structures
{
    // Публичный класс Graph, представляющий граф
    /// <summary>
    /// Класс, представляющий граф.
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Внутреннее поле adjacencyList, хранящее список смежности графа.
        /// </summary>
        internal Dictionary<Vertex, List<Edge>> adjacencyList;

        /// <summary>
        /// Свойство, содержащее имя графа.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Свойство, определяющее, является ли граф ориентированным.
        /// </summary>
        public bool IsDirected { get; private set; }

        /// <summary>
        /// Инициализирует новый пустой граф с указанным направлением (ориентированный или неориентированный).
        /// </summary>
        /// <param name="name">Имя графа.</param>
        /// <param name="isDirected">Флаг, указывающий, является ли граф ориентированным.</param>
        public Graph(string name, bool isDirected = false)
        {
            Name = string.IsNullOrEmpty(name) ? "NamelessGraph" : name;
            IsDirected = isDirected;
            adjacencyList = new Dictionary<Vertex, List<Edge>>();
        }

        /// <summary>
        /// Конструктор графа из файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <param name="name">Имя графа.</param>
        public Graph(string filePath, string name)
        {
            Name = string.IsNullOrEmpty(name) ? "NamelessGraph" : name;
            adjacencyList = new Dictionary<Vertex, List<Edge>>();

            // Открываем файл для чтения
            using (StreamReader reader = new(filePath))
            {
                // Читаем первую строку файла (тип графа: ориентированный или неориентированный)
                string? firstLine = reader.ReadLine();

                // Если файл пустой, вызываем исключение
                if (firstLine == null)
                {
                    throw new InvalidDataException("Файл пуст");
                }

                // Проверяем, является ли граф ориентированным
                IsDirected = firstLine.Trim().Equals("directed", StringComparison.CurrentCultureIgnoreCase);

                string? currentLine;

                // Читаем файл построчно до конца
                while ((currentLine = reader.ReadLine()) != null)
                {
                    var lineElems = currentLine.Split("=>"); // Разбили строку

                    if (lineElems.Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        var sourceVertexName = lineElems[0].Trim().ToUpper();
                        var edges = lineElems[1].Trim();

                        Vertex sourceVertex = GraphSearcher.FindVertexByName(sourceVertexName, this);

                        if (sourceVertex == null)
                        {
                            sourceVertex = new Vertex(sourceVertexName);
                            GraphManager.AddVertex(sourceVertex, this);
                        }

                        // Если строка содержит другие вершины.
                        if (lineElems.Length > 1)
                        {
                            var splittedEdges = edges.Trim().Split("|");
                            foreach (var edge in splittedEdges)
                            {
                                if (edge.ToString().Length == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    var splittedEdge = edge.ToString().Replace(" ", "").Trim().Split(",");
                                    string destinationVertexName = splittedEdge[0].ToString();

                                    Vertex destinationVertex = GraphSearcher.FindVertexByName(destinationVertexName, this);

                                    // Если конечная вершина не найдена, создаём и добавляем её
                                    if (destinationVertex == null)
                                    {
                                        destinationVertex = new Vertex(destinationVertexName);
                                        GraphManager.AddVertex(destinationVertex, this);
                                    }

                                    double? weight = null;
                                    double? capacity = null;
                                    if (splittedEdge.Length > 1)
                                    {
                                        for (int i = 1; i < splittedEdge.Length; i++)
                                        {
                                            var curElem = splittedEdge[i].ToString().ToLower();
                                            if (curElem.StartsWith("w:"))
                                                weight = double.TryParse(curElem.Substring(2), out double w) ? w : null;
                                            else if (curElem.StartsWith("c:"))
                                                capacity = double.TryParse(curElem.Substring(2), out double c) ? c : null;
                                        }
                                    }
                                    GraphManager.AddEdge(sourceVertexName, destinationVertexName, this, weight, capacity);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Конструктор копирования.
        /// </summary>
        /// <param name="other">Граф, который нужно скопировать.</param>
        public Graph(Graph other)
        {
            Name = other.Name + "_copy";

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
                    var edgeCopy = new Edge(sourceCopy, destinationCopy, edge.Weight, edge.Capacity);
                    adjacencyList[sourceCopy].Add(edgeCopy);
                }
            }
        }

        /// <summary>
        /// Конструктор графа из списка ребер.
        /// </summary>
        /// <param name="edges">Список ребер.</param>
        /// <param name="graphName">Имя графа.</param>
        /// <param name="isDirected">Флаг, указывающий, является ли граф ориентированным.</param>
        public Graph(List<Edge> edges, string graphName, bool isDirected)
        {
            Name = graphName + "_MST";

            // Копируем значение свойства IsDirected из другого графа
            IsDirected = isDirected;

            // Инициализируем новый пустой список смежности
            adjacencyList = new Dictionary<Vertex, List<Edge>>();

            List<Vertex> vertices = new List<Vertex>();

            foreach (var edge in edges)
            {
                vertices.Add(edge.Source);
                vertices.Add(edge.Destination);
            }

            vertices.Distinct(); //Уникальные значения 

            foreach (var vertex in vertices)
            {
                var newVertex = new Vertex(vertex.Name);
                adjacencyList[vertex] = new List<Edge>();
            }

            foreach (var edge in edges)
            {
                adjacencyList[edge.Source].Add(new Edge(edge.Source, edge.Destination, edge.Weight));
                if (!isDirected)
                {
                    adjacencyList[edge.Destination].Add(new Edge(edge.Destination, edge.Source, edge.Weight));
                }
            }
        }
    }

}