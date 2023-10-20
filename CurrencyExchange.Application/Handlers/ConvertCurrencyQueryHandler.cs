using System.Xml.Serialization;
using CurrencyExchange.Application.mnb;
using CurrencyExchange.Application.Models;
using CurrencyExchange.Application.Queries;
using MediatR;
using www.mnb.hu.webservices;
using www.mnb.hu.webservices.Models;

namespace CurrencyExchange.Application.Handlers;

public class ConvertCurrencyQueryHandler : IRequestHandler<ConvertCurrencyQuery, CurrencyConversionModel>
{
    private readonly IMediator _mediator;
    private readonly IMnbExchangeRateService _mnbExchangeRateService;

    public ConvertCurrencyQueryHandler(IMediator mediator, IMnbExchangeRateService mnbExchangeRateService)
    {
        _mediator = mediator;
        _mnbExchangeRateService = mnbExchangeRateService;
    }

    public async Task<CurrencyConversionModel> Handle(ConvertCurrencyQuery request, CancellationToken cancellationToken)
    {
        var getCurrentExchangeRates =
            await _mnbExchangeRateService.GetCurrentExchangeRatesAsync(new GetCurrentExchangeRatesRequestBody());
        var currentExchangeRatesResponse =
            getCurrentExchangeRates.GetCurrentExchangeRatesResponse1.GetCurrentExchangeRatesResult;

        var serializer = new XmlSerializer(typeof(MNBCurrentExchangeRates));
        MNBCurrentExchangeRates result;

        using (TextReader reader = new StringReader(currentExchangeRatesResponse))
        {
            result = (MNBCurrentExchangeRates)serializer.Deserialize(reader);
        }

        var fromCurrency = result.Day.Rates.FirstOrDefault(x => x.Currency == request.ConversionModel.ToCurrency).Value;

        request.ConversionModel.ToAmount = (float)fromCurrency * request.ConversionModel.FromAmount;

        return request.ConversionModel;
    }
}