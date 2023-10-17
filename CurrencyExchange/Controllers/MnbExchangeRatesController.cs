using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using www.mnb.hu.webservices;
using www.mnb.hu.webservices.Models;
using Formatting = Newtonsoft.Json.Formatting;

namespace CurrencyExchange.Controllers;

[ApiController]
[Route("[controller]")]
public class MnbExchangeRatesController : ControllerBase
{
    private readonly IMnbExchangeRateService _mnbExchangeRateService;

    public MnbExchangeRatesController(IMnbExchangeRateService mnbExchangeRateService)
    {
        _mnbExchangeRateService = mnbExchangeRateService;
    }

    [HttpGet]
    public async Task<JsonResult> Get()
    {
        GetExchangeRatesResponse getExchangeRatesResponse = await _mnbExchangeRateService.GetExchangeRatesAsync(
            new GetExchangeRatesRequestBody());

        var serializer = new XmlSerializer(typeof(MNBExchangeRates));
        MNBExchangeRates result;

        using (TextReader reader = new StringReader(getExchangeRatesResponse.GetExchangeRatesResponse1.GetExchangeRatesResult))
        {
            result = (MNBExchangeRates)serializer.Deserialize(reader);
        }

        Console.WriteLine(result);

        return new JsonResult(result);
    }

    [HttpPost]
    public async Task<JsonResult> Post([FromBody] GetExchangeRatesRequestBody body)
    {
        GetExchangeRatesResponse getExchangeRatesResponse = await _mnbExchangeRateService.GetExchangeRatesAsync(body);

        var serializer = new XmlSerializer(typeof(MNBExchangeRates));
        MNBExchangeRates result;

        using (TextReader reader =
               new StringReader(getExchangeRatesResponse.GetExchangeRatesResponse1.GetExchangeRatesResult))
        {
            result = (MNBExchangeRates)serializer.Deserialize(reader);
        }

        Console.WriteLine(result);

        return new JsonResult(result);
    }
}