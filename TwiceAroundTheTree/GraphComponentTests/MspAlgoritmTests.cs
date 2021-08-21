using GraphComponents;
using GraphComponents.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GraphComponentTests
{
    public class MspAlgoritmTests
    {
        protected Graph GetSmallTestGraph()
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

        protected Graph GetMediumTestGraph()
        {
            List<string> vertices = new() { "A", "B", "C", "D","E","F" };
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

        [Fact]
        public void AllSmallTestGraphMspsShouldHaveTheSameWeight()
        {
            Graph testGraph = GetSmallTestGraph();
            PrimsAlgorithm pa = new PrimsAlgorithm(testGraph,0);
            pa.FindMsp();
            pa = new PrimsAlgorithm(testGraph, 1);
            pa.FindMsp();
            pa = new PrimsAlgorithm(testGraph, 2);
            pa.FindMsp();
            pa = new PrimsAlgorithm(testGraph, 3);
            pa.FindMsp();
            KruskalsAlgorithm ka = new KruskalsAlgorithm(testGraph);
            ka.FindMsp();

            int firstWeight = testGraph.MSPGraphs[0].Weight;
            foreach (Graph mspGraph in testGraph.MSPGraphs) 
            {
                Assert.True(mspGraph.IsMSP);
                Assert.True(mspGraph.Weight == firstWeight);
            }

        }
        [Fact]
        public void AllMediumTestGraphMspsShouldHaveTheSameWeight()
        {
            Graph testGraph = GetMediumTestGraph();

            PrimsAlgorithm pa;
            for (int i = 0; i < testGraph.Vertices.Count; i++) 
            {
                pa = new PrimsAlgorithm(testGraph, i);
                pa.FindMsp();
            }
            
            KruskalsAlgorithm ka = new KruskalsAlgorithm(testGraph);
            ka.FindMsp();

            int firstWeight = testGraph.MSPGraphs[0].Weight;
            foreach (Graph mspGraph in testGraph.MSPGraphs)
            {
                Assert.True(mspGraph.IsMSP);
                Assert.True(mspGraph.Weight == firstWeight);
            }

        }

    }
}
