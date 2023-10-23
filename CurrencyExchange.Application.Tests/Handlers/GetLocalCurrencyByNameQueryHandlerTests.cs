using CurrencyExchange.Application.Handlers;
using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using FluentAssertions;

namespace CurrencyExchange.Application.Tests.Handlers;

public class GetLocalCurrencyByNameQueryHandlerTests : IDisposable, IAsyncDisposable
{
    private readonly TestExchangeRateDbContext _dbContext;
    private readonly GetLocalCurrencyByNameQueryHandler _sut;

    public GetLocalCurrencyByNameQueryHandlerTests()
    {
        _dbContext = TestUtilities.GetCleanDbForTest();
        _dbContext.Database.BeginTransaction();
        _sut = new GetLocalCurrencyByNameQueryHandler(_dbContext);
    }

    [Fact]
    private async Task WhenGetCurrencyByName_ThenReturnsCurrency()
    {
        var testUser = new ApplicationUser();
        _dbContext.Currencies.Add(new Currency
        {
            CreatedBy = testUser,
            CurrencyName = "EUR",
            Id = 1
        });
        await _dbContext.SaveChangesAsync();

        var result = await _sut.Handle(new GetLocalCurrencyByNameQuery(testUser, "EUR"), CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<Currency>();
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(1, false)]
    private async Task WhenGetCurrencyByName_ThenOnlyOwnCurrenciesReturned(int userIdx, bool isNotNull)
    {
        List<ApplicationUser> users = await SetupCurrenciesForTest();

        var result = await _sut.Handle(new GetLocalCurrencyByNameQuery(users[userIdx], "EUR"), CancellationToken.None);

        if (isNotNull)
            result.Should().NotBeNull();
        else
            result.Should().BeNull();
    }

    private async Task<List<ApplicationUser>> SetupCurrenciesForTest()
    {
        List<ApplicationUser> users;
        users = new List<ApplicationUser>
        {
            new() { Id = "1" },
            new() { Id = "2" }
        };
        _dbContext.Currencies.Add(new Currency
        {
            CreatedBy = users[0],
            CurrencyName = "EUR",
            Id = 1
        });
        _dbContext.Currencies.Add(new Currency
        {
            CreatedBy = users[0],
            CurrencyName = "USD",
            Id = 2
        });
        _dbContext.Currencies.Add(new Currency
        {
            CreatedBy = users[1],
            CurrencyName = "CHF",
            Id = 3
        });

        await _dbContext.SaveChangesAsync();
        return users;
    }

    public void Dispose()
    {
        _dbContext.Database.RollbackTransaction();
        _dbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
        await _dbContext.DisposeAsync();
    }
}