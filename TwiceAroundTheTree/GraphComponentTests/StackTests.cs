using GraphComponents.Algorithms.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GraphComponentTests
{
    public class StackTests
    {

        [Fact]
        public void StackShouldGrowAndShrinkWhenPushingAndPopping()
        {

            BasicStack<string> s = new BasicStack<string>();
            Assert.True(s.Count == 0);
            s.Push("A");
            Assert.True(s.Count == 1);
            s.Push("B");
            Assert.True(s.Count == 2);
            string str;
            s.TryPop(out str);
            Assert.True(s.Count == 1);
            s.TryPop(out str);
            Assert.True(s.Count == 0);

            
        }


        [Fact]
        public void TryPopShouldReturnsTrueIfPopSuccesfulAndFalseIfNot() {
            BasicStack<string> s = new BasicStack<string>();
            s.Push("A");
            Assert.True(s.Count == 1);
            string str;
            Assert.True(s.TryPop(out str));
            Assert.True(s.Count == 0);
            Assert.False(s.TryPop(out str));

        }

        [Fact]
        public void PopShouldGetCorrectItem() 
        {
            string str;
            BasicStack<string> s = new BasicStack<string>();
            Assert.True(s.Count == 0);
            s.Push("A");
            Assert.True(s.Count == 1);
            s.Push("B");
            Assert.True(s.Count == 2);
            
            s.TryPop(out str);
            Assert.True(str.Equals("B"));
            
            s.TryPop(out str);
            Assert.True(str.Equals("A"));

            Assert.True(s.Count == 0);
            s.Push("A");
            s.Push("B");

            s.TryPop(out str);
            Assert.True(str.Equals("B"));
            
            s.Push("C");
            s.TryPop(out str);
            Assert.True(str.Equals("C"));
            
            s.TryPop(out str);
            Assert.True(str.Equals("A"));
        }

        [Fact]  
        public void PeekShouldntChangeCount() 
        {   string str;
            BasicStack<string> s = new BasicStack<string>();
            str = s.Peek();
            Assert.True(s.Count == 0);
            s.Push("A");
            Assert.True(s.Count == 1);
            s.Push("B");
            Assert.True(s.Count == 2);

           
            str = s.Peek();
            Assert.True(str.Equals("B"));
            Assert.True(s.Count == 2);
            str = s.Peek();
            str = s.Peek();
            str = s.Peek();
            Assert.True(s.Count == 2);

        }


    }
}
