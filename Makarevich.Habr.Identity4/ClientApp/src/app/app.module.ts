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
import { LoginAccountComponent } from './account/components/account/login/login-account.component';
import { ForbiddenComponent } from './account/components/forbidden/forbidden.component';
import { UnauthorizedComponent } from './account/components/unauthorized/unauthorized.component';
import { InputComponent } from './account/shared/input/input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormComponent } from './account/shared/form/FormComponent';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DataComponent,
    ExchangeRateComponent,
    LoginAccountComponent,
    ForbiddenComponent,
    UnauthorizedComponent,
    InputComponent,
    FormComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
    ApiAuthorizationModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
