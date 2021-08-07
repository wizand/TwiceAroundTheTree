using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Graph
    {
        public List<Edge> Edges { get; set; }
        public List<Node> Vertices { get; set; }
        public Matrix AdjacencyMatrix { get; set; }
    }
}
