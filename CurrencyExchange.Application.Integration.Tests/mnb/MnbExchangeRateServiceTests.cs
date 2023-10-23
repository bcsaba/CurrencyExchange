using CurrencyExchange.Application.mnb;
using FluentAssertions;
using www.mnb.hu.webservices;

namespace CurrencyExchange.Application.Integration.Tests.mnb;

public class MnbExchangeRateServiceTests
{
    private readonly IMnbExchangeRateService _mnbExchangeRateService;

    public MnbExchangeRateServiceTests()
    {
        MNBArfolyamServiceSoap mnbArfolyamServiceSoap = new MNBArfolyamServiceSoapClient();
        _mnbExchangeRateService = new MnbExchangeRateService(mnbArfolyamServiceSoap);
    }

    [Fact]
    public async Task WhenRequestInfoAsync_ResultShouldNotBeNull()
    {
        var infoAsync = await _mnbExchangeRateService.GetInfoAsync();

        infoAsync.Should().NotBeNull();
    }

    [Fact]
    public async Task WhenRequestInfoAsync_ResultShouldContainMNBExchangeRatesQueryValuesRootElement()
    {
        var infoAsync = await _mnbExchangeRateService.GetInfoAsync();

        infoAsync.GetInfoResponse1.GetInfoResult
            .Should().Contain("<MNBExchangeRatesQueryValues>");
    }

    [Fact]
    public async Task WhenRequestInfoAsync_ResultShouldContainFirstAndLastDate()
    {
        var infoAsync = await _mnbExchangeRateService.GetInfoAsync();

        var infoResult = infoAsync.GetInfoResponse1.GetInfoResult;
        infoResult
            .Should()
            .MatchRegex(@"<FirstDate>\d{4}-\d{2}-\d{2}</FirstDate>");
        infoResult
            .Should()
            .MatchRegex(@"<LastDate>\d{4}-\d{2}-\d{2}</LastDate>");
    }

    [Fact]
    public async Task WhenRequestInfoAsync_ResultShouldContainCommonCurrencyNames()
    {
        var infoAsync = await _mnbExchangeRateService.GetInfoAsync();

        var infoResult = infoAsync.GetInfoResponse1.GetInfoResult;
        infoResult.Should().Contain("<Currencies>");
        infoResult.Should().Contain("<Curr>EUR</Curr>");
        infoResult.Should().Contain("<Curr>USD</Curr>");
        infoResult.Should().Contain("<Curr>CHF</Curr>");
    }

    [Fact]
    public async Task WhenRequestCurrentExchangeRates_ResultShouldNotBeNull()
    {
        var infoAsync = await _mnbExchangeRateService.GetInfoAsync();

        infoAsync.Should().NotBeNull();
    }

    [Fact]
    public async Task WhenRequestCurrentExchangeRates_ResultShouldContainMNBCurrentExchangeRatesRootElement()
    {
        var infoAsync = 
            await _mnbExchangeRateService.GetCurrentExchangeRatesAsync(new GetCurrentExchangeRatesRequestBody());

        infoAsync.GetCurrentExchangeRatesResponse1.GetCurrentExchangeRatesResult
            .Should().Contain("<MNBCurrentExchangeRates>");
    }

    [Fact]
    public async Task WhenRequestCurrentExchangeRates_ResultShouldContainDateElement()
    {
        var infoAsync = 
            await _mnbExchangeRateService.GetCurrentExchangeRatesAsync(new GetCurrentExchangeRatesRequestBody());

        infoAsync.GetCurrentExchangeRatesResponse1.GetCurrentExchangeRatesResult
            .Should().MatchRegex("<Day date=\"\\d{4}-\\d{2}-\\d{2}\">");
    }

    [Fact]
    public async Task WhenRequestCurrentExchangeRates_ResultShouldContainCommonCurrencies()
    {
        var infoAsync = 
            await _mnbExchangeRateService.GetCurrentExchangeRatesAsync(new GetCurrentExchangeRatesRequestBody());

        infoAsync.GetCurrentExchangeRatesResponse1.GetCurrentExchangeRatesResult
            .Should().MatchRegex($"<Rate unit=\"1\" curr=\"EUR\">\\d*,\\d*</Rate>");
        infoAsync.GetCurrentExchangeRatesResponse1.GetCurrentExchangeRatesResult
            .Should().MatchRegex($"<Rate unit=\"1\" curr=\"USD\">\\d*,\\d*</Rate>");
        infoAsync.GetCurrentExchangeRatesResponse1.GetCurrentExchangeRatesResult
            .Should().MatchRegex($"<Rate unit=\"1\" curr=\"CHF\">\\d*,\\d*</Rate>");
    }
}