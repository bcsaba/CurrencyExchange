using System.Xml.Serialization;

namespace www.mnb.hu.webservices.Models;

[XmlRoot]
public class Currencies
{
    [XmlElement(ElementName = "Curr")]
    public List<Currency> Currency { get; set; }
}