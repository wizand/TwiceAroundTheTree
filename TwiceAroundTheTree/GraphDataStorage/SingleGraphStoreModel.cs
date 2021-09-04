using GraphComponents;

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDataStorage
{
    public class SingleGraphStoreModel : AbstractGraphStoreModel
    {
        private Graph Graph { get; set; }

        public SingleGraphStoreModel(Graph graph)
        {
            Id = Guid.NewGuid();
            Graph = graph;
        }

        public SingleGraphStoreModel(Graph graph, Guid id)
        {
            Id = id;
            Graph = graph;
            
        }

        public Graph GetGraphCopy() {
            return new Graph(Graph); 
        
        }

    }
}
