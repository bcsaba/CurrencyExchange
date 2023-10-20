using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetLocalCurrenciesQuery : IRequest<List<Currency>>;