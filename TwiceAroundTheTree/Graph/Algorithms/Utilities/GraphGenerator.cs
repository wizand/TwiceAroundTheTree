using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponents.Algorithms.Utilities
{
    /// <summary>
    /// Utility class to generate random Graphs mostly for testing.
    /// 
    /// The generated graphs are guaranteed not to be empty, not directed and complete.
    /// 
    /// </summary>
    public class GraphGenerator
    {
        /// <summary>
        /// Generate a complete nondirected graph.
        /// 
        /// Graph should be of at least size 3 to be sensible. If size value below that is given,
        /// uses random value between 6 and 12
        /// </summary>
        /// <param name="size">Quatity of nodes. Values below 3 or over 50 are ignored and random value between 6 and 12 used instead</param>
        /// <returns>a Graph object</returns>
        public Graph GetRandomGraph(int size = 0)
        {
            Random r = new Random();

            int realSize = size;
            if (size > 50 || size < 3)
            {
                realSize = r.Next(6, 12);
            }
            int probabilityOfEdge = 15;
            int edgesAdded = 0;
            int minimumNumberOfEdges = (int)((float)(realSize * realSize) * 0.10);
            int edgeWeight;
            int[][] adjacencyMatrix = new int[realSize][];
            for (int y = 0; y < realSize; y++)
            {
                adjacencyMatrix[y] = new int[realSize];
            }

            for (int y = 0; y < realSize; y++)
            {
                for (int x = 0; x < realSize; x++)
                {
                    if (x == y)
                    {
                        adjacencyMatrix[y][x] = 0;
                        adjacencyMatrix[x][y] = 0;
                        continue; //No looping edges, thank you.
                    }
                    edgeWeight = 0;

                    //Default to no edge but add probabilityOfEdge amount of edges.
                    if (r.Next(0, 100) <= probabilityOfEdge)
                    {
                        edgeWeight = r.Next(1, 100);
                        edgesAdded += 1; //Keep track of how many edges there are
                    }
                    adjacencyMatrix[y][x] = edgeWeight;
                    adjacencyMatrix[x][y] = edgeWeight;
                }
            }

            //If there are not enough edges added, add some at random
            if (edgesAdded < minimumNumberOfEdges)
            {
                for (int additionalEdges = 0; additionalEdges <= (minimumNumberOfEdges - edgesAdded); additionalEdges++)
                {
                    int x = r.Next(0, realSize);
                    int y = r.Next(0, realSize);
                    edgeWeight = r.Next(1, 100);
                    if (x == y)
                    {
                        additionalEdges = additionalEdges - 1;
                        continue;
                    }

                    adjacencyMatrix[y][x] = edgeWeight;
                    adjacencyMatrix[x][y] = edgeWeight;
                }
            }
            List<string> verticeNames = new();
            for (int i = 0; i < realSize; i++)
            {
                verticeNames.Add(((char)('A' + i) + ""));
            }
            Matrix m = new Matrix(verticeNames, adjacencyMatrix);
            Graph g = new Graph(m);

            return g;
        }


        public bool isComplete(int[][] adjacencyMatrix)
        {
            for (int y = 0; y < adjacencyMatrix.Length; y++)
            {
                bool allZeroes = true;
                for (int x = 0; x < adjacencyMatrix.Length; x++)
                {
                    if (adjacencyMatrix[y][x] != 0)
                    {
                        allZeroes = false;
                        break;
                    }
                }
                if (allZeroes)
                    return false;
            }

            return true;
        }
    }
}
