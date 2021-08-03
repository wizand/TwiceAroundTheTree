using System;
using System.Collections.Generic;
using Matrix;


namespace Matrix
{
    public class Matrix
    {
        public List<Edge> Edges { get; set; }
        public List<Node> Nodes { get; set; }
        public int[][] MatrixTable {get; set;}

        public Matrix MinimumSpanninTreeFromMatrix {get; set;}
        public bool IsMST {get ;set;} = false;
    }
}
