using CurrencyExchange.Application.Commands;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Application.Handlers;

public class StoreCurrencyCommandHandler : IRequestHandler<StoreCurrencyCommand, Currency>
{
    private readonly ExchangeRateDbContext _dbContext;
    private readonly IMediator _mediator;

    public StoreCurrencyCommandHandler(ExchangeRateDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<Currency> Handle(StoreCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = request.currency;
        var currencyInDb = await _dbContext.Currencies.FirstOrDefaultAsync(c => c.CurrencyName == currency.CurrencyName, cancellationToken);
        if (currencyInDb is null)
        {
            await _dbContext.Currencies.AddAsync(currency, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return currency;
        }

        return currencyInDb;
    }
}