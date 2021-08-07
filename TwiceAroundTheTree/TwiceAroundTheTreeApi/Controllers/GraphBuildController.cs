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
        [HttpGet]
        [Route("FromEdges")]
        public string Get([FromQuery] GraphFromEdges graphParameters)
        {
            bool ok = graphParameters.ParseEdgesFromEdgeStrings();
            if ( !ok )
            {
                return graphParameters.GetErrorMessage() + "\n Please refer to <a href=\"https://localhost:44324/api/GraphBuild/HowToUse\">https://localhost:44324/api/GraphBuild/HowToUse</a>";
            }

            return graphParameters.ToString();
        }

        // POST api/<GraphBuildController>
        [HttpPost]
        [Route("FromMatrix")]
        public string Post([FromBody] GraphFromAdjacencyMatrix garphParameters)
        {
            return garphParameters.ToString();
        }

        // PUT api/<GraphBuildController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GraphBuildController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
