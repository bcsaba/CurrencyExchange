using CurrencyExchange.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Controllers;

[ApiController]
[Route("[controller]")]
public class StoredExchangeRate : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] ExchangeRateWithComment exchangeRateWithComment)
    {
        return Ok();
    }
}