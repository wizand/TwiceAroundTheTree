using System;
using System.Collections.Generic;

using GraphComponents;

using GraphDataStorage;

using Xunit;

namespace GraphDataStorageTests
{
    public class DataCacheTests
    {

        public Graph GetRandomGraph() {
            Random r = new Random();
            int size = r.Next(6, 12);
            int probabilityOfEdge = 15;
            int edgesAdded = 0;
            int minimumNumberOfEdges = (int)((float)(size * size) * 0.10);
            int edgeWeight;
            int[][] adjacencyMatrix = new int[size][];
            for(int y = 0; y < size; y++) 
            { 
                adjacencyMatrix[y] = new int[size];
            }

            for (int y = 0; y < size; y++) 
            {
                for ( int x = 0; x < size; x++) 
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
            if (edgesAdded < minimumNumberOfEdges) { 
                for( int additionalEdges = 0; additionalEdges <= (minimumNumberOfEdges-edgesAdded); additionalEdges++ ) 
                {
                    int x = r.Next(0, size);
                    int y = r.Next(0, size);
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
            for (int i = 0; i < size; i++)
            {
                verticeNames.Add(((char)('A' + i) + ""));
            }
            Matrix m = new Matrix(verticeNames, adjacencyMatrix);
            Graph g = new Graph(m);

            return g;
         }

        [Fact]
        public void StoringSingleGraphsShouldIncreaseTheCacheSize()
        {
            Graph testGraph = GetRandomGraph();

            DataCache.Instance.StoreGraph(testGraph);
            Assert.True(DataCache.Instance.GetCacheSize() == 1);
        }
    }
}
