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
        List<Edge> Forest = new List<Edge>();
        List<Node> V;

        public DepthFirstWalk(Graph sourceGraph, int startWalkFromModeIndex) 
        {
            SourceGraph = sourceGraph;
            BeginNode = SourceGraph.Vertices[startWalkFromModeIndex];
            V = new List<Node>();
            V.Add(BeginNode);
            foreach (Node v in SourceGraph.Vertices) {
                if (!V.Contains(v)) 
                {
                    V.Add(v);
                }
            }
            Av = SourceGraph.AdjacencyVertices();
        }

        public void CreateWalk() {           
            int i = 1;
            Dictionary<Node, int> jnum = new();


            foreach (Node u in V) 
            {
                jnum[u] = 0;
            }

            foreach (Node u in V)
            {
                if (jnum[u] == 0) 
                {
                    dfkulku(i, u, jnum);
                }

            }



        }

        private void dfkulku(int i, Node u, Dictionary<Node, int> jnum) 
        {
            jnum[u] = i;
            i += 1;
            foreach (Node r in Av[u]) 
            {
                if (jnum[r] == 0) 
                {
                    Forest.Add(SourceGraph.GetEdgeBetween(u, r));
                    dfkulku(i, r, jnum);
                }
            }

        }


    }
}
