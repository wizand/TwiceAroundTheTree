using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponents.Algorithms
{
    public class CartesianProductSearch
    {
        public CartesianProductSearch(Graph sourceGraph, List<Node> completeWalk, List<Node> nodesInOrder) 
        {


            Dictionary<Node, List<int>> nodePositions = new();

            foreach (Node n in nodesInOrder) 
            {
                nodePositions[n] = new List<int>();
            }

            int index = 1;
            foreach (Node walkNode in completeWalk) 
            {
                List<int> posList = nodePositions[walkNode];
                posList.Add(index);
                nodePositions[walkNode] = posList; 
                index += 1;
            }



        }

    }
}
