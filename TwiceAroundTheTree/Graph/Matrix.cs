using System;
using System.Collections.Generic;
using GraphComponents;


namespace GraphComponents
{
    public class Matrix
    {
        public  List<Node> Vertices { get; set; } //Could we get rid or Vertices in matrix? Do we need them here?

        public Matrix(List<Node> verticeNodes) 
        {
            Vertices = verticeNodes;
            MatrixTable = new int[verticeNodes.Count][];
            for (int y = 0; y < verticeNodes.Count; y++) {
                MatrixTable[y] = new int[verticeNodes.Count];
                for( int x = 0; x < verticeNodes.Count; x++)
                {
                    MatrixTable[y][x] = 0;
                }
                
            }
        }
        public Matrix(List<string> vertices, int[][] rowsAsIntArrays)
        {
            Vertices = new List<Node>();
            foreach(string v in vertices) {
                Node n = new Node(v);
                Vertices.Add(n);
            }
            
            this.MatrixTable = rowsAsIntArrays;
        }

        public int[][] MatrixTable {get; set;}

        public Matrix MinimumSpanninTreeFromMatrix {get; set;}
        public bool IsMST {get ;set;} = false;


        /// <summary>
        /// If the graph is non directed, the edges go both ways. Lets say from A to C there should be an edge from C to A as well.
        /// So, 
        /// 0 0 1 <- a to c
        /// 0 0 0
        /// 1 0 0 <- c to a
        /// 
        /// But if you can only go from a to c, not c to a:
        /// 0 0 1
        /// 0 0 0
        /// 1 0 0
        /// </summary>
        /// <returns></returns>
        public bool isDirected() { 
            for ( int y = 0; y < MatrixTable.Length; y++) 
            {
                for (int x = 0; x < MatrixTable[y].Length; x++) 
                {
                    int value = MatrixTable[y][x];
                    if (value > 0) 
                    {
                        if (MatrixTable[x][y] != value)
                            return true;
                    }
                }
            }
            return false;
        }

    }
}
