using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mupl.Dlna.ContentDirectory
{
    [XmlRoot("DIDL-Lite", Namespace = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/")]
    public class DidlLite : IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var containerSerializer = new XmlSerializer(typeof(Container));
            var itemSerializer = new XmlSerializer(typeof(Item));
            var elements = new List<DidlLiteElement>();

            while (reader.Read())
            {
                if (reader.Name == "container")
                {
                    var container = (Container)containerSerializer.Deserialize(reader.ReadSubtree());
                    elements.Add(container);
                }
                else if (reader.Name == "item")
                {
                    var item = (Item)itemSerializer.Deserialize(reader.ReadSubtree());
                    elements.Add(item);
                }
            }

            Elements = elements;
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DidlLiteElement> Elements { get; set; }
    }
}
