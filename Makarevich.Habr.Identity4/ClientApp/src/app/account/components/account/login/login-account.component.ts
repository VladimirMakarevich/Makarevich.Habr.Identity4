import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { SignInSandbox } from './SignInSandbox';
import { SignInForm } from './SignInForm';
import { Router } from '@angular/router';
import { FormComponent } from '../../../shared/form/FormComponent';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [SignInSandbox]
})
export class LoginAccountComponent implements OnInit, OnDestroy {
  public errorMessage: string;
  public signInForm: SignInForm;

  @ViewChild(FormComponent, { static: true })
  public form: FormComponent;

  public get actionLogin(): string {
    return '/api/account/login';
  }

  public constructor(
    public sandbox: SignInSandbox,
    public router: Router,
    private elementRef: ElementRef
  ) {
  }

  public htmlSubmit() {
    debugger;
    this.form.htmlSubmit();
  }

  public ngOnInit(): void {
    this.signInForm = this.sandbox.createForm();
  }

  public ngOnDestroy(): void {
    this.sandbox.dispose();
  }

  public handleSignIn(event: Event): void {
    event.stopPropagation();
    if (!this.signInForm.validate) {
      return;
    }

    this.sandbox.handleSignIn(this.signInForm);
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
}
