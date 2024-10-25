using Structures;

namespace Handlers
{
    public static class VertexAnalyzer
    {
        // Метод для получения полустепени исхода вершины
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

        // Метод для получения полустепени захода вершины
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
