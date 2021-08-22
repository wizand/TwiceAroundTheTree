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
        public void CreatesWalkFromMsp() 
        {
            Graph testGraph = ExampleGraphContainer.Get.SmallTestGraph();
            PrimsAlgorithm prims = new PrimsAlgorithm(testGraph, 0);
            prims.FindMsp();
            DepthFirstWalk dfw = new DepthFirstWalk(prims.MSPGraph,0);
            dfw.CreateWalk();
            Assert.True(false);

        }

    }
}
