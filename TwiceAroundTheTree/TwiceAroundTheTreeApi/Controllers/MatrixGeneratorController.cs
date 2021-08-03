using Microsoft.AspNetCore.Mvc;

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
        public string GetFromGraph(GraphRepresentation edges) {
            return edges.ToString();
        }
        

    }

}