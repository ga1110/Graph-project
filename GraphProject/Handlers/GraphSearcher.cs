using GraphProject.Algorithms;
using GraphProject.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphProject.Handlers
{
    public static class GraphSearcher
    {
        /// <summary>
        /// Метод поиска вершин у которых степень полуисхода больше чем у заданной
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        /// <param name="graph">Граф</param>
        /// <returns>Список вершин с большей полустепенью исхода</returns>
        public static List<Vertex>? FindVerticesWithGreaterOutDegree(string vertexName, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            // Получаем вершину по ее имени
            Vertex? givenVertex = FindVertexByName(vertexName, graph) ?? throw new ArgumentNullException($"Вершины {vertexName} не существует");

            // Проверяем, что вершина существует
            if (givenVertex == null)
            {
                return null;
            }

            // Получаем полустепень исхода заданной вершины
            int givenVertexOutDegree = VertexAnalyzer.GetOutDegree(givenVertex, graph);

            // Список вершин с большей полустепенью исхода
            List<Vertex> verticesWithGreaterOutDegree = new();

            // Проходим по всем вершинам в графе
            foreach (var vertex in graph.adjacencyList.Keys)
            {
                // Пропускаем заданную вершину
                if (vertex.Equals(givenVertex))
                {
                    continue;
                }

                // Получаем полустепень исхода текущей вершины
                int currentVertexOutDegree = VertexAnalyzer.GetOutDegree(vertex, graph);

                // Если полустепень исхода больше, добавляем в список
                if (currentVertexOutDegree > givenVertexOutDegree)
                {
                    verticesWithGreaterOutDegree.Add(vertex);
                }
            }

            return verticesWithGreaterOutDegree;
        }

        /// <summary>
        /// Метод для поиска вершин, которые не смежные с заданной
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        /// <param name="graph">Граф</param>
        /// <returns>Список не смежных вершин</returns>
        public static List<Vertex>? FindNonAdjacentVertices(string vertexName, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            // Получаем вершину по ее имени
            Vertex? givenVertex = FindVertexByName(vertexName, graph) ?? throw new ArgumentNullException($"Вершины {vertexName} не существует");

            // Список не смежных вершин
            List<Vertex> nonAdjacentVertices = new();

            // Проходим по всем вершинам в графе
            foreach (var vertex in graph.adjacencyList.Keys)
            {
                // Пропускаем заданную вершину
                if (vertex.Equals(givenVertex))
                {
                    continue;
                }

                bool isAdjacent = false;

                if (graph.IsDirected)
                {
                    // В ориентированном графе проверяем наличие исходящего ребра от заданной вершины к текущей
                    List<Edge> edgesFromGiven = graph.adjacencyList[givenVertex];
                    foreach (var edge in edgesFromGiven)
                    {
                        if (edge.Destination.Equals(vertex))
                        {
                            isAdjacent = true;
                            break;
                        }
                    }
                }
                else
                {
                    // В неориентированном графе проверяем наличие ребра между заданной вершиной и текущей
                    bool found = false;

                    // Проверяем исходящие ребра от заданной вершины
                    List<Edge> edgesFromGiven = graph.adjacencyList[givenVertex];
                    foreach (var edge in edgesFromGiven)
                    {
                        if (edge.Destination.Equals(vertex))
                        {
                            isAdjacent = true;
                            found = true;
                            break;
                        }
                    }

                    // Если ребро не найдено от заданной вершины, проверяем исходящие ребра от текущей вершины
                    if (!found)
                    {
                        List<Edge> edgesFromCurrent = graph.adjacencyList[vertex];
                        foreach (var edge in edgesFromCurrent)
                        {
                            if (edge.Destination.Equals(givenVertex))
                            {
                                isAdjacent = true;
                                break;
                            }
                        }
                    }
                }

                // Если вершина не смежна, добавляем ее в список
                if (!isAdjacent)
                {
                    nonAdjacentVertices.Add(vertex);
                }
            }
            return nonAdjacentVertices;
        }

        /// <summary>
        /// Метод для поиска ребер ведущих к листьям 
        /// </summary>
        /// <param name="graph">Граф</param>
        /// <returns>Список рёбер, которые нужно удалить</returns>
        public static List<Tuple<Vertex, Vertex>> FindLeafsEdges(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            // Список для хранения рёбер, которые нужно удалить
            List<Tuple<Vertex, Vertex>> leafsEdges = new();

            // Проходим по всем вершинам в графе
            foreach (var vertex in graph.adjacencyList.Keys.ToList()) // ToList() для предотвращения модификации коллекции во время итерации
            {
                // Создаем копию списка рёбер, чтобы безопасно модифицировать исходный список
                var edges = graph.adjacencyList[vertex].ToList();

                foreach (var edge in edges)
                {
                    bool isLeaf = false;

                    if (graph.IsDirected)
                    {
                        // В ориентированном графе листом считается вершина с out-degree == 0
                        if (VertexAnalyzer.GetOutDegree(edge.Destination, graph) == 0)
                        {
                            isLeaf = true;
                        }
                    }
                    else
                    {
                        // В неориентированном графе листом считается вершина с degree == 1
                        if (VertexAnalyzer.GetOutDegree(edge.Destination, graph) == 1)
                        {
                            isLeaf = true;
                        }
                    }

                    if (isLeaf)
                    {
                        // Добавляем ребро для удаления
                        leafsEdges.Add(Tuple.Create(vertex, edge.Destination));
                    }
                }
            }

            return leafsEdges;
        }

        /// <summary>
        /// Метод для получения вершины по ее имени
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        /// <param name="graph">Граф</param>
        /// <returns>Вершина</returns>
        public static Vertex? FindVertexByName(string vertexName, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            // Приводим имя к нижнему регистру для поиска без учета регистра
            string lowerName = vertexName.ToLower();

            if (graph.adjacencyList != null)
            {
                // Проходим по всем вершинам в списке смежности
                foreach (var vertex in graph.adjacencyList.Keys)
                {
                    // Если имя вершины совпадает с заданным (без учета регистра)
                    if (vertex.Name.Equals(lowerName, StringComparison.CurrentCultureIgnoreCase))
                        return vertex; // Возвращаем найденную вершину
                }
            }

            // Если вершина не найдена, выбрасываем исключение
            return null;
        }

        /// <summary>
        /// Метод поиска недостижимых вершин из данной 
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        /// <param name="graph">Граф</param>
        /// <returns>Список недостижимых вершин</returns>
        public static List<Vertex>? FindUnreachableVertices(string vertexName, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            // Получаем вершину по ее имени
            Vertex givenVertex = FindVertexByName(vertexName, graph) ?? throw new ArgumentNullException($"Вершины {vertexName} не существует");

            // Список не смежных вершин
            List<Vertex> unreachableVertices = new();
            List<Vertex> reachableVertices = new();

            foreach (var vertex in GraphTraversal.RecursiveGraphTraversal(graph, givenVertex).Keys)
            {
                reachableVertices.Add(vertex);
            }
            foreach (var currentVertex in graph.adjacencyList.Keys.ToList()) // ToList() для предотвращения модификации коллекции во время итерации
            {
                if (!reachableVertices.Contains(currentVertex) && currentVertex != givenVertex)
                {
                    unreachableVertices.Add(currentVertex);
                }
            }

            return unreachableVertices.Distinct().ToList();
        }
        /// <summary>
        /// Метод поиска вершин на расстоянии меньше или равном N от заданной вершины
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        /// <param name="graph">Граф</param>
        /// <param name="n">Максимальное расстояние</param>
        /// <returns>Словарь вершин и их расстояний от заданной вершины, удовлетворяющих условию</returns>
        public static Dictionary<Vertex, double>? FindVerticesDistanceLessOrEqualN(string vertexName, Graph graph, double n)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            // Получаем вершину по ее имени
            Vertex givenVertex = FindVertexByName(vertexName, graph) ?? throw new ArgumentNullException($"Вершины {vertexName} не существует");

            if (n == null)
            {
                n = 0;
            }

            var vertices = DijkstraAlgorithm.Execute(graph, givenVertex);
            foreach (var vertex in vertices)
            {
                if (vertex.Value > n || vertex.Key == givenVertex)
                {
                    vertices.Remove(vertex.Key);
                }
            }

            return vertices;
        }


        /// <summary>
        /// Метод для поиска кратчайших путей от заданной вершины до всех остальных вершин в графе
        /// </summary>
        /// <param name="vertexName">Имя вершины</param>
        /// <param name="graph">Граф</param>
        /// <returns>Словарь вершин и их расстояний от заданной вершины</returns>
        public static Dictionary<Vertex, double> FindShortestPathsFrom(string vertexName, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            // Получаем вершину по ее имени
            Vertex givenVertex = FindVertexByName(vertexName, graph) ?? throw new ArgumentNullException($"Вершины {vertexName} не существует");

            var distances = BellmanFord.Execute(graph, givenVertex);

            // Удаляем начальную вершину
            foreach (var vertex in distances.Keys)
            {
                if (vertex == givenVertex)
                {
                    distances.Remove(vertex);
                }
            }
            return distances;
        }


        /// <summary>
        /// Метод для поиска вершин на периферии графа на расстоянии больше или равном N от заданной вершины
        /// </summary>
        /// <param name="graph">Граф</param>
        /// <param name="source">Исходная вершина</param>
        /// <param name="N">Минимальное расстояние</param>
        /// <returns>Список вершин на периферии графа</returns>
        public static List<Vertex> FindNPeriphery(Graph graph, Vertex source, double N)
        {
            // Получаем все кратчайшие расстояния с помощью алгоритма Флойда-Уоршелла
            var allDistances = Floyd_Warshall.Execute(graph);

            var periphery = new List<Vertex>();

            foreach (var target in GraphManager.GetAdj(graph).Keys) // Предполагается, что Graph имеет свойство Vertices
            {
                if (source.Equals(target))
                    continue;

                if (allDistances.TryGetValue((source, target), out double distance))
                {
                    if (distance > N)
                    {
                        periphery.Add(target);
                    }
                }
                else
                {
                    // Если расстояние не найдено, это означает, что нет пути между source и target
                    // В зависимости от требований, можно решить, как обрабатывать такие случаи
                    // Например, можно считать расстояние бесконечным
                    periphery.Add(target);
                }
            }

            return periphery;
        }

        /// <summary>
        /// Метод для поиска максимального потока в графе от заданной исходной вершины к заданной конечной вершине
        /// </summary>
        /// <param name="graph">Граф</param>
        /// <param name="sourceName">Имя исходной вершины</param>
        /// <param name="sinkName">Имя конечной вершины</param>
        /// <returns>Максимальный поток</returns>
        public static double FindMaxFlow(Graph graph, string sourceName, string sinkName)
        {
            // Получаем вершину по ее имени
            Vertex source = FindVertexByName(sourceName, graph) ?? throw new ArgumentNullException($"Вершины {sourceName} не существует");
            // Получаем вершину по ее имени
            Vertex sink = FindVertexByName(sinkName, graph) ?? throw new ArgumentNullException($"Вершины {sinkName} не существует");
            return FordFulkerson.MaxFlow(graph, source, sink);
        }

    }
}
