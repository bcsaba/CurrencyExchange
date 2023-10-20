import { Component } from '@angular/core';
import {CurrencyConversionModel, ExchangeRatesService} from "../exchange-rates.service";

@Component({
  selector: 'app-huf-converter',
  templateUrl: './huf-converter.component.html',
  styleUrls: ['./huf-converter.component.css']
})
export class HufConverterComponent {
  public amount: number = 1;

  constructor(
    private exchangeRateService: ExchangeRatesService,
  ) {
  }

  convert() {
    let currencyConversionModel: CurrencyConversionModel = {
      fromCurrency: "HUF",
      fromAmount: this.amount,
      toCurrency: "EUR"};

    this.exchangeRateService.convertHufToEur(currencyConversionModel)
      .subscribe(
        {
          complete: () => console.log('HUF conversion completed'),
          error: err => console.error(err),
          next: value => {
            console.log(value);
          }
        }
      );
  }
}
