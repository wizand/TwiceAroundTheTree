using System.Collections.Generic;
using Matrix;
namespace TwiceAroundTheTreeApi.ControllerModels
{
    public class GraphFromEdges
    {

        public string VerticeNames { get; set; }
        public string EdgesString { get; set; }

        public override string ToString()
        {

            return "VerticeNames=" + VerticeNames + " EdgeString=" + EdgesString;
        }

    }
}