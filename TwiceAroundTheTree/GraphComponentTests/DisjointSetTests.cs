using GraphComponents.Algorithms.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GraphComponentTests
{
    public class DisjointSetTests
    {

        [Fact]
        public void AddingItemWithMakeSetCreatesNewSet() {
            DisjointSet<string> ds = new();

            ds.MakeSet("Eka");
            ds.MakeSet("Toka");
            ds.MakeSet("Kolmas");

            Assert.True(ds.SetsCount == 3);
            Assert.True(ds.GetItemsInSetCout("Eka") == 1);
        }

        [Fact]
        public void FindSetFindsCorrectSet() {
            DisjointSet<string> ds = new();

            ds.MakeSet("Eka");
            ds.MakeSet("Toka");

            string setParent = ds.FindSet("Eka");
            Assert.Equal("Eka", setParent);
        }

        [Fact]
        public void UnionShouldAddItemsFromAToB() {
            DisjointSet<string> ds = new();

            string A = "A";
            string B = "B";
            string C = "C";

            ds.MakeSet(A);
            ds.MakeSet(B);
            ds.MakeSet(C);

            ds.Union(A, B);
            string setParent = ds.FindSet(B);
            Assert.Equal(A, setParent);
            int bCount = ds.GetItemsInSetCout(B);
            Assert.True(bCount == 2);
            List<string> set = ds.GetSetWhereItem(A);
            Assert.True(set.Count == 2);
            set = ds.GetSetWhereItem(B);
            Assert.True(set.Count == 2);
            set = ds.GetSetWhereItem(C);
            Assert.True(set.Count == 1);

            ds.Union(A, C);
            set = ds.GetSetWhereItem(C);
            Assert.True(set.Count == 3);
            ds.GetSetWhereItem(A);
            Assert.True(set.Count == 3);


        }
    }
}
