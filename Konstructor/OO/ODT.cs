using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;

namespace Konstructor.OO
{
    class ODT
    {
        public string file_name = "writer.odt";
        public XmlDocument doc = new XmlDocument();
        public string llll = "";
        public ODT(string path) {
            file_name = path + file_name;
            doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"+
                        "<office:document-content xmlns:office=\"urn:oasis:names:tc:opendocument:xmlns:office:1.0\" xmlns:style=\"urn:oasis:names:tc:opendocument:xmlns:style:1.0\" xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" xmlns:table=\"urn:oasis:names:tc:opendocument:xmlns:table:1.0\" xmlns:draw=\"urn:oasis:names:tc:opendocument:xmlns:drawing:1.0\" xmlns:fo=\"urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:meta=\"urn:oasis:names:tc:opendocument:xmlns:meta:1.0\" xmlns:number=\"urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0\" xmlns:svg=\"urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0\" xmlns:chart=\"urn:oasis:names:tc:opendocument:xmlns:chart:1.0\" xmlns:dr3d=\"urn:oasis:names:tc:opendocument:xmlns:dr3d:1.0\" xmlns:math=\"http://www.w3.org/1998/Math/MathML\" xmlns:form=\"urn:oasis:names:tc:opendocument:xmlns:form:1.0\" xmlns:script=\"urn:oasis:names:tc:opendocument:xmlns:script:1.0\" xmlns:ooo=\"http://openoffice.org/2004/office\" xmlns:ooow=\"http://openoffice.org/2004/writer\" xmlns:oooc=\"http://openoffice.org/2004/calc\" xmlns:dom=\"http://www.w3.org/2001/xml-events\" xmlns:xforms=\"http://www.w3.org/2002/xforms\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:rpt=\"http://openoffice.org/2005/report\" xmlns:of=\"urn:oasis:names:tc:opendocument:xmlns:of:1.2\" xmlns:xhtml=\"http://www.w3.org/1999/xhtml\" xmlns:grddl=\"http://www.w3.org/2003/g/data-view#\" xmlns:tableooo=\"http://openoffice.org/2009/table\" xmlns:field=\"urn:openoffice:names:experimental:ooo-ms-interop:xmlns:field:1.0\" office:version=\"1.2\"><office:scripts/><office:font-face-decls><style:font-face style:name=\"Mangal1\" svg:font-family=\"Mangal\"/><style:font-face style:name=\"Times New Roman\" svg:font-family=\"&apos;Times New Roman&apos;\" style:font-family-generic=\"roman\" style:font-pitch=\"variable\"/><style:font-face style:name=\"Arial\" svg:font-family=\"Arial\" style:font-family-generic=\"swiss\" style:font-pitch=\"variable\"/><style:font-face style:name=\"Mangal\" svg:font-family=\"Mangal\" style:font-family-generic=\"system\" style:font-pitch=\"variable\"/><style:font-face style:name=\"Microsoft YaHei\" svg:font-family=\"&apos;Microsoft YaHei&apos;\" style:font-family-generic=\"system\" style:font-pitch=\"variable\"/><style:font-face style:name=\"SimSun\" svg:font-family=\"SimSun\" style:font-family-generic=\"system\" style:font-pitch=\"variable\"/></office:font-face-decls><office:automatic-styles><style:style style:name=\"P1\" style:family=\"paragraph\" style:parent-style-name=\"Standard\"><style:text-properties fo:language=\"en\" fo:country=\"US\"/></style:style><style:style style:name=\"P2\" style:family=\"paragraph\" style:parent-style-name=\"Standard\"><style:paragraph-properties fo:text-align=\"center\" style:justify-single-word=\"false\"/><style:text-properties fo:font-size=\"14pt\" fo:language=\"ru\" fo:country=\"RU\" fo:font-weight=\"bold\" style:font-size-asian=\"14pt\" style:font-weight-asian=\"bold\" style:font-size-complex=\"14pt\" style:font-weight-complex=\"bold\"/></style:style></office:automatic-styles><office:body><office:text><text:sequence-decls><text:sequence-decl text:display-outline-level=\"0\" text:name=\"Illustration\"/><text:sequence-decl text:display-outline-level=\"0\" text:name=\"Table\"/><text:sequence-decl text:display-outline-level=\"0\" text:name=\"Text\"/><text:sequence-decl text:display-outline-level=\"0\" text:name=\"Drawing\"/></text:sequence-decls></office:text></office:body></office:document-content>");
            llll = doc["office:document-content"].ChildNodes[3].ChildNodes[0].Name;
        }
        public void AddZag(string value) {
            XmlNode node = doc.CreateElement("text:p");
            XmlAttribute attr = doc.CreateAttribute("text:style-name");
            attr.Value = "P1";
            node.Attributes.Append(attr);
            node.InnerText = value;
            doc["office:document-content"].ChildNodes[3].ChildNodes[0].InnerXml = doc["office:document-content"].ChildNodes[3].ChildNodes[0].InnerXml + "<text:p text:style-name=\"P2\">" + value + "</text:p>";
            llll = doc["office:document-content"].ChildNodes[3].InnerXml;
        }
        public void AddElement(string value) {
            XmlNode node = doc.CreateElement("text:p");
            XmlAttribute attr = doc.CreateAttribute("text:style-name");
            attr.Value = "P1";
            node.Attributes.Append(attr);
            node.InnerText=value;
            doc["office:document-content"].ChildNodes[3].ChildNodes[0].InnerXml = doc["office:document-content"].ChildNodes[3].ChildNodes[0].InnerXml + "<text:p text:style-name=\"P1\">" + value + "</text:p>";
            llll = doc["office:document-content"].ChildNodes[3].InnerXml;
        }
        public string SaveFile() {
            string path = this.GetType().Module.FullyQualifiedName;
            int i = path.Length - 1;
            while (path[i] != '\\') {
                i--;
            }
            path = path.Substring(0,i+1)+"temp.ODT";
            int count;
            byte[] buf = new byte[4096];
            DateTime now = DateTime.Now;

            using (ZipInputStream zis = new ZipInputStream(File.OpenRead(path)))
            {
                using (ZipOutputStream zos = new ZipOutputStream(File.OpenWrite(file_name)))
                {
                    ZipEntry ze;
                    while ((ze = zis.GetNextEntry()) != null)
                    {
                        ZipEntry entry = new ZipEntry(ze.Name);
                        entry.DateTime = now;
                        zos.PutNextEntry(entry);

                        if (ze.Name == "content.xml")
                        {
                            string text = doc.OuterXml;
                            byte[] textBytes = Encoding.UTF8.GetBytes(text);
                            zos.Write(textBytes, 0, textBytes.Length);
                        }
                        else
                        {
                            while ((count = zis.Read(buf, 0, buf.Length)) > 0)
                            {
                                zos.Write(buf, 0, count);
                            }
                        }
                    }

                    zos.Finish();
                }
            }
            return path;
        }
    }
}
