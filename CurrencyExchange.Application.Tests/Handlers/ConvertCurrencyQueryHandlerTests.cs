using CurrencyExchange.Application.Handlers;
using CurrencyExchange.Application.mnb;
using CurrencyExchange.Application.Models;
using CurrencyExchange.Application.Queries;
using FluentAssertions;
using MediatR;
using NSubstitute;
using www.mnb.hu.webservices;

namespace CurrencyExchange.Application.Tests.Handlers;

public class ConvertCurrencyQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IMnbExchangeRateService _mnbExchangeRateService;
    private readonly ConvertCurrencyQueryHandler _sut;

    public ConvertCurrencyQueryHandlerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _mnbExchangeRateService = Substitute.For<IMnbExchangeRateService>();
        const string testXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>" +
                               "<MNBCurrentExchangeRates>" +
                               "    <Day date=\"2023-10-17\">" +
                               "        <Rate curr=\"AUD\" unit=\"1\">232,23000</Rate>" +
                               "        <Rate curr=\"EUR\" unit=\"1\">386,81000</Rate>" +
                               "        <Rate curr=\"BGN\" unit=\"1\">197,19000</Rate>" +
                               "    </Day>" +
                               "</MNBCurrentExchangeRates>";

        _mnbExchangeRateService
            .GetCurrentExchangeRatesAsync(
                Arg.Any<GetCurrentExchangeRatesRequestBody>())
            .Returns(new GetCurrentExchangeRatesResponse(
                    new GetCurrentExchangeRatesResponseBody
                    {
                        GetCurrentExchangeRatesResult = testXml
                    }));

        _sut = new ConvertCurrencyQueryHandler(_mediator, _mnbExchangeRateService);
    }

    [Fact]
    private async Task WhenGetCurrencies_ThenCallMnbService()
    {
        await _sut.Handle(new ConvertCurrencyQuery(new CurrencyConversionModel()), CancellationToken.None);

        await _mnbExchangeRateService.Received(1).GetCurrentExchangeRatesAsync(Arg.Any<GetCurrentExchangeRatesRequestBody>());
    }

    [Theory]
    [InlineData("HUF", 1f, "EUR", 386.81f)]
    [InlineData("HUF", 100f, "EUR", 38681f)]
    [InlineData("HUF", 1f, "AUD", 232.23f)]
    [InlineData("HUF", 100f, "AUD", 23223f)]
    private async Task WhenGetCurrencies_ThenReturnsExpectedValue(string fromCurrency, float fromAmount, string toCurrency, float toAmount)
    {
        var conversionModel = new CurrencyConversionModel
        {
            FromCurrency = fromCurrency,
            FromAmount = fromAmount,
            ToCurrency = toCurrency
        };
        var currencyConversionModel = await _sut.Handle(new ConvertCurrencyQuery(conversionModel), CancellationToken.None);

        currencyConversionModel.ToCurrency.Should().Be(toCurrency);
        currencyConversionModel.ToAmount.Should().Be(toAmount);
    }
}