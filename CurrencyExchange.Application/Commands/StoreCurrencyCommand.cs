using CurrencyExchange.Persistence.Models;
using MediatR;

namespace CurrencyExchange.Application.Commands;

public record StoreCurrencyCommand(Currency currency) : IRequest<Currency>;