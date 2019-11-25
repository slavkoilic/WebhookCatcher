using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebhookCatcher.Models
{
    public class CreateControllerModel
    {
        public int StatusCode { get; set; }
        public string ControllerName { get; set; }
        public JObject ResponseBody { get; set; }

        public CreateControllerModel Deserialize(string content) => JsonConvert.DeserializeObject<CreateControllerModel>(content);
    }

    
}
