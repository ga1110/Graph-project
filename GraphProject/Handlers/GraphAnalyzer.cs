using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structures;
namespace Handlers
{
    public static class GraphAnalyzer
    {
        public static bool IsGraphConnected(Graph graph)
        {
            var allVertex = graph.adjacencyList.Keys.ToList(); // Все вершины графа
            var contAllVertex = allVertex.ToList().Count;

            List<List<Vertex>> traversalList = [];

            foreach (var vertex in allVertex)
            {
                traversalList.Add([.. GraphTraversal.RecursiveGraphTraversal(graph, vertex).Keys]);
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
