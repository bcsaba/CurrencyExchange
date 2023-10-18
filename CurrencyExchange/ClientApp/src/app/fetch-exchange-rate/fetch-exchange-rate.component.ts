import {Component} from '@angular/core';
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {ExchangeRates, ExchangeRatesService, Rate, RateWithComment} from "../exchange-rates.service";

@Component({
  selector: 'app-fetch-exchange-rate',
  templateUrl: './fetch-exchange-rate.component.html',
  styleUrls: ['./fetch-exchange-rate.component.css']
})
export class FetchExchangeRateComponent {
  public exchangeRates: ExchangeRates | undefined;
  public activeRate: RateWithComment = {
    exchangeDate: new Date(),
    currency: '',
    exchangeUnit: 0,
    valueStr: '',
    value: 0,
    comment: ''}

  constructor(
    private modalService: NgbModal,
    private exchangeRatesService: ExchangeRatesService) {
    this.exchangeRatesService.getCurrentMnbRates()
      .subscribe(
        {
          complete: () => console.log('Get MNB rates completed'),
          error: err => console.error(err),
          next: value => {
            this.exchangeRates = value;
            console.log(value);
          }
        },
      );
  }

  open(content: any, selectedRate: Rate) {
    this.activeRate = {
      ...selectedRate,
      comment: '',
      exchangeDate: this.exchangeRates!.day.exchangeDate};

    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title', size: 'lg'}
    ).result.then((result: any) => {
      // call service to save data
      console.trace(this.activeRate);
    }, (reason) => {
      console.log(reason);
    },
    );
  }
}
