using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetLocalCurrencyByIdRequest(int Id) : IRequest<Currency> { };