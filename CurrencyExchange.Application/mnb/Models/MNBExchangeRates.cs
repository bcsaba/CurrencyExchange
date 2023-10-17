using System.Xml.Serialization;

namespace www.mnb.hu.webservices.Models;

[XmlRoot]
public class MNBExchangeRates
{
    [XmlElement(ElementName = "Day")]
    public List<Day> Days { get; set; }
}

public class Day
{
    [XmlAttribute("date")]
    public string ExchangeDate { get; set; }

    [XmlElement("Rate")]
    public List<Rate> Rates { get; set; }
}

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