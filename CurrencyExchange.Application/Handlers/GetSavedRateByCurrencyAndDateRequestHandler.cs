using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Handlers;

public class GetSavedRateByCurrencyAndDateRequestHandler : IRequestHandler<GetSavedRateByCurrencyAndDateRequest, SavedRate?>
{
    private readonly ExchangeRateDbContext _dbContext;

    public GetSavedRateByCurrencyAndDateRequestHandler(ExchangeRateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SavedRate?> Handle(GetSavedRateByCurrencyAndDateRequest request, CancellationToken cancellationToken)
    {
        return await _dbContext.SavedRates
            .Include(sr => sr.Currency)
            .SingleOrDefaultAsync(sr => sr.Currency.CurrencyName == request.currencyName && sr.RateDay == request.date);
    }
}