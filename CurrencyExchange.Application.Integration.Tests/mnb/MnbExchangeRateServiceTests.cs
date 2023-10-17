using FluentAssertions;
using www.mnb.hu.webservices;

namespace CurrencyExchange.Application.Integration.Tests.mnb;

public class MnbExchangeRateServiceTests
{
    private readonly MNBArfolyamServiceSoap _mnbArfolyamServiceSoap;
    private IMnbExchangeRateService _mnbExchangeRateService;

    public MnbExchangeRateServiceTests()
    {
        _mnbArfolyamServiceSoap = new MNBArfolyamServiceSoapClient();
        _mnbExchangeRateService = new MnbExchangeRateService(_mnbArfolyamServiceSoap);
    }

    [Fact]
    public async Task GivenNonNullResponseObject_WhenRequestInfoAsync_ResultShouldNotBeNull()
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
            .MatchRegex("<FirstDate>\\d{4}-\\d{2}-\\d{2}</FirstDate>");
        infoResult
            .Should()
            .MatchRegex("<LastDate>\\d{4}-\\d{2}-\\d{2}</LastDate>");
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
}