using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Application.Handlers;

public class GetLocalCurrencyByNameRequestHandler : IRequestHandler<GetLocalCurrencyByNameRequest, Currency?>
{
    private readonly ExchangeRateDbContext _dbContext;

    public GetLocalCurrencyByNameRequestHandler(ExchangeRateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Currency?> Handle(GetLocalCurrencyByNameRequest request, CancellationToken cancellationToken)
    {
        return await _dbContext.Currencies.SingleOrDefaultAsync(c => c.CurrencyName == request.name);
    }
}