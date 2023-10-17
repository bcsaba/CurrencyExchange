using System.Xml.Serialization;

namespace www.mnb.hu.webservices.Models;

public class Day
{
    [XmlAttribute("date")]
    public string ExchangeDateStr { get; set; }

    public DateOnly ExchangeDate => DateOnly.Parse(ExchangeDateStr);

    [XmlElement("Rate")]
    public List<Rate> Rates { get; set; }
}