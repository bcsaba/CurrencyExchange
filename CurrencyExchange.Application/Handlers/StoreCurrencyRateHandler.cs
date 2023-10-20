using CurrencyExchange.Application.Commands;
using CurrencyExchange.Application.Models;
using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Application.Handlers;

public class StoreCurrencyRateHandler : IRequestHandler<StoreCurrencyRateCommand, SaveRateViewModel>
{
    private readonly ExchangeRateDbContext _dbContext;

    public StoreCurrencyRateHandler(ExchangeRateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SaveRateViewModel> Handle(StoreCurrencyRateCommand request, CancellationToken cancellationToken)
    {
        var exchangeRate = request.exchangeRate;
        var currency = await _dbContext.Currencies.FirstOrDefaultAsync(
            c => c.CurrencyName == exchangeRate.Currency, cancellationToken);
        if (currency == null)
        {
            currency = new Currency
            {
                CurrencyName = exchangeRate.Currency,
                Unit = exchangeRate.ExchangeUnit,
                Created = DateTime.UtcNow
            };
            await _dbContext.Currencies.AddAsync(currency, cancellationToken);
        }

        var savedRate = new SavedRate
        {
            Currency = currency,
            Rate = exchangeRate.Value,
            Created = DateTime.UtcNow,
            Comment = exchangeRate.Comment,
            RateDay = exchangeRate.ExchangeDate
        };
        await _dbContext.SavedRates.AddAsync(savedRate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new SaveRateViewModel(
            currency.Id,
            currency.CurrencyName,
            savedRate.Rate,
            savedRate.Comment,
            currency.Unit,
            savedRate.Created,
            savedRate.RateDay);
    }
}