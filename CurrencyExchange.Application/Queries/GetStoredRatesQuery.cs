using CurrencyExchange.Application.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetStoredRatesQuery(string UserId) : IRequest<IEnumerable<ExchangeRateWithComment>>;
