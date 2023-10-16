import { Directive } from '@angular/core';
import { AbstractControl, Validator, NG_VALIDATORS, FormGroup } from '@angular/forms';

@Directive({
  selector: '[appPswUpLetter]',
  providers: [{
    provide: NG_VALIDATORS,
    useExisting: PswUpLetterDirective,
    multi: true
  }]
})
export class PswUpLetterDirective implements Validator {
  /*
   * A valid password must have:
   *    - minimum 8 character
   *    - at least 1 uppercase letter
   *    - at least 1 lowercase letter
   *    - at least 1 digit
   */
  private regex: RegExp = /^(?=.*?[A-Z])/

  validate(control: AbstractControl): { [key: string]: any } | null {
    if (control.value && !this.regex.test(control.value)) {
      return { 'pswInvalidUpLetter': true };
    }
    
    return null;
  }
}