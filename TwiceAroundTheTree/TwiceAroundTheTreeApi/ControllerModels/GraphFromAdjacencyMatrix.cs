using System.Collections.Generic;
using System.Text;

namespace TwiceAroundTheTreeApi.ControllerModels
{
    public class GraphFromAdjacencyMatrix {

        public string Vertices { get; set; }
        public List<string> Rows { get; set; }


        public override string ToString()
        {
            int index = 0;
            if (Vertices == null || Vertices.Length < Rows.Count) {
                Vertices = "";
                foreach ( var row in Rows )
                {
                    Vertices = Vertices + (char)('A' + index) + ",";
                    index += 1;
                }
            }
            string[] VerticeNames = Vertices.Split(",");
            index = 0;
            StringBuilder sb = new StringBuilder();
            foreach ( var row in Rows)
            {
                sb.Append(VerticeNames[index] + ": " + row + "\n");
                index += 1;
            }
            return sb.ToString();
        }

    }
}
