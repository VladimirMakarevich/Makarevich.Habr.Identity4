import { ChangeDetectionStrategy, Component, ElementRef, HostBinding, Input } from '@angular/core';

@Component({
  selector: 'app-form, [appForm]',
  template: `
    <ng-content></ng-content>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['./FormComponent.scss']
})
export class FormComponent {

  @HostBinding('class.okb-form') baseClass = true;

  @HostBinding('class.okb-form--view-mode') @Input() viewMode = false;

  constructor(
    private elementRef: ElementRef
  ) {
  }

  public htmlSubmit() {
    const element: HTMLFormElement = this.elementRef.nativeElement;

    if (element.submit) {
      element.submit();
    }
  }

}
