using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Application.Handlers;

public class GetLocalCurrenciesQueryHandler : IRequestHandler<Queries.GetLocalCurrenciesQuery, List<Currency>>
{
    private readonly ExchangeRateDbContext _dbContext;

    public GetLocalCurrenciesQueryHandler(ExchangeRateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Currency>> Handle(Queries.GetLocalCurrenciesQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.Currencies.ToListAsync(cancellationToken);
    }
}