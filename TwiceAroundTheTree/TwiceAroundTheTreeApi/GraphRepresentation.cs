using System.Collections.Generic;
using Matrix;

public class GraphRepresentation {

    List<Edge> Edges {get; set;}

    public override string ToString() {
        string s = "";
        foreach ( var e in Edges ) {
            s += e.ToString() + " ";
        }
        return s;

    }

}