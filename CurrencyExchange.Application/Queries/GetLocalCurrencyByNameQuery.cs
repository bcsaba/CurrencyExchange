using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetLocalCurrencyByNameQuery(string name) : IRequest<Currency?>;