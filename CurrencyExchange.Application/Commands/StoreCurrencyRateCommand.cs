using CurrencyExchange.Application.Models;
using MediatR;

namespace CurrencyExchange.Application.Commands;

public record StoreCurrencyRateCommand(ExchangeRateWithComment exchangeRate, string userId) : IRequest<SaveRateViewModel>;