using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WebhookCatcher.Utils;

namespace WebhookCatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatchController : ControllerBase
    {

        RequestToFile request = new RequestToFile();


        [HttpPost("{id}")]
        public IActionResult PostToCode(int id)
        {
            try
            {
                var headerDictionary = new Dictionary<string, StringValues>(Request.Headers);                

                string csv = "Headers" + Environment.NewLine;

                foreach (var pair in headerDictionary)
                {
                    csv += pair + Environment.NewLine;
                }

                StreamReader reader = new StreamReader(Request.Body);
                string body = reader.ReadToEnd();

                body = Environment.NewLine + "Body" + Environment.NewLine + body;

                string response = csv + body;

                request.ToFile(response, id);

                return StatusCode(id, response);
            }
            catch (Exception e)
            {
                request.ToFile(e.Message, id);
                return StatusCode(id, e.Message);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetCode(int id)
        {
            var headerDictionary = new Dictionary<string, StringValues>(Request.Headers);

            string csv = "Headers" + Environment.NewLine;

            foreach (var pair in headerDictionary)
            {
                csv += pair + Environment.NewLine;
            }


            StreamReader reader = new StreamReader(Request.Body);
            string body = reader.ReadToEnd();

            body = Environment.NewLine + "Body" + Environment.NewLine + body;

            string response = csv + body;

            return StatusCode(id, response);

        }


    }
}
