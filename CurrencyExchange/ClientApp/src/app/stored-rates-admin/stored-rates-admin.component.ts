import { Component } from '@angular/core';
import {ExchangeRates, ExchangeRatesService, RateWithComment} from "../exchange-rates.service";

@Component({
  selector: 'app-stored-rates-admin',
  templateUrl: './stored-rates-admin.component.html',
  styleUrls: ['./stored-rates-admin.component.css']
})
export class StoredRatesAdminComponent {
  public storedRates: RateWithComment[] = [];

  constructor(private exchangeRatesService: ExchangeRatesService) {
    this.exchangeRatesService.getStoredRates()
      .subscribe(
        {
          complete: () => console.log('Get stored rates completed'),
          error: err => console.error(err),
          next: value => {
            this.storedRates = value;
            console.log(value);
          }
        },
      )
  }
}
