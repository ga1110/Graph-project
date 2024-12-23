using System;
using System.Collections.Generic;
using System.Linq;
using GraphProject.Algorithms;
using GraphProject.Structures;

namespace GraphProject.Handlers
{
    public static class GraphAnalyzer
    {
        /// <summary>
        /// Проверяет, является ли граф связным.
        /// </summary>
        /// <param name="graph">Граф, который необходимо проверить.</param>
        /// <returns>True, если граф связный, иначе - false.</returns>
        /// <exception cref="ArgumentNullException">Если переданный граф равен null.</exception>
        public static bool IsGraphConnected(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            var allVertex = graph.adjacencyList.Keys.ToList();
            var contAllVertex = allVertex.ToList().Count;

            List<List<Vertex>> traversalList = new();

            foreach (var vertex in allVertex)
            {
                traversalList.Add(GraphTraversal.RecursiveGraphTraversal(graph, vertex).Keys.ToList());
            }

            foreach (var vertexList in traversalList)
            {
                if (vertexList.Count != contAllVertex)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
