using CurrencyExchange.Application.Models;
using MediatR;

namespace CurrencyExchange.Application.Queries;

public record ConvertCurrencyQuery(CurrencyConversionModel ConversionModel) : IRequest<CurrencyConversionModel>;