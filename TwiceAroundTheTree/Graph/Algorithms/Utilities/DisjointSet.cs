using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponents.Algorithms.Utilities
{
    public class DisjointSet<T>
    {
        SetNode root = new SetNode(null, null);
        Dictionary<T, List<T>> sets = new();
        Dictionary<T, T> parent = new();

        public int SetsCount { 
            get {
                return sets.Count;
            }
        }

        public int GetItemsInSetCout(T data)
        {
            return sets[FindSet(data)].Count;
        }

        public void MakeSet(T data) {
            List<T> tmpList = new List<T>();
            tmpList.Add(data);
            sets[data] = tmpList;
            parent[data] = data;
        }

        public List<T> GetSetFromParent(T parent) {
            return sets[parent];
        }

        public List<T> GetSetWhereItem(T data) 
        {
            return sets[FindSet(data)];
        }

        public T FindSet(T data) {
            return parent[data];
        }

        public void Union(T dataA, T dataB) 
        {

            T parentSetA = FindSet(dataA);
            T parentSetB = FindSet(dataB);
            List<T> aSet = sets[parentSetA];
            List<T> bSet = sets[parentSetB];
            
            //Copy the itesm from dataB set to the dataA set
            foreach (T bItem in bSet) 
            {
                if (aSet.Contains(bItem)) 
                {
                    continue;
                }
                aSet.Add(bItem);
            }

            //Clean up the old dataB set
            sets.Remove(FindSet(dataB));

            //Update the new parent for dataB
            //parent[dataB] = parent[dataA];
            updateAllParents(dataB, dataA);
        }

    
        private void updateAllParents(T from, T to) 
        {
            foreach(T parentKey in parent.Keys) 
            {
                if (parent[parentKey].Equals(from))
                    parent[parentKey] = to;
            }
        }
    }



    internal class SetNode {

        public SetNode(SetNode parent, object data) 
        {
            Parent = parent;
            Data = data;
        }
        public SetNode Parent { get; set; }
        public object Data { get; set; }
    }
}
