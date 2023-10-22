using CurrencyExchange.Persistence.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CurrencyExchange.Persistence;

public class ExchangeRateDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ExchangeRateDbContext(DbContextOptions<ExchangeRateDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions)
    : base(options, operationalStoreOptions)
    { }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; } = default!;

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
        return new ExchangeRateDbContext(builder.Options,
            new OptionsWrapper<OperationalStoreOptions>(new OperationalStoreOptions()));
    }
}

public class TestExchangeRateDbContext : ExchangeRateDbContext
{
    public TestExchangeRateDbContext(DbContextOptions<ExchangeRateDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    { }
}

public class TestDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestExchangeRateDbContext>
{
    public TestExchangeRateDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../CurrencyExchange/appsettings.json").Build();
        var builder = new DbContextOptionsBuilder<ExchangeRateDbContext>();
        var connectionString = configuration.GetConnectionString("TestExchangeRateDbConnection");
        builder.UseNpgsql(connectionString);
        return new TestExchangeRateDbContext(builder.Options,
            new OptionsWrapper<OperationalStoreOptions>(new OperationalStoreOptions()));
    }
}