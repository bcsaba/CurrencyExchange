using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetLocalCurrencyByNameQuery(ApplicationUser applicationUser, string name) : IRequest<Currency?>;