using System.Text;

namespace TwiceAroundTheTreeApi.ControllerModels
{
  
    public class HowToUse
    {
        public string HowToUseDescription {
            get {

                StringBuilder sb = new StringBuilder("Here's how you use this thing.");
                sb.AppendLine("You can build the graph by either giving the edges or adjacency matrix.");
                sb.AppendLine("After you send the get/post to the controller, you'll get a guid for the graph that was built from the data.");
                sb.AppendLine("The graph can be requested from --- with the Guid.");
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("You can build a set of MSP graphs from the graph with the guid and you'll get anotehr set of guids for the MSP graphs") ;

                return sb.ToString();
                    
            } 
        }
    }
}
