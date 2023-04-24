import { DecimalPipe } from "@angular/common";

export interface IProduct {
  id: number;
  name: string;
  slug: string;
  price: number;
  discount: number;
  discountedPrice: number;
  variantId: number;
  hexCode: string;
  supplierId: number;
  firebaseClientId: string;
  categoryId: number;
  subCategoryId:number;
  categoryGroupId: number;
  stockQuantity: number;
  actualVarientPrice: number;
  weight:any;
  traxCityId: number;
  bulkInventory: Array<bulkModel>;
  shippingCost?:number;
  fileName:string;
  filePath: string;
  varientName: string;
  isFreeShipping: Boolean;
}

export interface bulkModel {
  bulkId: number;
  minQuantity: number;
  maxQuantity: number;
  bulkDiscount:number;
  bulkPrice: number;
  bulkVarientId: number;
  bulkProductId: number;
}
export class SmsVM {
  MobileNumber: string;
  MobileNumberList: string[];
  Message: string;
}
