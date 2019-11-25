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
    public class HookController : ControllerBase
    {

        RequestToFile request = new RequestToFile();
        HookModel hookModel = new HookModel();
        HttpClient client = new HttpClient();




        [HttpPost]
        public async Task<IActionResult> PostHookAsync()
        {

            StreamReader reader = new StreamReader(Request.Body);
            string body = await reader.ReadToEndAsync();
            
            hookModel = hookModel.Deserialize(body);
            client.PostRequest(hookModel.WebhookUrl, hookModel.WebhookBody);
            

            return StatusCode(200, hookModel.ResponseBody);

        }
        
        
        [HttpPost("{code}")]
        public async Task<IActionResult> PostHookWithCodeAsync(int code)
        {
            
            StreamReader reader = new StreamReader(Request.Body);
            string body = await reader.ReadToEndAsync();

            hookModel = hookModel.Deserialize(body);
            client.PostRequest(hookModel.WebhookUrl, hookModel.WebhookBody);

            return StatusCode(code, hookModel.ResponseBody);

        }


    }
}
