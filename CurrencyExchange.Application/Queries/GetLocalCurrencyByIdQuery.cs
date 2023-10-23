using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetLocalCurrencyByIdQuery(ApplicationUser applicationUser, int Id) : IRequest<Currency?> { };