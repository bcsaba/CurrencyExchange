using CurrencyExchange.Application.Commands;
using CurrencyExchange.Application.Models;
using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CurrencyExchange.Application.Handlers;

public class StoreCurrencyRateCommandHandler : IRequestHandler<StoreCurrencyRateCommand, SaveRateViewModel>
{
    private readonly ExchangeRateDbContext _dbContext;
    private readonly IMediator _mediator;

    public StoreCurrencyRateCommandHandler(ExchangeRateDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<SaveRateViewModel> Handle(StoreCurrencyRateCommand request, CancellationToken cancellationToken)
    {
        var exchangeRate = request.exchangeRate;

        var applicationUser = await _mediator.Send(new GetUserByIdQuery(request.userId), cancellationToken);
        var currency = await AddOrGetCurrency(cancellationToken, exchangeRate, applicationUser);
        var savedRate = await AddOrUpdateSavedRate(cancellationToken, currency, exchangeRate, applicationUser);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new SaveRateViewModel(
            savedRate.Id,
            currency.Id,
            currency.CurrencyName,
            savedRate.Rate,
            savedRate.Comment,
            currency.Unit,
            savedRate.Created,
            savedRate.RateDay);
    }

    private async Task<SavedRate> AddOrUpdateSavedRate(CancellationToken cancellationToken, Currency currency,
        ExchangeRateWithComment exchangeRate, ApplicationUser applicationUser)
    {
        var savedRate = await _mediator.Send(
            new GetSavedRateByCurrencyAndDateQuery(applicationUser ,currency.CurrencyName, exchangeRate.ExchangeDate),
            cancellationToken);

        if (savedRate == null)
        {
            savedRate = new SavedRate
            {
                Currency = currency,
                Rate = exchangeRate.Value,
                CreatedBy = applicationUser,
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

    private async Task<Currency> AddOrGetCurrency(CancellationToken cancellationToken, ExchangeRateWithComment exchangeRate, ApplicationUser applicationUser)
    {
        var currency = await _mediator.Send(new GetLocalCurrencyByNameQuery(applicationUser, exchangeRate.Currency), cancellationToken);

        if (currency == null)
        {
            currency = new Currency
            {
                CurrencyName = exchangeRate.Currency,
                Unit = exchangeRate.ExchangeUnit,
                CreatedBy = applicationUser,
                Created = DateTime.UtcNow
            };
            await _dbContext.Currencies.AddAsync(currency, cancellationToken);
        }

        return currency;
    }
}