import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { LoginModel } from '../models/LoginModel';
import { AccountService } from './account.service';
import { SignInForm } from './SignInForm';

@Injectable()
export class SignInSandbox {

  private loginModel: LoginModel;

  private subscriptions: Subscription[] = [];

  public constructor(
    private router: Router,
    private service: AccountService,
  ) {
  }

  public handleSignIn = (signForm: SignInForm, returnUrl: string): void => {
    let request = { ...this.loginModel, ...signForm.getFormData() } as LoginModel;
    request.returnUrl = returnUrl;
    debugger;
    this.subscriptions.push(this.service.login(request)
      .subscribe((response: string) => {
        debugger;
        console.log(response);
        const splits = response.split('/');
        console.log(splits);
        // TODO: MVV: should be change saved response (remove token)
        window.location.href = response;
      }, error => {
        console.log(error);
        debugger;
      }));
  };

  public dispose(): void {
    if (this.subscriptions) {
      this.subscriptions.forEach(
        subscription => {
          subscription.unsubscribe();
          subscription = null;
        }
      );
    }
  }

  public createForm(returnUrl: string): SignInForm {
    this.loginModel = new LoginModel();
    return SignInForm.createForm(this.loginModel, returnUrl);
  }

}
