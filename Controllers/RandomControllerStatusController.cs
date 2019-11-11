using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebhookCatcher.Utils;

namespace WebhookCatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomControllerStatusController : ControllerBase
    {
        RequestToFile request = new RequestToFile();
        ControllerStatus controllerStatus = new ControllerStatus();
        private readonly Random _random = new Random();
        private int RandomPercentageOfFailure => _random.Next(100);

        [HttpPost("{id}")]
        public IActionResult PostToRandomControllerAvailability(string id)
        {
            

            StreamReader reader = new StreamReader(Request.Body);
            string body = reader.ReadToEnd();

            request.ToFile(body, id);

            return RandomPercentageOfFailure <= 40
                ? StatusCode(500)
                : StatusCode(200);

        }




    }
}
