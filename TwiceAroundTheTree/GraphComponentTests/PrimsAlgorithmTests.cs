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
            Graph testGraph = ExampleGraphContainer.Get.SmallTestGraph();
            PrimsAlgorithm pa = new PrimsAlgorithm(testGraph, 0);
            pa.FindMsp();
            Assert.True(pa.MSPGraph.Weight == 10 );
            

        }

        [Fact]
        public void EachNodeShouldBeExactlyOnceInMsp() 
        {
            Graph testGraph = ExampleGraphContainer.Get.SmallTestGraph();
            PrimsAlgorithm pa = new PrimsAlgorithm(testGraph, 0);
            pa.FindMsp();
            foreach (Node n in pa.MSPGraph.Vertices)
            {
                int count = 0;
                foreach (Node v in pa.MSPGraph.Vertices)
                {
                    if (v.Equals(n))
                    {
                        count += 1;
                    }
                }

                Assert.Equal(1, count);
            }
        }
    }
}
