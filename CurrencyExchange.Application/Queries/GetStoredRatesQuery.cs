using CurrencyExchange.Application.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetStoredRatesQuery : IRequest<IEnumerable<ExchangeRateWithComment>>;
