using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponents.Algorithms.Utilities
{
    public class PriorityQueue<T>
    {
        private PriorityComparer<T> priorityComparer = new PriorityComparer<T>();
        List<PriorityQueueItem<T>> items = new List<PriorityQueueItem<T>>();
        Dictionary<T, PriorityQueueItem<T>> itemsMap = new Dictionary<T, PriorityQueueItem<T>>();

        public void Insert(T item, int priority)
        {
            PriorityQueueItem<T> pqi = new PriorityQueueItem<T>(item, priority);
            items.Add(pqi);
            itemsMap.Add(item, pqi);
            items.Sort(priorityComparer);
        }

        public T Max() {
            return items.First().DataItem;
        }

        public T Min()
        {
            return items.Last().DataItem;
        }

        public T ExtractMax()
        {
            T item = items.First().DataItem;
            items.Remove(items.First());
            itemsMap.Remove(item);
            return item;
        }

        public T ExtractMin()
        {
            T item = items.Last().DataItem;
            items.Remove(items.Last());
            itemsMap.Remove(item);
            return item;
        }

        public bool Increase(T item)
        {
            if (!itemsMap.ContainsKey(item)) 
            {
                return false;
            }

            itemsMap[item].Priority += 1;
            items.Sort(priorityComparer);
            return true;
        }

        public bool Decrease(T item)
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

        public int GetItemPriority(T item) 
        {
            PriorityQueueItem<T> pqItem;
            if (itemsMap.TryGetValue(item, out pqItem)) 
            {
                return pqItem.Priority;
            }
            return int.MinValue;
            
            
        }

        public void SetPriorityTo(T item, int newPriority) 
        {
            if (!itemsMap.ContainsKey(item))
            {
                return;
            }

            itemsMap[item].Priority = newPriority;
            items.Sort(priorityComparer);
            return;
        }

    }

    public class PriorityQueueItem<T> 
    {
        public PriorityQueueItem(T item, int priority) 
        {
            DataItem = item;
            Priority = priority;
        }
        public T DataItem { get; set; }
        public int Priority { get; set; }
    }

    class PriorityComparer<T> : IComparer<PriorityQueueItem<T>>
    {
        public int Compare(PriorityQueueItem<T> x, PriorityQueueItem<T> y)
        {
            
            if (x.Priority == y.Priority)
                return 0;

            if (x.Priority < y.Priority)
                return 1;

            return -1;
        }
    }


}

