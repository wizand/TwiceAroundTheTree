using Graph;
using System.Collections.Generic;
using System.Text;

namespace TwiceAroundTheTreeApi.ControllerModels
{
    public class GraphFromAdjacencyMatrix {

        private const string TOKEN_DELIMITER = ",";
        public List<string> Vertices { get; set; }
        public List<string> RowsAsStrings { get; set; }
        private int[][] rowsAsIntArrays { get; set; }

        private string errorMessage { get; set; } = "All good.";
        private List<Node> parsedNodes = new List<Node>();
        private Matrix parsedAdjacencyMatrix;
        public bool parseMAtrixFromRowStrings() {
            int len = RowsAsStrings.Count;

            if (Vertices == null || Vertices.Count != len)
            {
                Vertices = new List<string>();
            }



            foreach(string row in RowsAsStrings)
            {
                string[] tokens = row.Split(TOKEN_DELIMITER);
                if (tokens.Length != len) {
                    errorMessage = "Matrix has to be square. Now there are " + len + " rows and apparently " + tokens.Length + "columns in the row " + row + ".";
                    return false;
                }
            }

            rowsAsIntArrays = new int[RowsAsStrings.Count][];
            //TODO: Start parsing the cells and build the matrix from the values

                return true;
        }


        public override string ToString()
        {
            /* int index = 0;
             if (Vertices == null || Vertices.Count < RowsAsStrings.Count) {
                 Vertices = "";
                 foreach ( var row in RowsAsStrings)
                 {
                     Vertices = Vertices + (char)('A' + index) + ",";
                     index += 1;
                 }
             }
             string[] VerticeNames = Vertices.Split(",");
             index = 0;
             StringBuilder sb = new StringBuilder();
             foreach ( var row in RowsAsStrings)
             {
                 sb.Append(VerticeNames[index] + ": " + row + "\n");
                 index += 1;
             }
             return sb.ToString();
            */
            return "TODO";
        }

    }
}
