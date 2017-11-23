using System.Xml.Serialization;

namespace Mupl.Dlna.ContentDirectory
{
    public abstract class DidlLiteElement
    {
        [XmlElement("title", Namespace = "http://purl.org/dc/elements/1.1/")]
        public string Title { get; set; }
    }
}
