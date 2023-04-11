export interface Address {
    $id:                             string;
    addressId:                       number;
    addressLine1:                    string;
    addressLine2:                    null;
    city:                            string;
    stateProvince:                   string;
    countryRegion:                   string;
    postalCode:                      string;
    rowguid:                         string;
    modifiedDate:                    string;
    customerAddresses:               Addresses;
    salesOrderHeaderBillToAddresses: Addresses;
    salesOrderHeaderShipToAddresses: Addresses;
}

export interface Addresses {
    $id:     string;
    $values: any[];
}