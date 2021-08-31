using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GraphComponents
{
    public class Graph
    {
        public List<Edge> Edges { get; set; } = new();
        [JsonIgnore]
        public Dictionary<Node, IList<Edge>> EdgesFromNode { get; set; } = new();
        public List<Node> Vertices { get; set; }
        public Matrix AdjacencyMatrix { get; set; }
        public bool IsDirectedGraph { get; set; } = false;


        public List<Graph> MSPGraphs { get; set; } = new List<Graph>();
        public bool IsMSP = false;
        public bool IsHamiltonianCircuit = false;
        public List<Graph> HamiltonianCircuitGraphs { get; set; } = new();
        private int? _weight = null;
        public int Weight {
            get
            {
                if (_weight == null) 
                {
                    _weight = 0;
                    foreach(Edge e in Edges)
                    {
                        _weight = _weight + e.Weight;
                    }
                    _weight = _weight / 2;
                }
                return _weight.Value;
            }
            set
            {
                _weight = value;
            }
        }

        public Graph(Graph createCopyFrom)
        {
            List<Node> V = new List<Node>();
            foreach (Node n in createCopyFrom.Vertices) 
            {
                V.Add(new Node(n.Name));
            }
            Vertices = V;

            List<Edge> E = new List<Edge>();
            foreach (Edge e in createCopyFrom.Edges) {
                E.Add(new Edge(e.Begin, e.End, e.Weight));
            }
            Edges = E;

            buildMatrixFromEdges();
            IsDirectedGraph = IsGraphDirected();
        }
        public Graph(Matrix adjacencyMatrix) {
            AdjacencyMatrix = adjacencyMatrix;
            Vertices = AdjacencyMatrix.Vertices;
            buildEdgesFromMatrix();
            IsDirectedGraph = IsGraphDirected();
        }

     
        public Graph(List<Edge> edges, List<Node> vertices) {
            Vertices = vertices;
            Edges = edges;
            foreach(Edge e in edges) 
            {
                addToEdgesDict(e.Begin, e);
            }
            buildMatrixFromEdges();
            IsDirectedGraph = IsGraphDirected(checkFromEdges: true); //Using the checkfromedges for example sake
        }

        private void buildMatrixFromEdges()
        {
            AdjacencyMatrix = new Matrix(Vertices);
            foreach (Edge e in Edges) 
            {
                AdjacencyMatrix.MatrixTable[Vertices.IndexOf(e.Begin)][Vertices.IndexOf(e.End)] = e.Weight;
                addToEdgesDict(e.Begin, e);
            }
        }

        /// <summary>
        /// Checks if each edge in the graph can be travelled both ways. This should be checked from the Matrix representation,
        /// but there the same functionality provided using edge dictionary. 
        /// </summary>
        /// <param name="checkFromEdges">If the directiness should be checked using the edges table. Probably shouldn't.</param>
        /// <returns>True if there are "one way" edges which means the graph is directed.</returns>
        private bool IsGraphDirected(bool checkFromEdges = false) 
        {
            if ( !checkFromEdges)
            {
                return AdjacencyMatrix.isDirected(); // Check if graph is directed from the matrix  
            } 
            else //Use the edges to see if there is an edge going from each begin node to end node as well as end node to begin node. Dont use this, just an example.
            {
                foreach (Node node in EdgesFromNode.Keys) //Go trough all the edges there are
                {
                    foreach (Edge edge in EdgesFromNode[node])  
                    {
                        Edge edgeToLookFor = edge.GetOtherWay();
                        IList<Edge> edgesForTheEndNode;
                        if (EdgesFromNode.TryGetValue(edge.End, out edgesForTheEndNode)) //If the graph is not directed, there should be edges starting from the end node.
                        {
                            bool found = false;
                            foreach (Edge endNodeEdge in edgesForTheEndNode)
                            {
                                if (endNodeEdge.Equals(edgeToLookFor))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                //No edge fromthe end node to begin node so the graph is directed
                                return true;
                            }
                        }
                        else {
                            //No edgges starting from the end node? Graph is directed then..
                            return true;
                        }
                    }
                }
                //All nodes that are as begin nodes in an edge, there is also edge starting from the end node. And thus graph is non directed.
                return false;
            }
        }

        private void buildEdgesFromMatrix()
        {
            int len = AdjacencyMatrix.MatrixTable.Length;

            for (int y = 0; y < len; y++) {
                for (int x = 0; x < AdjacencyMatrix.MatrixTable[y].Length; x++) 
                {
                    int value = AdjacencyMatrix.MatrixTable[y][x];
                    if (value > 0) 
                    {
                        //There is an edge
                        Edge e = new Edge(Vertices[y], Vertices[x], value);
                        Edges.Add(e);
                        addToEdgesDict(Vertices[y], e);
                    }
                }
            }

        }

        private void addToEdgesDict(Node node, Edge e)
        {
            IList<Edge> edges;
            if ( EdgesFromNode.ContainsKey(node) )
            { 
                edges = EdgesFromNode[node];
            } 
            else
            {
                edges = new List<Edge>();
            }
            
            if (!edges.Contains(e))
            {
                edges.Add(e);
            }

            EdgesFromNode[node] = edges;
        }

        private Dictionary<Node, List<Node>> _Av = null;
        public Dictionary<Node, List<Node>> AdjacencyVertices() { 
            if ( _Av != null ) { return _Av; }

            _Av = new Dictionary<Node, List<Node>>();
            foreach (Node v in Vertices) 
            {
                List<Node> adjacentNodes = new List<Node>();
                foreach (Edge e in Edges) 
                { 
                    if ( e.Begin.Equals(v) ) 
                    {
                        adjacentNodes.Add(e.End);
                    }
                }
                _Av[v] = adjacentNodes;
            }
            return _Av;
        }

        public string GetGraphDescriptionAsJson() {
            GraphJson gj = new GraphJson(Edges, Vertices, Weight, IsMSP, IsDirectedGraph);
            string serialized = System.Text.Json.JsonSerializer.Serialize(gj);
            return serialized;
        }

        public Edge GetEdgeBetween(Node u, Node r, bool directionMatters=true)
        {
            foreach (Edge e in EdgesFromNode[u])
            {
                if (e.End.Equals(r))
                {
                    return e;
                }
            }

            if ( !directionMatters ) 
            {
                foreach (Edge e in EdgesFromNode[r])
                {
                    if (e.End.Equals(u))
                    {
                        return e;
                    }
                }
            }

            return null;
        }
    }

    internal class GraphJson { 
    
        public GraphJson(List<Edge> E, List<Node> V, int weight, bool isMsp, bool isDirected ) 
        {
            Vertices = V;
            Weight = weight;
            IsMSP = isMsp;
            IsDirected = isDirected;
            if (!isDirected)
            {
                Edges = removeDoubleEdges(E);
            }
            else {
                Edges = E;
            }
        }

        private List<Edge> removeDoubleEdges(List<Edge> E)
        {
            List<Edge> onewayEdges = new List<Edge>();
            foreach( Edge e in E)
            {
                Edge otherWay = e.GetOtherWay();
                if (onewayEdges.Contains(otherWay)) 
                {
                    continue;
                }
                onewayEdges.Add(e);
            }
            return onewayEdges;
        }

        public bool IsMSP { get; set; }
        public int Weight { get; set; }
        public bool IsDirected { get; set; }
        public List<Edge> Edges { get; set; }
        public List<Node> Vertices { get; set; }
    }
}
