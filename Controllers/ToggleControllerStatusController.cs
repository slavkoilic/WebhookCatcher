using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using WebhookCatcher.Utils;

namespace WebhookCatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToggleControllerStatusController : ControllerBase
    {
        RequestToFile request = new RequestToFile();
        ControllerStatus controllerStatus = new ControllerStatus();

        [AcceptVerbs("GET", "POST", "PUT", "PATCH", "DELETE")]
        [Route("{id}")]
        public async Task<IActionResult> PostToToggleControllerAvailabilityAsync(string id)
        {
            int code = 200;

            if (controllerStatus.IsUp(id))
            {
                //TODO: Check if there's file in folder with _up.txt -> rename to _down.txt and return false
                controllerStatus.TakeItDown(id);
                code = 404;
            } else
            {
                //TODO: Check if there's file in folder with _down.txt -> rename to _up.txt and return true
                controllerStatus.BringItUp(id);
                code = 200;
            }

            StreamReader reader = new StreamReader(Request.Body);
            string body = await reader.ReadToEndAsync();

            request.ToFile(body, id);            

            return StatusCode(code);

        }


        [HttpGet("{id}/status")]
        public IActionResult GetStatusOfController(string id)
        {
            //TODO: Check if there's file in folder with _down.txt -> rename to _up.txt and return true
            //TODO: Check if there's file in folder with _up.txt -> rename to _down.txt and return false
            //TODO: If there's no file with _down.txt or _up.txt -> crate _up.txt and return true


            string body = "{\"IsUp\" : " + controllerStatus.IsUp(id) + "}";
            return StatusCode(200, body);

        }


    }
}
