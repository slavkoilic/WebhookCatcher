using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebhookCatcher.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IHostEnvironment _hostingEnvironment;

        public HomeController(IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public string Index()
        {            
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            return contentRootPath;
        }
    }
}
