using CurrencyExchange.Application.Queries;
using CurrencyExchange.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CurrencyExchange.Application.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserByIdQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(query.userId);
        return user;
    }
}