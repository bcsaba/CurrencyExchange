using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetSavedRateByCurrencyAndDateQuery(ApplicationUser applicationUser, string currencyName, DateOnly date) : IRequest<SavedRate?>;