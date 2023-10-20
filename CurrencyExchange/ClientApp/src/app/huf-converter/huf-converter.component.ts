import { Component } from '@angular/core';
import {CurrencyConversionModel, ExchangeRatesService} from "../exchange-rates.service";

@Component({
  selector: 'app-huf-converter',
  templateUrl: './huf-converter.component.html',
  styleUrls: ['./huf-converter.component.css']
})
export class HufConverterComponent {
  public fromAmount: number = 1;
  public toAmount: number | undefined;

  constructor(
    private exchangeRateService: ExchangeRatesService,
  ) {
  }

  convert() {
    let currencyConversionModel: CurrencyConversionModel = {
      fromCurrency: "HUF",
      fromAmount: this.fromAmount,
      toCurrency: "EUR"};

    this.exchangeRateService.convertHufToEur(currencyConversionModel)
      .subscribe(
        {
          complete: () => console.log('HUF conversion completed'),
          error: err => console.error(err),
          next: value => {
            this.toAmount = value.toAmount;
            console.log(value);
          }
        }
      );
  }
}
