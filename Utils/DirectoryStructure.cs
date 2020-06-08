using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebhookCatcher.Utils
{
    public class DirectoryStructure
    {
        readonly string RootDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        public void SetUp()
        {
            List<string> directories = new List<string>();
            directories.Add("Controllers");
            directories.Add("Logs");
            directories.Add("Archive");
            directories.Add("ControllerStatus");


            string dirPath = "";

            foreach (string dirName in directories)
            {
                dirPath = Path.Combine(RootDirectoryPath, dirName);

                if (!Directory.Exists(dirPath)) {
                    Directory.CreateDirectory(dirPath);
                }
            }

        }
        


    }
}
