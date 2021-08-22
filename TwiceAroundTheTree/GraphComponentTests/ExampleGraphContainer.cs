using GraphComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponentTests
{
    public class ExampleGraphContainer
    {
        public static ExampleGraphContainer Get { get {
                return new ExampleGraphContainer();
            } 
        }

        public Graph SmallTestGraph()
        {
            List<string> vertices = new() { "A", "B", "C", "D" };
            int[][] adjacencyMatrix = new int[][]
            {
               new [] { 0,3,0,5 },
               new [] { 3,0,5,0 },
               new [] { 0,5,0,2 },
               new [] { 5,0,2,0 }
            };
            Matrix m = new Matrix(vertices, adjacencyMatrix);
            Graph g = new Graph(m);
            return g;

        }

        public  Graph MediumTestGraph()
        {
            List<string> vertices = new() { "A", "B", "C", "D", "E", "F" };
            int[][] adjacencyMatrix = new int[][]
            {
               new [] { 0,3,10,0,8,0 },
               new [] { 3,0,3,0,0,7 },
               new [] { 10,3,0,5,0,9 },
               new [] { 0,0,5,0,2,0 },
               new [] { 8,0,0,2,0,0 },
               new [] { 0,7,9,0,0,0 }
            };
            Matrix m = new Matrix(vertices, adjacencyMatrix);
            Graph g = new Graph(m);
            return g;

        }

    }
}
