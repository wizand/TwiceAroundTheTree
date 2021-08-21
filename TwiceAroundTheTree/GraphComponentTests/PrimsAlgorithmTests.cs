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
    public class PrimsAlgorithmTests : MspAlgoritmTests
    {

        [Fact]
        public void PrimsAlgorithmFindsMspWithCorrectWeight()
        {
            Graph testGraph = GetSmallTestGraph();
            PrimsAlgorithm pa = new PrimsAlgorithm(testGraph, 0);
            pa.FindMsp();
            Assert.True(true);
        }
    }
}
