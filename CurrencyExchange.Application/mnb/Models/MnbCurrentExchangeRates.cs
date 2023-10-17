using System.Xml.Serialization;

namespace www.mnb.hu.webservices.Models;

[XmlRoot]
public class MNBCurrentExchangeRates
{
    [XmlElement(ElementName = "Day")] public Day Day { get; set; }
}