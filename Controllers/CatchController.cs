using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using WebhookCatcher.Utils;

namespace WebhookCatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatchController : ControllerBase
    {

        RequestToFile request = new RequestToFile();


        [HttpPost("{code}")]
        public IActionResult PostToCode(int code)
        {
            try
            {
                var headerDictionary = new Dictionary<string, StringValues>(Request.Headers);                

                string csv = "{" + Environment.NewLine + "\"Headers\" : ";

                csv += JsonConvert.SerializeObject(headerDictionary, Formatting.Indented) + "," + Environment.NewLine;
                
                StreamReader reader = new StreamReader(Request.Body);
                string body = reader.ReadToEnd();

                body = "\"Body\" : " + body + Environment.NewLine + "}";

                string response = csv + body;

                request.ToFile(response, code);

                return StatusCode(code, response);
            }
            catch (Exception e)
            {
                request.ToFile(e.Message, code);
                return StatusCode(code, e.Message);
            }
        }


        [HttpGet("{code}")]
        public IActionResult GetCode(int code)
        {
            var headerDictionary = new Dictionary<string, StringValues>(Request.Headers);

            string csv = "{" + Environment.NewLine + "\"Headers\" : ";

            csv += JsonConvert.SerializeObject(headerDictionary, Formatting.Indented) + "," + Environment.NewLine;

            StreamReader reader = new StreamReader(Request.Body);
            string body = reader.ReadToEnd();

            body = "\"Body\" : " + body + Environment.NewLine + "}";

            string response = csv + body;

            return StatusCode(code, response);

        }


    }
}
