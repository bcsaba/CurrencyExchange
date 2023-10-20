import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, Observable, of} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ExchangeRatesService {

  constructor(
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string) { }

  getCurrentMnbRates() : Observable<ExchangeRates> {
    return this.http.get<ExchangeRates>(this.baseUrl + 'mnbcurrentexchangerates')
      .pipe(catchError(this.handleError<ExchangeRates>('getCurrentMnbRates', undefined)));
  }

  saveRateWithComment(rateWithComment: RateWithComment) : Observable<RateWithComment> {
    return this.http.post<RateWithComment>(
      this.baseUrl + 'storedexchangerate', rateWithComment,
      {headers: {'Content-Type': 'application/json'}});
    // .pipe(catchError(this.handleError<RateWithComment>('saveRateWithComment', undefined)));
  }

  convertHufToEur(conversionModel: CurrencyConversionModel) : Observable<CurrencyConversionModel> {
    return this.http.post<CurrencyConversionModel>(
      this.baseUrl + 'currencyconversion', conversionModel,
      {headers: {'Content-Type': 'application/json'}});
  }

  getStoredRates() : Observable<RateWithComment[]> {
    return this.http.get<RateWithComment[]>(this.baseUrl + 'storedexchangerate')
      .pipe(catchError(this.handleError<RateWithComment[]>('getStoredRates', [])));
  }

  handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
}


export interface ExchangeRates {
  day: Day;
}

export interface Day {
  exchangeDate: Date;
  rates: Rate[];
}

export interface Rate {
  currency: string;
  exchangeUnit: number;
  valueStr: string;
  value: number;
}

export interface RateWithComment extends Rate {
  comment: string;
  exchangeDate: Date;
}


export interface CurrencyConversionModel {
  fromCurrency: string;
  fromAmount: number;
  toCurrency: string;
  toAmount?: number;
}
