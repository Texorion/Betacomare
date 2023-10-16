import { Customer } from "./customer";

export interface Address {
  username:           string;
  address1:           string;
  city:               string;
  postalCode:         string;
  stateProvince:      string;
  countryRegion:      string;
  state:              string;
  usernameNavigation: Customer;
}
