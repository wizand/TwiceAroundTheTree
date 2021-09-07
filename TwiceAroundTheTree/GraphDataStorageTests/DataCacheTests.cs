using System;
using System.Collections.Generic;

using GraphComponents;

using GraphDataStorage;

using Xunit;

namespace GraphDataStorageTests
{
    public class DataCacheTests
    {
        

        [Fact]
        public void StoringSingleGraphsShouldIncreaseTheCacheSize()
        {
            Graph testGraph = GetRandomGraph();

            DataCache.Instance.StoreGraph(testGraph);
            Assert.True(DataCache.Instance.GetCacheSize() == 1);
        }
    }
}
