using GraphComponents;
using System;
using System.Collections.Generic;

namespace GraphDataStorage
{
    public class DataCache : IGraphDataAccess
    {
        private IDictionary<Guid, GraphStoreModel> _graphStore = null;
        private object _graphStoreLock = new object();


        private static DataCache _instance = null;
        public static DataCache Instance { get {
                if (_instance == null)
                    _instance = new DataCache();
                return _instance;
            } 
        }

        private IDictionary<Guid, GraphStoreModel> GetGraphStore()
        {

            
                if ( _graphStore == null )
                {
                    _graphStore = new Dictionary<Guid, GraphStoreModel>();
                }
            
            return _graphStore;
        }

        public Guid StoreGraph(Graph graphToStore)
        {
            GraphStoreModel gtm = new GraphStoreModel(graphToStore);
            lock(_graphStoreLock)
            {
                GetGraphStore()[gtm.Id] = gtm;
            }
            return gtm.Id;
        }

        public Graph GetGraphFromStore(Guid id)
        {
            GraphStoreModel gtm = null;
            bool found = false;
            lock (_graphStoreLock)
            {
                found = GetGraphStore().TryGetValue(id, out gtm);
            }

            if (found) 
            {
                return gtm.GetGraphCopy();
            }

            return null;

        }

        public bool RemoveFromStore(Guid id)
        {
            bool found = false;
            lock (_graphStoreLock) {
                found = GetGraphStore().Remove(id);
            }
            return found;
        }
    }
}
