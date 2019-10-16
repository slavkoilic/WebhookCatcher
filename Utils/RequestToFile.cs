using System;
using System.IO;

namespace WebhookCatcher.Utils
{
    public class RequestToFile
    {
        public void ToFile(string requestContent, int responseCode)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            File.WriteAllText("./Logs/"+timestamp+"_"+ responseCode+".txt", requestContent);
        }
    }
}



