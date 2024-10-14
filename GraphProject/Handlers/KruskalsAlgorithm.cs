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
            Graph newGraph = new Graph($"{graph.GraphName} + _MST", false);

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

            foreach (var edge in MSTEdges)
            {
                Console.WriteLine(edge.ToString());
            }
            return null;
        }

        private static bool isAddInMST (List<Vertex> MSTVertex, Edge givenEdge) // Возвращает false если добавлять Edge не нужно
        {
            foreach (var vertex in MSTVertex)
            {
                if (MSTVertex.Contains(givenEdge.Destination))
                {
                    Console.WriteLine("Вершина не прошла 1-ю проверку: " + givenEdge.ToString());
                    Console.WriteLine();
                    return false;
                }
            }
            foreach(var vertex in MSTVertex)
            {
                if (!MSTVertex.Contains(givenEdge.Source))
                {
                   Console.WriteLine("Вершина не прошла 2-ю проверку: " + givenEdge.ToString());
                   Console.WriteLine();
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
                    Console.WriteLine("Вершина добавлена: " + edge.ToString());
                    MSTEdges.Add(edge);
                    MSTVertex.Add(edge.Source);
                    MSTVertex.Add(edge.Destination);
                    fillMST(MSTEdges, MSTVertex, sortedAllEdges);
                }
            }
            return;
        }
    }
}
// TODO: Изменить isAddInMST так что б проверяла наличие вершины в списке MSTVertex