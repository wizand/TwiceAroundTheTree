using System.Text.Json.Serialization;

namespace GraphComponents {
    public class Edge {
        private const int DEFAULT_WEIGHT = 1;

        public Edge(Node begin, Node end, int weight = DEFAULT_WEIGHT, bool isDirected = false) {
            Begin = begin; 
            End = end;
            Weight = weight;
            IsDirected = isDirected;
        }

        public Node Begin { get; set; }
        public Node End { get; set; }
        public int Weight {get; set; } = 1;
        [JsonIgnore]
        public bool IsDirected {get;set;} = false;



        public Edge GetOtherWay() {
            return new Edge(End, Begin, Weight, IsDirected);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            Edge other = (Edge)obj;
            
            if ( Begin.Equals(other.Begin) && End.Equals(other.End) && Weight == other.Weight ) 
            {
                return true;
            } 
            else 
            {
                return false;
            }
     
        }
        

        public override int GetHashCode()
        {
                string combined = Begin.ToString() + End.ToString() + Weight.ToString();
                return combined.GetHashCode();
        }

        public override string ToString() {
           return "<" + Begin + "-" + Weight + "-" + End + ">";
        }

    }
}