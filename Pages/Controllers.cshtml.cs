using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebhookCatcher.Utils;

namespace WebhookCatcher.Pages
{
    public class ControllersModel : PageModel
    {

        GetControllers controllers = new GetControllers();

        public string Time { get; set; }
        public string Controllers { get; set; }
        public string ControllersNumber { get; set; }
        public IActionResult OnPostArchiveControllers()
        {
            controllers.ArchiveControllers();
            return RedirectToPage("Controllers");
        }
        public void OnGet()
        {
            Time = DateTime.Now.ToShortTimeString();
            Controllers = controllers.ListAllFiles();
            ControllersNumber = controllers.NumberOfFiles();
        }

        
    }
}