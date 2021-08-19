using GraphComponents.Algorithms.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphComponents.Algorithms
{
    /**
     * Description
    The algorithm may informally be described as performing the following steps:
    
    - Initialize a tree with a single vertex, chosen arbitrarily from the graph.
    - Grow the tree by one edge: of the edges that connect the tree to vertices not yet in the tree, find the minimum-weight edge, and transfer it to the tree.
    - Repeat step 2 (until all vertices are in the tree).
    
    In more detail, it may be implemented following the pseudocode below.

    Associate with each vertex v of the graph a number C[v] (the cheapest cost of a connection to v) and an edge E[v] (the edge providing that cheapest connection). 
        To initialize these values, set all values of C[v] to +∞ (or to any number larger than the maximum edge weight) 
        and set each E[v] to a special flag value indicating that there is no edge connecting v to earlier vertices.
    
    Initialize an empty forest F and a set Q of vertices that have not yet been included in F (initially, all vertices).
    
    Repeat the following steps until Q is empty:
    Find and remove a vertex v from Q having the minimum possible value of C[v]
    Add v to F and, if E[v] is not the special flag value, also add E[v] to F
    Loop over the edges vw connecting v to other vertices w. For each such edge, if w still belongs to Q and vw has smaller weight than C[w], perform the following steps:
    Set C[w] to the cost of edge vw
    Set E[w] to point to edge vw.
    Return F

    As described above, the starting vertex for the algorithm will be chosen arbitrarily, because the first iteration of the main loop of the algorithm will have a set of vertices in Q that all have equal weights, 
    and the algorithm will automatically start a new tree in F when it completes a spanning tree of each connected component of the input graph. 
    The algorithm may be modified to start with any particular vertex s by setting C[s] to be a number smaller than the other values of C (for instance, zero), 
    and it may be modified to only find a single spanning tree rather than an entire spanning forest (matching more closely the informal description) by stopping whenever it encounters another vertex flagged as having no associated edge.

    Different variations of the algorithm differ from each other in how the set Q is implemented: as a simple linked list or array of vertices, 
    or as a more complicated priority queue data structure. This choice leads to differences in the time complexity of the algorithm. 
    In general, a priority queue will be quicker at finding the vertex v with minimum cost, 
    but will entail more expensive updates when the value of C[w] changes.
    */


    public class PrimsAlgorithm
    {
        private Graph SourceGraph { get; set; }
        private List<Node> V = new();
        Dictionary<Node, List<Node>> Av = new();
        private List<Edge> MSP = new List<Edge>();

        Graph MSPGraph;

        public PrimsAlgorithm(Graph graph)
        {
            SourceGraph = graph;
            V = graph.Vertices;
            Av = SourceGraph.AdjacencyVertices();

        }

        public int GetWeight() {
            int totalWeight = 0;
            foreach (Edge e in MSP) 
            {
                totalWeight += e.Weight;
            }
            return totalWeight;
        
        }

        private bool isReVminusS(Node n, List<Node> V, List<Node> S)
        {
            List<Node> VminusS = CutList(V, S);
            return VminusS.Contains(n);
        }

        public List<Node> CutList(List<Node> V, List<Node> S) 
        {
            List<Node> CutList = new List<Node>();
            foreach (Node v in V) 
            {
                if (S.Contains(v))
                {
                    continue;
                }
                CutList.Add(v);
            }

            return CutList;

        }

        public void FindMspStartingFromNode(Node beginNode)
        {
            Dictionary<Node, int> d = new Dictionary<Node, int>();
            Dictionary<Node, Edge> b = new Dictionary<Node, Edge>();
            List<Node> S = new List<Node>();
            MSP = new List<Edge>();

            foreach (Node v in SourceGraph.Vertices)
            {
                d[v] = int.MaxValue;
                b[v] = null;
            }

            d[beginNode] = 0;
            while (S.Count < V.Count)
            {
                bool isFirst = true;
                Node r = null;
                int currentMin = 0;
                //etsitään pistettä r, s.e. r ∈ (V − S) ja d(r) on mahdollsimman pieni
                foreach ( Node nodeNotYetInS in CutList(V,S) )
                {
                    if (isFirst) {
                        r = nodeNotYetInS;
                        currentMin = d[nodeNotYetInS];
                        isFirst = false;
                        continue;
                    }
                    if (d[nodeNotYetInS] < currentMin) {
                        r = nodeNotYetInS;
                        currentMin = d[nodeNotYetInS];
                    }
                }

                S.Add(r);

                if ( b[r] != null)
                {
                    MSP.Add(b[r]);
                }

                foreach (Node u in Av[r])
                {
                    if (S.Contains(u))
                        continue;
                    Edge e = GetEdgeBetween(u, r);
                    if( e != null ) 
                    {
                        if (e.Weight < d[u]) 
                        {
                            d[u] = e.Weight;
                            b[u] = e;
                        }
                    }

                }

            }
            MSPGraph = new Graph(MSP, V);
            MSPGraph.IsMSP = true;
            MSPGraph.Weight = GetWeight();
            SourceGraph.MSPGraphs.Add(MSPGraph);
            
        }

        private Edge GetEdgeBetween(Node u, Node r)
        {
            foreach (Edge e in SourceGraph.EdgesFromNode[u])
            {
                if (e.End.Equals(r))
                {
                    return e;
                }
            }
            return null;
        }
    }
}

