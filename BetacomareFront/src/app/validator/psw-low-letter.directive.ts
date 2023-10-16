import { Directive } from '@angular/core';
import { AbstractControl, Validator, NG_VALIDATORS, FormGroup } from '@angular/forms';

@Directive({
  selector: '[appPswLowLetter]',
  providers: [{
    provide: NG_VALIDATORS,
    useExisting: PswLowLetterDirective,
    multi: true
  }]
})
export class PswLowLetterDirective implements Validator {
  /*
   * A valid password must have:
   *    - minimum 8 character
   *    - at least 1 uppercase letter
   *    - at least 1 lowercase letter
   *    - at least 1 digit
   */
  private regex: RegExp = /^(?=.*?[a-z])/

  validate(control: AbstractControl): { [key: string]: any } | null {
    if (control.value && !this.regex.test(control.value)) {
      return { 'pswInvalidLowLetter': true };
    }
    
    return null;
  }
}
