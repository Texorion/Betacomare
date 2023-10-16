import { Directive } from '@angular/core';
import { AbstractControl, Validator, NG_VALIDATORS, FormGroup } from '@angular/forms';

@Directive({
  selector: '[appPswValidator]',
  providers: [{
    provide: NG_VALIDATORS,
    useExisting: PswValidatorDirective,
    multi: true
  }]
})
export class PswValidatorDirective implements Validator {
  /*
   * A valid password must have:
   *    - minimum 8 character
   *    - at least 1 uppercase letter
   *    - at least 1 lowercase letter
   *    - at least 1 digit
   */
    validate(control: AbstractControl): { [key: string]: any } | null {
    if (control.value && control.value.length < 8) {
      return { 'pswInvalidLength': true };
    }
    
    return null;
  }
}