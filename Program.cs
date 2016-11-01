using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ColladaFix
{
    class Program
    {

        private static void FixPaths(XmlElement element, string dir)
        {
            if (element == null) return;

            string prefix = "file:///";
            if (element.LocalName == "init_from" && element.InnerText.StartsWith(prefix))
            {
                string path = element.InnerText.Substring(prefix.Length);
                path = path.Replace("/", "\\");
                string fileName = Path.GetFileName(path);
                element.InnerText = fileName;
            }

            foreach (XmlNode child in element.ChildNodes)
            {
                if (!(child is XmlElement)) continue;
                FixPaths(child as XmlElement, dir);
            }
        }

        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.Error.WriteLine("Usage:");
                Console.Error.WriteLine("ColladaFix fileName.dae");
                Environment.Exit(1);
            }

            string fileName = args[0];
            string dir = Path.GetDirectoryName(fileName);

            // Make backup.
            string backup = $"{fileName}.bak_{DateTime.Now.ToString("yyyyMMddTHHmmss")}";
            File.Copy(fileName, backup, true);

            // Read model.
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            // Fix paths.
            dir = dir.Replace("\\", "/");
            FixPaths(doc.DocumentElement, dir);

            // Save model.
            doc.Save(fileName);

        }
    }
}
