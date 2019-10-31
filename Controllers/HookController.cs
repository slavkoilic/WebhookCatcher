using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        public IActionResult PostHook()
        {

            StreamReader reader = new StreamReader(Request.Body);
            string body = reader.ReadToEnd();
            
            hookModel = hookModel.Deserialize(body);
            client.PostRequest(hookModel.WebhookUrl, hookModel.WebhookBody);
            

            return StatusCode(200, hookModel.ResponseBody);

        }
        
        
        [HttpPost("{code}")]
        public IActionResult PostHookWithCode(int code)
        {
            
            StreamReader reader = new StreamReader(Request.Body);
            string body = reader.ReadToEnd();

            hookModel = hookModel.Deserialize(body);
            client.PostRequest(hookModel.WebhookUrl, hookModel.WebhookBody);

            return StatusCode(code, hookModel.ResponseBody);

        }


    }
}
