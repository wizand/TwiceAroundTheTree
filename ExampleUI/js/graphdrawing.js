
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
