﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphProject.Algorithms;
using GraphProject.Structures;

namespace GraphProject.Handlers
{
    public static class GraphAnalyzer
    {
        public static bool IsGraphConnected(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            var allVertex = graph.adjacencyList.Keys.ToList(); // Все вершины графа
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
