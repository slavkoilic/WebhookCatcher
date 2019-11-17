using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebhookCatcher.Models;
using WebhookCatcher.Utils;

namespace WebhookCatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersController : ControllerBase
    {
        ControllersUtils request = new ControllersUtils();
        CreateControllerModel controller = new CreateControllerModel();        

        [HttpPost("create")]
        public IActionResult PostToRandomControllerAvailability()
        {            
            StreamReader reader = new StreamReader(Request.Body);
            string body = reader.ReadToEnd();
            controller = controller.Deserialize(body);


            request.CreateControllerFile(controller.ControllerName, controller.ResponseBody);

            return StatusCode(controller.StatusCode);

        }

    }
}
