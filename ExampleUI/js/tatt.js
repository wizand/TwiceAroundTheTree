var edgesList = [];
const apiUrlBuildFromEdges = 'https://localhost:44324/api/GraphBuild/FromEdges';

function addToEdgeList() {
    var begin = document.getElementById("edgeInsertBeginNodeTxt").value;
    var weight = document.getElementById("edgeInsertWeightTxt").value;
    var end = document.getElementById("edgeInsertEndNodeTxt").value;
    var combined = "" + begin+ "-" + weight + "-" + end;
    edgesList.push(combined);
    document.getElementById("edgeInsertBeginNodeTxt").value = "A";
    document.getElementById("edgeInsertWeightTxt").value = "1";
    document.getElementById("edgeInsertEndNodeTxt").value = "B";
    
    updateEdgeString();
}

function cleaEdgeList() {
    edgesList = [];
    updateEdgeString();
}

function updateEdgeString() {
    var concated = ""
    edgesList.forEach(element => {
        concated += " " + element;
    });
    document.getElementById("edgesString").innerHTML = concated;
}






var tmpEdgesString = '{\"edgesStrings\": [\"a-1-b\",\"b-2-c\" ] }';

const putGraphFromEdgesFetch = async () => {
    const response = await fetch('https://localhost:44324/api/GraphBuild/FromEdges', {
      method: 'PUT',
      body: tmpEdgesString, // string or object
      headers: {
        'Content-Type': 'application/json',
        'accept': 'text/plain'
      }
    });
    const myJson = await response.text(); //extract JSON from the http response
    document.getElementById("putGraphFromEdgesTxt").innerHTML ="Data: " +  myJson;
  }