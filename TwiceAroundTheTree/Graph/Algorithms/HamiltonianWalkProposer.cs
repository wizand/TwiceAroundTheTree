using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphComponents.Algorithms
{
    public class HamiltonianWalkProposer
    {

        public HamiltonianWalkProposer(Graph sourceGraph, List<List<Node>> potentialWalks)
        {
            SourceGraph = sourceGraph;
            PotentialWalks = potentialWalks;
            
        }

        public Graph SourceGraph { get; set;  }
        public List<List<Node>> PotentialWalks { get; set; }
        public List<List<Edge>> HamiltonianWalksAsEdges { get; set; }


        public void BuildHamiltonianCircuits() {
            buildPotentialWalks();
            foreach (List<Edge> walk in HamiltonianWalksAsEdges) 
            {
                Graph g = new Graph(walk, SourceGraph.Vertices);
                int weight = g.Weight;
                g.IsHamiltonianCircuit = true;
                SourceGraph.HamiltonianCircuitGraphs.Add(g);

            }

            SourceGraph.HamiltonianCircuitGraphs.Sort((a, b) => a.Weight.CompareTo(b.Weight));

        }

        private void buildPotentialWalks() 
        {
            foreach (List<Node> potentialWalk in PotentialWalks)
            {
                if (potentialWalk.First().Equals(potentialWalk.Last()))
                {
                    continue;
                }

                potentialWalk.Add(new Node(potentialWalk.First().Name));

            }

            int fakes = 0;
            HamiltonianWalksAsEdges = new();
            foreach (List<Node> potentialWalk in PotentialWalks)
            {
                bool allEdgesOk = true;
                List<Edge> potentialWalkAsEdges = new();
                for (int i = 0; i < potentialWalk.Count-1; i++)
                {
                    Node u = potentialWalk[i];
                    Node v = potentialWalk[i + 1];
                    Edge e = SourceGraph.GetEdgeBetween(u,v, true);
                    if (e == null)
                    {
                        allEdgesOk = false;
                        //This walk was not real since there is no such edge in the graph.
                        break;
                    }
                    
                    potentialWalkAsEdges.Add(e);
                }
                if (allEdgesOk)
                {
                    HamiltonianWalksAsEdges.Add(potentialWalkAsEdges);
                } else
                {
                    fakes += 1;
                }
            }

        }

    }
}
