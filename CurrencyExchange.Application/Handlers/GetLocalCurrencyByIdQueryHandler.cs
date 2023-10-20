using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Application.Handlers;

public class GetLocalCurrencyByIdQueryHandler : IRequestHandler<GetLocalCurrencyByIdQuery, Currency>
{
    private readonly ExchangeRateDbContext _dbContext;

    public GetLocalCurrencyByIdQueryHandler(ExchangeRateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Currency> Handle(GetLocalCurrencyByIdQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.Currencies.SingleOrDefaultAsync(c => c.Id == query.Id);
    }
}