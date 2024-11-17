using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structures;
using Handlers;
using Microsoft.Msagl.Drawing;
using System.Windows.Media.Media3D;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Layout.Layered;
using static Microsoft.Msagl.Core.Layout.LgNodeInfo;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.WpfGraphControl;
using System.Data;

namespace GraphVisualization.Algorithms
{
    public static class GraphConverter
    {
        public static Microsoft.Msagl.Drawing.Graph Execute(Structures.Graph myGraph)
        {
            // Необходимо для корректного отображения
            List<Structures.Edge> edgesInGraph = new();

            var adj = GraphManager.GetAdj(myGraph);
            Microsoft.Msagl.Drawing.Graph MsaglGraph = new();
            foreach (var element in adj)
            {
                var edges = element.Value;
                var vertex = element.Key;
                MsaglGraph.AddNode(vertex.Name);
                foreach (var edge in edges)
                {
                    if (!myGraph.IsDirected)
                    {
                        if (!isDublicate(edgesInGraph, edge))
                        {
                            Microsoft.Msagl.Drawing.Edge MsaglEdge = MsaglGraph.AddEdge(edge.Source.Name
                                                                                         , edge.Weight.ToString()
                                                                                         , edge.Destination.Name);
                            makeUndirected(MsaglEdge);
                            edgesInGraph.Add(edge);
                        }
                        else
                            continue;
                    }
                    else
                    {
                        Microsoft.Msagl.Drawing.Edge MsaglEdge = MsaglGraph.AddEdge(edge.Source.Name
                                                                                     , edge.Weight.ToString()
                                                                                     , edge.Destination.Name);
                        edgesInGraph.Add(edge);
                    }
                }
            }
            return MsaglGraph;
        }
        private static bool isDublicate(List<Structures.Edge> edges, Structures.Edge currentEdge)
        {
            foreach (var edge in edges)
            {
                if (edge.Source == currentEdge.Destination && edge.Destination == currentEdge.Source)
                    return true;
            }
            return false;
        }
        private static void makeUndirected(Microsoft.Msagl.Drawing.Edge edge)
        {
            edge.Attr.ArrowheadAtTarget = ArrowStyle.None;
            edge.Attr.ArrowheadAtSource = ArrowStyle.None;
        }
    }
}
