using System.Collections.Generic;
using Microsoft.Msagl.Drawing;
using GraphProject.Handlers;

namespace GraphVisualization.Algorithms
{
    public static class GraphConverter
    {
        /// <summary>
        /// Метод для преобразования графа в формате GraphProject.Structures.Graph в граф в формате Microsoft.Msagl.Drawing.Graph
        /// </summary>
        /// <param name="myGraph">Граф в формате GraphProject.Structures.Graph</param>
        /// <returns>Граф в формате Microsoft.Msagl.Drawing.Graph</returns>
        public static Microsoft.Msagl.Drawing.Graph Execute(GraphProject.Structures.Graph myGraph)
        {
            // Необходимо для корректного отображения
            List<GraphProject.Structures.Edge> edgesInGraph = new();

            var adj = GraphManager.GetAdj(myGraph);
            Microsoft.Msagl.Drawing.Graph MsaglGraph = new();
            foreach (var element in adj)
            {
                var edges = element.Value;
                var vertex = element.Key;

                MsaglGraph.AddNode(vertex.Name);
                foreach (var edge in edges)
                {
                    var edgeFlow = "";
                    var edgeWeight = "";
                    if (edge.Capacity != null)
                    {
                        edgeFlow = $"Поток: {edge.Flow} / {edge.Capacity}";
                    }
                    if (edge.Weight != null)
                    {
                        edgeWeight = $"Вес: {edge.Weight}";
                    }
                    var mark = edgeWeight + " " + edgeFlow;
                    if (!myGraph.IsDirected)
                    {
                        if (!IsDublicate(edgesInGraph, edge))
                        {
                            Microsoft.Msagl.Drawing.Edge MsaglEdge = MsaglGraph.AddEdge(edge.Source.Name
                                                                                             , mark
                                                                                             , edge.Destination.Name);
                            MakeUndirected(MsaglEdge);
                            edgesInGraph.Add(edge);
                        }
                        else
                            continue;
                    }
                    else
                    {
                        Microsoft.Msagl.Drawing.Edge MsaglEdge = MsaglGraph.AddEdge(edge.Source.Name
                                                                                         , mark
                                                                                         , edge.Destination.Name);
                        edgesInGraph.Add(edge);
                    }
                }
            }
            return MsaglGraph;
        }

        /// <summary>
        /// Метод для проверки наличия дубликата ребра в списке ребер
        /// </summary>
        /// <param name="edges">Список ребер</param>
        /// <param name="currentEdge">Текущее ребро</param>
        /// <returns>True, если ребро является дубликатом, иначе False</returns>
        private static bool IsDublicate(List<GraphProject.Structures.Edge> edges, GraphProject.Structures.Edge currentEdge)
        {
            foreach (var edge in edges)
            {
                if (edge.Source == currentEdge.Destination && edge.Destination == currentEdge.Source)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Метод для установки атрибутов ребра в неориентированный вид
        /// </summary>
        /// <param name="edge">Ребро</param>
        private static void MakeUndirected(Microsoft.Msagl.Drawing.Edge edge)
        {
            edge.Attr.ArrowheadAtTarget = ArrowStyle.None;
            edge.Attr.ArrowheadAtSource = ArrowStyle.None;
        }
    }
}
