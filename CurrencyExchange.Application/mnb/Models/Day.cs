using System.Xml.Serialization;

namespace www.mnb.hu.webservices.Models;

public class Day
{
    [XmlAttribute("date")]
    public string ExchangeDate { get; set; }

    [XmlElement("Rate")]
    public List<Rate> Rates { get; set; }
}