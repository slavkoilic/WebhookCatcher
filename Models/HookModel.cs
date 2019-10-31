using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebhookCatcher.Models
{
    public class HookModel
    {
        public string WebhookUrl { get; set; }
        public string WebhookBody { get; set; }
        public string ResponseBody { get; set; }
        public HookModel Deserialize(string content) => JsonConvert.DeserializeObject<HookModel>(content);

    }
}
