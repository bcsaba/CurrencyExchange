import {Component, OnDestroy, TemplateRef} from '@angular/core';
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {ExchangeRates, ExchangeRatesService, Rate, RateWithComment} from "../exchange-rates.service";
import {ToastService} from "../toast.service";

@Component({
  selector: 'app-fetch-exchange-rate',
  templateUrl: './fetch-exchange-rate.component.html',
  styleUrls: ['./fetch-exchange-rate.component.css']
})
export class FetchExchangeRateComponent implements OnDestroy {
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
    private exchangeRatesService: ExchangeRatesService,
    public toastService: ToastService
  ) {
    this.exchangeRatesService.getCurrentMnbRates()
      .subscribe(
        {
          complete: () => console.log('Get MNB rates completed'),
          error: err => console.error(err),
          next: value => {
            // this.showSuccess('MNB rates loaded');
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
      this.exchangeRatesService.saveRateWithComment(this.activeRate)
        .subscribe(
          {
            complete: () => console.log('Save rate completed'),
            error: err => {
              this.showDanger('Save of exchange rate failed!');
              console.error(err)
            },
            next: value => {
              this.showSuccess('Exchange rate saved successfully');
              console.log(value);
            }
          }
        )
      console.trace(this.activeRate);
    }, (reason) => {
      console.log(reason);
    },
    );
  }

  showStandard(textOrTpl: string | TemplateRef<any>) {
    this.toastService.show(textOrTpl);
  }

  showSuccess(textOrTpl: string | TemplateRef<any>) {
    this.toastService.show(textOrTpl, { classname: 'bg-success text-light', delay: 10000 });
  }

  showDanger(textOrTpl: string | TemplateRef<any>) {
    this.toastService.show(textOrTpl, { classname: 'bg-danger text-light', delay: 15000 });
  }

  ngOnDestroy() {
    this.toastService.clear();
  }
}
