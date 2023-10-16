/* -- Carrello ordine effettuato -- */
export interface ShoppingCart{
    username:           string;   // username che ha effettuato l'ordine
    //orderId:            number;   // ID dell'ordine (numero d'ordine)
    productId:          number;   // ID del prodotto acquistato in quell'ordine
    qty:                number;   // quantita' del prodotto in questione
    //addressId:          number;   // indirizzo di spedizione
    name:               string;
    productNumber:      string;
    size:               string;
    color:              string;
    weight:             number;
    listPrice:          number;
}