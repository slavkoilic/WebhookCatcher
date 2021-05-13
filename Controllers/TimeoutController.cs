using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebhookCatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeoutController : ControllerBase
    {
        [AcceptVerbs("GET", "POST", "PUT", "PATCH", "DELETE")]
        [Route("{timeout}/{**catchAll}")]
        public async Task<IActionResult> TimeoutAsync(string timeout)
        {
            StreamReader reader = new StreamReader(Request.Body);
            string body = await reader.ReadToEndAsync();

            string response = body;
            
            try
            {
                if (Int32.TryParse(timeout, out int timeoutInt))
                {
                    Thread.Sleep(timeoutInt*1000);
                }
                else
                {
                    Thread.Sleep(35000);
                }
                

            }
            catch
            {
                Thread.Sleep(35000);
                
            }

            return StatusCode(200, response);

        }
    }
}