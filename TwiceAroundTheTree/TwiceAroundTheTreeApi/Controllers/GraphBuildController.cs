using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public string Put([FromQuery] GraphFromEdges graphParameters)
        {
            bool ok = graphParameters.ParseEdgesFromEdgeStrings();
            if ( !ok )
            {
                return graphParameters.GetErrorMessage() + "\n Please refer to <a href=\"https://localhost:44324/api/GraphBuild/HowToUse\">https://localhost:44324/api/GraphBuild/HowToUse</a>";
            }

            return graphParameters.ToString();
        }

        // POST api/<GraphBuildController>
        [HttpPut]
        [Route("FromMatrix")]
        public string Put([FromBody] GraphFromAdjacencyMatrix garphParameters)
        {
            return garphParameters.ToString();
        }

        // PUT api/<GraphBuildController>/5
        [HttpGet("{id}")]
        public Graph.Graph Get(Guid graphId)
        {
            return new Graph.Graph();
        }

        // DELETE api/<GraphBuildController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
