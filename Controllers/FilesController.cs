using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebhookCatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet("{code}")]
        public IActionResult GetFilesWithCode(int code)
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


        [HttpGet]
        public IActionResult GetAllFiles()
        {
            var headerDictionary = new Dictionary<string, StringValues>(Request.Headers);

            string csv = "{" + Environment.NewLine + "\"Headers\" : ";

            csv += JsonConvert.SerializeObject(headerDictionary, Formatting.Indented) + "," + Environment.NewLine;

            StreamReader reader = new StreamReader(Request.Body);
            string body = reader.ReadToEnd();

            body = "\"Body\" : " + body + Environment.NewLine + "}";

            string response = csv + body;

            return StatusCode(200, response);

        }
    }
}
