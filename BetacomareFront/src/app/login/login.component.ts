import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpStatusCode } from '@angular/common/http';
import { HttpService } from '../features/http.service';
import { Login } from '../features/models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  hide = true; // true: maschera password ; false: 
  loading = false; // flag per presenza o meno della rotella di caricamento
  returnMessage: string;

  constructor(private srv: HttpService, private router: Router) { }

  checkCredentials(frm) {
    console.log("checkCredential");
    // http.service effettua il login col metodo LoginBack(login)
    this.srv.LoginBack(frm.value)
      .subscribe((resp) => { // ok
        // autorizza
        this.srv.setAuthorizeHttpHeaders(frm.value.user, frm.value.psw)

        // verifica che il codice restituito dalla chiamata senza errori sia OK(200)
        switch (resp.status) {
          case HttpStatusCode.Ok:
            this.router.navigate(['/customer']);
            this.loading = false;
            this.returnMessage = null;
            sessionStorage.setItem('username', frm.value.user);
            break;
        }
      },
        // gestione errori dal Back End
        err => {
          switch (err.status) {
            case HttpStatusCode.InternalServerError:
              this.returnMessage = "Internal Server Error (500)";
              break;
            case HttpStatusCode.Unauthorized:
              this.returnMessage = "Login failed; Invalid user ID or password"
              this.loading = false;
              break;
            default:
              this.loading = false;
              this.returnMessage = "Generic Error"
              break;
          }
        }
      );
  }

  checkEnterSubmit(event: KeyboardEvent, frm){
    if(event.key === 'Enter'){
      this.loading = !this.loading;
      this.checkCredentials(frm);
    }
  }


}
