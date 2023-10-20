using CurrencyExchange.Application.Commands;
using CurrencyExchange.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Controllers;

[ApiController]
[Route("[controller]")]
public class StoredExchangeRate : ControllerBase
{
    private IMediator _mediator;

    public StoredExchangeRate(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<JsonResult> Post([FromBody] ExchangeRateWithComment exchangeRateWithComment)
    {
        var savedRate = await _mediator.Send(new StoreCurrencyRateCommand(exchangeRateWithComment));

        return new JsonResult(savedRate);
    }
}