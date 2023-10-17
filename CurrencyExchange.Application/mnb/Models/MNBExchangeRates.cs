using System.Xml.Serialization;

namespace www.mnb.hu.webservices.Models;

[XmlRoot]
public class MNBExchangeRates
{
    [XmlElement(ElementName = "Day")]
    public List<Day> Days { get; set; }
}