using System.Runtime.InteropServices.JavaScript;

namespace www.mnb.hu.webservices;

public interface IMnbExchangeRateService
{
    Task<GetExchangeRatesResponse> GetExchangeRatesAsync(GetExchangeRatesRequestBody body);
}

public class MnbExchangeRateService : IMnbExchangeRateService
{
    private readonly MNBArfolyamServiceSoap _mnbArfolyamServiceSoap;

    public MnbExchangeRateService(MNBArfolyamServiceSoap mnbArfolyamServiceSoap)
    {
        _mnbArfolyamServiceSoap = mnbArfolyamServiceSoap;
    }

    public async Task<GetExchangeRatesResponse> GetExchangeRatesAsync(GetExchangeRatesRequestBody body)
    {
        var exchangeRatesRequest = new GetExchangeRatesRequest(body);
        var getExchangeRatesResponse = await _mnbArfolyamServiceSoap.GetExchangeRatesAsync(exchangeRatesRequest);
        return getExchangeRatesResponse;
    }
}