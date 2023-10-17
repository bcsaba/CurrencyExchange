using FluentAssertions;
using NSubstitute;
using www.mnb.hu.webservices;

namespace CurrencyExchange.Application.Tests.mnb;

public class MnbExchangeRateServiceTests
{
    private readonly MNBArfolyamServiceSoap _mnbArfolyamServiceSoap;
    private IMnbExchangeRateService _mnbExchangeRateService;

    public MnbExchangeRateServiceTests()
    {
        _mnbArfolyamServiceSoap = Substitute.For<MNBArfolyamServiceSoap>();
        _mnbExchangeRateService = new MnbExchangeRateService(_mnbArfolyamServiceSoap);
    }

    [Fact]
    public async Task WhenRequestInfoAsync_SoapCallUsedToRetrieveInformation()
    {
        _mnbArfolyamServiceSoap.GetInfoAsync(Arg.Any<GetInfoRequest>())
            .Returns(new GetInfoResponse());

        var infoAsync = await _mnbExchangeRateService.GetInfoAsync();

        await _mnbArfolyamServiceSoap.Received(1)
            .GetInfoAsync(Arg.Any<GetInfoRequest>());
    }

    [Fact]
    public async Task GivenNonNullResponseObject_WhenRequestInfoAsync_ResultShouldNotBeNull()
    {
        _mnbArfolyamServiceSoap.GetInfoAsync(Arg.Any<GetInfoRequest>())
            .Returns(new GetInfoResponse());

        var infoAsync = await _mnbExchangeRateService.GetInfoAsync();

        infoAsync.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenResponseObjectWithXml_WhenRequestInfoAsync_ResultShouldContainExpectedXml()
    {
        var xmlResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>\n" +
                                   "<MNBExchangeRatesQueryValues>\n" +
                                   "    <FirstDate>1949-01-03</FirstDate>\n" +
                                   "    <LastDate>2023-10-17</LastDate>\n" +
                                   "    <Currencies>\n" +
                                   "        <Curr>HUF</Curr>\n" +
                                   "        <Curr>EUR</Curr>\n" +
                                   "        <Curr>AUD</Curr>\n" +
                                   "        <Curr>XEU</Curr>\n" +
                                   "        <Curr>XTR</Curr>\n" +
                                   "        <Curr>YUD</Curr>\n" +
                                   "    </Currencies>\n" +
                                   "</MNBExchangeRatesQueryValues>\n";

        GetInfoResponseBody getInfoResponseBody = new()
        {
            GetInfoResult = xmlResponse
        };

        _mnbArfolyamServiceSoap
            .GetInfoAsync(Arg.Any<GetInfoRequest>())
            .Returns(new GetInfoResponse
            {
                GetInfoResponse1 = new GetInfoResponseBody
                {
                    GetInfoResult = xmlResponse
                }
            });

        var infoAsync = await _mnbExchangeRateService.GetInfoAsync();

        infoAsync.GetInfoResponse1.GetInfoResult.Should().Be(xmlResponse);
    }
}