import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../../models/customer';
@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent {
  customers: Customer[];
  constructor(private http: HttpClient) {


    http.get('https://localhost:7032/api/Customers').subscribe(result => {
      this.customers = result as Customer[];
    })
  }

}
