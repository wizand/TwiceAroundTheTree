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