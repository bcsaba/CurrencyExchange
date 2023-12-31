using System.Security.Claims;
using CurrencyExchange.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Controllers;

[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET
    [HttpGet]
    [Route("[controller]")]
    public async Task<JsonResult> Get()
    {
        return new JsonResult(await _mediator.Send(new GetLocalCurrenciesQuery()));
    }

    [HttpGet]
    [Route("[controller]/{id:int}")]
    public async Task<JsonResult> Get(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var applicationUser = await _mediator.Send(new GetUserByIdQuery(userId));

        return new JsonResult(await _mediator.Send(new GetLocalCurrencyByIdQuery(applicationUser, id)));
    }
}