var edgesList = [];
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


var apiUrl = 'https://localhost:44324/api/GraphBuild/FromEdges';
function putGraphFromEdges() 
{

    var Httpreq = new XMLHttpRequest();
    Httpreq.open("GET", apiUrl, false);
    
    Httpreq.send(null);
    return Httpreq.responseText;
}

const putGraphFromEdgesFetch = async () => {
    const response = await fetch('https://localhost:44324/api/GraphBuild/FromEdges', {
      method: 'PUT',
      body: "sdfs", // string or object
      headers: {
        'Content-Type': 'application/json'
      }
    });
    const myJson = await response.json(); //extract JSON from the http response
    document.getElementById("putGraphFromEdgesTxt").innerHTML ="Data: " +  myJson;
  }