import { Injectable } from '@angular/core';
import { Product } from './features/models/product';
import { ShoppingCart } from './features/models/shoppingCart';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {
  shoppingCart: Array<ShoppingCart> = [];
  productDisplayed: ShoppingCart;

  constructor() { }

  /* -- Shopping Cart -- */

  /* -- Controller product on the shopping cart list from the button of the product list -- */
  // make and add a new product to the shopping cart from button on products list
  shoppingCartAdd(p: Product) {
    this.productDisplayed = {
      username: sessionStorage.getItem('username'),
      productId: p.productId,
      qty: 1,
      name: p.name,
      productNumber: p.productNumber,
      color: p.color,
      listPrice: p.listPrice,
      size: p.size,
      weight: p.weight
    }

    // aggiunge un nuovo elemento all'array shoppingCart
    this.shoppingCart.push(this.productDisplayed);
    this.shoppingCart.sort();
  }

  // remove a product from shopping cart when the user click on the button on product list
  shoppingCartRemovePItems(p: Product) {
    // cerco la corrispondenza tra p.productId e shoppingCart.productId.
    // Poi ricerco l'indice nel'array per l'elemento trovato e lo vado a rimuovere (1, quell'indice).
    this.shoppingCart.splice(this.shoppingCart.indexOf(this.shoppingCart.filter(items => items.productId == p.productId)[0]), 1);
    this.shoppingCart.sort();
  }

  /* -- Controller qty from button of the shopping cart -- */
  // (+): add one occurrence of a product from shopping cart list
  addPItem(p_sc: ShoppingCart) {
    p_sc.qty++;

    console.log(this.shoppingCart);
  }

  // (-): remove one occurrence of a product from shopping cart list
  removeItem(p_sc: ShoppingCart) {
    if (p_sc.qty == 1) {
      // se aveva solo piu' 1 occorrenza, rimuove il prodotto dalla lsita del carrello
      this.shoppingCart.splice(this.shoppingCart.indexOf(p_sc), 1); // (da quale indice, num elementi da rimuovere)
      this.shoppingCart.sort();
    } else {
      // se aveva piu' di un'occorrenza (qty), allora riduce qty e lascia il prodotto in lista
      p_sc.qty--;
    }
  }


  getShoppingCart(): Array<ShoppingCart> {
    return this.shoppingCart;
  }

  /* -- count the items in the Shopping Cart */
  getNumItems(): number {
    let numItems = 0;
    this.shoppingCart.map(elem => { numItems += elem.qty });
    return numItems;
  }

  getTotalPrice() {
    let tot = 0;
    this.shoppingCart.map(elem => { tot += (elem.listPrice * elem.qty) })
    return tot;
  }

  elementExist(p: Product): boolean {
    // cerca se e' presente un elemento con il productId (chiave) di p corrispondente ad uno in shopping address
    if (this.shoppingCart.filter(items => items.productId == p.productId).length > 0) {
      return true;
    }
    return false;
  }

  // qty of corresponding (to this.shoppingCart) product
  qty(p: Product): number {
    let qty = this.shoppingCart.filter(items => items.productId == p.productId)[0].qty;
    if (qty > 0) {
      return qty;
    }
    return 0;
  }
}