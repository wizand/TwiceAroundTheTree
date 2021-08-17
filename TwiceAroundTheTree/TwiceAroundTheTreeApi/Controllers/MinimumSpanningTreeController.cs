using GraphComponents;
using GraphComponents.Algorithms;
using GraphDataStorage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwiceAroundTheTreeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinimumSpanningTreeController : ControllerBase
    {
        [HttpGet]
        public Graph Get(Guid graphId)
        {

            Graph graph = DataCache.Instance.GetGraphFromStore(graphId);
            PrimsAlgorithm pa = new PrimsAlgorithm(graph);
            pa.FindMspStartingFromNode(graph.Vertices[0]);

            return graph;
        }
    }
}
