import { Customer } from "./customer";

export interface AddressGet {
  username:           string;
  address1:           string;
  city:               string;
  postalCode:         string;
  stateProvince:      string;
  countryRegion:      string;
  state:              string;
  shoppingCart:       any[];
  usernameNavigation: Customer;
}