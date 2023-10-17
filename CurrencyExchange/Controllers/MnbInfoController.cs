using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using www.mnb.hu.webservices;
using www.mnb.hu.webservices.Models;

namespace CurrencyExchange.Controllers;

[ApiController]
[Route("[controller]")]
public class MnbInfoController : ControllerBase
{
    private readonly IMnbExchangeRateService _mnbExchangeRateService;

    public MnbInfoController(IMnbExchangeRateService mnbExchangeRateService)
    {
        _mnbExchangeRateService = mnbExchangeRateService;
    }

    [HttpGet]
    public async Task<JsonResult> Get()
    {
        var getInfoResponse = await _mnbExchangeRateService.GetInfoAsync();
        var infoResult = getInfoResponse.GetInfoResponse1.GetInfoResult;


        var serializer = new XmlSerializer(typeof(MNBExchangeRatesQueryValues));
        MNBExchangeRatesQueryValues result;

        using (TextReader reader =
               new StringReader(infoResult))
        {
            result = (MNBExchangeRatesQueryValues)serializer.Deserialize(reader);
        }


        return new JsonResult(result);
    }
}