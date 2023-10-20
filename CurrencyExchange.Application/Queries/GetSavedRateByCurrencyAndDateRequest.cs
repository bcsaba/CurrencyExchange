using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetSavedRateByCurrencyAndDateRequest(string currencyName, DateOnly date) : IRequest<SavedRate?>;