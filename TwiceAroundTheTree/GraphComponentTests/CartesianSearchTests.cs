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
    public class CartesianSearchTests
    {

        [Fact]
        public void ShouldBuildIndexMapForNodes()
        {
            Graph testGraph = ExampleGraphContainer.Get.MediumTestGraph();
            KruskalsAlgorithm krus = new KruskalsAlgorithm(testGraph);
            krus.FindMsp();
            DepthFirstWalk dfw = new DepthFirstWalk(krus.MSPGraph, 0);
            dfw.CreateWalk();
            CartesianProductSearch cartesianSearch = new CartesianProductSearch(krus.MSPGraph, dfw.CompleteWalkAsNodes, krus.MSPGraph.Vertices);
            Assert.True(cartesianSearch.nodePositions.Keys.Count > 0);
            Assert.True(cartesianSearch.nodePositions.Values.Count > 0);
            Assert.True(cartesianSearch.nodePositions.Values.First().Count > 0);

            testGraph = ExampleGraphContainer.Get.G1TestGraph();
            krus = new KruskalsAlgorithm(testGraph);
            krus.FindMsp();
            dfw = new DepthFirstWalk(krus.MSPGraph, 0);
            dfw.CreateWalk();
            cartesianSearch = new CartesianProductSearch(krus.MSPGraph, dfw.CompleteWalkAsNodes, krus.MSPGraph.Vertices);
            Assert.True(cartesianSearch.nodePositions.Keys.Count > 0);
            Assert.True(cartesianSearch.nodePositions.Values.Count > 0);
            Assert.True(cartesianSearch.nodePositions.Values.First().Count > 0);


        }

        [Fact]
        public void ShouldCreateWalks() 
        {
            Graph testGraph = ExampleGraphContainer.Get.MediumTestGraph();
            KruskalsAlgorithm krus = new KruskalsAlgorithm(testGraph);
            krus.FindMsp();
            DepthFirstWalk dfw = new DepthFirstWalk(krus.MSPGraph, 0);
            dfw.CreateWalk();
            CartesianProductSearch cartesianSearch = new CartesianProductSearch(krus.MSPGraph, dfw.CompleteWalkAsNodes, krus.MSPGraph.Vertices);

            Assert.True(cartesianSearch.PotentialWalks.Count == 24);
            foreach (List<Node> walk in cartesianSearch.PotentialWalks)
            {
                Assert.True(walk.Count == 6);
            }


            testGraph = ExampleGraphContainer.Get.G1TestGraph();
            PrimsAlgorithm pa = new PrimsAlgorithm(testGraph,0);
            pa.FindMsp();
            dfw = new DepthFirstWalk(testGraph.MSPGraphs[0], 0);
            dfw.CreateWalk();
            cartesianSearch = new CartesianProductSearch(testGraph.MSPGraphs[0], dfw.CompleteWalkAsNodes, testGraph.MSPGraphs[0].Vertices);
            Assert.True(cartesianSearch.PotentialWalks.Count == 11);
            foreach (List<Node> walk in cartesianSearch.PotentialWalks)
            {
                Assert.True(walk.Count == 7);
            }
        }
    }
}
