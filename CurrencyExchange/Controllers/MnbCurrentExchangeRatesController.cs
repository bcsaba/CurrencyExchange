using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using www.mnb.hu.webservices;
using www.mnb.hu.webservices.Models;

namespace CurrencyExchange.Controllers;

[ApiController]
[Route("[controller]")]
public class MnbCurrentExchangeRatesController : ControllerBase
{
    private readonly IMnbExchangeRateService _mnbExchangeRateService;

    public MnbCurrentExchangeRatesController(IMnbExchangeRateService mnbExchangeRateService)
    {
        _mnbExchangeRateService = mnbExchangeRateService;
    }

    [HttpGet]
    public async Task<JsonResult> Get()
    {
        var getCurrentExchangeRates = await _mnbExchangeRateService.GetCurrentExchangeRatesAsync(new GetCurrentExchangeRatesRequestBody());
        var currentExchangeRatesResponse = getCurrentExchangeRates.GetCurrentExchangeRatesResponse1.GetCurrentExchangeRatesResult;


        var serializer = new XmlSerializer(typeof(MNBCurrentExchangeRates));
        MNBCurrentExchangeRates result;

        using (TextReader reader = new StringReader(currentExchangeRatesResponse))
        {
            result = (MNBCurrentExchangeRates)serializer.Deserialize(reader);
        }

        return new JsonResult(result);
    }
}