using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WebhookCatcher.Utils;

namespace WebhookCatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatcherController : ControllerBase
    {

        RequestToFile request = new RequestToFile();

        [HttpPost("{id}")]
        public IActionResult PostToCode(int id, [FromBody] string body)
        {
            var headerDictionary = new Dictionary<string, StringValues>(Request.Headers);
            //IHeaderDictionary headers1 = ;

            //headers1.GetCommaSeparatedValues();

            string csv = "Headers" + Environment.NewLine;

            foreach(var pair in headerDictionary)
            {
                csv += pair + Environment.NewLine;
            }

            body = Environment.NewLine +"Body" + Environment.NewLine + body;

            string response = csv + body;

            request.ToFile(response, id);

            return StatusCode(id, response);

        }


    }
}
