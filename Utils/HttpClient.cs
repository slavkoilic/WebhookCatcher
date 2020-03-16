using RestSharp;

namespace WebhookCatcher.Utils
{
    public class HttpClient
    {
        public void PostRequest(string url, string body)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(body);
            client.ExecuteAsync(request);
        }
    }
}
