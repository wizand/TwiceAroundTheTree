using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponents.Algorithms
{
    public class CartesianProductSearch
    {

        public Dictionary<Node, List<int>> nodePositions { get; set; }  = new();
        private List<Node> V;
        public List<List<Node>> PotentialWalks { get; set; }  = new();
        private List<Node> CompleteWalk;

        public CartesianProductSearch(Graph sourceGraph, List<Node> completeWalk, List<Node> nodesInOrder) 
        {

            V = nodesInOrder;
            CompleteWalk = completeWalk;

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


            int i = 0;
            int[][] nodeSets = new int[nodePositions.Values.Count][];
            foreach (Node n in nodesInOrder)
            {

                nodeSets[i] = nodePositions[n].ToArray();
                i = i + 1;
            }
            var cartesianProductOfNodeSets = CartesianProduct(nodeSets);

            PotentialWalks = CreatePotentialWalks(cartesianProductOfNodeSets);


        }


        /// <summary>
        /// n-ary cartesian product via LINQ. Thanks to
        /// http://www.interact-sw.co.uk/iangblog/2010/07/28/linq-cartesian-1
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        private IEnumerable<int[]> CartesianProduct(params int[][] inputs)
        {
            return inputs.Aggregate(
                (IEnumerable<int[]>)new int[][] { Array.Empty<int>() },
                (soFar, input) =>
                    from prevProductItem in soFar
                    from item in input
                    select prevProductItem.Concat(new int[] { item }).ToArray());
        }


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="cartesianProductOfNodeSets"></param>
        /// <returns></returns>
        private List<List<Node>> CreatePotentialWalks(IEnumerable<int[]> cartesianProductOfNodeSets ) {
            
            List<List<Node>> walks = new();
            foreach (var walkInt in cartesianProductOfNodeSets)
            {
                List<Node> walkAsNodes = new();
                Array.Sort(walkInt);
                foreach (int nodeInt in walkInt)
                {
                    walkAsNodes.Add(CompleteWalk[nodeInt - 1]);
                }
                walks.Add(walkAsNodes);
            }

            return walks;
        }

    }
}
