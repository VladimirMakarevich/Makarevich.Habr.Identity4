import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { DataComponent } from './data/data.component';
import { ApiAuthorizationModule } from './api-authorization/api-authorization.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ExchangeRateComponent } from './exchange-rate/exchange-rate.component';
import { AuthorizeInterceptor } from './api-authorization/authorize.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DataComponent,
    ExchangeRateComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ApiAuthorizationModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
