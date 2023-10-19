using CurrencyExchange.Application.mnb;
using CurrencyExchange.Controllers;
using FluentAssertions;
using NSubstitute;
using www.mnb.hu.webservices;
using www.mnb.hu.webservices.Models;

namespace CurrencyExchange.Tests.Controllers;

public class MnbCurrentExchangeRatesControllerTests
{
    private readonly IMnbExchangeRateService _mnbExchangeRateService;
    private readonly MnbCurrentExchangeRatesController _mnbCurrentExchangeRatesController;

    public MnbCurrentExchangeRatesControllerTests()
    {
        _mnbExchangeRateService = Substitute.For<IMnbExchangeRateService>();
        _mnbExchangeRateService
            .GetCurrentExchangeRatesAsync(Arg.Any<GetCurrentExchangeRatesRequestBody>())
            .Returns(new GetCurrentExchangeRatesResponse
            {
                GetCurrentExchangeRatesResponse1 = new GetCurrentExchangeRatesResponseBody
                {
                    GetCurrentExchangeRatesResult = "<MNBCurrentExchangeRates></MNBCurrentExchangeRates>"
                }
            });
        _mnbCurrentExchangeRatesController =
            new MnbCurrentExchangeRatesController(_mnbExchangeRateService);
    }

    [Fact]
    public async Task WhenRequestCurrentExchangeRates_ResultShouldNotBeNull()
    {
        var result = await _mnbCurrentExchangeRatesController.Get();

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task WhenRequestCurrentExchangeRates_ShouldCallServiceForData()
    {
        var result = await _mnbCurrentExchangeRatesController.Get();

        await _mnbExchangeRateService.Received(1)
            .GetCurrentExchangeRatesAsync(Arg.Any<GetCurrentExchangeRatesRequestBody>());
    }

    [Fact]
    public async Task GivenValidCurrentExchangeRateData_WhenRequestCurrentExchangeRates_ShouldReturnExpectedType()
    {
        SetupMnbExchangeRateServiceWithRealData();

        var result = await _mnbCurrentExchangeRatesController.Get();

        result.Value.Should().BeOfType<MNBCurrentExchangeRates>();
    }

    [Fact]
    public async Task GivenValidCurrentExchangeRateData_WhenRequestCurrentExchangeRates_ShouldReturnExpectedExchangeDate()
    {
        SetupMnbExchangeRateServiceWithRealData();

        var result = await _mnbCurrentExchangeRatesController.Get();

        ((MNBCurrentExchangeRates)result.Value!).Day.ExchangeDate.Should().Be(DateOnly.FromDateTime(new DateTime(2023, 10, 17)));
    }

    [Theory]
    [InlineData("CHF", "72,65000", 72.65000)]
    [InlineData("EUR", "232,23000", 232.23000)]
    [InlineData("USD", "197,19000", 197.19000)]
    public async Task GivenValidCurrentExchangeRateData_WhenRequestCurrentExchangeRates_ShouldReturnExpectedCurrencyValues(
        string currencyName, string currencyValueStr, decimal currencyValue)
    {
        SetupMnbExchangeRateServiceWithRealData();

        var result = await _mnbCurrentExchangeRatesController.Get();

        ((MNBCurrentExchangeRates)result.Value!).Day.Rates.Should()
            .ContainSingle(r =>
                r.Currency == currencyName
                && r.ValueStr == currencyValueStr
                && r.Value == currencyValue);
    }

    private void SetupMnbExchangeRateServiceWithRealData()
    {
        _mnbExchangeRateService
            .GetCurrentExchangeRatesAsync(Arg.Any<GetCurrentExchangeRatesRequestBody>())
            .Returns(new GetCurrentExchangeRatesResponse
            {
                GetCurrentExchangeRatesResponse1 = new GetCurrentExchangeRatesResponseBody
                {
                    GetCurrentExchangeRatesResult = "<MNBCurrentExchangeRates>" +
                                                    "    <Day date=\"2023-10-17\">\n" +
                                                    "        <Rate curr=\"CHF\" unit=\"1\">72,65000</Rate>\n" +
                                                    "        <Rate curr=\"EUR\" unit=\"1\">232,23000</Rate>\n" +
                                                    "        <Rate curr=\"USD\" unit=\"1\">197,19000</Rate>\n" +
                                                    "    </Day>\n" +
                                                    "</MNBCurrentExchangeRates>"
                }
            });
    }
}