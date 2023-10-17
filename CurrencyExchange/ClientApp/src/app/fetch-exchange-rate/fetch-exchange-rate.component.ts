import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-fetch-exchange-rate',
  templateUrl: './fetch-exchange-rate.component.html',
  styleUrls: ['./fetch-exchange-rate.component.css']
})
export class FetchExchangeRateComponent {
  public exchangeRates: ExchangeRates | undefined;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<ExchangeRates>(baseUrl + 'mnbcurrentexchangerates').subscribe(
      {complete: () => console.log('complete'),
        error: err => console.error(err),
        next: value => {
          this.exchangeRates = value;
          console.log(this.exchangeRates);
        }
      },
    );
  }

}

interface ExchangeRates {
  day: Day;
}

interface Day {
  exchangeDate: string;
  rates: Rate[];
}

interface Rate {
  currency: string;
  exchangeUnit: number;
  value: number;
}
