namespace CurrencyExchange.Models;

public class ExchangeRateWithComment
{
    public int? Id { get; set; }
    public DateOnly ExchangeDate { get; set; }
    public string Currency { get; set; }
    public int ExchangeUnit { get; set; } = 1;
    public float Value { get; set; }
    public string Comment { get; set; }
}