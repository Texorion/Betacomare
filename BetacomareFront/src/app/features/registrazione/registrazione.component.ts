import { Component, OnInit } from '@angular/core';
import { Customer } from '../models/customer';
import { HttpService } from '../http.service';
import { Login } from '../models/login';
import { Address } from '../models/adress';
import { Countries } from './countries';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-registra',
  templateUrl: './registrazione.component.html',
  styleUrls: ['./registrazione.component.css']
})
export class RegistrazioneComponent implements OnInit {
  hide = true;
  colorPsw = false;

  /* options for mat-select of Status */
  status = ['', 'Student', 'Worker', 'Retired'];

  /* data for autocomplete of States */
  countries: Countries;           // lista dei nomi di States, letti da file .json in assets con chiamata http.get
  countriesFiltered: Countries;   // lista aggiornata mostrata in mat-option per tendina autocompletamento input States

  /* ogg da inviare al server tramite InsertCustomer() che lancia una post in http.service */
  credential;
  login: Login;
  customer: Customer;
  address: Address;

  /* per il check, tramite le direttive in Validator, sul template-driven form */
  passwords: { psw: '', pswCheck: '' };
  phone = ''; // appPhoneValidator

  constructor(private src: HttpService, private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get('assets/countries.json').subscribe(res => {
      this.countries = res as Countries;
      this.countriesFiltered = res as Countries;
    });
  }

  addForm(frm) {
      // credenziali da inserire nel DB Utenti
      this.credential = {
        Username: frm.value.username,
        PswHash: frm.value.passwords.psw,
        Salt: ""
      };

      this.login = {
        user: frm.value.username,
        psw: frm.value.passwords.psw
      }

      // dati customer da inserire in Betacomare.Customer
      this.customer = {
        username: frm.value.username,
        title: frm.value.title,
        firstName: frm.value.firstName,
        middleName: frm.value.middleName,
        lastName: frm.value.lastName,
        status: frm.value.stat,
        phone: frm.value.phone,
        birthday: frm.value.birthday
      };

      this.address = {
        username: frm.value.username,
        address1: frm.value.address,
        city: frm.value.city,
        postalCode: frm.value.postalCode,
        stateProvince: frm.value.stateProvince,
        countryRegion: frm.value.countryRegion,
        state: frm.value.state,
        usernameNavigation: this.customer
      }

      // passaggio campi customer del form alla post per l'inserimento
      this.src.InsertCustomer(this.customer, this.address, this.credential, this.login);

      // clear template-driven form
      this.resetForm(frm);
  }



  /* -- Autocomplete for Input of State -- */
  autocomplete(event: Event) {
    /*
     * (input)="autocomplete($event)", nell'input di registrazione di State, prede gli eventi sulla casella di input 
     * vengono poi trasformati da evento a stringa di input col cast e i prelievo del valore: (event.target as HTMLInputElement).value
     * 
     * La ricerca viene fatta sui campi name degli ogg in countries ad ogni nuovo evento, andando a modificare la lista per mat-option:
     *    filter restituisce gli ogg di countries che, per cs.name (lista coi soli nomi di countries), 
     *    contengono la stringa in includes("stringa_match"), ovvero il nostro input.
     *    includes() restituisce treu se cs.name ha un match e quindi viene selezionato da countries ed incluso da filter() in countriesFiltered.
     */
    this.countriesFiltered =
      this.countries.filter(cs => {
        return cs.name.toLowerCase().includes((event.target as HTMLInputElement).value.toLowerCase() || '');
      });
  }

  // for button reset form
  resetForm(frm) {
    this.countriesFiltered = this.countries; // reset autocomplete drop-down
    frm.reset(); // reset field of template-driven form
  }
}
