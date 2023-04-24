
export var StatusCode = {
  OK: 200,
  Error: 505,
  failure: 500,
  Restricted: 403,
  partialContent: 206,
  Conflict: 409
}

export class ShippingDetails {
  firstName: string="";
  lastName: string="";
  PhoneNumber: string="";
  email: string=""
}

export enum TargetDatabase {
  Customer = 1,

  Tradesman = 2,

  Supplier = 3
}


export class ResponseVm {
  status: number=0;
  message: string="";
  resultData: object={} as object;
}

export interface IResponseVM {
  status: number,
  message: string,
  resultData: object
}

export interface ITraxResponse{
  status: number;
  message: string;
}
export var wishListBasket =  {
  addProduct: true,
  removeProduct:false
}

export var AspnetRoles = {
  CRole: "3",
  TRole: "1",
  ORole: "2",
  SRole: "4",
  Admin:"5"
}
export var OrderStatus = {
	Received:1,
	Inprogress:2,
	Delievred:3,
	Completed:4,
  Declined: 5,
  PackedAndShipped:6,
  Cancelled : 7
}

export var PaymentMethod = {
  JazzCash: 1,
  CashPayment:3
}

export var PaymentStatus = {
  Pending : 1,
  Posted : 2,
  Inprogress : 3,
  Declined : 4,
  Completed : 5
}

export enum TrackingStatusType {
  ShipperRelatedTracking = 0,
  GeneralTracking = 1
}

export enum AspnetUserRoles  {
 CRole = "Customer",
 TRole = "Tradesman",
 ORole = "Organization",
 SRole = "Supplier"
}

export enum TraxServiceType{
  Regular = 1,
  Replacement = 2,
  TryAndBuy = 3
}

export enum TraxShippingMode {
  Rush = 1,
  Saverplus = 2,
  Swift = 3,
  Same_day = 4
}

export enum TraxStatusCode {
  OK = 0,
  Error = 1
}