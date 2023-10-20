namespace CurrencyExchange.Application.Models;

public record SaveRateViewModel(
    int CurrencyId,
    string Currency,
    float Rate,
    string? Comment,
    int ExchangeUnit,
    DateTime Created,
    DateOnly RateDay);