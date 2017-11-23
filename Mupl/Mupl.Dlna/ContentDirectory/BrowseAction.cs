using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Web.Http;

namespace Mupl.Dlna.ContentDirectory
{
    public class BrowseAction : IAction<Result>
    {
        private static XNamespace envelopeNs = "http://schemas.xmlsoap.org/soap/envelope/";

        private static XNamespace upnpNs = "urn:schemas-upnp-org:service:ContentDirectory:1";

        public static string RootObjectId = "0";

        public static int RequestAll = 0;

        public async Task<Result> ExecuteAsync(HttpClient httpClient, Device device, Uri uri)
        {
            var xml = await BuildXmlAsync();

            var requestContent = new HttpStringContent(xml, Windows.Storage.Streams.UnicodeEncoding.Utf8, "text/xml");
            var response = await httpClient.PostAsync(uri, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            return Result.CreateFromXml(responseContent);
        }

        public string ObjectId { get; set; }

        public BrowseFlag BrowseFlag { get; set; }

        public Filter Filter { get; set; }

        public int StartingIndex { get; set; }

        public int RequestedCount { get; set; }

        private async Task<string> BuildXmlAsync()
        {
            var browseActionXml =
                new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(envelopeNs + "Envelope",
                        new XAttribute(XNamespace.Xmlns + "s", envelopeNs),
                        new XElement(envelopeNs + "Body",
                            new XElement(upnpNs + "Browse",
                                new XAttribute(XNamespace.Xmlns + "u", upnpNs),
                                new XElement("ObjectID", new XText(ObjectId)),
                                new XElement("BrowseFlag", new XText(BrowseFlag.ToString())),
                                new XElement("Filter", new XText(Filter.ToString())),
                                new XElement("StartingIndex", new XText(StartingIndex.ToString())),
                                new XElement("RequestedCount", new XText(RequestedCount.ToString())),
                                new XElement("SortCriteria", new XText(""))))));
            using (var memory = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(memory, new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 }))
                {
                    browseActionXml.WriteTo(writer);
                    writer.Flush();
                }

                memory.Flush();
                memory.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(memory))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
