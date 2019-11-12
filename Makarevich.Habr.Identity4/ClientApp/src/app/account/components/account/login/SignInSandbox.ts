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

  public handleSignIn(signForm: SignInForm): void {
    let request = { ...this.loginModel, ...signForm.getFormData() } as LoginModel;
    request.returnUrl = 'https://localhost:5001/home';
    debugger;
    this.subscriptions.push(this.service.login(request)
      .subscribe((response: any) => {
        debugger;
        console.log(response);
        // TODO: MVV: should be change saved response (remove token)
        this.router.navigate(['/', 'home']);
      }, error => {
        console.log(error);
        debugger;
      }));
    // this.subscriptions.push(this.storage.applicationStorage.getCurrentUser(true)
    //   .subscribe());
  }

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

  public createForm(): SignInForm {
    this.loginModel = new LoginModel();
    return SignInForm.createForm(this.loginModel);
  }

}