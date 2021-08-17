using GraphComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDataStorage
{
    public class GraphStoreModel
    {
        public int i = 1;
        public Guid Id { get; set; }      
        private Graph Graph { get; set; }

        public GraphStoreModel(Graph graph)
        {
            Id = Guid.NewGuid();
            Graph = graph;
        }

        public GraphStoreModel(Graph graph, Guid id)
        {
            Id = id;
            Graph = graph;
        }

        public Graph GetGraphCopy() {
            return new Graph(Graph); 
        
        }

    }
}
