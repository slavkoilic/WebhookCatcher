using System;
using System.IO;

namespace WebhookCatcher.Utils
{
    public class RequestToFile
    {
        public void ToFile(string requestContent, int responseCode)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssffff");
            string guid = Guid.NewGuid().ToString("N").Substring(0,4);

            File.WriteAllText("./wwwroot/Logs/" + timestamp + guid + "_"+ responseCode+".txt", requestContent);
        }

        public void ToFile(string requestContent, string name)
        {
            File.WriteAllText("./wwwroot/Logs/" + name + ".txt", requestContent);
        }

        public string GetPrefix()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssffff");
            string guid = Guid.NewGuid().ToString("N").Substring(0,4);
            string prefix = timestamp + guid;
            

            return prefix;
        }
    }
}



