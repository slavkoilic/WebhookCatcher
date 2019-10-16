using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebhookCatcher.Utils;

namespace WebhookCatcher.Pages
{
    public class IndexModel : PageModel
    {
        GetLogs logs = new GetLogs();

        public string Time { get; set; }
        public string Logs { get; set; }
        public void OnGet()
        {
            Time = DateTime.Now.ToShortTimeString();
            Logs = logs.ListAllFiles();
        }
    }
}
