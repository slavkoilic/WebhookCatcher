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

            string responseString = "";

            
            try
            {
                //responseString = Regex.Unescape(request.ReadControllerFile(controllerId.ToLower())); //only works with json
                responseString = request.ReadControllerFile(controllerId.ToLower()); //only works with xml/string
                strController = strController.Deserialize(responseString); //if json -> throws exception


                if (strController.ResponseBody.Contains("?xml") || strController.ResponseBody.Contains("CDATA"))
                {
                    //responseString = request.ReadControllerFile(controllerId.ToLower());
                    //strController = strController.Deserialize(responseString);
                    var doc = XDocument.Parse(strController.ResponseBody);
                    return StatusCode(strController.StatusCode, strController.ResponseBody);
                } else
                {
                    responseString = Regex.Unescape(request.ReadControllerFile(controllerId.ToLower()));
                    controller = controller.Deserialize(responseString);
                    return StatusCode(controller.StatusCode, controller.ResponseBody);
                }
                
            }
            //if ResponseBody is string
            catch 
            {
                
                //if ResponseBody string is JSON
                try
                {
                    responseString = Regex.Unescape(request.ReadControllerFile(controllerId.ToLower()));
                    controller = controller.Deserialize(responseString);
                    return StatusCode(controller.StatusCode, controller.ResponseBody);                   
                }
                //if ResponseBody string is just a string
                catch
                {
                    strController = strController.Deserialize(responseString);
                    return StatusCode(strController.StatusCode, strController.ResponseBody);
                }



            }           
            

        }


        public bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }

        public bool IsXml(string input)
        {
            input = input.Trim();
            return input.StartsWith("\"<") && input.EndsWith(">\"");
        }

    }
}
