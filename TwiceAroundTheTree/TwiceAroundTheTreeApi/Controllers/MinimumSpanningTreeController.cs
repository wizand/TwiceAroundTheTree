using GraphComponents;
using GraphComponents.Algorithms;
using GraphDataStorage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TwiceAroundTheTreeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinimumSpanningTreeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(Guid graphId)
        {

            Graph graph = DataCache.Instance.GetGraphFromStore(graphId);

            if (graph == null)
            {
                return BadRequest("No graph for id " + graphId);
            }
            
            if ( graph.IsDirectedGraph )
            {
                return BadRequest("Cannot perform MSP search for directed graph.");
            }

            PrimsAlgorithm pa;
            for (int i = 0; i < graph.Vertices.Count; i++)
            {
                pa = new PrimsAlgorithm(graph, i);
                pa.FindMsp();
            }

            KruskalsAlgorithm ka = new KruskalsAlgorithm(graph);
            ka.FindMsp();

            string combined = "[";
            foreach (Graph mspGraph in graph.MSPGraphs) 
            {
                combined += mspGraph.GetGraphDescriptionAsJson() + ",";
            }
            //Rempove the last comma and end json array
            combined = combined.Substring(0, combined.Length-1) +"]";
            
            return Ok(combined);
        }
    }
}
