using CurrencyExchange.Application.Commands;
using CurrencyExchange.Application.Models;
using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Handlers;

public class StoreCurrencyRateHandler : IRequestHandler<StoreCurrencyRateCommand, SaveRateViewModel>
{
    private readonly ExchangeRateDbContext _dbContext;
    private readonly IMediator _mediator;

    public StoreCurrencyRateHandler(ExchangeRateDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<SaveRateViewModel> Handle(StoreCurrencyRateCommand request, CancellationToken cancellationToken)
    {
        var exchangeRate = request.exchangeRate;

        var currency = await AddOrGetCurrency(cancellationToken, exchangeRate);
        var savedRate = await AddOrUpdateSavedRate(cancellationToken, currency, exchangeRate);

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

    private async Task<SavedRate> AddOrUpdateSavedRate(CancellationToken cancellationToken, Currency currency,
        ExchangeRateWithComment exchangeRate)
    {
        var savedRate = await _mediator.Send(
            new GetSavedRateByCurrencyAndDateRequest(currency.CurrencyName, exchangeRate.ExchangeDate),
            cancellationToken);

        if (savedRate == null)
        {
            savedRate = new SavedRate
            {
                Currency = currency,
                Rate = exchangeRate.Value,
                Created = DateTime.UtcNow,
                Comment = exchangeRate.Comment,
                RateDay = exchangeRate.ExchangeDate
            };
            await _dbContext.SavedRates.AddAsync(savedRate, cancellationToken);
        }
        else
        {
            savedRate.Rate = exchangeRate.Value;
            savedRate.Comment = exchangeRate.Comment;
            savedRate.RateDay = exchangeRate.ExchangeDate;
            savedRate.LastUpdated = DateTime.UtcNow;
        }

        return savedRate;
    }

    private async Task<Currency> AddOrGetCurrency(CancellationToken cancellationToken, ExchangeRateWithComment exchangeRate)
    {
        var currency = await _mediator.Send(new GetLocalCurrencyByNameRequest(exchangeRate.Currency), cancellationToken);

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

        return currency;
    }
}