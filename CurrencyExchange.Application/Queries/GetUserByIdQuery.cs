using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record GetUserByIdQuery(string userId) : IRequest<ApplicationUser>;