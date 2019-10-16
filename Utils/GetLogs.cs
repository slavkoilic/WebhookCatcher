using System.IO;
using System.Linq;
using System.Text;
namespace WebhookCatcher.Utils
{
    public class GetLogs
    {
        public string ListAllFiles()
        {
            DirectoryInfo dir;
            StringBuilder sb = new StringBuilder();
            FileInfo[] files;

            dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "/Logs");
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
                sb.Append("<td><a href=\"/Logs/" + f.Name + "\" download>");
                sb.Append(f.Name + "</a></td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }
    }
}
