import { Component, DoCheck } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../../http.service';
import { Customer } from '../../models/customer';
import { Address } from '../../models/adress';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { AddressGet } from '../../models/adressGet';


@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css'],
})
export class CustomersComponent {
  customerData: Customer;
  logCondition: boolean = false;
  addresses: AddressGet[] = [];
  displayedColumns: string[] = ['address', 'city', 'postalCode', 'stateProvince', 'countryRegion', 'state'];
  dataSource;


  constructor(private http: HttpClient, public src: HttpService, private router: Router, private _snackBar: MatSnackBar,) {
    if (sessionStorage.getItem('username') === null) {
      this.logCondition;
    } else {
      src.GetCustomer(sessionStorage.getItem('username')).subscribe(res => { this.customerData = res; this.logCondition = true });
      src.GetAllAddresses(sessionStorage.getItem('username')).subscribe(res => { this.dataSource = new MatTableDataSource<AddressGet>(res as AddressGet[]) });
    }
  }

  async DeleteUser() {
    this.src.DeleteCustomer(sessionStorage.getItem('username')).subscribe();
    this.src.DeleteCredetial(sessionStorage.getItem('username')).subscribe();
    this.src.setAuthorizeHttpHeaders('', '');
    this.router.navigate(['/register']);
    this._snackBar.open("Utente eliminato :(", "Goodbye", {
      horizontalPosition: 'center',
      verticalPosition: 'top'
    });
    await new Promise(f => setTimeout(f, 2000)); // sleep 1 sec
    sessionStorage.clear();
    window.location.reload();
  }


}
