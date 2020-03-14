using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using WebhookCatcher.Utils;

namespace WebhookCatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EchoController : ControllerBase
    {

        RequestToFile request = new RequestToFile();


        [AcceptVerbs("GET", "POST", "PUT", "PATCH", "DELETE")]
        public async Task<IActionResult> GetEchoAsync()
        {          

            StreamReader reader = new StreamReader(Request.Body);
            string body = await reader.ReadToEndAsync();            

            string response =body;

            return StatusCode(200, response);

        }

               
        [AcceptVerbs("GET", "POST", "PUT", "PATCH", "DELETE")]
        [Route("{code}")]
        public async Task<IActionResult> GetEchoWithCodeAsync(int code)
        {

            StreamReader reader = new StreamReader(Request.Body);
            string body = await reader.ReadToEndAsync();

            string response = body;

            return StatusCode(code, response);

        }
        

    }
}
