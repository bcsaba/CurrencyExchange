import {Component, TemplateRef} from '@angular/core';
import {CurrencyConversionModel, ExchangeRatesService} from "../exchange-rates.service";
import {ToastService} from "../toast.service";

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
    public toastService: ToastService
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
          error: err => {
            this.toastService.show('Conversion failed! Try again, please!', { classname: 'bg-danger text-light', delay: 15000 });
            },
          next: value => {
            this.toAmount = value.toAmount;
          }
        }
      );
  }
}
