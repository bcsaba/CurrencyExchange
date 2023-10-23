import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { FetchExchangeRateComponent } from "./fetch-exchange-rate/fetch-exchange-rate.component";
import {NgbModule} from "@ng-bootstrap/ng-bootstrap";
import {ToastsContainer} from "./toasts-container/toasts-container.component";
import { HufConverterComponent } from './huf-converter/huf-converter.component';
import { StoredRatesAdminComponent } from './stored-rates-admin/stored-rates-admin.component';
import { ApiAuthorizationModule } from "../api-authorization/api-authorization.module";
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    FetchExchangeRateComponent,
    HufConverterComponent,
    StoredRatesAdminComponent
  ],
    imports: [
        BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
        HttpClientModule,
        FormsModule,
        NgbModule,
        ApiAuthorizationModule,
        RouterModule.forRoot([
            {path: '', component: HomeComponent, pathMatch: 'full'},
            {path: 'counter', component: CounterComponent},
            {path: 'huf-converter', component: HufConverterComponent, canActivate: [AuthorizeGuard] },
            {path: 'fetch-exchange-rate', component: FetchExchangeRateComponent, canActivate: [AuthorizeGuard] },
            {path: 'stored-rates-admin', component: StoredRatesAdminComponent, canActivate: [AuthorizeGuard] }
        ]),
        ToastsContainer
    ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
