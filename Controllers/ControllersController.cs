using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebhookCatcher.Models;
using WebhookCatcher.Utils;

namespace WebhookCatcher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllersController : ControllerBase
    {
        ControllersUtils request;
        CreateControllerModel controller;
        RequestToFile log;

        [HttpPost("create")]
        public async Task<IActionResult> PostToCreateNewControllerAsync()
        {
            request = new ControllersUtils();
            controller = new CreateControllerModel();
            log = new RequestToFile();

            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string body = await reader.ReadToEndAsync();
                log.ToFile(body, "create");

                controller = controller.Deserialize(body);
                request.CreateControllerFile(controller.ControllerName.ToLower(), body);
            }

            return StatusCode(201);

        }


        [HttpPost("{controllerId}/{**catchAll}")]
        public async Task<IActionResult> PostToGetResponseFromControllerAsync(string controllerId, string catchAll)
        {
            request = new ControllersUtils();
            controller = new CreateControllerModel();
            log = new RequestToFile();

            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string body = await reader.ReadToEndAsync();
                log.ToFile(body, controllerId);
            }


            string jsonString = Regex.Unescape(request.ReadControllerFile(controllerId.ToLower()));
            controller = controller.Deserialize(jsonString);

            return StatusCode(controller.StatusCode, controller.ResponseBody);

        }


        [HttpPut("{controllerId}/{**catchAll}")]
        public async Task<IActionResult> PutToGetResponseFromControllerAsync(string controllerId, string catchAll)
        {
            request = new ControllersUtils();
            controller = new CreateControllerModel();
            log = new RequestToFile();

            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string body = await reader.ReadToEndAsync();
                log.ToFile(body, controllerId);
            }


            string jsonString = request.ReadControllerFile(controllerId.ToLower());
            controller = controller.Deserialize(jsonString);


            return StatusCode(controller.StatusCode, controller.ResponseBody);

        }


        [HttpGet("{controllerId}/{**catchAll}")]
        public async Task<IActionResult> GetResponseFromControllerAsync(string controllerId)
        {
            request = new ControllersUtils();
            log = new RequestToFile();
            controller = new CreateControllerModel();

            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string body = await reader.ReadToEndAsync();
                log.ToFile(body, controllerId);
            }

            string jsonString = request.ReadControllerFile(controllerId.ToLower());            
            controller = controller.Deserialize(jsonString);
            
            return StatusCode(controller.StatusCode, controller.ResponseBody);

        }

    }
}
