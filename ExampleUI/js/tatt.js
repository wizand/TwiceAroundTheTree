var edgesList = [];
var serverHost = "https://localhost:44324/api";
var apiUrlBuildFromEdges =  'https://localhost:44324/api/GraphBuild/FromEdges';


function updateServerHost(e) 
{
  serverHost = document.getElementById("serverHost").value;
  apiUrlBuildFromEdges = serverHost += "/GraphBuild/FromEdges";
}




function addToEdgeList() {
    var begin = document.getElementById("edgeInsertBeginNodeTxt").value;
    var weight = document.getElementById("edgeInsertWeightTxt").value;
    var end = document.getElementById("edgeInsertEndNodeTxt").value;
    var combined = "" + begin+ "-" + weight + "-" + end;
    
    if ( edgesList.includes(combined))
      return;
    
      edgesList.push(combined);
    // document.getElementById("edgeInsertBeginNodeTxt").value = "A";
    // document.getElementById("edgeInsertWeightTxt").value = "1";
    // document.getElementById("edgeInsertEndNodeTxt").value = "B";
    
    updateEdgeString();
    updateEdgesDd();
}

function updateEdgesDd() {

  var ddElement = document.getElementById("edgesDd");
  var optionsHtml =""
  edgesList.forEach(item => {
    optionsHtml += "<li><a class=\"dropdown-item\" href=\"#\">" + item + "</a></li>";
    //optionsHtml += "<option>"+item+"</option>";
  });
  ddElement.innerHTML=optionsHtml;
}

function cleaEdgeList() {
    edgesList = [];
    updateEdgeString();
    updateEdgesDd();
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
    const response = await fetch(apiUrlBuildFromEdges, {
      method: 'PUT',
      body: tmpEdgesString,
      headers: {
        'Content-Type': 'application/json',
        'accept': 'text/plain'
      }
    });
    const myJson = await response.text(); //extract JSON from the http response
    document.getElementById("putGraphFromEdgesTxt").innerHTML ="Data: " +  myJson;
  }