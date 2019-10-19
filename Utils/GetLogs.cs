using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebhookCatcher.Controllers;

namespace WebhookCatcher.Utils
{
    public class GetLogs
    {
        public IEnumerable<FileInfo> Files()
        {
            DirectoryInfo dir;
            FileInfo[] files;
            string pathToLogsDir = Directory.GetCurrentDirectory() + "/wwwroot/Logs";
            dir = new DirectoryInfo(pathToLogsDir);
            files = dir.GetFiles();
            var orderedFiles = files.OrderBy(f => f.CreationTime).Reverse<FileInfo>();

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
                sb.Append("<tr>");
                sb.Append("<td>" + f.CreationTime.ToString("yyyyMMdd_HHmmssfff") + "</td>");
                sb.Append("<td>200</td>");
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
    }
}
