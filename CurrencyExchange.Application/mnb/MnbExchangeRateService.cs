using System.Runtime.InteropServices.JavaScript;

namespace www.mnb.hu.webservices;

public interface IMnbExchangeRateService
{
    Task<GetExchangeRatesResponse> GetExchangeRatesAsync(GetExchangeRatesRequestBody body);
    Task<GetInfoResponse> GetInfoAsync();

    Task<GetCurrentExchangeRatesResponse> GetCurrentExchangeRatesAsync(GetCurrentExchangeRatesRequestBody body);
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

    public async Task<GetInfoResponse> GetInfoAsync()
    {
        var infoRequest = new GetInfoRequest(new GetInfoRequestBody());
        var getInfoResponse = await _mnbArfolyamServiceSoap.GetInfoAsync(infoRequest);
        return getInfoResponse;
    }

    public async Task<GetCurrentExchangeRatesResponse> GetCurrentExchangeRatesAsync(GetCurrentExchangeRatesRequestBody body)
    {
        var currentExchangeRatesRequest = new GetCurrentExchangeRatesRequest(body);
        var getCurrentExchangeRatesResponse = await _mnbArfolyamServiceSoap.GetCurrentExchangeRatesAsync(currentExchangeRatesRequest);
        return getCurrentExchangeRatesResponse;
    }
}