import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";

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
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private modalService: NgbModal )
  {
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

interface ExchangeRates {
  day: Day;
}

interface Day {
  exchangeDate: Date;
  rates: Rate[];
}

interface Rate {
  currency: string;
  exchangeUnit: number;
  valueStr: string;
  value: number;
}

interface RateWithComment extends Rate {
  comment: string;
  exchangeDate: Date;
}
