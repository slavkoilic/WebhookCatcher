using System;
using System.IO;

namespace WebhookCatcher.Utils
{
    public class RequestToFile
    {
        public void ToFile(string requestContent, int responseCode)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");

            File.WriteAllText("./wwwroot/Logs/" + timestamp+"_"+ responseCode+".txt", requestContent);
        }
    }
}



