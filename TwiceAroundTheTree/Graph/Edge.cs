namespace GraphComponents {
    public class Edge {
        private const int NOT_WEIGHTED = -9999;

        public Edge(Node begin, Node end, int weight = NOT_WEIGHTED, bool isDirected = false) {
            Begin = begin; 
            End = end;
            Weight = weight;
            IsDirected = isDirected;
        }

        public Node Begin { get; set; }
        public Node End { get; set; }
        public int Weight {get; set; } = -9999;
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
            string weightString = "";
            if ( Weight != NOT_WEIGHTED) 
            {
                weightString = Weight +"-";
            } 
           return "<" + Begin + "-" + weightString + End + ">";
        }

    }
}