using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using WebhookCatcher.Models;
using WebhookCatcher.Utils;

namespace WebhookCatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatchController : ControllerBase
    {

        RequestToFile request = new RequestToFile();
        CatchRequestModel catchRequest = new CatchRequestModel();

                
        [AcceptVerbs("GET", "POST", "PUT","PATCH","DELETE")]
        [Route("{code}/{**catchAll}")]
        public async Task<IActionResult> PostToCodeAsync(int code, string catchAll)
        {
            if (catchAll != null)
            {
                catchAll = catchAll.Replace('/', '-');
            }
            
            try
            {
                var headerDictionary = new Dictionary<string, StringValues>(Request.Headers);
                var queryDictionary = new Dictionary<string, StringValues>(Request.Query);
                catchRequest.RequestPath = Request.Path;
                catchRequest.Query = JsonConvert.SerializeObject(queryDictionary, Formatting.Indented);
                catchRequest.Headers = JsonConvert.SerializeObject(headerDictionary, Formatting.Indented);
                

                string csv = "{" +
                             Environment.NewLine +
                             "\"RequestPath\" : " + catchRequest.RequestPath + "," +
                             Environment.NewLine +
                             "\"Query\" : " + catchRequest.Query + "," +
                             Environment.NewLine +
                             "\"Headers\" : ";

                csv += catchRequest.Headers; //JsonConvert.SerializeObject(headerDictionary, Formatting.Indented) + "," + Environment.NewLine;
                
                StreamReader reader = new StreamReader(Request.Body);
                string body = await reader.ReadToEndAsync();

                body = "\"Body\" : " + body + Environment.NewLine + "}";

                string response = csv + body;

                request.ToFile(response, code + "_" + catchAll);

                return StatusCode(code, response);
            }
            catch (Exception e)
            {
                request.ToFile(e.Message, code + "_" + catchAll);
                return StatusCode(500, e.Message);
            }
        }
    }
}
