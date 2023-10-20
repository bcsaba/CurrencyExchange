namespace CurrencyExchange.Application.Models;

public record SaveRateViewModel(
    int CurrencyId,
    string CurrencyName,
    float Rate,
    string? Comment,
    int ExchangeUnit,
    DateTime Created,
    DateOnly RateDay)