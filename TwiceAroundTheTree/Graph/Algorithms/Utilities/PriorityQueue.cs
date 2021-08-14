using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponents.Algorithms.Utilities
{
    public class PriorityQueue
    {
        private PriorityComparer priorityComparer = new PriorityComparer();
        List<PriorityQueueItem> items = new List<PriorityQueueItem>();
        Dictionary<object, PriorityQueueItem> itemsMap = new Dictionary<object, PriorityQueueItem>();

        public void Insert(object item, int priority)
        {
            PriorityQueueItem pqi = new PriorityQueueItem(item, priority);
            items.Add(pqi);
            itemsMap.Add(item, pqi);
            items.Sort(priorityComparer);
        }

        public object Max() {
            return items?.First()?.DataItem;
        }

        public object Min()
        {
            return items?.Last()?.DataItem;
        }

        public object ExtractMax()
        {
            object item = items?.First()?.DataItem;
            items.Remove(items.First());
            itemsMap.Remove(item);
            return item;
        }

        public object ExtractMin()
        {
            object item = items?.Last()?.DataItem;
            items.Remove(items.Last());
            itemsMap.Remove(item);
            return item;
        }

        public bool Increase(object item)
        {
            if (!itemsMap.ContainsKey(item)) 
            {
                return false;
            }

            itemsMap[item].Priority += 1;
            items.Sort(priorityComparer);
            return true;
        }

        public bool Decrease(object item)
        {
            if (!itemsMap.ContainsKey(item))
            {
                return false;
            }

            itemsMap[item].Priority -= 1;
            items.Sort(priorityComparer);
            return true;
        }

        public int GetQueueSize() {
            return items.Count;
        }

        public int GetItemPriority(object item) 
        {
            PriorityQueueItem pqItem;
            if (itemsMap.TryGetValue(item, out pqItem)) 
            {
                return pqItem.Priority;
            }
            return int.MinValue;
            
            
        }

    }

    public class PriorityQueueItem 
    {
        public PriorityQueueItem(object item, int priority) 
        {
            DataItem = item;
            Priority = priority;
        }
        public object DataItem { get; set; }
        public int Priority { get; set; }
    }

    class PriorityComparer : IComparer<PriorityQueueItem>
    {
        public int Compare(PriorityQueueItem x, PriorityQueueItem y)
        {
            
            if (x.Priority == y.Priority)
                return 0;

            if (x.Priority < y.Priority)
                return 1;

            return -1;
        }
    }


}

