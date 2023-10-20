using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Application.Handlers;

public class GetSavedRateByCurrencyAndDateQueryHandler : IRequestHandler<GetSavedRateByCurrencyAndDateQuery, SavedRate?>
{
    private readonly ExchangeRateDbContext _dbContext;

    public GetSavedRateByCurrencyAndDateQueryHandler(ExchangeRateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SavedRate?> Handle(GetSavedRateByCurrencyAndDateQuery query, CancellationToken cancellationToken)
    {
        var singleOrDefaultAsync = await _dbContext.SavedRates
            .Include(sr => sr.Currency)
            .SingleOrDefaultAsync(sr =>
                sr.Currency.CurrencyName == query.currencyName
                && sr.RateDay == query.date);
        return singleOrDefaultAsync;
    }
}