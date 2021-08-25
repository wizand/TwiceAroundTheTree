using GraphComponents.Algorithms.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponents.Algorithms
{
    public class DepthFirstWalk
    {
        public Graph SourceGraph { get; set; }
        public Node BeginNode { get; set; }
        public Dictionary<Node, List<Node>> Av;
        public List<Edge> CompleteWalkAsEdges { get; set; } = new List<Edge>();
        List<Node> V;
        List<Edge> E;
        public List<Node> CompleteWalkAsNodes { get; set; } = new List<Node>();

        public DepthFirstWalk(Graph sourceGraph, int startWalkFromModeIndex) 
        {

            //TODO: ..this probably should check that the graph is msp or at least that it doesnt contain circuits?

            SourceGraph = sourceGraph;
            
            BeginNode = SourceGraph.Vertices[startWalkFromModeIndex];
            E = new List<Edge>();
            V = new List<Node>();

            //Initialize the nodes so that the begin node comes first.
            V.Add(BeginNode);
            foreach (Node v in SourceGraph.Vertices) {
                if (!V.Contains(v)) 
                {
                    V.Add(v);
                }
            }
            Av = SourceGraph.AdjacencyVertices();
            foreach (Node n in Av.Keys) 
            {
                foreach ( Node v in Av[n] ) 
                {
                    List<Node> adjacents = Av[v];
                    if (adjacents.Contains(n)) 
                    {
                        continue;
                    }
                    else 
                    {
                        adjacents.Add(n);
                    }
                    Av[v] = adjacents;
                }
            }

            foreach (Edge e in SourceGraph.Edges ) 
            {
                E.Add(e);
                Edge otherWayE = e.GetOtherWay();
                if (!E.Contains(otherWayE))
                    E.Add(otherWayE);
                    
            }


        }

        public void CreateWalk() {           
            int i = 1;
            Dictionary<Node, int> orderNumber = new();

            foreach (Node u in V) 
            {
                orderNumber[u] = 0;
            }

            foreach (Node u in V)
            {
                if (orderNumber[u] == 0) 
                {
                    CompleteWalkAsNodes.Add(u);
                    depthFirstWalking(i, u, orderNumber);
                }
            }



        }

        private void depthFirstWalking(int i, Node u, Dictionary<Node, int> orderNumber) 
        {
            orderNumber[u] = i;
            i = i + 1;
            foreach (Node r in Av[u]) 
            {
                if (orderNumber[r] == 0) 
                {
                    CompleteWalkAsNodes.Add(r);
                    CompleteWalkAsEdges.Add(GetEdgeBetween(u,r));
                    depthFirstWalking(i, r, orderNumber);
                    CompleteWalkAsNodes.Add(u);
                    CompleteWalkAsEdges.Add(GetEdgeBetween(r, u));
                }
            }
        }


        public Edge GetEdgeBetween(Node u, Node r)
        {
            foreach (Edge e in E)
            {
                if (e.Begin.Equals(u) && e.End.Equals(r))
                    return e;
            }
            return null;
        }
    }


}
