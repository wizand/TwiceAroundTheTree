﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Graph
    {
        public List<Edge> Edges { get; set; } = new();
        private Dictionary<Node, IList<Edge>> edgesFromNode = new();
        public List<Node> Vertices { get; set; }
        public Matrix AdjacencyMatrix { get; set; }
        private bool isDirected = false;

        public Graph(Matrix adjacencyMatrix) {
            AdjacencyMatrix = adjacencyMatrix;
            Vertices = AdjacencyMatrix.Vertices;
            buildEdgesFromMatrix();
            isDirected = IsGraphDirected();
        }

     
        public Graph(List<Edge> edges, List<Node> vertices) {
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
                foreach (Node node in edgesFromNode.Keys) //Go trough all the edges there are
                {
                    foreach (Edge edge in edgesFromNode[node])  
                    {
                        IList<Edge> edgesForTheEndNode;
                        if (edgesFromNode.TryGetValue(edge.End, out edgesForTheEndNode)) //If the graph is not directed, there should be edges starting from the end node.
                        {
                            bool found = false;
                            foreach (Edge endNodeEdge in edgesForTheEndNode)
                            {
                                if (endNodeEdge.Equals(edge))
                                {
                                    found = true;
                                }
                                if (!found)
                                {
                                    //No edge fromthe end node to begin node so the graph is directed
                                    return true;
                                }
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
            if ( edgesFromNode.ContainsKey(node) )
            { 
                edges = edgesFromNode[node];
            } 
            else
            {
                edges = new List<Edge>();
            }
            
            if (!edges.Contains(e))
            {
                edges.Add(e);
            }

            edgesFromNode[node] = edges;
        }
    }
}
