using GraphComponents;
using GraphComponents.Algorithms.Utilities;
using System;
using Xunit;

namespace GraphComponentTests
{
    public class PriorityQueueTests
    {

        public PriorityQueue<Node> InitializePriorityQueue(int itemNumber) {
            PriorityQueue<Node> pq = new PriorityQueue<Node>();


            for (int i = 0; i < itemNumber; i++) {
                pq.Insert(new Node(('A' + i).ToString()), i + 1);
            }

            return pq;
            
        }

        [Fact]
        public void CanAddItemsToPriorityQueue()
        {

            PriorityQueue<Node> pq = InitializePriorityQueue(4);

            Assert.True(pq.GetQueueSize() == 4);

            pq.Insert(new Node("New"), 10);
            Assert.True(pq.GetQueueSize() == 5);
        }

        [Fact]
        public void ShouldBeInOrderByPriority() {
            PriorityQueue<Node> pq = new PriorityQueue<Node>();
            pq.Insert(new Node("HighPriority"), 10);

            Node maxPriority = (Node)pq.Max();
            Assert.True(maxPriority.Name.Equals("HighPriority"));

            pq.Insert(new Node("LowPriority"), 5);

            maxPriority = (Node)pq.Max();
            Assert.True(maxPriority.Name.Equals("HighPriority"));

            pq.Insert(new Node("HigherPriority"), 100);
            maxPriority = (Node)pq.Max();
            Assert.True(maxPriority.Name.Equals("HigherPriority"));

            Node minPriority = (Node)pq.Min();
            Assert.True(minPriority.Name.Equals("LowPriority"));
        }

        [Fact]
        public void MaxExctractionRemovesItemFromTop()
        {
            PriorityQueue<Node> pq = new PriorityQueue<Node>();
            pq.Insert(new Node("HigherPriority"), 100);
            pq.Insert(new Node("HighPriority"), 10);
            pq.Insert(new Node("LowPriority"), 5);

            Node maxPriority = (Node)pq.Max();
            Assert.True(maxPriority.Name.Equals("HigherPriority"));

            maxPriority = (Node)pq.ExtractMax();
            Assert.True(maxPriority.Name.Equals("HigherPriority"));
            Assert.True(pq.GetQueueSize() == 2);

            maxPriority = (Node)pq.Max();
            Assert.True(maxPriority.Name.Equals("HighPriority"));

            maxPriority = (Node)pq.ExtractMax();
            Assert.True(maxPriority.Name.Equals("HighPriority"));
            Assert.True(pq.GetQueueSize() == 1);

            maxPriority = (Node)pq.Max();
            Assert.True(maxPriority.Name.Equals("LowPriority"));
        }

        [Fact]
        public void MinExctractionRemovesItemFromBottom()
        {
            PriorityQueue<Node> pq = new PriorityQueue<Node>();
            pq.Insert(new Node("HigherPriority"), 100);
            pq.Insert(new Node("HighPriority"), 10);
            pq.Insert(new Node("LowPriority"), 5);

            Node maxPriority = (Node)pq.Max();
            Assert.True(maxPriority.Name.Equals("HigherPriority"));

            Node minPriority = (Node)pq.Min();
            Assert.True(minPriority.Name.Equals("LowPriority"));

            minPriority = (Node)pq.ExtractMin();
            Assert.True(minPriority.Name.Equals("LowPriority"));
            Assert.True(pq.GetQueueSize() == 2);

            maxPriority = (Node)pq.Max();
            Assert.True(maxPriority.Name.Equals("HigherPriority"));
            minPriority = (Node)pq.Min();
            Assert.True(minPriority.Name.Equals("HighPriority"));

            minPriority = (Node)pq.ExtractMin();
            Assert.True(minPriority.Name.Equals("HighPriority"));
            Assert.True(pq.GetQueueSize() == 1);

            maxPriority = (Node)pq.Max();
            minPriority = (Node)pq.Min();
            Assert.True(maxPriority.Name.Equals("HigherPriority"));
            Assert.True(minPriority.Name.Equals("HigherPriority"));
        }

        [Fact]
        public void IncreaseShouldIncreasePriorityByOne() 
        {
            PriorityQueue<Node> pq = new PriorityQueue<Node>();
            pq.Insert(new Node("HigherPriority"), 100);
            pq.Insert(new Node("HighPriority"), 10);
            pq.Insert(new Node("LowPriority"), 5);

            Node maxPriorityItem = (Node)pq.Max();
            Node minPriorityItem = (Node)pq.Min();

            int priority = pq.GetItemPriority(maxPriorityItem);
            Assert.True(priority == 100);
            priority = pq.GetItemPriority(minPriorityItem);
            Assert.True(priority == 5);

            pq.Increase(minPriorityItem);
            priority = pq.GetItemPriority(minPriorityItem);
            Assert.True(priority == 6);

            pq.Increase(minPriorityItem);
            priority = pq.GetItemPriority(minPriorityItem);
            Assert.True(priority == 7);

            pq.Increase(maxPriorityItem);
            priority = pq.GetItemPriority(maxPriorityItem);
            Assert.True(priority == 101);
        }

        [Fact]
        public void DecreaseShouldDecreasePriorityByOne()
        {
            PriorityQueue<Node> pq = new PriorityQueue<Node>();
            pq.Insert(new Node("HigherPriority"), 100);
            pq.Insert(new Node("HighPriority"), 10);
            pq.Insert(new Node("LowPriority"), 5);

            Node maxPriorityItem = (Node)pq.Max();
            Node minPriorityItem = (Node)pq.Min();

            int priority = pq.GetItemPriority(maxPriorityItem);
            Assert.True(priority == 100);
            priority = pq.GetItemPriority(minPriorityItem);
            Assert.True(priority == 5);

            pq.Decrease(minPriorityItem);
            priority = pq.GetItemPriority(minPriorityItem);
            Assert.True(priority == 4);

            pq.Decrease(minPriorityItem);
            priority = pq.GetItemPriority(minPriorityItem);
            Assert.True(priority == 3);

            pq.Decrease(maxPriorityItem);
            priority = pq.GetItemPriority(maxPriorityItem);
            Assert.True(priority == 99);
        }

        [Fact]
        public void ChangingPriorirtyShouldReorderQueue()
        {
            PriorityQueue<Node> pq = new PriorityQueue<Node>();
            pq.Insert(new Node("HigherPriority"), 10);
            pq.Insert(new Node("LowPriority"), 9);

            Node maxPriorityItem = (Node)pq.Max();
            Node minPriorityItem = (Node)pq.Min();

            int priority = pq.GetItemPriority(maxPriorityItem);
            Assert.True(priority == 10);
            priority = pq.GetItemPriority(minPriorityItem);
            Assert.True(priority == 9);

            pq.Increase(minPriorityItem);
            priority = pq.GetItemPriority(minPriorityItem);
            Assert.True(priority == 10);

            minPriorityItem = (Node)pq.Min();
            Assert.True(minPriorityItem.Name.Equals("LowPriority"));

            pq.Increase(minPriorityItem);
            priority = pq.GetItemPriority(minPriorityItem);
            Assert.True(priority == 11);

            minPriorityItem = (Node)pq.Min();
            Assert.True(minPriorityItem.Name.Equals("HigherPriority"));

            maxPriorityItem = (Node)pq.Max();
            Assert.True(maxPriorityItem.Name.Equals("LowPriority"));

            pq.Decrease(maxPriorityItem);
            pq.Decrease(maxPriorityItem);
            priority = pq.GetItemPriority(maxPriorityItem);
            Assert.True(priority == 9);

            minPriorityItem = (Node)pq.Min();
            Assert.True(minPriorityItem.Name.Equals("LowPriority"));

    
        }


    }
}
