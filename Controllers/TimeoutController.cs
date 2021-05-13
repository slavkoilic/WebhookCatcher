using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebhookCatcher.Controllers
{
    public class TimeoutController : ControllerBase
    {
        [AcceptVerbs("GET", "POST", "PUT", "PATCH", "DELETE")]
        [Route("{timeout}/{**catchAll}")]
        public async Task<IActionResult> GetEchoWithCodeAsync(int timeout)
        {
            StreamReader reader = new StreamReader(Request.Body);
            string body = await reader.ReadToEndAsync();

            string response = body;
            
            try
            {
                Thread.Sleep(timeout*1000);

            }
            catch
            {
                Thread.Sleep(30000);
                
            }

            return StatusCode(200, response);

        }
    }
}