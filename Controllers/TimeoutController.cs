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
                Thread.Sleep(35000);
                
            }

            return StatusCode(200, response);

        }
        
        
        
        
        
        [AcceptVerbs("GET", "POST", "PUT", "PATCH", "DELETE")]
        [Route("{timeout}/{**catchAll}")]
        public async Task<IActionResult> GetEchoWithCodeAsync(string timeout)
        {
            StreamReader reader = new StreamReader(Request.Body);
            string body = await reader.ReadToEndAsync();

            string response = body;
            
            Thread.Sleep(35000);
            
            return StatusCode(200, response);
        }
    }
}