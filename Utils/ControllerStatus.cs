using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebhookCatcher.Utils
{
    public class ControllerStatus
    {
        readonly string ControllerStatusDirectoryPath = Directory.GetCurrentDirectory() + "/wwwroot/ControllerStatus";
        public bool IsUp(string code)
        {
            //TODO: Check if there's file in folder with _down.txt -> return false
            //TODO: Check if there's file in folder with _up.txt -> return true
            //TODO: If there's no file with _down.txt or _up.txt -> crate _up.txt and return true

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

        public void CreateSwitchFile(string path)
        {
            // Create the file, or overwrite if the file exists.
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }
    }
}
