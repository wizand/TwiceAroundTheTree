using GraphComponents;
using GraphDataStorage;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using TwiceAroundTheTreeApi.ControllerModels;

namespace TwiceAroundTheTreeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphBuildController : ControllerBase
    {
        [HttpGet]
        [Route("HowToUse")]
        public string Get()
        {
            return new HowToUse().HowToUseDescription;
        }

        // GET: api/<GraphBuildController>
        [HttpPut]
        [Route("FromEdges")]
        [SwaggerOperation(
            Summary = "Build a graph using edges representation",
            Description = "Uses the given list of string representing the graph edges to build an Graph and store it in the data store. Returns a guid that can be later used to fetch the graph.",
            OperationId = "CreateGraphFromEdges",
            Tags = new[] { "Build graph" }
        )]
        //TODO: Remove this eventually. It is easier to test with query parameters tho so its left here for convinience
        //public string Put([FromQuery] GraphFromEdges graphParameters)
        public IActionResult Put([FromBody] GraphFromEdges graphParameters)
        {
            bool ok = graphParameters.ParseEdgesFromEdgeStrings();
            if ( !ok )
            {
                return BadRequest(new { error = graphParameters.GetErrorMessage() + "\n Please refer to <a href=\"https://localhost:44324/api/GraphBuild/HowToUse\">https://localhost:44324/api/GraphBuild/HowToUse</a>" });
            }
            Graph graphFromEdges = new Graph(graphParameters.GetEdges(), graphParameters.GetVerticeNames());
            Guid storedId = DataCache.Instance.StoreGraph(graphFromEdges);
            return Ok(new { GUID = storedId.ToString(), guidMessage = "GUID FOR GRAPH: [GUID]" + storedId.ToString() + "[/ GUID]", error = graphParameters.GetErrorMessage(), graphParameters = graphParameters.ToString() });
        }

        // POST api/<GraphBuildController>
        [HttpPut]
        [Route("FromMatrix")]
        [SwaggerOperation(
            Summary = "Build a graph using matrix representation",
            Description = "Uses the given list of string representing the graph as adjacency matrix to create an Graph and store it in the data store. Returns a guid that can be later used to fetch the graph.",
            OperationId = "CreateGraphFromMatrix",
            Tags = new[] { "Build graph" }
        )]
        public IActionResult Put([FromBody] GraphFromAdjacencyMatrix graphParameters)
        {
            bool ok = graphParameters.parseMAtrixFromRowStrings();
            if (!ok) {
                return BadRequest(new { error = graphParameters.GetErrorMessage() + "\n Please refer to <a href=\"https://localhost:44324/api/GraphBuild/HowToUse\">https://localhost:44324/api/GraphBuild/HowToUse</a>" });
            }

            Graph graphFromMatrix = new Graph(graphParameters.GetMatrix());
            Guid storedId = DataCache.Instance.StoreGraph(graphFromMatrix);
            return Ok(new { GUID = storedId.ToString(), guidMessage = "GUID FOR GRAPH: [GUID]" + storedId.ToString() + "[/ GUID]", error = graphParameters.GetErrorMessage(), graphParameters = graphParameters.ToString() });
        }

        // PUT api/<GraphBuildController>/AAAAAAAA-BBBB-cccc-DDDD-EEEEEEEEEEEE
        [HttpGet("{graphId}")]
        public IActionResult Get(Guid graphId)
        {
            Graph g = DataCache.Instance.GetGraphFromStore(graphId);
            if (g == null) {
                return BadRequest(new { error = "No graph for id=[" + graphId + "]" });
            }
            string graphAsJson = g.GetGraphDescriptionAsJson();
            return Ok(graphAsJson);
        }

        // DELETE api/<GraphBuildController>/AAAAAAAA-BBBB-cccc-DDDD-EEEEEEEEEEEE
        [HttpDelete("{graphId}")]
        public string Delete(Guid graphId)
        {
            bool removeHappened = DataCache.Instance.RemoveFromStore(graphId);
            return "Removed graphId=["+graphId+"]:" + removeHappened;
        }
    }
}
