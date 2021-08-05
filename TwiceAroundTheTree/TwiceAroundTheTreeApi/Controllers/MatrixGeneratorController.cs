using Microsoft.AspNetCore.Mvc;
using TwiceAroundTheTreeApi.ControllerModels;

namespace TwiceAroundTheTreeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatrixGeneratorController : ControllerBase {

        [HttpGet]
        public string Get() {

            return "Hello";
        }

        [HttpPost]
        public string GetFromGraph(GraphFromEdges edges) {
            return edges.ToString();
        }
        

    }

}