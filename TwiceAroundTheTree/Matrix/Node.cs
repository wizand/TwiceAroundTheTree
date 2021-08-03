namespace Matrix {

    public class Node {
        public Node(string name) {
            Name = name;
        }
        public string Name {get; set;}

        public override string ToString() {
            return Name;
        }
    }
}