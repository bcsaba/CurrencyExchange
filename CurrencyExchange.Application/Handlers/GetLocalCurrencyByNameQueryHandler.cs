using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Application.Handlers;

public class GetLocalCurrencyByNameQueryHandler : IRequestHandler<GetLocalCurrencyByNameQuery, Currency?>
{
    private readonly ExchangeRateDbContext _dbContext;

    public GetLocalCurrencyByNameQueryHandler(ExchangeRateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Currency?> Handle(GetLocalCurrencyByNameQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.Currencies
            .SingleOrDefaultAsync(c =>
                    c.CreatedBy == query.applicationUser
                    && c.CurrencyName == query.name
                , cancellationToken);
    }
}