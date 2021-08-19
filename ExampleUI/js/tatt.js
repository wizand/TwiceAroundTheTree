var edgesList = [];
var serverHost = "https://localhost:44324/api";
var apiUrlBuildFromEdges =  'https://localhost:44324/api/GraphBuild/FromEdges';
var apuUrlGetGraphWithId = 'https://localhost:44324/api/GraphBuild/';
var latestGraphGuid = "";
var latestErrors = "";
var graphObject;

function updateServerHost(e) 
{
  serverHost = document.getElementById("serverHost").value;
  apiUrlBuildFromEdges = serverHost += "/GraphBuild/FromEdges";
  apuUrlGetGraphWithId = serverHost +"/GraphBuild/";
}


function addToEdgeListFromInputs() {
  var begin = document.getElementById("edgeInsertBeginNodeTxt").value;
  var weight = document.getElementById("edgeInsertWeightTxt").value;
  var end = document.getElementById("edgeInsertEndNodeTxt").value;
  var combined = "" + begin+ "-" + weight + "-" + end;
  addToEdgeList(combined);
}

function addToEdgeList(edgeString) {
    if ( edgesList.includes(edgeString))
      return;
   
    edgesList.push(edgeString);
    
    updateEdgeString();
    updateEdgesDd();
}



function addExampleGraphToEdgeList() {
  clearEdgeList();
  var tmp = ["A-2-B","A-4-D",
              "B-2-A", "B-4-C",
              "C-4-B", "C-3-D", "C-2-E",
              "D-4-A", "D-3-C", "D-3-E",
              "E-2-C","E-3-D"];
  tmp.forEach(item => {addToEdgeList(item)});

            
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

function clearEdgeList() {
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

function updateLatestGraphGuid(newGuid) 
{
    latestGraphGuid = newGuid;
    document.getElementById("txtLatestGuid").value = latestGraphGuid;
}
function updateLatestErrors(error) 
{
    latestErrors = error;
    document.getElementById("txtLatestErrors").value = latestErrors;
}

function getEdgeParametersAsJson() 
{
  var edgesJson = {};
  edgesJson.edgesStrings = [];
  edgesList.forEach(edgeString => {edgesJson.edgesStrings.push(edgeString)});
  return JSON.stringify(edgesJson);
}

const putGraphFromEdgesFetch = async () => {

    const response = await fetch(apiUrlBuildFromEdges, {
      method: 'PUT',
      body: getEdgeParametersAsJson(),
      headers: {
        'Content-Type': 'application/json',
        'accept': 'text/plain'
      }
    });
    
    const myJsonTxt = await response.text(); //extract JSON from the http response
    var resultObject = JSON.parse(myJsonTxt);
    updateLatestGraphGuid(resultObject.guid);
    updateLatestErrors(resultObject.error);
    document.getElementById("putGraphFromEdgesTxt").innerHTML ="Data: " + myJsonTxt;
  
  }

const getGraphWithGuid = async () => {
  const response = await fetch(apuUrlGetGraphWithId + latestGraphGuid, {
    method: 'GET',
    
    headers: {
      'Content-Type': 'application/json',
      'accept': 'text/plain'
    }
  });

  const myJsonTxt = await response.text(); //extract JSON from the http response
  var resultObject = JSON.parse(myJsonTxt);
  graphObject = resultObject;
  document.getElementById("putGraphFromEdgesTxt").innerHTML ="Data: " + myJsonTxt;
}  