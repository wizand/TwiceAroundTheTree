using System.Collections.Generic;

namespace GraphComponents.Algorithms
{
    public abstract class AbstractMspAlgorithm
    {

        public Graph SourceGraph { get; set; }
        public List<Edge> MSP { get; set; }
        public Graph MSPGraph { get; set; }
        protected List<Node> V { get; set; }

        public AbstractMspAlgorithm(Graph sourceGraph) {
            SourceGraph = sourceGraph;
            V = SourceGraph.Vertices;
        }

        public abstract void FindMsp();

        public int GetWeight()
        {
            int totalWeight = 0;
            foreach (Edge e in MSP)
            {
                totalWeight += e.Weight;
            }
            MSPGraph.Weight = totalWeight;
            return MSPGraph.Weight;

        }
    }
}