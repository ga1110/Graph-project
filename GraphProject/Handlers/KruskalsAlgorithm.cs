using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structures;

namespace GraphProject.Handlers
{
    public static class KruskalsAlgorithm
    {
        public static Graph FindMST (Graph graph)
        {
            List<Edge> allEdges = new List<Edge>();

            foreach (var vertex in graph.adjacencyList.Keys)
            {
                foreach (var edge in graph.adjacencyList[vertex])
                {
                    allEdges.Add(edge);
                }
            }

            var sortedAllEdges = allEdges.OrderBy(e => e.Weight).ToList(); // Сортировка по весу

            var MSTEdges = new List<Edge>();
            var MSTVertex = new List<Vertex>();

            fillMST(MSTEdges, MSTVertex, sortedAllEdges);
            Graph graph_mst = new Graph(MSTEdges, graph.GraphName, false);
            return graph_mst;
        }

        private static bool isAddInMST (List<Vertex> MSTVertex, Edge givenEdge) // Возвращает false если добавлять Edge не нужно
        {
            foreach (var vertex in MSTVertex)
            {
                if (MSTVertex.Contains(givenEdge.Destination))
                {
                    return false;
                }
            }
            foreach(var vertex in MSTVertex)
            {
                if (!MSTVertex.Contains(givenEdge.Source))
                {
                   return false;
                }
            }
            return true;
        }

        private static void fillMST(List<Edge> MSTEdges, List<Vertex> MSTVertex, List<Edge> sortedAllEdges)
        {
            foreach (var edge in sortedAllEdges)
            {
                if (isAddInMST(MSTVertex, edge))
                {
                    MSTEdges.Add(edge);
                    MSTVertex.Add(edge.Source);
                    MSTVertex.Add(edge.Destination);
                    fillMST(MSTEdges, MSTVertex, sortedAllEdges);
                }
            }
            return;
        }

        public static bool IsGraphCorrectForMST (Graph graph)
        {
            if (graph == null)
            {
                Console.WriteLine("Граф - пустой и/или равен null");
                return false;
            }
            foreach (var vertex in graph.adjacencyList)
            { 
                foreach (var edge in vertex.Value)
                {
                    if (edge.Weight == null)
                    {
                        Console.WriteLine($"В графе есть вершина без веса ({edge.ToString()})");
                        return false;
                    }
                }
            }
            if (graph.IsDirected)
            {
                Console.WriteLine($"Граф - ориентированный");
                return false;
            }

            return true;
        }
    }
}