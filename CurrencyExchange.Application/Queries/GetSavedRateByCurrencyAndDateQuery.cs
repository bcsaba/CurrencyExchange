using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetSavedRateByCurrencyAndDateQuery(string currencyName, DateOnly date) : IRequest<SavedRate?>;