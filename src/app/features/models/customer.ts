export interface Customer {
    $id:               string;
    customerId:        number;
    nameStyle:         boolean;
    title:             string;
    firstName:         string;
    middleName:        string;
    lastName:          string;
    suffix:            null;
    companyName:       string;
    salesPerson:       string;
    emailAddress:      string;
    phone:             string;
    passwordHash:      string;
    passwordSalt:      string;
    rowguid:           string;
    modifiedDate:      string;
    customerAddresses: CustomerAddresses;
    salesOrderHeaders: CustomerAddresses;
}

export interface CustomerAddresses {
    $id:     string;
    $values: any[];
}