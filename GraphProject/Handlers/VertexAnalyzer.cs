using GraphProject.Structures;
using System;

namespace GraphProject.Handlers
{
    public static class VertexAnalyzer
    {
        /// <summary>
        /// Метод для получения полустепени исхода вершины
        /// </summary>
        /// <param name="vertex">Вершина</param>
        /// <param name="graph">Граф</param>
        /// <returns>Полустепень исхода вершины</returns>
        public static int GetOutDegree(Vertex vertex, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            // Проверяем, содержит ли граф данную вершину
            if (graph.adjacencyList.ContainsKey(vertex))
            {
                // Возвращаем количество исходящих ребер
                return graph.adjacencyList[vertex].Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Метод для получения полустепени захода вершины
        /// </summary>
        /// <param name="vertex">Вершина</param>
        /// <param name="graph">Граф</param>
        /// <returns>Полустепень захода вершины</returns>
        public static int GetInDegree(Vertex vertex, Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            int count = 0;
            foreach (var adjacencyListElement in graph.adjacencyList)
            {
                Vertex currVertex = adjacencyListElement.Key;
                foreach (var edge in graph.adjacencyList[currVertex])
                {
                    if (edge.Destination.Equals(vertex))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }

}
