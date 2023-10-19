using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Application.Handlers;

public class GetLocalCurrencyByIdHandler : IRequestHandler<GetLocalCurrencyByIdRequest, Currency>
{
    private readonly ExchangeRateDbContext _dbContext;

    public GetLocalCurrencyByIdHandler(ExchangeRateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Currency> Handle(GetLocalCurrencyByIdRequest request, CancellationToken cancellationToken)
    {
        return await _dbContext.Currencies.SingleOrDefaultAsync(c => c.Id == request.Id);
    }
}