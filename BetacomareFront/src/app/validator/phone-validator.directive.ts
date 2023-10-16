import { Directive } from '@angular/core';
import { AbstractControl, Validator, NG_VALIDATORS } from '@angular/forms';

@Directive({
  selector: '[appPhoneValidator]',
  providers: [{
    provide: NG_VALIDATORS,
    useExisting: PhoneValidatorDirective,
    multi: true
  }]
})
export class PhoneValidatorDirective implements Validator {
  private regex: RegExp = /[0-9]{10}/;

  /*
   * return
   *    - an object, with a name of error, if the input value not match
   *    - null if the input value match
   */
  validate(control: AbstractControl): { [key: string]: any } | null {
    // if(false) => valid number

    /* It is not a valid number */
    if (control.value && control.value.length != 10) {
      // true => appare il messaggio di errore in mat-error con *ngIf
      return { 'phoneLengthInvalid': true };
    } else if (control.value && !this.regex.test(control.value)) {
      // return true if a string matched to only 10 digits
      return { 'phoneNumberInvalid': true };
    }
    
    /* It is a valid number */
    return null;
  }
}
