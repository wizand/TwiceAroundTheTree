using GraphComponents.Algorithms.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponents.Algorithms
{
    /// <summary>
    /// This is approximate implementation of Kruskal's algorithm for finding the MSP of a graph.
    /// 
    /// 
    /// From https://en.wikipedia.org/wiki/Kruskal%27s_algorithm
    /// Kruskal's algorithm finds a minimum spanning forest of an undirected edge-weighted graph. 
    /// If the graph is connected, it finds a minimum spanning tree. 
    /// (A minimum spanning tree of a connected graph is a subset of the edges that forms a tree that includes every vertex, where the sum of the weights of all the edges in the tree is minimized. 
    /// For a disconnected graph, a minimum spanning forest is composed of a minimum spanning tree for each connected component.) 
    /// 
    /// It is a greedy algorithm in graph theory as in each step it adds the next lowest-weight edge that will not form a cycle to the minimum spanning forest.[1]
    /// 
    /// 
    /// 
    /// </summary>
    public class KruskalsAlgorithm : AbstractMspAlgorithm
    {


        public KruskalsAlgorithm(Graph sourceGraph) : base(sourceGraph)
        {
            
            
        }


        /**
         * Algorithm
                create a forest F (a set of trees), where each vertex in the graph is a separate tree
                create a set S containing all the edges in the graph
                while S is nonempty and F is not yet spanning
                remove an edge with minimum weight from S
                if the removed edge connects two different trees then add it to the forest F, combining two trees into a single tree
                At the termination of the algorithm, the forest forms a minimum spanning forest of the graph. If the graph is connected, the forest has a single component and forms a minimum spanning tree.
         * 
         * algorithm Kruskal(G) is
            F:= ∅
            for each v ∈ G.V do
                MAKE-SET(v)
            for each (u, v) in G.E ordered by weight(u, v), increasing do
                if FIND-SET(u) ≠ FIND-SET(v) then
                    F:= F ∪ {(u, v)} ∪ {(v, u)}
                    UNION(FIND-SET(u), FIND-SET(v))
            return F
        */
        public override void FindMsp()
        {
            List<Edge> F = new List<Edge>();
            MSP = new List<Edge>();
            DisjointSet<Node> ds = new DisjointSet<Node>();
            PriorityQueue<Edge> pq = new PriorityQueue<Edge>();

            foreach (Node v in V) 
            {
                ds.MakeSet(v);
            }

            foreach (Edge e in SourceGraph.Edges) 
            {
                pq.Insert(e, e.Weight);
            }

            while( pq.GetQueueSize() > 0)
            {
                Edge e = pq.ExtractMin();
                if (ds.FindSet(e.Begin) != ds.FindSet(e.End)) 
                {
                    F.Add(e);
                    //  F.Add(e.GetOtherWay());
                    //ds.Union(ds.FindSet(e.Begin), ds.FindSet(e.End));
                    ds.Union(ds.FindSet(e.Begin), ds.FindSet(e.End));
                }
            }

            foreach (Edge mspEdge in F) {
                MSP.Add(mspEdge);
            }

            MSPGraph = new Graph(MSP, V);
            MSPGraph.IsMSP = true;
            MSPGraph.Weight = GetWeight();
            SourceGraph.MSPGraphs.Add(MSPGraph);

        }
    }
}
