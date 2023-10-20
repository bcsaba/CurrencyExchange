namespace CurrencyExchange.Application.Models;

public record CurrencyConversionModel
{
    public string FromCurrency { get; set; } = "HUF";
    public float FromAmount { get; set; }
    public string ToCurrency { get; set; } = "EUR";
    public float ToAmount { get; set; }
}