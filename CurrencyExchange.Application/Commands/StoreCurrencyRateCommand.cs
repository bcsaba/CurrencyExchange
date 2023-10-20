using CurrencyExchange.Application.Models;
using MediatR;

namespace CurrencyExchange.Application.Commands;

public record StoreCurrencyRateCommand(ExchangeRateWithComment exchangeRate) : IRequest<SaveRateViewModel>;