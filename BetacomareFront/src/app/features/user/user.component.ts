import { HttpClient } from '@angular/common/http';
import { Component, DoCheck } from '@angular/core';
import { HttpService } from '../http.service';
import { User } from '../models/user';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements DoCheck {
  users: User[] = [];
  usersOnPage: User[];
  
  // Users per pag del paginator
  startIndex: number = 0;
  endIndex: number = 10;

  constructor(private http: HttpClient, public httpsrc: HttpService) {
    httpsrc.GetAll('Users').subscribe((result) => {
      this.users = result as User[];
    });
  }
  
  ngDoCheck(): void {
    console.log("ngOnInit");
    // assegna a usersesOnPage i customer da stampare a video secondo quanto impostato nel paginator
    this.usersOnPage = this.users.slice(this.startIndex, this.endIndex);
  }

  // aggiorna la variabile contenente i customers da stampare a video a seconda di quanto richiesto dall'utente col paginator
  onPageChange(event: PageEvent) {
    console.log(event);
    this.startIndex = event.pageIndex * event.pageSize; // numero della nuova pagina * numero di elementi da inserire nella pagina
    this.endIndex = this.startIndex + event.pageSize;

    // controlla che gli elementi non siano meno di quelli previsti per endIndex
    if (this.endIndex > this.users.length) {
      this.endIndex = this.users.length;
    }

    // aggiorna la variabile contenente i customer da visualizzare sulla pagina
    this.usersOnPage = this.users.slice(this.startIndex, this.endIndex);
  }
}
