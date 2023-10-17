using System.Xml.Serialization;

namespace www.mnb.hu.webservices.Models;

public class Currency
{
    [XmlText] public string Name { get; set; }
}