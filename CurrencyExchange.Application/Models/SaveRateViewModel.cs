namespace CurrencyExchange.Application.Models;

public record SaveRateViewModel(
    int CurrencyId,
    string Currency,
    float Value,
    string? Comment,
    int ExchangeUnit,
    DateTime Created,
    DateOnly ExchangeDate);