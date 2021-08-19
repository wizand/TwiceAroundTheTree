
/**
 * This example shows the available edge label renderers for the canvas
 * renderer.
 */
var i,
    s,
    N = 10,
    E = 30,
    g = {
      nodes: [],
      edges: []
    };

// Generate a random graph:
for (i = 0; i < N; i++)
  g.nodes.push({
    id: 'n' + i,
    label: 'Node ' + i,
    x: Math.random(),
    y: Math.random(),
    size: Math.random(),
    color: '#666'
  });

for (i = 0; i < E; i++)
  g.edges.push({
    id: 'e' + i,
    label: 'Edge ' + i,
    source: 'n' + (Math.random() * N | 0),
    target: 'n' + (Math.random() * N | 0),
    size: Math.random(),
    color: '#ccc',
    type: ['line', 'curve', 'arrow', 'curvedArrow'][Math.random() * 4 | 0]
  });

// Instantiate sigma:
s = new sigma({
  graph: g,
  renderer: {
    container: document.getElementById('graph-container'),
    type: 'canvas'
  },
  settings: {
    edgeLabelSize: 'propotional',
    
    edgeLabelThreshold: 0
  }
});

//var s = new sigma();

function drawGraph() {
  s.graph.clear();
  var nodeName = "";
  var beginName = "";
  var endName = "";
  var weight = 0;
  var index = 0;
 // g.edges = [];
 // g.nodes = [];
  graphObject.Vertices.forEach(nodeElement => {
    nodeName = nodeElement.Name;
    s.graph.addNode({id: nodeName, label: nodeName, 
      x: Math.random(),
      y: Math.random(), 
      size: 1,
    color: '#666'});
  });
    graphObject.Edges.forEach(edgeElement => {
      beginName = edgeElement.Begin.Name;
      endName = edgeElement.End.Name;
      weight = edgeElement.Weight;
      s.graph.addEdge({
        id: 'e'+index, label: 'w: ' + weight, 
        source: beginName, 
        target: endName, 
        size: 1, color: '#ccc', type: 'curve'})
      index += 1;
    });
    
   // s.graph = g;
    s.refresh();
    s.startForceAtlas2({ gravity: 0.5});
    window.setTimeout(function() {s.killForceAtlas2()}, 3000);

   /* s = new sigma({
      graph: g,
      renderer: {
        container: document.getElementById('graph-container'),
        type: 'canvas'
      },
      settings: {
        edgeLabelSize: 'propotional',
        
        edgeLabelThreshold: 0
      }
    });*/
}