import { HttpClient } from '@angular/common/http';
import { Component, DoCheck } from '@angular/core';
import { HttpService } from '../http.service';
import { Address } from '../models/adress';
import { PageEvent } from '@angular/material/paginator';


@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.css']
})
export class AddressComponent implements DoCheck {
  address: Address[] = [];
  addressesOnPage: Address[];

  // Addresses per pag del paginator
  startIndex: number = 0;
  endIndex: number = 10;

  constructor(private http: HttpClient, public httpsrc: HttpService) {
    httpsrc.GetAll('Addresses').subscribe((result) => {
      this.address = result as Address[];
    });
  }

  ngDoCheck(): void {
    console.log("ngOnInit");
    // assegna a addressesOnPage i customer da stampare a video secondo quanto impostato nel paginator
    this.addressesOnPage = this.address.slice(this.startIndex, this.endIndex);
  }

  // aggiorna la variabile contenente i customers da stampare a video a seconda di quanto richiesto dall'utente col paginator
  onPageChange(event: PageEvent) {
    console.log(event);
    this.startIndex = event.pageIndex * event.pageSize; // numero della nuova pagina * numero di elementi da inserire nella pagina
    this.endIndex = this.startIndex + event.pageSize;

    // controlla che gli elementi non siano meno di quelli previsti per endIndex
    if (this.endIndex > this.address.length) {
      this.endIndex = this.address.length;
    }

    // aggiorna la variabile contenente i customer da visualizzare sulla pagina
    this.addressesOnPage = this.address.slice(this.startIndex, this.endIndex);
  }

}
