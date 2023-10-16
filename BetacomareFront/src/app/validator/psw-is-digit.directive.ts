import { Directive } from '@angular/core';
import { AbstractControl, Validator, NG_VALIDATORS, FormGroup } from '@angular/forms';

@Directive({
  selector: '[appPswIsDigit]',
  providers: [{
    provide: NG_VALIDATORS,
    useExisting: PswIsDigitDirective,
    multi: true
  }]
})
export class PswIsDigitDirective implements Validator {
  /*
   * A valid password must have:
   *    - minimum 8 character
   *    - at least 1 uppercase letter
   *    - at least 1 lowercase letter
   *    - at least 1 digit
   */
  private regex: RegExp = /^(?=.*?[0-9])/;

  validate(control: AbstractControl): { [key: string]: any } | null {
    if (control.value && !this.regex.test(control.value)) {
      return { 'pswInvalidIsDigit': true };
    }
    
    return null;
  }
}