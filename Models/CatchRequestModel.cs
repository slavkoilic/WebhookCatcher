using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace WebhookCatcher.Models
{
    public class CatchRequestModel
    {
        public string RequestPath { get; set; }
        public string Query { get; set; }
        public string Headers { get; set; }
        public string ResponseBody { get; set; }
        public CatchRequestModel Deserialize(string content) => JsonConvert.DeserializeObject<CatchRequestModel>(content);

    }
}
