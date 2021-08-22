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


        [Fact]
        public void AllSmallTestGraphMspsShouldHaveTheSameWeight()
        {
            Graph testGraph = ExampleGraphContainer.Get.SmallTestGraph();
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
            Graph testGraph = ExampleGraphContainer.Get.MediumTestGraph();

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
