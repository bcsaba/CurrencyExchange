using System.Xml.Serialization;

namespace www.mnb.hu.webservices.Models;

[XmlRoot("Rate")]
public class Rate
{
    [XmlAttribute("curr")]
    public string Currency { get; set; }

    [XmlAttribute("unit")]
    public string ExchangeUnit { get; set; }

    [XmlText]
    public string Value { get; set; }
}