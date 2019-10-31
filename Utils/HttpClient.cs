using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebhookCatcher.Utils
{
    public class HttpClient
    {
        public void PostRequest(string url, string body)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(body);
            client.ExecuteTaskAsync(request);
        }
    }
}
