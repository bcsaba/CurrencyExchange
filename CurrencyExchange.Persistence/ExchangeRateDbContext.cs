using CurrencyExchange.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CurrencyExchange.Persistence;

public class ExchangeRateDbContext : DbContext
{
    public ExchangeRateDbContext(DbContextOptions<ExchangeRateDbContext> options)
    : base(options)
    { }

    public DbSet<Currency> Currencies { get; set; } = default!;
    public DbSet<SavedRate> SavedRates { get; set; } = default!;
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExchangeRateDbContext>
{
    public ExchangeRateDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../CurrencyExchange/appsettings.json").Build();
        var builder = new DbContextOptionsBuilder<ExchangeRateDbContext>();
        var connectionString = configuration.GetConnectionString("ExchangeRateDbConnection");
        builder.UseNpgsql(connectionString);
        return new ExchangeRateDbContext(builder.Options);
    }
}

public class TestExchangeRateDbContext : DbContext
{
    public TestExchangeRateDbContext(DbContextOptions<TestExchangeRateDbContext> options)
        : base(options)
    { }

    public DbSet<Currency> Currencies { get; set; } = default!;
    public DbSet<SavedRate> SavedRates { get; set; } = default!;
}

public class TestDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestExchangeRateDbContext>
{
    public TestExchangeRateDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../CurrencyExchange/appsettings.json").Build();
        var builder = new DbContextOptionsBuilder<TestExchangeRateDbContext>();
        var connectionString = configuration.GetConnectionString("ExchangeRateDbConnection");
        builder.UseNpgsql(connectionString);
        return new TestExchangeRateDbContext(builder.Options);
    }
}