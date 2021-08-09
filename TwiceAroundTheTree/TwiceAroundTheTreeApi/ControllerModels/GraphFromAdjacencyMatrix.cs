using Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwiceAroundTheTreeApi.ControllerModels
{
    public class GraphFromAdjacencyMatrix {

        private const string TOKEN_DELIMITER = ",";
        public List<string> Vertices { get; set; }
        public List<string> RowsAsStrings { get; set; }
        private int[][] rowsAsIntArrays { get; set; }

        private string errorMessage { get; set; } = "No parsing done yet.";
        private List<Node> parsedNodes = new List<Node>();
        private Matrix parsedAdjacencyMatrix;
        private bool parseOk = false;
        public bool parseMAtrixFromRowStrings() {
            try
            {
                parseOk = true;
                int len = RowsAsStrings.Count;

                if (Vertices == null || Vertices.Count != len || !verticeNamesAreUnique())
                {
                    Vertices = new List<string>();
                    for(int index = 0; index < len; index++)
                    {
                        Vertices.Add((char)('A' + index) + "");
                    }
                }

                int y = 0;
                rowsAsIntArrays = new int[len][];
                foreach (string row in RowsAsStrings)
                {
                    string[] tokens = row.Split(TOKEN_DELIMITER);
                    rowsAsIntArrays[y] = new int[len];

                    if (tokens.Length != len)
                    {
                        errorMessage = "Matrix has to be square. Now there are " + len + " rows and apparently " + tokens.Length + " columns in the row " + row + ".";
                        parseOk = false;
                        return parseOk;
                    }

                    for (int x = 0; x < tokens.Length; x++)
                    {
                        int value = int.Parse(tokens[x].Trim());
                        rowsAsIntArrays[y][x] = value;
                    }
                    y = y + 1;
                }
            }
            catch (Exception e) {
                errorMessage = "Something wrong with parsing the matrix: " + e.Message;
                parseOk = false;
                return parseOk;
            }

            parsedAdjacencyMatrix = new Matrix(Vertices, rowsAsIntArrays);

            errorMessage = "All good.";
            return parseOk;
        }

        internal string GetErrorMessage()
        {
            return errorMessage;
        }

        internal Matrix GetMatrix() 
        {
            return parsedAdjacencyMatrix;
        }

        private bool verticeNamesAreUnique() {
            foreach (string v in Vertices) 
            {
                if (Vertices.FindAll(x => x.Equals(v)).Count > 1)
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            if (!parseOk) 
            {
                return errorMessage;
            }
            StringBuilder sb = new StringBuilder();
            int y = 0;
            foreach(int[] row in rowsAsIntArrays)
            {
                sb.Append(Vertices[y] + " [");
                for (int x = 0; x < row.Length; x++) 
                {
                    sb.Append(row[x]);
                    if (x < row.Length - 1)
                        sb.Append(" ");
                }
                sb.Append("]\n");
                y += 1;
            }

            return sb.ToString();

        }

    }
}
