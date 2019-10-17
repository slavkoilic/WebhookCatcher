using System.IO;
using System.Linq;
using System.Text;
using WebhookCatcher.Controllers;

namespace WebhookCatcher.Utils
{
    public class GetLogs
    {
        public string ListAllFiles()
        {
           
            DirectoryInfo dir;
            StringBuilder sb = new StringBuilder();
            FileInfo[] files;

            string pathToLogsDir = Directory.GetCurrentDirectory() + "/wwwroot/Logs";
            dir = new DirectoryInfo(pathToLogsDir);
            files = dir.GetFiles();
            var orderedFiles = files.OrderBy(f => f.CreationTime).Reverse<FileInfo>();

            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<th>Timestamp</th>");
            sb.Append("<th>Code</th>");
            sb.Append("<th>File</th>");
            sb.Append("</tr>");

            foreach (FileInfo f in orderedFiles)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + f.CreationTime.ToString("yyyyMMdd_HHmmss") + "</td>");
                sb.Append("<td>200</td>");
                sb.Append("<td><a href=\"/Logs/"+ f.Name + "\">");
                sb.Append(f.Name + "</a></td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }
    }
}
