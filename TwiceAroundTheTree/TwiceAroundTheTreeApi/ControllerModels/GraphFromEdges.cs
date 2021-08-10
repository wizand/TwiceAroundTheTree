using GraphComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwiceAroundTheTreeApi.ControllerModels
{
    public class GraphFromEdges
    {
        public List<string> EdgesStrings { get; set; }
        private List<Node> parsedNodes { get; set; }
        private string errorMessage { get; set; } = "All good.";
        private List<Edge> parsedEdges;
        private const string TOKEN_DELIMITER = "-";

        public bool ParseEdgesFromEdgeStrings() {
            if (EdgesStrings == null || EdgesStrings.Count == 0) {
                errorMessage = "No edge strings to parse!";
                return false;
            }

            parsedEdges= new List<Edge>();
            parsedNodes = new List<Node>();

            try
            {
                foreach (string edgeString in EdgesStrings) 
                {
                    string[] tokens = edgeString.Trim().Split(TOKEN_DELIMITER);
                    Node begin, end;
                    bool hasWeight = false;
                    int weight = 0;

                    //No weights
                    if (tokens.Length == 2)
                    {
                        begin = new Node(tokens[0]);
                        end = new Node(tokens[1]);
                    //Weight included
                    } else if (tokens.Length == 3) {
                        hasWeight = true;
                        begin = new Node(tokens[0]);
                        end = new Node(tokens[2]);

                        addToNodesList(begin, end);

                        weight = int.Parse(tokens[1]);
                    //Wrong number of parsed tokens.
                    } else {
                        errorMessage = "Wrong number of tokens in edgeString " + edgeString + ". tokens found " + tokens.Length + " when there should be either 2" +
                            " for non weighted edge or 3 for weighted. Make sure the edge strings are in form or 'Verticename"+TOKEN_DELIMITER+ "optionalWeight" + TOKEN_DELIMITER + "Verticename'.";
                        return false;
                    }

                    Edge e;
                    if (hasWeight)
                        e = new(begin, end, weight);
                    else
                        e = new(begin, end);
                
                    parsedEdges.Add(e);
                }
            } 
            catch( Exception e )
            {
                errorMessage = "Exception while parsing edges: " + e.Message;
                return false;
            }
 
            return true;
        }

        internal List<Node> GetVerticeNames()
        {
            return parsedNodes;
        }

        private void addToNodesList(params Node[] nodesToAdd )
        {
            foreach (Node nodeToAdd in nodesToAdd) {
                if (parsedNodes.Contains(nodeToAdd)) {
                    continue;
                }
                parsedNodes.Add(nodeToAdd);
            }
        }

        public string GetErrorMessage() {
            return errorMessage;
        }
        public List<Edge> GetEdges() {
            return parsedEdges;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Source edge strings: ");
            foreach (string es in EdgesStrings)
            {
                sb.AppendLine(es.ToString());
            }
            sb.AppendLine("Vertices: ");
            foreach (Node n in parsedNodes)
            {
                sb.AppendLine(n.ToString());
            }

            sb.AppendLine("Created edges: ");
            foreach (Edge e in parsedEdges) 
            {
                sb.AppendLine(e.ToString());
            }
            sb.AppendLine("Info message: " + errorMessage);

            return sb.ToString();
        }

    }
}