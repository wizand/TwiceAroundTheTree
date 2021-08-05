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
        [Microsoft.AspNetCore.Mvc.Route("HowToUse")]
        public string Get()
        {
            return new HowToUse().HowToUseDescription;
        }

        // GET: api/<GraphBuildController>
        [HttpGet]
        public string Get([FromQuery] GraphFromEdges graphParameters)
        {

           return "https://localhost:44324/api/GraphBuild/HowToUse";
            //return graphParameters.ToString();
        }

        // POST api/<GraphBuildController>
        [HttpPost]
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
