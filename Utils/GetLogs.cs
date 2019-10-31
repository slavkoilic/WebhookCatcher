using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebhookCatcher.Controllers;

namespace WebhookCatcher.Utils
{
    public class GetLogs
    {
        string pathToLogsDir = Directory.GetCurrentDirectory() + "/wwwroot/Logs";
        string pathToArchiveDir = Directory.GetCurrentDirectory() + "/wwwroot/Archive";
        

        public IEnumerable<FileInfo> Files()
        {

            DirectoryInfo LogsDir = new DirectoryInfo(pathToLogsDir);
            FileInfo[] LogFiles = LogsDir.GetFiles();
            var orderedFiles = LogFiles.OrderBy(f => f.CreationTime).Reverse<FileInfo>();

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
                sb.Append("<td><a href=\"/Logs/"+ f.Name + "\" target=\"_blank\"> ");
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

        public void ArchiveLogs()
        {
            DirectoryInfo LogsDir = new DirectoryInfo(pathToLogsDir);
            foreach (var file in LogsDir.GetFiles())
            {
                file.MoveTo($@"{pathToArchiveDir}\{file.Name}");
            }

        }
      
    }
}
