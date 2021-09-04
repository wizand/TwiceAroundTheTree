using GraphComponents;
using System;
using System.Collections.Generic;

namespace GraphDataStorage
{
    public class DataCache : IGraphDataAccess
    {
        private IDictionary<Guid, AbstractGraphStoreModel> _graphStore = null;
        private object _graphStoreLock = new object();


        private static DataCache _instance = null;
        public static DataCache Instance { get {
                if (_instance == null)
                    _instance = new DataCache();
                return _instance;
            }
        }

        private IDictionary<Guid, AbstractGraphStoreModel> GetGraphStore()
        {


            if (_graphStore == null)
            {
                _graphStore = new Dictionary<Guid, AbstractGraphStoreModel>();
            }

            return _graphStore;
        }

        public Guid StoreMultipleGraphs(List<Graph> graphs)
        {
            MultipleGraphsStoreModel storeModel = new MultipleGraphsStoreModel(graphs);
        
            lock(_graphStoreLock)
            {
                foreach (SingleGraphStoreModel singleStoreModel in storeModel.StoredGraphs) 
                {
                    GetGraphStore()[singleStoreModel.Id] = singleStoreModel;
                }
                
                GetGraphStore()[storeModel.Id] = storeModel;
            }

            return storeModel.Id;
        }

        public Guid StoreGraph(Graph graphToStore)
        {
            SingleGraphStoreModel storeModel = new SingleGraphStoreModel(graphToStore);
            lock(_graphStoreLock)
            {
                GetGraphStore()[storeModel.Id] = storeModel;
            }
            return storeModel.Id;
        }

        public Graph GetGraphFromStore(Guid id)
        {
            AbstractGraphStoreModel gtm = null;
            bool found = false;
            lock (_graphStoreLock)
            {
                found = GetGraphStore().TryGetValue(id, out gtm);
            }

            if (found) 
            {
                //TODO: This could probably be done a bit more elegantly via polymorfism
                if (gtm is SingleGraphStoreModel)
                {
                    return ((SingleGraphStoreModel)gtm).GetGraphCopy();
                }
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

        public int GetCacheSize()
        {
            int count = 0;
            lock (_graphStoreLock)
            {
                count = GetGraphStore().Count;
            }
            return count;
        }
    }
}
