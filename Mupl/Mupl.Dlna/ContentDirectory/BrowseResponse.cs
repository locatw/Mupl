using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mupl.Dlna.ContentDirectory
{
    [XmlRoot(Namespace = "")]
    public class BrowseResponse : IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement();
            try
            {
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    switch (reader.Name)
                    {
                        case "Result":
                            Result = DeserializeResult(reader);
                            break;
                        case "NumberReturned":
                            NumberReturned = reader.ReadElementContentAsInt();
                            break;
                        case "TotalMatches":
                            TotalMatches = reader.ReadElementContentAsInt();
                            break;
                        case "UpdateID":
                            UpdateID = reader.ReadElementContentAsInt();
                            break;
                    }
                }
            }
            finally
            {
                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public DidlLite Result { get; set; }

        public int NumberReturned { get; set; }

        public int TotalMatches { get; set; }

        public int UpdateID { get; set; }

        private DidlLite DeserializeResult(XmlReader reader)
        {
            string resultContent = reader.ReadElementContentAsString();
            using (var resultContentReader = new StringReader(resultContent))
            {
                var serializer = new XmlSerializer(typeof(DidlLite));

                return (DidlLite)serializer.Deserialize(resultContentReader);
            }
        }
    }
}
