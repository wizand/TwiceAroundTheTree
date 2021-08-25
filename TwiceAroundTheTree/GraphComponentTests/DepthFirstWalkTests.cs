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
    public class DepthFirstWalkTests
    {

        [Fact]
        public void CreatesWalkFromMspBeginsAndEndsToSameNode() 
        {
            Graph testGraph = ExampleGraphContainer.Get.G1TestGraph();
            PrimsAlgorithm prims = new PrimsAlgorithm(testGraph, 0);
            prims.FindMsp();
            DepthFirstWalk dfw = new DepthFirstWalk(prims.MSPGraph,0);
            dfw.CreateWalk();
            Assert.True(dfw.CompleteWalkAsNodes.First().Equals(dfw.CompleteWalkAsNodes.Last()));

            dfw = new DepthFirstWalk(prims.MSPGraph, 3);
            dfw.CreateWalk();
            Assert.True(dfw.CompleteWalkAsNodes.First().Equals(dfw.CompleteWalkAsNodes.Last()));

            testGraph = ExampleGraphContainer.Get.SmallTestGraph();
            prims = new PrimsAlgorithm(testGraph, 0);
            prims.FindMsp();
            dfw = new DepthFirstWalk(prims.MSPGraph, 0);
            dfw.CreateWalk();
            Assert.True(dfw.CompleteWalkAsNodes.First().Equals(dfw.CompleteWalkAsNodes.Last()));
        }


        [Fact]
        public void CompleteWalkVisitsInEachNode() {
            Graph testGraph = ExampleGraphContainer.Get.G1TestGraph();
            PrimsAlgorithm prims = new PrimsAlgorithm(testGraph, 0);
            prims.FindMsp();
            DepthFirstWalk dfw = new DepthFirstWalk(prims.MSPGraph, 0);
            dfw.CreateWalk();
            
            foreach ( Node n in prims.MSPGraph.Vertices ) 
            {
                Assert.Contains(n, dfw.CompleteWalkAsNodes);
            }

            KruskalsAlgorithm ka = new KruskalsAlgorithm(testGraph);
            ka.FindMsp();
            dfw = new DepthFirstWalk(ka.MSPGraph, 0);
            dfw.CreateWalk();
            foreach (Node n in prims.MSPGraph.Vertices)
            {
                Assert.Contains(n, dfw.CompleteWalkAsNodes);
            }


            dfw = new DepthFirstWalk(prims.MSPGraph, 3);
            dfw.CreateWalk();
            foreach (Node n in prims.MSPGraph.Vertices)
            {
                Assert.Contains(n, dfw.CompleteWalkAsNodes);
            }

            testGraph = ExampleGraphContainer.Get.SmallTestGraph();
            prims = new PrimsAlgorithm(testGraph, 0);
            prims.FindMsp();
            dfw = new DepthFirstWalk(prims.MSPGraph, 0);
            dfw.CreateWalk();
            foreach (Node n in prims.MSPGraph.Vertices)
            {
                Assert.Contains(n, dfw.CompleteWalkAsNodes);
            }

            testGraph = ExampleGraphContainer.Get.MediumTestGraph();
            prims = new PrimsAlgorithm(testGraph, 0);
            prims.FindMsp();
            dfw = new DepthFirstWalk(prims.MSPGraph, 0);
            dfw.CreateWalk();
            foreach (Node n in prims.MSPGraph.Vertices)
            {
                Assert.Contains(n, dfw.CompleteWalkAsNodes);
            }
        }

        [Fact]
        public void EachEdgeIsTravelledTwice() {
            Graph testGraph = ExampleGraphContainer.Get.G1TestGraph();
            PrimsAlgorithm prims = new PrimsAlgorithm(testGraph, 0);
            prims.FindMsp();
            DepthFirstWalk dfw = new DepthFirstWalk(prims.MSPGraph, 0);
            dfw.CreateWalk();

            foreach (Edge e in prims.MSPGraph.Edges) 
            {
                Assert.True(dfw.CompleteWalkAsEdges.Contains(e) || dfw.CompleteWalkAsEdges.Contains(e.GetOtherWay()));
            }

            foreach (Edge e in prims.MSPGraph.Edges)
            {
                int countOfEdges = 0;

                foreach (Edge walkEdge in dfw.CompleteWalkAsEdges) {
                    if ( e.Equals(walkEdge) || e.GetOtherWay().Equals(walkEdge))
                    {
                        countOfEdges += 1;
                    }
                }
                Assert.True(countOfEdges == 2);
            }
        }
    }
}
