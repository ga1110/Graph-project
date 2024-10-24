using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structures;
using Handlers;
using Microsoft.Msagl.Drawing;
using System.Windows.Media.Media3D;
namespace GraphVisualization
{
    public static class GraphConverter
    {
        public static Microsoft.Msagl.Drawing.Graph Execute(Structures.Graph MyGraph)
        {
            var adj = GraphManager.GetAdj(MyGraph);
            Microsoft.Msagl.Drawing.Graph MsaglGraph = new();
            foreach (var element in adj)
            {
                var edges = element.Value;
                foreach (var edge in edges)
                {
                    Microsoft.Msagl.Drawing.Edge MsaglEdge = MsaglGraph.AddEdge( edge.Source.Name
                                                                               , edge.Weight.ToString()
                                                                               , edge.Destination.Name );
                    if (!MyGraph.IsDirected)
                        MakeUndirected(MsaglEdge);
                }                    
            }
            return MsaglGraph;
        }
        private static void MakeUndirected(Microsoft.Msagl.Drawing.Edge edge)
        {
            edge.Attr.ArrowheadAtTarget = ArrowStyle.None;
            edge.Attr.ArrowheadAtSource = ArrowStyle.None;
        }
    }
}
