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
            if ( graph.IsDirectedGraph )
            {
                return BadRequest("Cannot perform MSP search for directed graph.");
                
            }
            
            PrimsAlgorithm pa = new PrimsAlgorithm(graph);
            pa.FindMspStartingFromNode(graph.Vertices[0]);
            var MSPGraph = graph.MSPGraphs[0];
            string MSPGraphJson = MSPGraph.GetGraphDescriptionAsJson();
            return Ok(MSPGraphJson);
        }
    }
}
