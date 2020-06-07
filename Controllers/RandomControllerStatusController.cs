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
        private readonly Random _random = new Random();
        private int RandomPercentageOfFailure => _random.Next(100);

        
        [AcceptVerbs("GET", "POST", "PUT", "PATCH", "DELETE")]
        [Route("{id}")]
        public async Task<IActionResult> PostToRandomControllerAvailabilityAsync(string id)
        {
            

            StreamReader reader = new StreamReader(Request.Body);
            string body = await reader.ReadToEndAsync();

            request.ToFile(body, id);

            return RandomPercentageOfFailure <= 40
                ? StatusCode(500)
                : StatusCode(200);

        }




    }
}
