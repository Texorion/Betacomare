import { Component, DoCheck, ElementRef, EventEmitter, OnInit, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { HttpClient } from '@angular/common/http';
import { HttpService } from '../http.service';
import { Product } from '../models/product';
import { NonNullableFormBuilder } from '@angular/forms';
import { ShoppingCartService } from 'src/app/shopping-cart.service';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements DoCheck, OnInit {
  products: Product[] = []; // lista di prodotti attuali da visualizzare
  productsOnPage: Product[]; // lista di prodotti perpagina estrapolati da products

  shoppingCart: Array<Product> = [];

  // Products per pag del paginator
  startIndex: number = 0;
  endIndex: number = 10;
  productsLength: number;
  percentage: number = 0;

  color: string = null;
  color_tmp: string = this.color;
  colors: Array<string> = [];

  size: string = null;
  sizes: Array<string> = [];
  size_tmp: string = this.size;
  // slider price
  min: number = 0;
  max: number = 4000;
  min_tmp: number = this.min;
  max_tmp: number = this.max;

  // min max weight
  min2: number = null;
  max2: number = null;
  min2_tmp: number = this.min2;
  max2_tmp: number = this.max2;

  //name search
  name: string = ""
  name_tmp: string = "";

  /* inietto:
        HttpClient, libreria di Angular, per le chiamate HTTP;
        HttpService, che ci da accesso ai metodi e alle variabili da noi definiti per le chiamate HTTP;
        ShoppingCartService, che ci permette di maneggiare globalmente (essendo in app/) i metodi e l'array di prodotti per il carrello.
   */
  constructor(private http: HttpClient, public httpsrc: HttpService, public shopCart: ShoppingCartService) { }


  ngOnInit(): void {
    this.httpsrc.GetAll('Products').subscribe((result) => {
      this.products = result as Product[];
    });

    this.httpsrc.GetProductColors().subscribe(res => this.colors = res);
    this.httpsrc.GetProductSizes().subscribe(res => this.sizes = res);
  }

  resetFilter() {
    this.size = null;
    this.size_tmp = this.size;
    this.color = null
    this.color_tmp = null
    this.min = 0;
    this.max = 4000;
    this.min_tmp = this.min;
    this.max_tmp = this.max;
    this.min2 = null;
    this.max2 = null;
    this.min2_tmp = this.min2;
    this.max2_tmp = this.max2;
    this.httpsrc.GetAll('Products').subscribe((result) => {
      this.products = result as Product[];
      this.percentage = 0;
      this.onPageChange;
    });
  }

  ngDoCheck(): void {
    /* -- Aggiorna il paginator --  */
    // assegna a productsOnPage i products da stampare a video secondo quanto impostato nel paginator
    this.productsOnPage = this.products.slice(this.startIndex, this.endIndex);

    // setta productLength e calcola percentage la prima volta
    if ((this.productsLength = this.products.length) != 0 && this.percentage == 0) {
      this.percentageCalc();
    }


    /* -- Funzioni che lanciano le query nel backend per i filtri dei prodotti visualizzati, in tempo reale -- */
    // prodotti filtrati per PREZZO, PESO e COLORE
    if (this.name == "" && this.name != this.name_tmp) {
      console.log("vuoto");
      this.name_tmp = this.name;
      this.httpsrc.GetAll('Products').subscribe((result) => {
        this.products = result as Product[];
        this.percentage = 0;
        this.onPageChange;
      });
    }
    else if ((this.name == "")) {
      if (this.color != this.color_tmp) {
        this.color_tmp = this.color;
        this.httpsrc.GetProductColor(this.color).subscribe(result => {
          this.products = result as Array<Product>;
          this.onPageChange;
        });
      } else if (this.size != this.size_tmp) {
        this.size_tmp = this.size;
        this.httpsrc.GetProductSize(this.size).subscribe(result => {
          this.products = result as Array<Product>;
          this.onPageChange;
        });
      } else if (this.min != this.min_tmp || this.max != this.max_tmp) {
        console.log("entro")
        this.min_tmp = this.min;
        this.max_tmp = this.max;
        this.httpsrc.GetProductPrice(this.min, this.max).subscribe(result => {
          this.products = result as Array<Product>;
          this.onPageChange;
        });
      } else if (this.min2 != this.min2_tmp || this.max2 != this.max2_tmp) {
        console.log("entro2")
        this.min2_tmp = this.min2;
        this.max2_tmp = this.max2;
        this.httpsrc.GetProductWeight(this.min2, this.max2).subscribe(result => {
          this.products = result as Array<Product>;
          this.onPageChange;
        });
      }
      else {

        if ((this.min != this.min_tmp || this.max != this.max_tmp) || (this.min2 != this.min2_tmp || this.max2 != this.max2_tmp)) {
          this.min_tmp = this.min;
          this.max_tmp = this.max;

          this.min2_tmp = this.min2;
          this.max2_tmp = this.max2;

          this.httpsrc.GetProductWeiPri(this.min, this.max, this.min2, this.max2).subscribe(result => {
            this.products = result as Array<Product>;
            this.onPageChange;
          });
        } else if ((this.min != this.min_tmp || this.max != this.max_tmp) || (this.color != this.color_tmp)) {
          this.min_tmp = this.min;
          this.max_tmp = this.max;

          this.color_tmp = this.color;

          this.httpsrc.GetProductColPri(this.color, this.min, this.max).subscribe(result => {
            this.products = result as Array<Product>;
            this.onPageChange;
          });
        } else if ((this.min2 != this.min2_tmp || this.max2 != this.max2_tmp) || (this.color != this.color_tmp)) {
          this.min2_tmp = this.min2;
          this.max2_tmp = this.max2;
          this.color_tmp = this.color;

          this.httpsrc.GetProductWeiColor(this.min2, this.max2, this.color).subscribe(result => {
            this.products = result as Array<Product>;
            this.onPageChange;
          });
        } else if ((this.size != this.size_tmp) || (this.color != this.color_tmp)) {
          this.color_tmp = this.color;
          this.size_tmp = this.size;
          this.httpsrc.GetProductSizeCol(this.size, this.color).subscribe(result => {
            this.products = result as Array<Product>;
            this.onPageChange;
          });
        } else if ((this.size != this.size_tmp) || (this.min != this.min_tmp || this.max != this.max_tmp)) {
          this.min_tmp = this.min;
          this.max_tmp = this.max;
          this.size_tmp = this.size;
          this.httpsrc.GetProductSizePr(this.size, this.min, this.max).subscribe(result => {
            this.products = result as Array<Product>;
            this.onPageChange;
          });
        } else if ((this.size != this.size_tmp) || (this.min2 != this.min2_tmp || this.max2 != this.max2_tmp)) {
          this.min2_tmp = this.min2;
          this.max2_tmp = this.max2;
          this.size_tmp = this.size;

          this.httpsrc.GetProductSizeWei(this.size, this.min2, this.max2).subscribe(result => {
            this.products = result as Array<Product>;
            this.onPageChange;
          });
        } else {
          if (((this.min != this.min_tmp || this.max != this.max_tmp) || (this.min2 != this.min2_tmp || this.max2 != this.max2_tmp)) && ((this.color != this.color_tmp) || (this.size !== this.size_tmp))) {
            // aggiorno confronto precedenza PREZZO
            this.min_tmp = this.min;
            this.max_tmp = this.max;

            // aggiorno confronto precedenza PESO
            this.min2_tmp = this.min2;
            this.max2_tmp = this.max2;

            // aggiorno confronto precedenza COLORE
            this.color_tmp = this.color;
            this.size_tmp = this.size;

            this.httpsrc.GetProductSizeColWeiPr(this.size, this.min, this.max, this.min2, this.max2, this.color).subscribe(result => {
              this.products = result as Array<Product>;
              this.onPageChange;
            })
          } else {
            if (((this.min != this.min_tmp || this.max != this.max_tmp) || (this.min2 != this.min2_tmp || this.max2 != this.max2_tmp)) && (this.color != this.color_tmp)) {
              // aggiorno confronto precedenza PREZZO
              this.min_tmp = this.min;
              this.max_tmp = this.max;

              // aggiorno confronto precedenza PESO
              this.min2_tmp = this.min2;
              this.max2_tmp = this.max2;

              // aggiorno confronto precedenza COLORE
              this.color_tmp = this.color;
              this.size_tmp = this.size;

              this.httpsrc.GetProductWeiColPri(this.min, this.max, this.min2, this.max2, this.color).subscribe(result => {
                this.products = result as Array<Product>;
                this.onPageChange;
              })
            } else {
              if (((this.min != this.min_tmp || this.max != this.max_tmp) || (this.color != this.color_tmp)) && (this.size != this.size_tmp)) {

                this.min_tmp = this.min;
                this.max_tmp = this.max;

                // aggiorno confronto precedenza COLORE
                this.color_tmp = this.color;
                this.size_tmp = this.size;

                this.httpsrc.GetProductSizeColPr(this.size, this.color, this.min, this.max).subscribe(result => {
                  this.products = result as Array<Product>;
                  this.onPageChange;
                })

              } else {
                if ((this.min2 != this.min2_tmp || this.max2 != this.max2_tmp) && (this.color != this.color_tmp) || (this.size != this.size_tmp)) {

                  // aggiorno confronto precedenza PESO
                  this.min2_tmp = this.min2;
                  this.max2_tmp = this.max2;

                  // aggiorno confronto precedenza COLORE
                  this.color_tmp = this.color;
                  this.size_tmp = this.size;

                  this.httpsrc.GetProductSizeColWei(this.size, this.color, this.min2, this.max2).subscribe(result => {
                    this.products = result as Array<Product>;
                    this.onPageChange;
                  })

                } else {
                  if (((this.min != this.min_tmp || this.max != this.max_tmp) || (this.min2 != this.min2_tmp || this.max2 != this.max2_tmp)) && (this.size != this.size_tmp)) {

                    // aggiorno confronto precedenza PREZZO
                    this.min_tmp = this.min;
                    this.max_tmp = this.max;

                    // aggiorno confronto precedenza PESO
                    this.min2_tmp = this.min2;
                    this.max2_tmp = this.max2;

                    // aggiorno confronto precedenza COLORE

                    this.size_tmp = this.size;

                    this.httpsrc.GetProductSizeWeiPr(this.size, this.min, this.max, this.min2, this.max2).subscribe(result => {
                      this.products = result as Array<Product>;
                      this.onPageChange;
                    })
                  }

                }
              }
            }
          }
        }
      }
    } else {
      if ((this.min != this.min_tmp || this.max != this.max_tmp) && (this.min2 != this.min2_tmp || this.max2 != this.max2_tmp) && (this.color != this.color_tmp) && (this.size !== this.size_tmp) && (this.name_tmp != this.name)) {

        // aggiorno confronto precedenza PREZZO
        this.min_tmp = this.min;
        this.max_tmp = this.max;

        // aggiorno confronto precedenza PESO
        this.min2_tmp = this.min2;
        this.max2_tmp = this.max2;

        // aggiorno confronto precedenza COLORE
        this.color_tmp = this.color;
        this.size_tmp = this.size;
        this.name_tmp = this.name

        this.httpsrc.GetProductSizeNameWeiPreCol(this.size, this.name, this.min, this.max, this.min2, this.max2, this.color).subscribe(result => {
          this.products = result as Array<Product>;
          this.onPageChange;
        })

      } else {
        if ((this.min2 != this.min2_tmp || this.max2 != this.max2_tmp) && (this.color != this.color_tmp) && (this.min != this.min_tmp || this.max != this.max_tmp)) {
          this.min2_tmp = this.min2;
          this.max2_tmp = this.max2;
          this.min_tmp = this.min;
          this.max_tmp = this.max;
          this.color_tmp = this.color;
          this.name_tmp = this.name
          this.httpsrc.GetProductNameWeiPreCol(this.name, this.min, this.max, this.min2, this.max2, this.color).subscribe(result => {
            this.products = result as Array<Product>;
            this.onPageChange;
          });

        } else if ((this.min2 != this.min2_tmp || this.max2 != this.max2_tmp) && (this.size != this.size_tmp) && (this.min != this.min_tmp || this.max != this.max_tmp)) {
          this.min2_tmp = this.min2;
          this.max2_tmp = this.max2;
          this.min_tmp = this.min;
          this.max_tmp = this.max;
          this.size_tmp = this.size;
          this.name_tmp = this.name;
          this.httpsrc.GetProductNameWeiPreSize(this.name, this.min, this.max, this.min2, this.max2, this.size).subscribe(result => {
            this.products = result as Array<Product>;
            this.onPageChange;
          });
        } else if ((this.min2 != this.min2_tmp || this.max2 != this.max2_tmp) && (this.size != this.size_tmp) && (this.min != this.min_tmp || this.max != this.max_tmp)) {
          this.min2_tmp = this.min2;
          this.max2_tmp = this.max2;
          this.min_tmp = this.min;
          this.max_tmp = this.max;
          this.size_tmp = this.size;
          this.name_tmp = this.name
          this.httpsrc.GetProductNameWeiPreSize(this.name, this.min, this.max, this.min2, this.max2, this.size).subscribe(result => {
            this.products = result as Array<Product>;
            this.onPageChange;
          });
        } else {
          if ((this.min != this.min_tmp || this.max != this.max_tmp) && (this.size_tmp != this.size) && (this.name_tmp != this.name) && this.color_tmp != this.color) {
            this.size_tmp = this.size;
            this.min_tmp = this.min;
            this.max_tmp = this.max;
            this.name_tmp = this.name;
            this.color_tmp = this.color
            this.httpsrc.GetProductNameSizePreCol(this.name, this.min, this.max, this.size, this.color).subscribe(result => {
              this.products = result as Array<Product>;
              this.onPageChange;
            });
          } else if ((this.min != this.min_tmp || this.max != this.max_tmp) && (this.size_tmp != this.size) && (this.name_tmp != this.name)) {
            this.min2_tmp = this.min2;
            this.max2_tmp = this.max2;
            this.min_tmp = this.min;
            this.max_tmp = this.max;
            this.name_tmp = this.name;
            this.httpsrc.GetProductNameWeiPrie(this.name, this.min, this.max, this.min2, this.max2).subscribe(result => {
              this.products = result as Array<Product>;
              this.onPageChange;
            })
          } else {

            if ((this.name_tmp != this.name) && (this.min2 != this.min2_tmp || this.max2 != this.max2_tmp)) {
              this.min2_tmp = this.min2;
              this.max2_tmp = this.max2;
              this.name_tmp = this.name;
              this.httpsrc.GetProductNameWei(this.name, this.min2, this.max2).subscribe(result => {
                this.products = result as Array<Product>;
                this.onPageChange;
              });
            } else if ((this.name_tmp != this.name) && (this.min != this.min_tmp || this.max != this.max_tmp)) {
              this.min_tmp = this.min;
              this.max_tmp = this.max;
              this.name_tmp = this.name;
              this.httpsrc.GetProductNamePre(this.name, this.min2, this.max2).subscribe(result => {
                this.products = result as Array<Product>;
                this.onPageChange;
              });
            } else if ((this.name_tmp != this.name) && this.color != this.color_tmp) {
              this.min_tmp = this.min;
              this.max_tmp = this.max;
              this.name_tmp = this.name;
              this.httpsrc.GetProductNameColor(this.name, this.color).subscribe(result => {
                this.products = result as Array<Product>;
                this.onPageChange;
              });
            } else if ((this.name_tmp != this.name) && this.size != this.size_tmp) {
              this.min_tmp = this.min;
              this.max_tmp = this.max;
              this.name_tmp = this.name;
              this.httpsrc.GetProductSizeNam(this.size, this.name).subscribe(result => {
                this.products = result as Array<Product>;
                this.onPageChange;
              });
            } else {
              if (this.name_tmp != this.name) {
                this.name_tmp = this.name;
                this.httpsrc.GetProductName(this.name).subscribe(result => {
                  this.products = result as Array<Product>;
                  this.onPageChange;
                });
              }
            }
          }
        }

      }
    }

  }


  // aggiorna la variabile contenente i products da stampare a video a seconda di quanto richiesto dall'utente col paginator
  onPageChange(event: PageEvent) {
    this.startIndex = event.pageIndex * event.pageSize; // numero della nuova pagina * numero di elementi da inserire nella pagina
    this.endIndex = this.startIndex + event.pageSize;

    // controlla che gli elementi non siano meno di quelli previsti per endIndex
    if (this.endIndex > this.products.length) {
      this.endIndex = this.products.length;
    }

    // aggiorna la variabile contenente i customer da visualizzare sulla pagina
    this.productsOnPage = this.products.slice(this.startIndex, this.endIndex);
    console.log(this.percentage);
    this.percentageCalc();
  }


  // aggiorna il valore percentuale per la ProgressBar
  percentageCalc() {
    this.percentage = (this.endIndex / this.productsLength) * 100;
  }


  Search(frm, event: KeyboardEvent) {

    if (event.key === 'Enter') {
      console.log(this.name)
    }
  }
}


// if (frm.value.search == '') {
//   if ((this.min != 0) || (this.max != 4000)) {
//     this.httpsrc.GetProductColPri(frm.value.search, this.min, this.max).subscribe(result => {
//       this.products = result as Array<Product>;
//       this.onPageChange
//     })
//   } else {
//     this.httpsrc.GetProductColor(frm.value.search).subscribe(result => {
//       this.products = result as Array<Product>;
//       this.onPageChange
//     })
//   }
// }
