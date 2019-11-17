using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebhookCatcher.Models
{
    public class CreateControllerModel
    {
        public int StatusCode { get; set; }
        public string ControllerName { get; set; }
        public string ResponseBody { get; set; }

        public CreateControllerModel Deserialize(string content) => JsonConvert.DeserializeObject<CreateControllerModel>(content);
    }

}
