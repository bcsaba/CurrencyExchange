using CurrencyExchange.Persistence.Models;

namespace CurrencyExchange.Application.Services;

public interface IExchangeRateService
{
    Task<List<Currency>> GetCurrencies();
    Task<Currency> GetCurrency(int id);
    Task<Currency> AddCurrency(Currency currency);
    Task<List<SavedRate>> GetSavedRates();
    Task<SavedRate> GetSavedRate(int id);
    Task<IEnumerable<SavedRate>> GetSavedRatesByCurrencyId(int id);
    Task<IEnumerable<SavedRate>> GetSavedRatesByCurrencyName(string name);
    Task<SavedRate> AddSavedRate(SavedRate savedRate);
    Task<SavedRate> UpdateSavedRate(SavedRate savedRate);
    Task DeleteSavedRate(int id);
}