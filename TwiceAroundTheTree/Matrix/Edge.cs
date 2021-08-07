namespace Graph {

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
            if ( IsDirected ) 
            {
                if ( Begin == other.Begin && End == other.End && Weight == other.Weight ) 
                {
                    return true;
                } 
                else 
                {
                    return false;
                }
            } 
            else 
            {
                if ( (Begin == other.Begin || Begin == other.End) && (End == other.Begin || End == other.End) &&  Weight == other.Weight) 
                {
                    return true;
                } 
                else 
                {
                    return false;
                }
            }
        }
        

        public override int GetHashCode()
        {
            if ( IsDirected ) 
            {
                string combined = Begin.ToString() + End.ToString() + Weight.ToString() + IsDirected.ToString();
                return combined.GetHashCode();
            } else {
                return Begin.GetHashCode() + End.GetHashCode() + Weight;
            }
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