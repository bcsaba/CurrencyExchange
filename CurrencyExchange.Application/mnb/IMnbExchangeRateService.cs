using www.mnb.hu.webservices;

namespace CurrencyExchange.Application.mnb;

public interface IMnbExchangeRateService
{
    Task<GetInfoResponse> GetInfoAsync();
    Task<GetCurrentExchangeRatesResponse> GetCurrentExchangeRatesAsync(GetCurrentExchangeRatesRequestBody body);
}