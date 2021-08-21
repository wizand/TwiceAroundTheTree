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
    public class KruskalsAlgorthmTests : MspAlgoritmTests
    {


        [Fact]
        public void KruskalFindsMSP()
        {
            Graph testGraph = GetSmallTestGraph();
            KruskalsAlgorithm ka = new KruskalsAlgorithm(testGraph);
            ka.FindMsp();

            Assert.True(ka.SourceGraph.MSPGraphs[0].IsMSP == true);
            Assert.True(ka.SourceGraph.MSPGraphs[0].Weight == 10);

            


        }

    }
}
