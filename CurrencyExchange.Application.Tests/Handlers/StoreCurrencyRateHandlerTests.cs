using CurrencyExchange.Application.Commands;
using CurrencyExchange.Application.Handlers;
using CurrencyExchange.Application.Models;
using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.Models;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace CurrencyExchange.Application.Tests.Handlers;

public class StoreCurrencyRateHandlerTests : IDisposable, IAsyncDisposable
{
    private readonly IMediator _mediator;
    private TestExchangeRateDbContext _dbContext;
    private StoreCurrencyRateCommandHandler _sut;
    private const string TestCurrencyName = "EUR";
    private ApplicationUser _testUser;
    private readonly DateOnly _testExchangeRateDate = new(2021, 1, 1);

    public StoreCurrencyRateHandlerTests()
    {
        _mediator = Substitute.For<IMediator>();

        _dbContext = TestUtilities.GetCleanDbForTest();
        _dbContext.Database.BeginTransaction();

        SetupUsersForTests();
        _sut = new StoreCurrencyRateCommandHandler(_dbContext, _mediator);
    }

    [Fact]
    private async Task WhenSaveRate_ThenCheckForCurrency()
    {
        await _sut.Handle(new StoreCurrencyRateCommand(new ExchangeRateWithComment
        {
            Currency = TestCurrencyName,
            ExchangeDate = _testExchangeRateDate,
            Value = 1.0f,
            Comment = "Test"
        }, _testUser.Id), CancellationToken.None);

        await _mediator.Received(1).Send(Arg.Any<GetLocalCurrencyByNameQuery>());
    }

    [Fact]
    private async Task GivenNoCurrency_WhenSaveRate_ThenAddsCurrency()
    {
        await _sut.Handle(new StoreCurrencyRateCommand(new ExchangeRateWithComment
        {
            Currency = TestCurrencyName,
            ExchangeDate = _testExchangeRateDate,
            Value = 1.0f,
            Comment = "Test"
        }, _testUser.Id), CancellationToken.None);

        _dbContext.Currencies.Count().Should().Be(1);
        _dbContext.Currencies.FirstAsync().Result.CurrencyName.Should().Be(TestCurrencyName);
    }

    [Fact]
    private async Task GivenNeededCurrency_WhenSaveRate_ThenDoNotAddCurrencyAgain()
    {
        var currency = new Currency { CurrencyName = TestCurrencyName };
        await SetupInitialData(currency, null);

        await _sut.Handle(new StoreCurrencyRateCommand(new ExchangeRateWithComment
        {
            Currency = TestCurrencyName,
            ExchangeDate = _testExchangeRateDate,
            Value = 1.0f,
            Comment = "Test"
        }, _testUser.Id), CancellationToken.None);

        _dbContext.Currencies.Count().Should().Be(1);
        _dbContext.Currencies.FirstAsync().Result.CurrencyName.Should().Be(TestCurrencyName);
    }

    [Fact]
    private async Task GivenNoSavedRate_WhenSaveRate_ThenRateSaved()
    {
        await _sut.Handle(new StoreCurrencyRateCommand(new ExchangeRateWithComment
        {
            Currency = TestCurrencyName,
            ExchangeDate = _testExchangeRateDate,
            Value = 1.0f,
            Comment = "Test"
        }, _testUser.Id), CancellationToken.None);

        _dbContext.SavedRates.Count().Should().Be(1);
    }

    [Fact]
    private async Task GivenExistingSavedRate_WhenSaveRate_ThenNewSavedRateNotAdded()
    {
        var currency = new Currency { CurrencyName = TestCurrencyName };
        var savedRate = new SavedRate
        {
            Currency = currency,
            RateDay = _testExchangeRateDate,
            Comment = "Test comment",
            Rate = 123.5f
        };
        await SetupInitialData(currency, savedRate);

        await _sut.Handle(new StoreCurrencyRateCommand(new ExchangeRateWithComment
        {
            Currency = TestCurrencyName,
            ExchangeDate = _testExchangeRateDate,
            Value = 1.0f,
            Comment = "Test"
        }, _testUser.Id), CancellationToken.None);

        _dbContext.SavedRates.Count().Should().Be(1);
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

    private async Task SetupInitialData(Currency currency, SavedRate? savedRate)
    {
        var createdBy = _dbContext.ApplicationUsers.First();
        currency.CreatedBy = createdBy;

        await _dbContext.Currencies.AddAsync(currency);
        if (savedRate != null)
        {
            savedRate.CreatedBy = createdBy;
            await _dbContext.SavedRates.AddAsync(savedRate);
        }
        await _dbContext.SaveChangesAsync();

        _mediator.Send(Arg.Any<GetLocalCurrencyByNameQuery>())
            .Returns(currency);
        _mediator.Send(Arg.Any<GetSavedRateByCurrencyAndDateQuery>())
            .Returns(savedRate);
    }

    private void SetupUsersForTests()
    {
        _dbContext.ApplicationUsers.Add(new ApplicationUser { Email = "test@test.com" });
        _dbContext.SaveChanges();
        _testUser = _dbContext.ApplicationUsers.First();
        _mediator.Send(Arg.Any<GetUserByIdQuery>()).Returns(_testUser);
    }
}