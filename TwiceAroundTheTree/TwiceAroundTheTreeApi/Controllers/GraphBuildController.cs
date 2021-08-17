using GraphComponents;
using GraphDataStorage;
using Microsoft.AspNetCore.Mvc;
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
        //TODO: Remove this eventually. It is easier to test with query parameters tho so its left here for convinience
        //public string Put([FromQuery] GraphFromEdges graphParameters)
        public string Put([FromBody] GraphFromEdges graphParameters)
        {
            bool ok = graphParameters.ParseEdgesFromEdgeStrings();
            if ( !ok )
            {
                return graphParameters.GetErrorMessage() + "\n Please refer to <a href=\"https://localhost:44324/api/GraphBuild/HowToUse\">https://localhost:44324/api/GraphBuild/HowToUse</a>";
            }
            Graph graphFromEdges = new Graph(graphParameters.GetEdges(), graphParameters.GetVerticeNames());
            Guid storedId = DataCache.Instance.StoreGraph(graphFromEdges);
            return graphParameters.ToString() + "\nGUID FOR GRAPH: " + storedId.ToString();
        }

        // POST api/<GraphBuildController>
        [HttpPut]
        [Route("FromMatrix")]
        public string Put([FromBody] GraphFromAdjacencyMatrix graphParameters)
        {
            bool ok = graphParameters.parseMAtrixFromRowStrings();
            if (!ok) {
                return graphParameters.GetErrorMessage() + "\n Please refer to <a href=\"https://localhost:44324/api/GraphBuild/HowToUse\">https://localhost:44324/api/GraphBuild/HowToUse</a>";
            }

            Graph graphFromMatrix = new Graph(graphParameters.GetMatrix());
            Guid storedId = DataCache.Instance.StoreGraph(graphFromMatrix);
            return graphParameters.ToString() + "\nGUID FOR GRAPH: " + storedId.ToString();
        }

        // PUT api/<GraphBuildController>/AAAAAAAA-BBBB-cccc-DDDD-EEEEEEEEEEEE
        [HttpGet("{graphId}")]
        public Graph Get(Guid graphId)
        {
            Graph g = DataCache.Instance.GetGraphFromStore(graphId);
          
            return g;
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
