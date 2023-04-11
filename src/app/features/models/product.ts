export interface Product {
    $id:                    string;
    productId:              number;
    name:                   string;
    productNumber:          string;
    color:                  string;
    standardCost:           number;
    listPrice:              number;
    size:                   string;
    weight:                 number;
    productCategoryId:      number;
    productModelId:         number;
    sellStartDate:          string;
    sellEndDate:            null;
    discontinuedDate:       null;
    thumbNailPhoto:         string;
    thumbnailPhotoFileName: string;
    rowguid:                string;
    modifiedDate:           string;
    productCategory:        null;
    productModel:           null;
    salesOrderDetails:      SalesOrderDetails;
}

export interface SalesOrderDetails {
    $id:     string;
    $values: any[];
}