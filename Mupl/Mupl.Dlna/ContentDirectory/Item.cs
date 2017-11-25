using System.Xml.Serialization;

namespace Mupl.Dlna.ContentDirectory
{
    [XmlRoot("item", Namespace = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/")]
    public class Item : DidlLiteElement
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlAttribute("parentID")]
        public string ParentID { get; set; }

        [XmlAttribute("restricted")]
        public int Restricted { get; set; }

        [XmlAttribute("searchable")]
        public int Searchable { get; set; }

        [XmlElement("class", Namespace = "urn:schemas-upnp-org:metadata-1-0/upnp/")]
        public string Class { get; set; }
    }
}
