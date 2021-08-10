namespace GraphComponents {

    public class Node {
        public Node(string name) {
            Name = name;
        }
        public string Name {get; set;}

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Name.Equals(((Node)obj).Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString() {
            return Name;
        }
    }
}