using CurrencyExchange.Application.Models;
using CurrencyExchange.Application.Queries;
using CurrencyExchange.Controllers;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace CurrencyExchange.Tests.Controllers;

public class StoredExchangeRateTests
{
    private readonly IMediator _mediator;
    private readonly StoredExchangeRateController _storedExchangeRateController;

    public StoredExchangeRateTests()
    {
        _mediator = Substitute.For<IMediator>();
        _mediator.Send(Arg.Any<object>())
            .Returns(new object());
        _storedExchangeRateController = new StoredExchangeRateController(_mediator);
    }

    [Fact]
    public async Task WhenRequestStoredExchangeRates_ResultShouldNotThrow()
    {
        var act = async () => await _storedExchangeRateController.Get();

        await act.Should().NotThrowAsync<NotImplementedException>();
    }

    [Fact]
    public async Task WhenRequestStoredExchangeRates_ResultShouldNotBeNull()
    {
        var result = await _storedExchangeRateController.Get();

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task WhenRequestStoredExchangeRates_ShouldCallMediatorForDataWithExpectedQuery()
    {
        var result = await _storedExchangeRateController.Get();

        await _mediator.Received(1)
            .Send(Arg.Any<GetStoredRatesQuery>());
    }

    [Theory]
    [MemberData(nameof(GetStoredRates))]
    public async Task GivenResultFromMediator_WhenRequestStoredExchangeRates_ShouldReturnExpectedDatas(IEnumerable<ExchangeRateWithComment> savedRates)
    {
        _mediator.Send(Arg.Any<GetStoredRatesQuery>())
            .Returns(savedRates);

        var result = await _storedExchangeRateController.Get();

        ((IEnumerable<ExchangeRateWithComment>)result.Value!).Count().Should().Be(savedRates.Count());
        result.Value.Should().BeEquivalentTo(savedRates);
    }

    public static IEnumerable<object[]> GetStoredRates()
    {
        yield return new object[]
        {
            new List<ExchangeRateWithComment>
            {
                new()
                {
                    Currency = "EUR",
                    Value = 356.3f,
                    Comment = "Test",
                    ExchangeDate = new DateOnly(2023, 10, 12)
                }
            }
        };

        yield return new object[]
        {
            new List<ExchangeRateWithComment>
            {
                new()
                {
                    Currency = "EUR",
                    Value = 356.3f,
                    Comment = "Test",
                    ExchangeDate = new DateOnly(2023, 10, 12)
                },
                new ExchangeRateWithComment
                {
                    Currency = "USD",
                    Value = 346.8f,
                    Comment = "Other comment",
                    ExchangeDate = new DateOnly(2023, 10, 12)
                }
            }
        };
    }
}