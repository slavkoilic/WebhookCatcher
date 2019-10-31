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
    public class EchoController : ControllerBase
    {

        RequestToFile request = new RequestToFile();        


        [HttpGet]
        public IActionResult GetEcho()
        {          

            StreamReader reader = new StreamReader(Request.Body);
            string body = reader.ReadToEnd();            

            string response =body;

            return StatusCode(200, response);

        }

        [HttpPost]
        public IActionResult PostEcho()
        {

            StreamReader reader = new StreamReader(Request.Body);
            string body = reader.ReadToEnd();

            string response = body;

            return StatusCode(200, response);

        }


        [HttpGet("{code}")]
        public IActionResult GetEchoWithCode(int code)
        {

            StreamReader reader = new StreamReader(Request.Body);
            string body = reader.ReadToEnd();

            string response = body;

            return StatusCode(code, response);

        }

        [HttpPost("{code}")]
        public IActionResult PostEchoWithCode(int code)
        {

            StreamReader reader = new StreamReader(Request.Body);
            string body = reader.ReadToEnd();

            string response = body;

            return StatusCode(code, response);

        }


    }
}
