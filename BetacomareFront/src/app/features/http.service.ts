import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpStatusCode } from '@angular/common/http';
import { Observable } from 'rxjs';

/* - interface - */
import { Address } from './models/adress';
import { Customer } from './models/customer';
import { Login } from './models/login';
import { Product } from './models/product';
import { User } from './models/user';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from './dialog/dialog.component';


@Injectable({
  providedIn: 'root'
})
export class HttpService {
  myUrl: string = 'https://localhost:7266/api';

  /*
   * Ogg da passare alle chiamate HTTP che necessitano delle credenziali di login:
   *    - 'Content-Type' <-- specifica che e' richista un'autorizzazione
   *    - Authorization <-- specifica il tipo di autorizzazione:
   *          'Basic' + window.btoa('user : psw') <-- la tipologia di autorizzazione e le credenziali passate per ottenerla
  */

  headerOptions = new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'Basic'
  });

  userLogged: boolean = false;
  returnMessage: string = "";


  constructor(private http: HttpClient, private router: Router, public dialog: MatDialog) { }

  /* -- Post form data and LogIn. Return null if Ok, else string with details of the error -- */
  InsertCustomer(customer: Customer, address: Address, credential, login: Login) {
    console.log("Into method InsertCustomer(): ");

    /* __ Inserimento Customer __ */
    // If birthday (type Date) is empty, http.post run into an error (at the first call)
    //this.http.post(`${this.myUrl}/Customers/InsertCustomer`, customer, { headers: this.headerOptions, observe: 'response' }).subscribe((res) => {console.log(res)}, err => {this.handleError(err) });

    console.log(address);
    /* __ Inserimento customer Address e a cascata, per impostazione del server, customer in Customer __ */
    // passate alla post: url post, ogg address (contenente customer al campo usernameNavigation), eventuale autorizzazione (header) e richiesta ogg observable come response alla subscribe.
    this.http.post(`${this.myUrl}/Addresses/InsertAddress`, address, { headers: this.headerOptions, observe: 'response' })
      .subscribe((res) => {
        console.log(res); // stampa in console la risposta dal backend: Address + Customer inseriti

        /* __ Inserimento in DB Utenti (credenziali di Login) e LogIn __ */
        this.http.post(`${this.myUrl}/Utentis/Registra`, credential, { headers: this.headerOptions }).subscribe(res => {
          console.log(res);

          /* Tentativo di Login */
          // SE la post (inserimento) delle credenziali e' andato a buon fine
          console.log("tentativo login");
          this.LoginBack(login)
            .subscribe((resp) => { // ok
              // autorizza il  customer con le corrette credential
              this.setAuthorizeHttpHeaders(login.user, login.psw);

              // verifica che il codice restituito dalla chiamata senza errori sia OK(200)
              switch (resp.status) {
                case HttpStatusCode.Ok:
                  this.router.navigate(['/customer']);
                  console.log("succefully LogIn after registration");
                  sessionStorage.setItem('username', login.user);
                  break;
              }
            });
        },
          // gestione errori subscribe post Registra (DB Utenti)
          error => { this.openDialog(error.status); }
        ); // end post insert Credential
      },
        // gestione errori (Conflict, BadRequest,..) post Address
        error => { this.openDialog(error.status); }
      ); // end subscribe post Address
  }


  /* -- post (senza subscribe) credenziali Customer su DB Utenti -- */
  LoginBack(login: Login): Observable<any> {
    console.log(login);
    return this.http.post(`${this.myUrl}/Utentis/Login`, login, { observe: 'response' });
  }

  /* -- set authorization for Basic Authorization back end -- */
  setAuthorizeHttpHeaders(user: string, pwd: string) {
    this.userLogged = true
    this.headerOptions =
      this.headerOptions.set('Authorization', 'Basic ' + window.btoa(user + ':' + pwd))
  }

  openDialog(statusCode: HttpStatusCode) {
    switch (statusCode) {
      case HttpStatusCode.InternalServerError:
        // implementare come Conflict, ma per: "Internal Server Error (500)";
      case HttpStatusCode.Conflict:
        // apre un alert personalizzato per indirizzare l'utente al login o alla ricompialzione del form di registrazione
        this.dialog.open(DialogComponent, {
          width: '30%'
        });
      default:
        // implementare come Conflict, ma per: "Generic Error";
    }


  }


  /* -- Generic get List. path is a piece of string of some controller -- */
  GetAll(path: string) {
    return this.http.get(`${this.myUrl}/${path}`, { responseType: 'json' }); // responseType: 'json' is a default setting
  }
  /* Generic get List, with observa variant. (return more information from back end about the transaction) */
  GetAllObserve(path: string): Observable<any> {
    return this.http.get(`${this.myUrl}/${path}`, { observe: 'response' });
  }

  /* ## Customer ## */
  /* -- get a specific customer. Passing a username of serched user -- */
  GetCustomer(custUsername: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Customers/${custUsername}`, { headers: this.headerOptions });
  }

  GetAllAddresses(username: string) {
    return this.http.get(`${this.myUrl}/Addresses/GetAllAddresses/${username}`, { headers: this.headerOptions }); // responseType: 'json' is a default setting
  }

  DeleteCustomer(username: string) {
    return this.http.delete(`${this.myUrl}/Customers/${username}`, { headers: this.headerOptions });
  }

  DeleteCredetial(username: string) {
    return this.http.delete(`${this.myUrl}/Utentis/${username}`, { headers: this.headerOptions });
  }

  InsertProduct(products: Product) {
    console.log(`Http Service: Post Product ${products.productId}`);
    this.http.post(`${this.myUrl}/Products/${products.productId}`, { headers: this.headerOptions });
  }


  /* -- get all distinct colors of the products -- */
  GetProductColors(): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductColors`, { headers: this.headerOptions });
  }


  /* -- return a list of products filtered for price, weight and color -- */
  GetProductWeiColPri(min: number, max: number, min2: number, max2: number, color: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductWeiPriCol/${min}/${max}/${min2}/${max2}/${color}`, { headers: this.headerOptions });
  }

  GetProductWeiPri(min: number, max: number, min2: number, max2: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductWeiPri/${min}/${max}/${min2}/${max2}`, { headers: this.headerOptions });
  }

  GetProductColPri(color: string, min: number, max: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductColPri/${color}/${min}/${max}`, { headers: this.headerOptions });
  }

  GetProductWeiColor(min2: number, max2: number, color: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductWeiCol/${min2}/${max2}/${color}`, { headers: this.headerOptions });
  }

  GetProductPrice(min: number, max: number): Observable<Array<Product>> {
    return this.http.get<Array<Product>>(`${this.myUrl}/Products/GetProductPrice/${min}/${max}`, { headers: this.headerOptions });
  }

  GetProductWeight(min: number, max: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductWeight/${min}/${max}`, { headers: this.headerOptions });
  }

  GetProductName(name: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductName/${name}`, { headers: this.headerOptions });
  }

  GetProductNamePre(name: string, min: number, max: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductNamePr/${name}/${min}/${max}`, { headers: this.headerOptions });
  }

  GetProductNameWei(name: string, min2: number, max2: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductNameWei/${name}/${min2}/${max2}`, { headers: this.headerOptions });
  }

  GetProductNameWeiPrie(name: string, min: number, max: number, min2: number, max2: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductNamePrWei/${name}/${min}/${max}/${min2}/${max2}`, { headers: this.headerOptions });
  }
  GetProductNameColor(name: string, color: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductNameColor/${name}/${color}`, { headers: this.headerOptions });
  }

  //retturn all sizes
  GetProductSizes(): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizes`, { headers: this.headerOptions });
  }
  GetProductSize(size: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSize/${size}`, { headers: this.headerOptions });
  }
  GetProductSizeCol(size: string, color: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeColor/${size}/${color}`, { headers: this.headerOptions });
  }
  GetProductSizeNam(size: string, name: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeName/${size}/${name}`, { headers: this.headerOptions });
  }
  GetProductSizePr(size: string, min: number, max: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizePr/${size}/${min}/${max}`, { headers: this.headerOptions });
  }
  GetProductSizeWei(size: string, min2: number, max2: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeWei/${size}/${min2}/${max2}`, { headers: this.headerOptions });
  }
  GetProductSizeColName(size: string, color: string, name: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeColorNam/${size}/${color}/${name}`, { headers: this.headerOptions });
  }
  GetProductSizeColPr(size: string, color: string, min: number, max: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeColorPr/${size}/${color}/${min}/${max}`, { headers: this.headerOptions });
  }
  GetProductSizeColWei(size: string, color: string, min2: number, max2: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeColorWei/${size}/${color}/${min2}/${max2}`, { headers: this.headerOptions });
  }
  GetProductSizeColWeiPr(size: string, min: number, max: number, min2: number, max2: number, color: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeColorWeiPr/${size}/${min}/${max}/${min2}/${max2}/${color}`, { headers: this.headerOptions });
  }
  GetProductSizeNamePr(size: string, name: string, min: number, max: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeNamePr/${size}/${name}/${min}/${max}`, { headers: this.headerOptions });
  }
  GetProductSizeNameWei(size: string, name: string, min2: number, max2: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeNameWei/${size}/${name}/${min2}/${max2}`, { headers: this.headerOptions });
  }
  GetProductSizeWeiPr(size: string, min: number, max: number, min2: number, max2: number): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizePrWei/${size}/${min}/${max}/${min2}/${max2}`, { headers: this.headerOptions });
  }

  GetProductSizeNameWeiPreCol(size: string, name: string, min: number, max: number, min2: number, max2: number, color: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeNameColPrWei/${size}/${name}/${min}/${max}/${min2}/${max2}/${color}`, { headers: this.headerOptions });
  }
  GetProductNameWeiPreSize(name: string, min: number, max: number, min2: number, max2: number, size: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductNameSizeWeiPr/${name}/${min}/${max}/${min2}/${max2}/${size}}`, { headers: this.headerOptions });
  }
  /* -- return a list of products filtered for color: string -- */
  GetProductColor(color: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductColor/${color}`, { headers: this.headerOptions });
  }
  GetProductNameWeiPreCol(name: string, min: number, max: number, min2: number, max2: number, color: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeNameColPrWei/${name}/${min}/${max}/${min2}/${max2}/${color}`, { headers: this.headerOptions });
  }
  GetProductNameSizePreCol(name: string, min: number, max: number, size: string, color: string): Observable<any> {
    return this.http.get(`${this.myUrl}/Products/GetProductSizeNameColPrSize/${name}/${min}/${max}/${size}/${color}`, { headers: this.headerOptions });
  }
}
