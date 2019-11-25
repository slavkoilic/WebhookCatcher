using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebhookCatcher.Utils
{
    public class GetControllers
    {
        readonly string pathToControllersDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Controllers");
        readonly string pathToArchiveDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Archive");
        

        public IEnumerable<FileInfo> Files()
        {

            DirectoryInfo ControllersDir = new DirectoryInfo(pathToControllersDir);
            FileInfo[] ControllerFiles = ControllersDir.GetFiles();
            var orderedFiles = ControllerFiles.OrderBy(f => f.CreationTime).Reverse<FileInfo>();

            return orderedFiles;
        }
        public string ListAllFiles()
        {

            var orderedFiles = Files();
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id=\"webhooklogs\">");
            sb.Append("<tr>");
            sb.Append("<th>Timestamp</th>");
            sb.Append("<th>Code</th>");
            sb.Append("<th>File</th>");
            sb.Append("</tr>");

            foreach (FileInfo f in orderedFiles)
            {
                var file = System.IO.Path.GetFileNameWithoutExtension(f.Name);
                string[] parts = file.Split('_');

                sb.Append("<tr>");
                sb.Append("<td>" + f.CreationTime.ToString("yyyyMMdd_HHmmssfff") + "</td>");
                sb.Append("<td>"+ parts.Last() +"</td>");
                sb.Append("<td><a href=\"/Controllers/"+ f.Name + "\" target=\"_blank\"> ");
                sb.Append(f.Name + "</a></td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }

        public string NumberOfFiles()
        {
            var numberOfFiles = Files().Count().ToString();
            return numberOfFiles;
        }

        public void ArchiveControllers()
        {
            
            foreach (var file in Files())
            {
                string pathToArchivedFile = Path.Combine(pathToArchiveDir, file.Name);
                if (File.Exists(pathToArchivedFile)){
                    File.Delete(pathToArchivedFile);
                }

                //file.MoveTo($@"{pathToArchiveDir}\{file.Name}");
                file.MoveTo(pathToArchivedFile);
            }

        }
      
    }
}
