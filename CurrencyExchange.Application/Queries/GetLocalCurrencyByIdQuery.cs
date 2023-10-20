using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetLocalCurrencyByIdQuery(int Id) : IRequest<Currency> { };