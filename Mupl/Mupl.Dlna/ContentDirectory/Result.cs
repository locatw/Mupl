using System.IO;
using System.Xml.Serialization;

namespace Mupl.Dlna.ContentDirectory
{
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Result
    {
        public static Result CreateFromXml(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(Result));

                return (Result)serializer.Deserialize(reader);
            }
        }

        public Body Body { get; set; }
    }
}
