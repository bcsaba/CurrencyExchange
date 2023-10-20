using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Application.Handlers;

public class GetLocalCurrenciesRequestHandler : IRequestHandler<Queries.GetLocalCurrenciesRequest, List<Currency>>
{
    private readonly ExchangeRateDbContext _dbContext;

    public GetLocalCurrenciesRequestHandler(ExchangeRateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Currency>> Handle(Queries.GetLocalCurrenciesRequest request, CancellationToken cancellationToken)
    {
        return await _dbContext.Currencies.ToListAsync(cancellationToken);
    }
}