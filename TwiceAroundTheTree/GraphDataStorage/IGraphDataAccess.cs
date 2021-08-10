using GraphComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDataStorage
{
    public interface IGraphDataAccess
    {

        public Guid StoreGraph(Graph graphToStore);
        public Graph GetGraphFromStore(Guid id);
        public bool RemoveFromStore(Guid id);
    }
}
