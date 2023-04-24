export var loginsecurity = {
  Role: "Admin",
  TRole:"Tradesman",
  ORole: "Oragnization",
  SRole: "Supplier",
  CRole:"Customer",
}

export var aspNetUserRoles = {
  TRole: "1",
  ORole: "2",
  SRole: "4",
  CRole: "3",
  Admin:"5"
}
export var SupplierRole = {
  localSupplier: "1",
  hoomworkSupplier: "2"
}

export var CampaignTypes = {
  Promotion: "1",
  Advertisement: "2"
}
export var httpStatus = {
  Ok: "200",
  loginError:"invalid email address/phone number or password."

}
export var LoginValidation = {
  emailOrPhoneNumber: "Please enter email address or phone Number.",
  password: "Please enter password.",
  unauthorizedUser: "You are Un-Authorized User.",
  InvalidCredentials: "Invalid Credentials.",
  Invalidrecaptcha: "Please enter captcha.",
  BlockedUSer: "This user is blocked."
}
export var forgotPassword = { 
  forgotPasswordEmailError: "please enter email address.",
  unauthorizedUser: "Can't Send Email.",
  unauthorizedErrormessage:"You are unauthorized user."
}
export var resetPassword = {
  password: "Please enter Password.",
  confirmPassword: "Please enter Confirm Password.",
  passwordMatch:"Passwords not match."
}
export var ReportDatesValidation = {
  startDateErrorMessage: "Please enter start date.",
  endDateErrorMessage: "Please enter end date.",
}


export var RegistrationErrorMessages = {
  passwordErrorMessage: "Please enter password.",
  passwordMinLenghtErrorMessage: "Please enter minimum 6 charcters",
  phonenumberErrorMessage: "Please enter Phone Number.",
  emailErrorMessage: "Please enter Email.",
  UserNameErrorMessage: "Please enter User Name.",
  UserSecurityRoleMessage: "Please select security role.",
  emailAlreadyRegisteredErrorMessage: "User Already Registered with this Email.",
  mobileNumberAlreadyRegisteredErrorMessage : "User Already Registered with this Mobile Number"

  

}

export var BidStatus = {
  Active: 1,
  Declined: 2,
  Completed: 3,
  Accepted: 4,
  Pending: 5,
  Urgent: 6,
  Deleted: 7,
}
export let CSJobStatus =  {
  Callnotpicked : 1,
  Slotbooking : 2,
  Waitingduetoissue :3,
  Notinterested : 4,
  Outofstation : 5
}

export var escalateIssueStatus = {
  Pending: 0,
  Approve: 1
}

export var GLAccountTypes = {
  AccountReceiveables: 1,
  AccountPayable: 2,
  Assets:3
}
export var GLAccounts = {
  Receiveables: 3,
  Payable: 2,
  Assets:1
}
export var Orders = {
  Received: 1,
  Inprogress: 2,
  Delivered: 3,
  Completed: 4,
  Declined: 5,
  PackedAndShipped: 6,
  Cancelled : 7
}

export var Payments = {
  Pending: 1,
  Posted: 2,
  Inprogress: 3,
  Declined: 4,
  Completed : 5
}



export var TransectionTypes = {

  TTransection: "TopUp",
  SDTransection: "Self Deposit",
  RTransection:"Referral",
  WTransection: "WithDraw",
  PBWTransectioin: "Paid By Wallet",

}

export interface IResponseVM {
  status: number,
  message: string,
  resultData: object
}

export var StatusCode = {
  OK: 200,
  Error: 505,
  failure: 500,
  Restricted: 403,
  partialContent: 206,
  Conflict: 409
}

export enum TrackingStatusType {
  ShipperRelatedTracking = 0,
  GeneralTracking = 1
}
export interface IOrders {
  orderId: number;
  firebaseClientId: string;
  customerId: number;
  supplierId: number;
  orderFrom: string;
  mobileNumber: string;
  shippingAddress: string;
  orderStatus: number;
  orderStatusName: string;
  orderDate: string;
  price: number;
  orderMessage: string;
  actualAmount: number;
  shippingCost: number;
  totalPayable: number;
  commission: number;
  quantity: number;
  discount: number;
  promotionAmount: number;
  orderBySales: any[] | undefined | null;
  orderitem: any[] | undefined | null
}
export interface IOrderItem {
  orderId: number;
  trackingNumber: string;
  supplierId: number;
  productId: number;
  productTitle: string;
  variantId: number;
  variant: string;
  commission: number;
  orderStatus: number;
  orderFrom: string;
  orderDate: string;
  quantity: number;
  price: number;
  actualAmount: number;
  shippingCost: number;
  totalPayable: number;
  discountedAmount: number;
  promotionAmount: number;
}
