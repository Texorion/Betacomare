import { Directive } from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl, FormGroup } from '@angular/forms';

@Directive({
  selector: '[appPswMatch]',
  providers: [{
    provide: NG_VALIDATORS,
    useExisting: PswMatchDirective,
    multi: true
  }]
})
export class PswMatchDirective implements Validator {
  validate(control: AbstractControl): { [key: string]: any } | null {
    // console.log(control.value && (control.value.psw + " " + control.value.pswCheck));
    // console.log(control.value && control.value.psw !== control.value.pswCheck);
    if (control.value && control.value.psw !== control.value.pswCheck) {
      return { 'pswNotMatch': true };
    }

    return null;
  }
}