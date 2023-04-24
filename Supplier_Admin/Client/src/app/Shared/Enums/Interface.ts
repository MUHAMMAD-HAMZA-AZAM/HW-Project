import { DecimalPipe } from "@angular/common";
import { TargetDatabase } from "./enums";

export interface Ipromotion {
  promoStartDate: string;
  promotionStartDate: string | null;
  promotionEndDate: string | null;
  categoryId: number;
  subCategoryId: number;
  createdBy: string;
  promotionId: number;
  updatedBy: string;
  isActive: boolean;
  categoryGroupId: number;
}
export interface ISupplierPromotions {

  promotionId: number;
  supplierId: number;
  promotionTypeId: number;
  promotionName: string;
  promotionStartDate: Date;
  promotionEndDate: Date;
  discountPercentPrice: number;
  categoryId: number;
  subCategoryId: number;
  categoryGroupId: number;
  entityStatus: string;
  categoryName: string;
  subCategoryName: string;
  categoryGroupName: string;
  isActive: boolean;
  amount: number;
  amountType: string;
  createdBy: string;
  updatedBy: string;
  createdDate: Date;
  updatedDate: Date;
}
export interface IProductCategory {
  id: number;
  text: string;

}
export interface IProductSubCategory {
  subCategoryName: string;
  isActive: boolean;
  productSubCategoryId: number;

}

export interface IPromotionTypes {
  promotionTypeId: number;
  promotionTypeCode: string;
  promotionTypeName: string;
  entityStatus: string;
  isActive: boolean;
  createdDate: string;
  createdBy: string;
  updatedDate: string;
  updatedBy: string;
}
export interface IPersonalDetails {
  accountType: number;
  address: string;
  phoneNumber: string;
  sellerType: number;
  city: string;
  cityId: number;
  cnic: string;
  customerId: number;
  dateOfBirth: string;
  email: string;
  entityId: number;
  featuredSupplier: boolean;
  firstName: string;
  gender: number;
  isEmailConfirmed: boolean;
  isNumberConfirmed: boolean;
  jobQuotationId: number;
  lastName: string;
  latLong: string;
  mobileNumber: string;
  profileImage: string[];
  relationship: string
  role: string;
  shopName: string;
  supplierId: number;
  town: string;
  townId: number;
  userId: string
  userRole: string

}
export interface IVerify {
  id: string;
  phoneNumber: string;

}
export interface IValForm {
  email: string
  phoneNumber: string,
  role: string,
  accountType: number,
  sellerType: number,
  password: string,
  termsAndConditions: boolean,
  userName: string,
  emailOrPhoneNumber: string,
  shopName: string,
  city: string,
  isAllGoodStatus: boolean
}
export interface ICountryList {
  countryId: number;
  countryName: string;
}

export interface IStateList {
  stateId: number;
  countryId: number;
  stateName: string;
  active: boolean;
  name:string
}
export interface IAreaList {
  areaId: number;
  stateId: number;
  areaName: string;
  active: boolean;
}
export interface ILocationList {
  locationId: number;
  areaId: number;
  locationName: string;
  active: boolean;
}
export interface BankList {
  bankId: number;
  bankName: string;
  active: boolean;
}

export interface ISupplierDetails {
  userId?: string;
  publicId?: string;
  emailAddress?: string;
  mobileNumber?: string;
  firstName?: string;
  lastName?: string;
  cnic?: string;
  gender?: number;
  dob?: string;
  companyName?: string;
  registrationNumber?: string;
  primaryTrade?: string;
  productCategoryId?: number;
  state?: string;
  cityId?: number;
  businessAddress?: string;
  gpsCoordinates?: string;
  deliveryRadius?: number;
  isActive?: boolean;
  createdOn?: string;
  createdBy?: string;
  modifiedOn?: string;
  modifiedBy?: string;
  isActiv?: boolean;
  supplierId?: number;
  featuredSupplier?: boolean;
  supplierRole?: number;
  shopName?: string;
  shopUrl?: string;
  holidayMode?: boolean;
  holidayStart?: string;
  hoilidayEnd?: string;
  countryId?: number;
  idfrontImage?: string;
  shopCoverImage?: string;
  idfrontImageName?: string;
  idbackImage?: string;
  idbackImageName?: string;
  businessDescription?: string;
  areaId?: number;
  locationId?: number;
  inChargePerson?: string;
  inchargePersonMobileNo?: string;
  inchargePersonEmail?: string;
  ntnnumber?: string;
}
export interface iQuerypram {
  categoryId: number;
  subCategoryId: number;
  categoryGroupId: number;
  supplierId: string;
  pageNumber: number;
  pageSize: number;
  id: number,
  name: string | null;
  price: number;
  toPrice: number;

}
export interface IProductCategoryGroupList {
  id: number;
  name: string;
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
export interface IFreeShippingList {
  freeShippingId: Number;
  categoryName: string;
  categoryId: Number;
  subCategoryName: string;
  subCategoryId: Number;
  categoryGroupName: string;
  CategoryGroupId: Number;
  starDate: string;
  endDate: string;
  createdOn: Date;
  status: boolean
}

export interface ISelectedCategoryList {
  category?: string | null,
  categoryId: string | null,
  subCategory?: string | null,
  subCategoryId: string | null,
  categoryGroup: string,
  subCategoryGroupId: string | null

}
export interface ISubcategoryGroupList {
  active: boolean;
  createdBy: string;
  createdOn: string;
  id: number;
  modifiedBy: string;
  modifiedOn: string;
  name: string;
  subCategoryId: number;

}
export interface IProductStockDTO {
  productId: number;
  varientId: number;
  quantity: number;
}
export interface IProduct {
  id: number;
  supplierId: number;
  quantity: number;
  pageNumber: number;
  noOfRecords: number;
  pageSize: number;
  price: number;
  discountedPrice: number;
  discountInPercentage: number;
  toPrice: number;
  name: string;
  isActive: boolean;
  description: string;
  youtubeURL: string;
  variantId: number;
  bulkVarientId: number;
  fileId: number;
  variantName: string;
  fileName: string;
  filePath: string;
  isMain: boolean;
  shopName: string;
  availability: boolean;
  hexCode: string;
  attributeId: number;
  attributeValue: string;
  attributeName: string;
  action: string;
  categoryLevel: string;
  categoryId: number;
  weight: DecimalPipe;
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
  bulkdiscount: number;
  minQuantity: number;
  maxQuantity: number;
  bulkPrice: number;
  groupItem: IGroupItem[],
  images: string[],
  productAttributes: Array<any>;
  productInventory: Array<any>;
  bulkInventory: Array<any>;
  searchTags: Array<any>;
  display: string;
  value: number;
  tagId: number;
}

export interface IProductAttributeValue {
  attributeId: number;
  attributeValue: string;
}

export interface IBulkOrdering {
  minQuantity: number;
  bulkId?: number;
  maxQuantity: number;
  bulkPrice: number;
  bulkDiscount?: number;
  varientId?: number;
  bulkVarientId?: number;
  index: number;
}

export interface IVariantDetails {
  price: number,
  discountedPrice: number,
  availability: boolean,
  quantity: number,
}
export interface IGroupItem {
  hexCode: string;
  price: number;
  discountedPrice: number;
  attributeName: string;
  attributeValue: string
  variantId: number;
  quantity: number;
}
export interface IProductDetail {
  id: number;
  name: string;
  price: number;
  discountedPrice: number;
  description: string;
  availability: boolean;
  categoryName: string;
  categoryId: number;
  subCategoryName: string;
  subCategoryId: number;
  categoryGroupId: number;
  categoryGroupName: string;
  groupItem: any[] | undefined | null,
  images: any[] | undefined | null
}
export interface IVarientDetails {
  colorName: string;
  createdBy: string;
  createdOn: string;
  hexCode: string;
  id: number;
  isActive: boolean;
  modifiedBy: string;
  modifiedOn: string;
}
export interface IBulkDetails {
  id: number;
  name: string;
  description: string;
  isActive: boolean
  categoryName: string;
  categoryId: number;
  subCategoryName: string;
  subCategoryId: number;
  categoryGroupId: number;
  categoryGroupName: string;
  productAttributes: Array<any>;
  productInventory: Array<any>;
  bulkInventory: Array<any>;
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
  totalShippingAmount: number;
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


export interface ISubItem {
  discountedAmount: number;
  promotionAmount: number;
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
  totalShippingAmount: number;
  actualAmount: number;
  shippingCost: number;
  totalPayable: number;
  discountedAmount: number;
  promotionAmount: number;
}

export interface IProfileVerification {
  sellerAccountVerification: boolean;
  businessInformationVerification: boolean;
  bankAccountVerification: boolean;
  wareHouseAddressVerification: boolean;
  returnAddressVerification: boolean;
  isAllGoodStatus: boolean
}

export interface IResponse {
  status: number;
  message: string;
  resultData: any;

}

export interface IShipmentResponse {
  status: number;
  message: string;
  details: any;

}
export interface ILocation {
  locationId: number;
  areaId: number;
  locationName: string;
  active: boolean;
}
export interface IPayementHistoryDetail {
  supplierName: string;
  productId: number;
  title: string;
  paymentStatusName: string;
  noOfRecords: number;
  itemCost: number;
  totalItemCost: number;
  supplierId: number;
  orderId: number;
  customerName: string;
}
export interface INotificationLogging {
  notificationLoggingId: number;
  senderId: string;
  receiverId: string;
  type: number;
  payLoad: string;
  isRecived: boolean;
  receivedAt: string;
  isRead: boolean;
  readAt: string;
  isAborted: boolean;
  isSent: boolean;
  reasonToAbort: string;
  retries: number;
  createdBy: string;
  createdOn: string;
  modifiedBy: string;
  modifiedOn: string;
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

export interface IResponseVM {
  status: number,
  message: string,
  resultData: object
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
  phone_number_1: string;
  phone_number_2: Object;
  destination: string;
  address: string;
}

export interface Item {
  product_type: string;
  description: string;
  quantity: number;
}

export interface IOrderInformation {
  items: Item[];
  weight: number;
  instructions: string;
}

export interface ITrackingHistory {
  date_time: string;
  status: string;
  status_reason: Object;
}

export interface IDetails {
  tracking_number: string;
  pickup: IPickup;
  shipper: IShipper;
  consignee: IConsignee;
  order_information: IOrderInformation;
  tracking_history: ITrackingHistory[];
}

export interface ICityList {
  id: number;
  value: string;
}
export interface ICustomerOrderItems {
  orderId: number;
  orderFrom: string;
  quantity: number;
  orderStatus: number;
  orderStatusName: string;
  orderDate: Date;
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
  cityName: string;
}
export interface IValidationError {
  key: string ;
  dsecription: string;
}
export interface  ICity {
  cityId: number;
  code: string;
  name: string;
  stateId: number;
  isActive: boolean;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
    }
export interface IWithDrawalRequestList {
    withdrawalRequestId: number;
    customerId: number | null;
    customerName: string;
    tradesmanId: number | null;
    tradesmanName: string;
    supplierId: number | null;
    supplierName: string;
    role: string;
    phoneNumber: string;
    cnic: string;
    amount: number | null;
    paymentStatusId: number | null;
    paymentStatusName: string;
    createdOn: string | null;
    createdBy: string;
    modifiedOn: string | null;
    modifiedBy: string;
    idfrontImage: string;
    idbackImage: string;
    totalWithDrawAmount: number | null;
}
