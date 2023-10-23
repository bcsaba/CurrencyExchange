using CurrencyExchange.Persistence;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CurrencyExchange.Application.Tests.Handlers;

public static class TestUtilities
{
    public static TestExchangeRateDbContext GetCleanDbForTest()
    {
        var options = new DbContextOptionsBuilder<ExchangeRateDbContext>()
            .UseNpgsql("Host=127.0.0.1;Port=5432;Database=test_exchange_rates_development;Username=exchangerate;Password=exchangerate;Timeout=30")
            .Options;

        var dbContext = new TestExchangeRateDbContext(options,
            new OptionsWrapper<OperationalStoreOptions>(new OperationalStoreOptions()));
        dbContext.Currencies.RemoveRange(dbContext.Currencies);
        dbContext.SavedRates.RemoveRange(dbContext.SavedRates);
        dbContext.SaveChanges();
        return dbContext;
    }

}