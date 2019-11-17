using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebhookCatcher.Utils
{
    public class ControllersUtils
    {
        readonly string ControllersDirectoryPath = Directory.GetCurrentDirectory() + "/wwwroot/Controllers";

        /***
        public bool IsUp(string code)
        {         

            string upFilePath = ControllerStatusDirectoryPath + "/" + code + "_up.txt";
            string downFilePath = ControllerStatusDirectoryPath + "/" + code + "_down.txt";

            bool status;

            if (File.Exists(upFilePath))
            {
                status = true;
            }
            else if (File.Exists(downFilePath))
            {
                status = false;
            }
            else
            {
                CreateSwitchFile(ControllerStatusDirectoryPath + "/" + code + "_up.txt");
                status = true;
            }


            return status;
        }

        public void TakeItDown(string code)
        {
            string upFilePath = ControllerStatusDirectoryPath + "/" + code + "_up.txt";
            string downFilePath = ControllerStatusDirectoryPath + "/" + code + "_down.txt";

            File.Move(upFilePath, downFilePath);

        }

        public void BringItUp(string code)
        {
            string upFilePath = ControllerStatusDirectoryPath + "/" + code + "_up.txt";
            string downFilePath = ControllerStatusDirectoryPath + "/" + code + "_down.txt";

            File.Move(downFilePath, upFilePath);
        }
        ***/

        public void CreateControllerFile(string controllerName, string responseBody)
        {
            string controllerFilePath = ControllersDirectoryPath + "/" + controllerName + "_controller.txt";


            // Create the file, or overwrite if the file exists.

            using (FileStream fs = File.Create(controllerFilePath))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(responseBody);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }
    }
}
