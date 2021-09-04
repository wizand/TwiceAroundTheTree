using GraphComponents;

using System;
using System.Collections.Generic;

namespace GraphDataStorage
{
    public class MultipleGraphsStoreModel : AbstractGraphStoreModel
    {
        
        public List<SingleGraphStoreModel> StoredGraphs { get; set; }

        public MultipleGraphsStoreModel(List<Graph> Graphs)
        {
            Id = new Guid();
            StoredGraphs = new List<SingleGraphStoreModel>();
            foreach (Graph g in Graphs) {
                StoredGraphs.Add(new SingleGraphStoreModel(g));
            }
        }
    }
}
