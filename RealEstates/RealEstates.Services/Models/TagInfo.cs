using System.Xml.Serialization;

namespace RealEstates.Services.Models
{
    [XmlType("Tag")]
    public class TagInfo
    {
        [XmlElement("Name")]
        public string Name { get; set; }
    }
}
