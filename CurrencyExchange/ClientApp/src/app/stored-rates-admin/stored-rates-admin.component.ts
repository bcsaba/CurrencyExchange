import {Component, OnDestroy, OnInit, TemplateRef} from '@angular/core';
import {ExchangeRates, ExchangeRatesService, RateWithComment} from "../exchange-rates.service";
import {ToastService} from "../toast.service";
import {NgxSpinnerService} from "ngx-spinner";

@Component({
  selector: 'app-stored-rates-admin',
  templateUrl: './stored-rates-admin.component.html',
  styleUrls: ['./stored-rates-admin.component.css']
})
export class StoredRatesAdminComponent implements OnInit, OnDestroy {
  public storedRates: StoredDateEdit[] = [];

  constructor(
    private exchangeRatesService: ExchangeRatesService,
    private toastService: ToastService,
    private spinnerService: NgxSpinnerService
  ) { }


  ngOnInit(): void {
    this.spinnerService.show().then(r => {
      this.exchangeRatesService.getStoredRates()
        .subscribe(
          {
            complete: () => {
              console.log('Get stored rates completed');
            },
            error: err => {
              console.error(err);
              this.spinnerService.hide();
            },
            next: value => {
              this.spinnerService.hide()
              this.storedRates = value.map(r =>
                ({...r, editMode: false, initialComment: r.comment}));
              console.log(value);
            }
          },
        )
    });
  }

  updateStoredRate(exchangeRate: StoredDateEdit) {
    this.exchangeRatesService.saveRateWithComment(exchangeRate)
      .subscribe(
        {
          complete: () => console.log('Update stored rate completed'),
          error: err => console.error(err),
          next: value => {
            this.showSuccess(`Exchange rate for ${value.currency} updated successfully`);
            let index = this.storedRates.findIndex(
              r => r.currency === value.currency && r.exchangeDate === value.exchangeDate);
            if (index >= 0) {
              console.log(`replacing item (${value.currency}, ${value.exchangeDate})`);
              this.storedRates[index] = {...value, editMode: false, initialComment: value.comment};
            }
            console.log(value);
          }
        },
      )
  }

  showSuccess(textOrTpl: string | TemplateRef<any>) {
    this.toastService.show(textOrTpl, { classname: 'bg-success text-light', delay: 10000 });
  }

  showDanger(textOrTpl: string | TemplateRef<any>) {
    this.toastService.show(textOrTpl, { classname: 'bg-danger text-light', delay: 15000 });
  }

  ngOnDestroy(): void {
    this.toastService.clear();
  }
}

interface StoredDateEdit extends RateWithComment {
  editMode: boolean;
  initialComment: string;
};
