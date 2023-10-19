using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Application.Services;

public class ExchangeRateService : IExchangeRateService
{
    ExchangeRateDbContext _dbContext;

    public ExchangeRateService(ExchangeRateDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Currency>> GetCurrencies()
    {
        return await _dbContext.Currencies.ToListAsync();
    }
    
    public async Task<Currency> GetCurrency(int id)
    {
        return await _dbContext.Currencies.SingleOrDefault(id);
    }
    
    public async Task<Currency> AddCurrency(Currency currency)
    {
        _dbContext.Currencies.Add(currency);
        await _dbContext.SaveChangesAsync();
        return currency;
    }
    
    public async Task<List<SavedRate>> GetSavedRates()
    {
        return await _dbContext.SavedRates.ToListAsync();
    }
    
    public async Task<SavedRate> GetSavedRate(int id)
    {
        return await _dbContext.SavedRates.SingleOrDefault(id);
    }

    public async Task<IEnumerable<SavedRate>> GetSavedRatesByCurrencyId(int id)
    {
        return await _dbContext.SavedRates
            .Where(x => x.Currency.Id == id)
            .ToListAsync();
    }

    public async Task<IEnumerable<SavedRate>> GetSavedRatesByCurrencyName(string name)
    {
        return await _dbContext.SavedRates
            .Where(x => x.Currency.CurrencyName == name)
            .ToListAsync());
    }

    public async Task<SavedRate> AddSavedRate(SavedRate savedRate)
    {
        _dbContext.SavedRates.Add(savedRate);
        await _dbContext.SaveChangesAsync();
        return savedRate;
    }
    
    public async Task<SavedRate> UpdateSavedRate(SavedRate savedRate)
    {
        _dbContext.Entry(savedRate).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return savedRate;
    }
    
    public async Task DeleteSavedRate(int id)
    {
        var savedRate = await _dbContext.SavedRates.FindAsync(id);
        _dbContext.SavedRates.Remove(savedRate);
        await _dbContext.SaveChangesAsync();
    }
}