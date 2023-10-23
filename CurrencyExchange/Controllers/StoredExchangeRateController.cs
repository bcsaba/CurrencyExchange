using System.Security.Claims;
using CurrencyExchange.Application.Commands;
using CurrencyExchange.Application.Models;
using CurrencyExchange.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class StoredExchangeRateController : ControllerBase
{
    private readonly IMediator _mediator;

    public StoredExchangeRateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<JsonResult> Get()
    {
        var storedRates = await _mediator.Send(new GetStoredRatesQuery());
        return new JsonResult(storedRates);
    }

    [HttpPost]
    public async Task<JsonResult> Post([FromBody] ExchangeRateWithComment exchangeRateWithComment)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

        var savedRate = await _mediator.Send(new StoreCurrencyRateCommand(exchangeRateWithComment, userId));

        return new JsonResult(savedRate);
    }
}