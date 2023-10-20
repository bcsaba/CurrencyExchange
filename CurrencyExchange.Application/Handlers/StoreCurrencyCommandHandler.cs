using CurrencyExchange.Application.Commands;
using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Handlers;

public class StoreCurrencyCommandHandler : IRequestHandler<StoreCurrencyCommand, Currency>
{
    private readonly CurrencyExchangeDbContext _dbContext;

    public StoreCurrencyCommandHandler(CurrencyExchangeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Currency> Handle(StoreCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = request.Currency;
        var currencyInDb = await _dbContext.Currencies.FirstOrDefaultAsync(c => c.Name == currency.Name, cancellationToken);
        if (currencyInDb is null)
        {
            await _dbContext.Currencies.AddAsync(currency, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return currency;
        }

        return currencyInDb;
    }
}