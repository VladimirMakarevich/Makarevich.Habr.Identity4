import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { SignInSandbox } from './SignInSandbox';
import { SignInForm } from './SignInForm';
import { ActivatedRoute, Router } from '@angular/router';
import { FormComponent } from '../../../shared/form/FormComponent';
import { ApplicationPaths, ReturnUrlType } from '../../../../api-authorization/api-authorization.constants';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [SignInSandbox]
})
export class LoginAccountComponent implements OnInit, OnDestroy {
  public returnValue: string;
  public signInForm: SignInForm;

  @ViewChild(FormComponent, { static: true })
  public form: FormComponent;

  public get actionLogin(): string {
    return '/api/account/login';
  }

  public constructor(
    public sandbox: SignInSandbox,
    public router: Router,
    private activatedRoute: ActivatedRoute,
    private elementRef: ElementRef
  ) {
  }

  public htmlCompSubmit() {
    debugger;
    this.form.htmlSubmit();
  }

  public ngOnInit(): void {
    this.returnValue = this.getReturnUrl();
    this.signInForm = this.sandbox.createForm(this.returnValue);
  }

  public ngOnDestroy(): void {
    this.sandbox.dispose();
  }

  public handleSignIn(event: Event): void {
    event.stopPropagation();
    if (!this.signInForm.validate) {
      return;
    }

    this.sandbox.handleSignIn(this.signInForm, this.returnValue);
  }

  public handleSignUp(): void {
    this.router.navigate(['/', 'home']);
  }

  public handleGoBack(): void {
    this.router.navigate(['/', 'home']);
  }

  public get isValidSignInForm(): boolean {
    return this.signInForm.validate();
  }

  private getReturnUrl(state?: INavigationState): string {
    const fromQuery = (this.activatedRoute.snapshot.queryParams as INavigationState).returnUrl;
    // If the url is comming from the query string, check that is either
    // a relative url or an absolute url
    if (fromQuery &&
      !(fromQuery.startsWith(`${window.location.origin}/`) ||
        /\/[^\/].*/.test(fromQuery))) {
      // This is an extra check to prevent open redirects.
      throw new Error('Invalid return url. The return url needs to have the same origin as the current page.');
    }
    return (state && state.returnUrl) ||
      fromQuery ||
      ApplicationPaths.DefaultLoginRedirectPath;
  }
}

interface INavigationState {
  [ReturnUrlType]: string;
}

