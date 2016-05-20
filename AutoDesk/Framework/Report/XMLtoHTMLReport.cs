using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace AutoDesk.Framework.Report
{
    public class XMLtoHTMLReport
    {
        public void TransformXMLToHTML(string XMLPath)
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode; int i = 0;
            FileStream fs = new FileStream(XMLPath, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("test-case");
            StringBuilder htmltable = new StringBuilder();
            htmltable.Append("<table style='width:600px;'>");
            htmltable.Append("<tr>");
            htmltable.Append("<td style='border: 1px solid black;width:80%;'><b>TC Name</b></th>");
            htmltable.Append("<td style='border: 1px solid black;width:20%;'><b>Status</b></th>");
            htmltable.Append("<td style='border: 1px solid black;width:20%;'><b>Executed</b></th>");
            htmltable.Append("</tr>");

            for (i = 0; i < xmlnode.Count; i++)
            {
                htmltable.Append("<tr>");
                htmltable.Append("<td style='border: 1px solid black;color:blue;'>" + xmlnode[i].Attributes[0].Value + "</td>");
                if (xmlnode[i].Attributes["result"].Value == "Success")
                {
                    htmltable.Append("<td style='border: 1px solid black;background-color:green;color:white;'>" + xmlnode[i].Attributes["result"].Value + "</td>");
                }
                else
                {
                    htmltable.Append("<td style='border: 1px solid black;background-color:red;'>" + xmlnode[i].Attributes["result"].Value + "</td>");
                }

                htmltable.Append("<td style='border: 1px solid black;color:blue;'>" + xmlnode[i].Attributes["executed"].Value + "</td>");
                htmltable.Append("</tr>");
            }

            using (FileStream fs1 = new FileStream("test.html", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs1, Encoding.UTF8))
                {
                    w.WriteLine("<H1>TC Results</H1>");

                    w.WriteLine(htmltable);
                }
            }
        }
    
    }
}
