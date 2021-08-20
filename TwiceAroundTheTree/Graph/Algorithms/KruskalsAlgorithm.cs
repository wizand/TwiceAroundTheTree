using GraphComponents.Algorithms.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponents.Algorithms
{
    public class KruskalsAlgorithm
    {
        public Graph SourceGraph;
        public List<Edge> MSP;

        public KruskalsAlgorithm(Graph sourceGraph) 
        {
            SourceGraph = sourceGraph;
            FindMsp();
        }


        /**
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
        public void FindMsp()
        {
            List<Edge> F = new List<Edge>();
            
            List<Edge> S = new List<Edge>();
            DisjointSet<Node> ds = new DisjointSet<Node>();
            foreach (Node v in SourceGraph.Vertices) 
            {
                ds.MakeSet(v);
            }

            PriorityQueue<Edge> pq = new PriorityQueue<Edge>();
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
                    F.Add(e.GetOtherWay());
                    ds.Union(ds.FindSet(e.Begin), ds.FindSet(e.End));
                }
            }




        }
    }
}
