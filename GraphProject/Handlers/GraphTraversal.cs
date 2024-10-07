using Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Handlers
{
    public static class GraphTraversal
    {
        public static List<Vertex> RecursiveGraphTraversal(Graph graph, Vertex currentVertex)
        {
            List<Vertex> visited = new List<Vertex>();
            RecursiveGraphTraversalHelp(graph, currentVertex, visited);
            return visited;
        }
        private static void RecursiveGraphTraversalHelp(Graph graph, Vertex currentVertex, List<Vertex> visited)
        {
            // Проверяем, была ли вершина уже посещена
            if (visited.Contains(currentVertex))
                return;

            // Добавляем текущую вершину в список посещенных
            visited.Add(currentVertex);

            // Проверяем, существуют ли смежные вершины для текущей вершины
            if (graph.adjacencyList.ContainsKey(currentVertex))
            {
                var edges = graph.adjacencyList[currentVertex];
                foreach (var edge in edges)
                {
                    RecursiveGraphTraversalHelp(graph, edge.Destination, visited);
                }
            }
        }
    }
}
