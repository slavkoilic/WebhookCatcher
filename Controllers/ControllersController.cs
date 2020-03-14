using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        CreateControllerWithStringModel strController;
        RequestToFile log;

        [HttpPost("create")]
        public async Task<IActionResult> PostToCreateNewControllerAsync()
        {
            request = new ControllersUtils();
            controller = new CreateControllerModel();
            strController = new CreateControllerWithStringModel();
            log = new RequestToFile();

            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string body = await reader.ReadToEndAsync();
                log.ToFile(body, "create");

                try {
                    controller = controller.Deserialize(body);
                    request.CreateControllerFile(controller.ControllerName.ToLower(), body);
                }
                catch
                {
                    strController = strController.Deserialize(body);
                    request.CreateControllerFile(strController.ControllerName.ToLower(), body);
                }

                
            }

            return StatusCode(201);

        }

        [AcceptVerbs("GET","POST","PUT")]
        [Route("{controllerId}/{**catchAll}")]
        public async Task<IActionResult> PostToGetResponseFromControllerAsync(string controllerId, string catchAll)
        {
            request = new ControllersUtils();
            controller = new CreateControllerModel();
            strController = new CreateControllerWithStringModel();
            log = new RequestToFile();

            using (StreamReader reader = new StreamReader(Request.Body))
            {
                
                string body = await reader.ReadToEndAsync();
                body = Request.Path.ToString() + Environment.NewLine + body;
                log.ToFile(body, controllerId);
            }

            string jsonString = "";

            //if ResponseBody is jsonObject
            try
            {
                jsonString = Regex.Unescape(request.ReadControllerFile(controllerId.ToLower()));
                controller = controller.Deserialize(jsonString);
                return StatusCode(controller.StatusCode, controller.ResponseBody);
            }
            //if ResponseBody is string
            catch 
            {
                
                jsonString = request.ReadControllerFile(controllerId.ToLower());
                strController = strController.Deserialize(jsonString);
                //if ResponseBody string is XML
                try
                {
                    var doc = XDocument.Parse(strController.ResponseBody);
                    return StatusCode(strController.StatusCode, doc);

                }
                //if ResponseBody string is just a string
                catch
                {
                    return StatusCode(strController.StatusCode, strController.ResponseBody);
                }

            }           
            

        }

    }
}
