using CurrencyExchange.Application.Models;
using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Application.Handlers;

public class GetStoredRatesQueryHandler : IRequestHandler<GetStoredRatesQuery, IEnumerable<ExchangeRateWithComment>>
{
    private readonly ExchangeRateDbContext _exchangeRateDbContext;

    public GetStoredRatesQueryHandler(ExchangeRateDbContext exchangeRateDbContext)
    {
        _exchangeRateDbContext = exchangeRateDbContext;
    }

    public async Task<IEnumerable<ExchangeRateWithComment>> Handle(GetStoredRatesQuery request, CancellationToken cancellationToken)
    {
        var storedRates = await _exchangeRateDbContext.SavedRates
            .Include(x => x.Currency)
            .Select(x => new ExchangeRateWithComment
            {
                Id = x.Id,
                ExchangeDate = x.RateDay,
                Currency = x.Currency.CurrencyName,
                ExchangeUnit = x.Currency.Unit,
                Value = x.Rate,
                Comment = x.Comment ?? string.Empty
            })
            .OrderBy(r => r.Currency)
            .ThenByDescending(r => r.ExchangeDate)
            .ToListAsync(cancellationToken);

        return storedRates;
    }
}