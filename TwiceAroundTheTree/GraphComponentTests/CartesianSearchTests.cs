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

            Assert.True(false);
        
        
            
        }
    }
}
