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
    private ExchangeRateDbContext _dbContext;
    private StoreCurrencyRateHandler _sut;
    private const string TestCurrencyName = "EUR";
    private readonly DateOnly TestExchangeRateDate = new(2021, 1, 1);

    public StoreCurrencyRateHandlerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _mediator.Send(Arg.Any<GetLocalCurrencyByNameRequest>())
            .Returns(new Currency
            {
                Id = 1,
                CurrencyName = "EUR",
                SavedRates = new List<SavedRate>()
            });
        _mediator.Send(new GetSavedRateByCurrencyAndDateRequest("EUR", TestExchangeRateDate))
            .Returns(default(SavedRate));


        // new SavedRate
        // {
        //     Id = 1,
        //     Currency = new Currency
        //     {
        //         Id = 1,
        //         CurrencyName = "EUR",
        //         SavedRates = new List<SavedRate>()
        //     },
        //     Rate = 1.0m,
        //     Created = DateTime.UtcNow,
        //     Comment = "Test",
        //     RateDay = DateTime.UtcNow
        // }


    }

    [Fact]
    private async Task WhenSaveRate_ThenCheckForCurrency()
    {
        GetCleanDbForTest();

        _sut = new StoreCurrencyRateHandler(_dbContext, _mediator);

        await _sut.Handle(new StoreCurrencyRateCommand(new ExchangeRateWithComment
        {
            Currency = TestCurrencyName,
            ExchangeDate = TestExchangeRateDate,
            Value = 1.0f,
            Comment = "Test"
        }), CancellationToken.None);

        await _mediator.Received(1).Send(Arg.Any<GetLocalCurrencyByNameRequest>());
    }

    [Fact]
    private async Task GivenNoCurrency_WhenSaveRate_ThenAddsCurrency()
    {
        GetCleanDbForTest();

        _sut = new StoreCurrencyRateHandler(_dbContext, _mediator);

        await _sut.Handle(new StoreCurrencyRateCommand(new ExchangeRateWithComment
        {
            Currency = TestCurrencyName,
            ExchangeDate = TestExchangeRateDate,
            Value = 1.0f,
            Comment = "Test"
        }), CancellationToken.None);

        _dbContext.Currencies.Count().Should().Be(1);
        _dbContext.Currencies.FirstAsync().Result.CurrencyName.Should().Be(TestCurrencyName);
    }

    [Fact]
    // [Fact(Skip = "Fails with currency can not be tracked message only in in-memory DB testing.")]
    // This fails because the currency is not tracked by the context with in-memory DB testing.
    // "The instance of entity type 'Currency' cannot be tracked because another instance with the key value '{Id: 1}' is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached."
    // Works fine if _dbContext is used directly instead of _mediator call.
    private async Task GivenNeededCurrency_WhenSaveRate_ThenDoNotAddCurrencyAgain()
    {
        GetCleanDbForTest();
        _dbContext.Currencies.Count().Should().Be(0);

        var currency = new Currency
        {
            CurrencyName = TestCurrencyName
        };
        await _dbContext.Currencies.AddAsync(currency);
        await _dbContext.SaveChangesAsync();
        _dbContext.Currencies.Entry(currency).State = EntityState.;
        _dbContext.Currencies.Count().Should().Be(1);

        _sut = new StoreCurrencyRateHandler(_dbContext, _mediator);

        await _sut.Handle(new StoreCurrencyRateCommand(new ExchangeRateWithComment
        {
            Currency = TestCurrencyName,
            ExchangeDate = TestExchangeRateDate,
            Value = 1.0f,
            Comment = "Test"
        }), CancellationToken.None);

        _dbContext.Currencies.Count().Should().Be(1);
        _dbContext.Currencies.FirstAsync().Result.CurrencyName.Should().Be(TestCurrencyName);
    }

    private void GetCleanDbForTest()
    {
        var options = new DbContextOptionsBuilder<ExchangeRateDbContext>()
            .UseInMemoryDatabase("TestDb")
            .EnableSensitiveDataLogging(true)
            .Options;

        _dbContext = new ExchangeRateDbContext(options);
        _dbContext.Currencies.RemoveRange(_dbContext.Currencies);
        _dbContext.SavedRates.RemoveRange(_dbContext.SavedRates);
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}