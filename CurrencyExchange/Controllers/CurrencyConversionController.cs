using CurrencyExchange.Application.Models;
using CurrencyExchange.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyConversionController : Controller
{
    private readonly IMediator _mediator;

    public CurrencyConversionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<JsonResult> Post([FromBody] CurrencyConversionModel conversionModel)
    {
        var result = await _mediator.Send(new ConvertCurrencyQuery(conversionModel));

        return new JsonResult(result);
    }
}