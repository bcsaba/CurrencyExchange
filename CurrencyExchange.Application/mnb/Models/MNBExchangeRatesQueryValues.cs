using System.Xml.Serialization;

namespace www.mnb.hu.webservices.Models;

[XmlRoot]
public class MNBExchangeRatesQueryValues
{
    [XmlElement(ElementName = "FirstDate")]
    public string FirstDateStr { get; set; }

    [XmlElement(ElementName = "LastDate")]
    public string LastDateStr { get; set; }

    public DateOnly FirstDate { get => DateOnly.FromDateTime(DateTime.Parse(FirstDateStr)); }

    public DateOnly LastDate { get => DateOnly.FromDateTime(DateTime.Parse(LastDateStr)); }

    [XmlElement(ElementName = "Currencies")]
    public Currencies Currencies { get; set; }
}