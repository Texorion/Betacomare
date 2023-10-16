import { Component, DoCheck, OnInit } from '@angular/core';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { HttpService } from 'src/app/features/http.service';
import { ShoppingCartService } from 'src/app/shopping-cart.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  popUpDuration: number = 4 * 1000; // 4 sec
  message: string = "Mi stai abbandonato :(";
  username: string;

  constructor(private srv: HttpService, private router: Router, private _snackBar: MatSnackBar, public shopCart: ShoppingCartService) { }

  ngOnInit(): void {
    // ripulisce tutta la Session Storage all'inizio di una nuova sessione
    sessionStorage.clear();
  }

  /* -- LogOut -- */
  // pulisce credenziali SPA
  async clearCredentials(): Promise<void> {
    this.srv.setAuthorizeHttpHeaders('', '');
    this.router.navigate(['/login']);
    this._snackBar.open("Mi hai abbandonato :(", "Goodbye", {
      horizontalPosition: 'center',
      verticalPosition: 'top'
    });
    await new Promise(f => setTimeout(f, 1000)); // sleep 1 sec
    sessionStorage.clear();
    window.location.reload();
  }

  isLogged(): boolean {
    this.username = sessionStorage.getItem('username');
    return this.username != null;
  }
}
