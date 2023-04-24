import { DecimalPipe } from "@angular/common";
import { ITraxResponse, TargetDatabase } from "./enum";

export interface IProductsWishList {
  id: number;
  customerId: number;
  supplierId: number;
  productId: string;
  isFavorite: boolean;
  active: boolean;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
  noOfRecords: number;
  slug: string;
  name: string;
  description: string;
  fileName: string;
  filePath: string;
  isMain: boolean;
  action: string;
  product: any;
}
export interface ICustomerFeedBackParams {
  customerId: number;
  productId: number;
  pagesNumber: number;
  pageSize: number;
}

export interface ICustomerFeedBack {
  id: number;
  description: string;
  rating: number;
  customerId: number;
  fiveStars: number;
  foureStars: number;
  threeStars: number;
  twoStars: number;
  oneStars: number;
  productId: number;
  createdBy: string;
  modifiedOn: Date;
  createdOn: Date;
  modifiedBy: string;
  totalReviews: number;
  averageRating: number;
}
export interface ICustomerOrderItems {
  orderId: number;
  trackingNumber: string;
  orderFrom: string;
  quantity: number;
  orderStatus: number;
  orderStatusName: string;
  orderDate: string;
  noOfRecords: number;
  supplierId: number;
  supplierName: string;
  supplierEmailAddress: string;
  supplierMobileNumber: string;
  companyName: string;
  productId: number;
  productName: string;
  price: number;
  actualPrice: number;
  discountedAmount: number;
  promotionAmount: number;
  payableAmount: number;
  shippingCost: number;
  slug: string;
  fileName: string;
  filePath: string;
  isMain: boolean;
  isPaymentReceived: boolean;
  isPaymentModeConfirm: boolean;
  varientColour: string;
  variantId: number;
  customerId: number;
  customerName: string;
  contactNumber: string;
  shippingAddress: string;
  comments: string;
  reasonName: string;
  pageNumber: number;
  pageSize: number;
}

export interface IPayementHistory {
  customerId: number;
  paymentStatusId: number;
  noOfRecords: number;
  orderId: number;
  customerName: string;
  createdDate: string;
  totalOrderCost: number;
  paymentMethod: string;
  isPaymentReceived: boolean;
  totalDeliveryCost: number;
  paymentStatusName: string;
}
export interface IOrderDetail {
  customerName: string;
  address: string;
  phoneNumber: string;
  totalAmount: number;
  cityName: string;
}
export interface IPersonalDetails {
  firstName: string;
  lastName: string;
  isNumberConfirmed: boolean;
  isEmailConfirmed: boolean;
  email: string;
  cnic: string;
  phoneNumberConfirmed: boolean;
  emailConfirmed: boolean;
  role: string;
  mobileNumber: string;
  city: string;
  cityId: number;
  address: string;
  customerId: number;
  PhoneNumber: string;
}
export interface ICityList {
  id: number;
  value: string;
}
export interface IIdValue {
  id: number;
  text: string;
}
export interface ICityListTrax {
  id: number;
  value: string;
  traxCityId: number;
}
export interface IPromotions {
  promotionId: number;
  productId: number;
  promotionIdTypeId: number;
  categoryId: number;
  subCategoryId: number;
  categoryGroupId: number;
  categoryName: string;
  promotionCode: string;
  promotionName: string;
  promoStartDate: string;
  promotionEndDate: string;
  entityStatus: string;
  isActive: boolean;
  createdDate: string;
  createdBy: string;
  updatedDate: string;
  updatedBy: string;
  promotionTypeName: string;
  amount: number;
  supplierId: number;
}


export interface IProductVM {
  id: number;
  supplierId: number;
  firebaseClientId: string;
  quantity: number;
  pageNumber: number;
  noOfRecords: number;
  pageSize: number;
  price: number;
  discountedPrice: number;
  discount: number;
  discountInPercentage: number;
  toPrice: number;
  name: string;
  isActive: boolean;
  description: string;
  youtubeURL: string;
  variantId: number;
  fileId: number;
  variantName: string;
  fileName: string;
  filePath: string;
  isMain: boolean;
  shopName: string;
  shopUrl: string;
  availability: boolean;
  hexCode: string;
  attributeId: number;
  attributeValue: string;
  attributeName: string;
  action: string;
  categoryLevel: string;
  categoryId: number;
  categoryName: string;
  subCategoryId: number;
  subCategoryName: string;
  categoryGroupId: number;
  categoryGroupName: string;
  active: boolean;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
  bulkId: number;
  traxCityId: number;
  weight: DecimalPipe;
  bulkdiscount: number;
  minQuantity: number;
  maxQuantity: number;
  bulkPrice: number;
  bulkVarientId: number;
  bulkProductId: number;
  slug: string;
  tagName: string;
  isFreeShipping: Boolean;
}
export interface IProductDetail {
  id: number;
  name: string;
  firebaseClientId: string;
  price: number;
  discountedPrice: number;
  discount: number;
  description: string;
  youtubeURL: string;
  availability: boolean;
  categoryName: string;
  categoryId: number;
  subCategoryName: string;
  subCategoryId: number;
  categoryGroupId: number;
  slug: string;
  supplierId: number;
  attributeValue: string;
  attributeName: string;
  variantId: number;
  hexCode: string;
  shopName: string;
  shopUrl?: string;
  variantName?: string;
  weight?: DecimalPipe;
  traxCityId?: number;
  isFreeShipping: Boolean;
  categoryGroupName: string;
  groupItem: any[] | undefined | null,
  attributes: any[] | undefined | null,
  images: any[] | undefined | null,
  tags?: any[] | undefined | null;
}
export interface IShippingCost {
  id?: number;
  cost?: number;
  cityId?: number;
  createdOn?: string;
  createdBy?: string;
  modifiedOn?: string;
  modifiedBy?: string;
}

export interface ICityShippingCost {
  cost?: number;

}

export interface ISupplierSlider {
  id: number;
  imageName: string;
  localImageName: string;
  imagePath: string;
  status: boolean;
  startDate: string;
  endDate: string;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
}

export interface ISupplierProduct {
  supplierId: number;
  title: string;
  noOfRecords: number;
  slug: string;
  productId: number;
  price: number;
  discountedPrice: number;
  discountInPercentage: number;
  availability: boolean;
  fileName: string;
  supplierName: string;
  productCategoryName: string;
  subCategoryName: string;
  categoryGroupName: string;
  name: string;
  categoryId: number;
  subCategoryId: number;
  categoryGroupId: number;
  filePath: string;
  fileId: number;
seoDescription: string;
 seoTitle: string;
 ogTitle: string;
 ogDescription: string;
 canonical: string;
}
export interface IProductCategory {
  productCategoryId: number;
  name: string;
}
export interface IProductCategoryGroup {
  id: number;
  name: string;
  text: string;
  subCategoryId: number;
  active: boolean;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
}
export interface IProductCategoryList {
  productCategoryId: number;
  code: string;
  text: string;
  id: number;
  name: string;
  isActive: boolean;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
  orderByColumn: number;
  productImage: string[];
  hasSubItem: boolean;
}
export interface IProductSubCategoryList {
  productCategoryId: number;
  productSubCategoryId: number;
  subCategoryName: string;
  isActive: boolean;
  hasSubItems: boolean;
}

export interface IUserRegistrationConfirm {
  userId?: string;
  firstName?: string;
  lastName?: string;
  email?: string;
  mobileNumber?: string;
  customerId?: number;
}
export interface LoginVM {
  emailOrPhoneNumber: string;
  rememberMe: boolean,
  password: string;
  role: string;
  facebookClientId: string;
  googleClientId: string;
}

export interface ILoggedUserDetails {
  customerId?: number;
  firstName?: string;
  lastName?: string;
  email?: string;
  mobileNumber?: string;
  cnic?: string;
  cityId?: number;
  city?: string;
  address?: string;
  emailConfirmed?: boolean;
  phoneNumberConfirmed?: boolean;
  role?: string;
  firebaseClientId?: string;

}
export interface IPersonalDetailsVM {
  userId: string;
  entityId: number;
  firebaseClientId: string;
  shopName: string;
  jobQuotationId: number;
  /*[Display(Name = "First Name")]*/
  firstName: string;
  /*[Display(Name = "Last Name")]*/
  lastName: string;
  /*[Display(Name = "Email Address")]*/
  email: string;
  /*[Display(Name = "CNIC #")]*/
  cnic: string;
  isNumberConfirmed: boolean;
  isEmailConfirmed: boolean;
  gender: number;
  role: string;
  profileImage: string[];
  /*[Display(Name = "Date of Birth")]*/
  dateOfBirth: string;
  /*[Display(Name = "Mobile Number")]*/
  mobileNumber: string;
  latLong: string;
  town: string;
  townId: number;
  cityId: number;
  address: string;
  relationship: string;
  userRole: string;
  supplierId: number;
  customerId: number;
  accountType: number;
  featuredSupplier: boolean;
  city: string;
}
export interface IPromoCheckOut {
  promotionId: number;
  amount: number;
  productId: number;
}

  export interface IQueryPramProductByCatagory {
    pageNumber: number,
    pageSize: number,
    categoryId: number,
    categorylevel: string,
    minPrice?: number,
    maxPrice?: number,
    supplierId?:number
}
export interface IQueryPramProductByName {
  pageNumber: number,
  pageSize: number,
  name: string,
}
//export interface IQueryPramProductByUser {
//  pageNumber: number,
//  pageSize: number,
//  supplierId: number,
//}
export interface IProductByUser {
  supplierId: number;
  businessDescription: string;
  shopCoverImage: string;
  title: string;
  profileImage: string[];
  noOfRecords: number;
  shopName: string;
  slug: string;
  fileName: string;
  filePath: string;
  productId: number;
  price: number;
  discountedPrice: number;
  discountInPercentage: number;
}


export interface IUserRegister {
  id: string;
  email: string;
  password: string;
  confirmPassword: string;
  phoneNumber: string;
  isNumberConfirmed: boolean;
  isEmailConfirmed: boolean;
  role: string;
  sellerType: number;
  accountType: number;
  subrole: string;
  firebaseClientId: string;
  facebookUserId: string;
  googleUserId: string;
  appleUserId: string;
  verificationCode: string;
  securityRole: string;
  shopName: string;
  userName: string;
  clientId: number;
  hasPin: boolean;
  termsAndConditions: boolean;
  fromPersonalDetails: boolean;
  latLong: string;
}

export interface IUserObj {
  email: string;
  emailOrPhoneNumber: string;
  facebookUserId: string;
  firstName: string;
  googleUserId: string;
  lastName: string;
  password: string;
  phoneNumber: string;
  city: string;
  role: string;
  termsAndConditions: boolean;
  userName: string;
  facebookClientId: string | null;
  googleClientId: string | null;
}


export interface ILogIn {
  emailOrPhoneNumber: string;
  password: string;
  role: string;
  facebookClientId: string;
  googleClientId: string;
}

export interface IPostNotificationVM {
  notificationId: number;
  title: string;
  isFromWeb?: boolean;
  body: string;
  to?: string;
  senderEntityId?: string;
  targetActivity?: string;
  senderUserId?: string;
  tragetUserId?: string;
  createdOn: string;
  targetDatabase?: TargetDatabase;
  jobReferenceId?: number;
  jobRetries?: number;
  unreadNotifictionsRecord?: number;
  jobAbortReason?: string;
  isAborted?: boolean;
  isRead: boolean;
  profileImage?: number[];
  tradesmenList?: string[];
}

export interface IDisplayNotification {
  createdOn: string;
  title: string;
  content: string;
  isRead: boolean;
  notificationId: number;
  targetActivity: string;
}

export interface IPayLoad {
  data: IData;
  to: string;
}
export interface IData {
  title: string;
  body: string;
  sound: string;
  tag: string;
}
//export interface ICustomerOrderItemsDTO {
//  orderId: number;
//  orderFrom: string;
//  quantity: number;
//  orderStatus: number;
//  orderStatusName: string;
//  orderDate: string;
//  noOfRecords: number;
//  supplierId: number;
//  supplierName: string;
//  supplierEmailAddress: string;
//  supplierMobileNumber: string;
//  companyName: string;
//  productId: number;
//  productName: string;
//  price: number;
//  actualPrice: number;
//  discountedAmount: number;
//  promotionAmount: number;
//  payableAmount: number;
//  shippingCost: number;
//  slug: string;
//  fileName: string;
//  filePath: string;
//  isMain: boolean;
//  isPaymentReceived: boolean;
//  varientColour: string;
//  variantId: number;
//  customerId: number;
//  customerName: string;
//  contactNumber: string;
//  shippingAddress: string;
//  comments: string;
//  reasonName: string;
//  pageNumber: number;
//  pageSize: number;
//}
export interface ICustomerOrdersList {
  noOfRecords: number,
  orderId: number,
  customerId: number,
  orderDate: string,
  quantity: number,
  orderStatus: number,
  customerName: string,
  payableAmount: number,
  contactNumber: string,
  shippingAdress: string,
  shippingCost: number,
  productId: number,
  slug: string,
  supplierId: number,
  supplierName: string,
  isPaymentReceived: boolean,
  orderStatusName: string,
  reasonName: string,
  comments: string | undefined

}

//---------------- For Shipment Tracking

export interface IShipper {
  name: string;
  account_number: number;
  phone_number_1: string;
  phone_number_2: string;
  address: string;
  origin: string;
}

export interface IPickup {
  origin: string;
  person_of_contact: string;
  phone_number: string;
  email: string;
  address: string;
}

export interface IConsignee {
  name: string;
  email: string;
  phone_number_1: string;
  phone_number_2: Object;
  destination: string;
  address: string;
}

export interface Item {
  order_id: string;
  product_type: string;
  description: string;
  quantity: number;
}

export interface IOrderInformation {
  items: Item[];
  weight: number;
  shipping_mode: string;
  amount: number;
  instructions: Object;
}

export interface ITrackingHistory {
  date_time: string;
  status: string;
  status_reason: Object;
}

export interface IDetails {
  tracking_number: string;
  order_id: string;
  shipper: IShipper;
  pickup: IPickup;
  consignee: IConsignee;
  order_information: IOrderInformation;
  tracking_history: ITrackingHistory[];
}

export interface IOrigin {
  city: string;
  zone: string;
}
export interface IDestination {
  city: string;
  class: string;
}
export interface ICharges {
  weight: number;
  cash_handling: number;
  fuel_surcharge: number;
  total_charges: number;
  gst: number;
  net_payable: number;
}
export interface IInformation {
  origin: IOrigin;
  destination: IDestination;
  charges: ICharges;
  chargeable_weight: string;
}
export interface ICalculateShippingRates extends ITraxResponse {
  informaton:IInformation
}
export interface IPageSeoVM {
  pageId: number;
  pageName: string;
  pageTitle: string;
  keywords: string;
  description: string;
  canonical: string;
  ogDescription: string;
  ogTitle: string;
}
export interface IProductNames {
  title: string;
}
export class SocialLinks {
  linkedinUrl: string;
  facebookUrl: string;
  twitterUrl: string;
  youtubeUrl: string;
}
