using System.IO;
using System.Text;

namespace WebhookCatcher.Utils
{
    public class ControllersUtils
    {
        readonly string ControllersDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Controllers");
                
        public void CreateControllerFile(string controllerName, string responseBody)
        {
            string controllerFilePath = Path.Combine(ControllersDirectoryPath,controllerName + "_controller.txt");

            // Create the file, or overwrite if the file exists.
            File.WriteAllText(controllerFilePath, responseBody);            
        }


        public string ReadControllerFile(string controllerName)
        {
            string controllerFilePath = Path.Combine(ControllersDirectoryPath, controllerName + "_controller.txt");
            string json = "";            

            if (!File.Exists(controllerFilePath))
            {
                json = "{\"Error\":\"Controller not found. Do You need to create it first?\"}";
            } 
            else
            {
                json = File.ReadAllText(controllerFilePath);
            }

            return json;
        }
    }
}
