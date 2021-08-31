using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphComponents;
using GraphComponents.Algorithms;

using Xunit;

namespace GraphComponentTests
{
    public class HamiltonianWalkProposerTests
    {

        [Fact]
        public void ShouldFilterOutNonrealisticEdges() {
            Graph testGraph = ExampleGraphContainer.Get.G1TestGraph();
            PrimsAlgorithm pa = new PrimsAlgorithm(testGraph, 0);
            pa.FindMsp();
            DepthFirstWalk dfw = new DepthFirstWalk(testGraph.MSPGraphs[0], 0);
            dfw.CreateWalk();
            CartesianProductSearch cps = new CartesianProductSearch(testGraph.MSPGraphs[0], dfw.CompleteWalkAsNodes, testGraph.Vertices);
            HamiltonianWalkProposer hwp = new HamiltonianWalkProposer(testGraph, cps.PotentialWalks);
            hwp.BuildHamiltonianCircuits();
            Assert.True(hwp.HamiltonianWalksAsEdges.Count > 0);
        
        }


        [Fact]
        public void ShouldAddFoundHamiltonCircuitGraphsToSourceGraph()
        {
            Graph testGraph = ExampleGraphContainer.Get.G1TestGraph();
            PrimsAlgorithm pa = new PrimsAlgorithm(testGraph, 0);
            pa.FindMsp();
            DepthFirstWalk dfw = new DepthFirstWalk(testGraph.MSPGraphs[0], 0);
            dfw.CreateWalk();
            CartesianProductSearch cps = new CartesianProductSearch(testGraph.MSPGraphs[0], dfw.CompleteWalkAsNodes, testGraph.Vertices);
            HamiltonianWalkProposer hwp = new HamiltonianWalkProposer(testGraph, cps.PotentialWalks);
            hwp.BuildHamiltonianCircuits();
            //TODO: Fix weights
            Assert.True(hwp.HamiltonianWalksAsEdges.Count > 0);

        }

    }
}
