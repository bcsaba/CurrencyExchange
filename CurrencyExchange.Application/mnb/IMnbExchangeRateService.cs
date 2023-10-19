namespace www.mnb.hu.webservices;

public interface IMnbExchangeRateService
{
    Task<GetInfoResponse> GetInfoAsync();
    Task<GetCurrentExchangeRatesResponse> GetCurrentExchangeRatesAsync(GetCurrentExchangeRatesRequestBody body);
}