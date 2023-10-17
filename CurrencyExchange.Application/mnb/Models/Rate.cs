using System.Globalization;
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
    public string ValueStr { get; set; }

    public decimal Value => decimal.Parse(ValueStr, new CultureInfo("hu-HU"));
}