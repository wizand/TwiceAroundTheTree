using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponents.Algorithms.Utilities
{
    public class BasicStack<T>
    {

        private StackItem<T> Root { get; set; }

        public BasicStack()
        {
            Root = null;
        }

        public void Push(T data)
        {
            if ( Root == null ) 
            {
                Root = new StackItem<T>(data);
                return;
            }
            StackItem<T> last = Root;
            for (; last.Next != null; last = last.Next) 
            {
            }
                last.Next = new StackItem<T>(data);
                last.Next.Prev = last;
        }

        public bool TryPop(out T data) 
        {
            if (Root == null)
            {
                data = default(T);
                return false;
            }

            StackItem<T> last = Root;
            for (; last.Next != null; last = last.Next)
            {
            }


            data = last.Data;
            if (last.Prev == null)
            {
                Root = null;
            } 
            else
            {
                last.Prev.Next = null;
            }
            return true;

        }

        public int Count
        {
            get
            {

                if (Root == null)
                {
                    return 0;
                }
                int i = 1;
                for (StackItem<T> last = Root; last.Next != null; last = last.Next)
                {
                    i += 1;
                }

                return i;
            }
        }

        public T Peek()
        {
            if (Root == null)
            {
                return default;
            }
            StackItem<T> last = Root;
            for (; last.Next != null; last = last.Next)
            {
            }
            return last.Data;
        }

    }

    internal class StackItem<T> 
    {
        public T Data { get; set; }
        public StackItem<T> Next { get; set; } = null;
        public StackItem<T> Prev { get; set; } = null;
        
        public StackItem(T data) {
            Data = data;
            Next = null;
        }
    }

}
