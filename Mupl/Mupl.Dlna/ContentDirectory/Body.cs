using System.Xml.Serialization;

namespace Mupl.Dlna.ContentDirectory
{
    public class Body
    {
        [XmlElement(Namespace = "urn:schemas-upnp-org:service:ContentDirectory:1")]
        public BrowseResponse BrowseResponse { get; set; }
    }
}
