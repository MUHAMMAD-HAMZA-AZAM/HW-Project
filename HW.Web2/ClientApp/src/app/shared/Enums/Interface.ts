import { DecimalPipe } from "@angular/common";
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
export interface IPayLoad {
  notification: INotification;
  to: string;
}
export interface INotification {
  title: string;
  body: string;
  sound: string;
  tag: string;
}

export interface ISupplierShopWebVM {
  supplierId: number;
  supplierCompanyName: string;
  supplierAddress: string;
  latLong: string;
  contactNo: string;
  supplierImage: string[];
  email: string;
  supplierAds: Array<IShopSupplierAd>;
  supplierFeedbacks: Array<ISuppliersFeedback>;
}

export interface IShopSupplierAd {
  adId: number;
  adTitle: string;
  categoryId: number;
  price: number;
  adImage: string[];
  imageName: string;
  supplierCompanyName: string;
  adImageId: number;
}
export interface ISuppliersFeedback {
  supplierFeedbackId: number;
  customerProfileImage: string[];
  customerName: string;
  customerId: number;
  comments: string;
  rating: number;
  createdOn: string;
}

export interface IImage {
  id: number;
  filePath: string;
  isMain: boolean;
  imageContent: string;
  imageBase64: string;
  localUrl: string;
  thumbImage: string;
  thumbImageContent: string[];
}
export interface ITownList {
  name: string;
  townId: number;
}
export interface ITownListSearch {
  value: string;
  id: number;
}
export interface IPersonalDetails {
  firstName: string; //
  lastName: string; //
  email: string; //
  phoneNumberConfirmed: boolean; //
  isNumberConfirmed: boolean; //
  isEmailConfirmed: boolean;
  role: string; //
  profileImage: string[]; // 
  mobileNumber: string; //
  latLong: string; //
  cityId: number; //
  address: string; //
  dateOfBirth: string;
  firebaseClientId: string;
}
export interface IErrorList{
  cityIdError: string;
  townError: string;
  areaError: string;
  numberOfBidsError: string;
  startedDateError: string;
  startedTimeError: string;
  budgetError: string;
  streetAddressError: string;
  budgetPatternError: string;
}
export interface ISubSkill {
  subSkillId: number;
  name: string;
  skillId: number;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
  isActive: boolean;
  orderByColumn: number;
  subSkillImage: string[];
  metaTags: string;
  imagePath: string;
  description: string;
  slug: string;
  subSkillTitle: string;
  subSkillPrice: number;
  visitCharges: number;
}
export interface IIdValue {
  id: number;
  value: string|null;
}
export interface IMyQuotations {
  bidCount: number;
  callCount: number;
  jobQuotationId: number;
  postedDate: string;
  workTitle: string;
  workingAddress: string;
  workStartDate: string;
  jobImage: string[];
}
export interface IJobQuotationDetail {
  jobQuotationId: number;
  categoryId: number;
  catagory: string;
  subCatagory: IIdValue[];
  title: string;
  jobDescription: string;
  images: IImage[];
  video: number[];
  videoFileName: string;
  videoUpdated: boolean;
  budget: number;
  startingDateTime: Date;
  jobStartingDate: string;
  jobStartingTime: string;
  cityId: number;
  area: string;
  address: string;
  quotesQuantity: number;
  selectiveTradesman: boolean;
  subSkillId: number;
  tradesmanList: IIdValue[];
  citiesList: IIdValue[];
  workStartTime: string;
  statusId: number;
  startTime: timeObj;
}
export interface timeObj {
  hour: number;
  minute: number;
}
export interface IApplicationSetting {
  applicationSettingId: number;
  settingName: string;
  action: string;
  userId: string;
  applictaionSettingDetailId: number;
  settingKeyName: string;
  settingKeyValue: string;
  isActive: boolean;
  createdBy: string;
  modifiedBy: string;
  createdOn: string;
  modifiedOn: string;
}
export interface IAdditionalChargesObj {
  paymentMethod: any;
  bidId: number;
  jobQuotationId: number;
  tradesmanOffer: number;
  otherCharges: number;
}
export interface IResponse {
  status: any;
  message: string;
  resultData: any;
}
export interface INotifications {
  body: string;
  createdOn: string;
  isAborted: boolean;
  jobAbortReason: string;
  jobReferenceId: number;
  jobRetries: number;
  notificationId: number;
  senderEntityId: string;
  senderUserId: string;
  targetActivity: string;
  targetDatabase: number;
  title: string;
  to: string;
  tragetUserId: string;
}
export interface IBidDetails {
  bidId: number;
  tradesmanUserId: string;
  tradesmanId: number;
  customerId: number;
  tradesmanName: string;
  tradesmanProfileImage: string[];
  jobImage: string[];
  jobTitle: string;
  customerBudget: number;
  tradesmanOffer: number;
  jobDescription: string;
  bidAudioMessage: number[];
  bidAudioFileName: string;
  mobileNumber: string;
  email: string;
  jobQuotationId: number;
  bidPostedOn: string;
  tradesmanAddress: string;
  customerName: string;
  bidStatusId: number;
  jobDetailsId: number;
  visitCharges: number;
  serviceCharges: number;
  otherCharges: number;
  skillId: number;
  workStartDate: string;
  isStarted: boolean;
  isFinished: boolean;
  action: string;
  paymentMethod: number;
  pageNumber: number;
  pageSize: number;
}
export interface IEsclateOption {
  id: number;
  name: string;
  active: boolean;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
  userRole: number;
}
export interface IEsclateObj {
  esclateOptionId: number;
  comment: string;
  tradesmanId: number;
  customerId: number;
  jobQuotationId: number;
  status: number;
  userRole: string;
  active: number;
}
export interface IInProgressJobList {
  address: string;
  area: string;
  city: string;
  createdOn: string;
  getcount: number;
  jobQuotationId: number;
  lastName: string;
  title: string;
  tradesmanId: number;
  tradesmanName: string;
  jobImage: string;
  isFinished: boolean;
  bidsId: number;
  tradesmanOffer: DecimalPipe;
  jobDetailId: number;
}
export interface IUpdateAdditionalChargesObj {
  bidId: number;
  otherCharges: number;
  jobQuotationId: number;
  tradesmanOffer: number;
  action: string;
}
export interface IInProgressJobDetails {
  catagoryName: string;
  cityName: string;
  desiredBids: number;
  directPayment: boolean;
  jobQuotationId: number;
  selectiveTradesman: false
  streetAddress: string;
  subCatagoryName: string;
  town: string;
  tradesmanId: number;
  workBudget: number;
  workDescription: string;
  workStartDate: string
  workTitle: string;
  imageList: string[];
}
export interface IReceivedBidVM {
  bidAudioFileName: string;
  bidAudioMessage: string;
  bidId: number;
  customerName: string;
  bidPostedOn: string;
  customerBudget: number;
  jobDescription: string;
  jobQuotationId: number;
  jobTitle: string;
  mobileNumber: string;
  tradesmanAddress: string;
  tradesmanId: number;
  tradesmanName: string;
  tradesmanOffer: number;
  tradesmanProfileImage: string;
  tradesmanUserId: string;
  visitCharges: number;
  serviceCharges: number;
  otherCharges: number;
  skillId: number;
}
export interface IJobDetail {
  jobDetailId: number;
  jobQuotationId: number;
  customerId: number;
  tradesmanId: number;
  skillId: number;
  subSkillId: number;
  title: string;
  description: string;
  statusId: number;
  serviceCharges: number;
  otherCharges: number;
  budget: number;
  startDate: string;
  endDate: string;
  customerMessage: string;
  tradesmanBudget: number;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
  paymentStatus: number;
  materialCharges: number;
  additionalCharges: number;
  totalJobValue: number;
  estimatedCommission: number;
  chargesDescription: string;
  csJobStatus: number;
  isFinished: boolean;
}
export interface IFinishedJob {
  jobDetailId: number;
  tradesmanId: number;
  jobQuotationId: number;
  jobImage: string;
  fileName: string;
  jobTitle: string;
  tradesmanName: string;
  lastName: string;
  city: string;
  streetAddress: string;
  tradesmanEmail: string;
  mobileNumber: string;
  town: string;
  jobEndTime: string;
  rating: number;
}
export interface ISkillAndSubSkill {
  subSkillId: number;
  subSkillName: string;
  subSkillTitle: string;
  skillId: number;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
  skillName: string;
  isActive: boolean;
  orderByColumn: number;
  skillImage: string[];
  subSkillImage: string[];
  base64Image: string;
  metaTags: string;
  skillTitle: string;
  description: string;
  slug: string;
  imagePath: string;
  subSkillPrice: number;
  skillIconPath: string;
  visitCharges: number;
}
export interface IGetMarkeetPlaceProducts {
  supplierAdId: number;
  supplierAdTitle: string;
  supplierCompanyName: string;
  price: number;
  imageContent: string[];
  adImageId: number;
  isSaved: number;
  isLiked: number;
  adStatus: number;
  adRating: number;
  productCategoryId: number;
  totalProducts: number;
  thumbImageContent: string[];
}
export interface IReceivedBids {
  bidBy: string;
  bidId: number;
  bidImage: string;
  bidImageName: string;
  bidOn: string;
  isSelected: boolean;
  jobQuotationTitle: string;
  tradesmanAddress: string;
  tradesmanId: number;
}
export interface IJobImages {
  bidImageId: number;
  fileName: string;
  bidImage: string[];
  isMain: boolean;
  jobQuotationId: number;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
}
export interface IReceivedBid {

  bidAudioFileName: string;
  bidAudioMessage: string;
  bidId: number;
  customerName: string;
  bidPostedOn: string;
  customerBudget: number;
  jobDescription: string;
  jobQuotationId: number;
  jobTitle: string;
  mobileNumber: string;
  tradesmanAddress: string;
  tradesmanId: number;
  tradesmanName: string;
  tradesmanOffer: number;
  tradesmanProfileImage: string;
  tradesmanUserId: string;
  visitCharges: number;
  serviceCharges: number;
  otherCharges: number;
  skillId: number;
}

export interface ISubCategorySettings {
  singleSelection?: boolean;
  idField?: string;
  textField?: string;
  allowSearchFilter?: boolean;
  itemsShowLimit?: number;
  selectAllText?: string;
  unSelectAllText?: string;
  enableCheckAll?: boolean;
  closeDropDownOnSelection?: boolean;
}
export interface IGetCitiesAndDistance {
  cities: IIdValue[];
  distances: IIdValue[];
  productCategories: IProductCategory[];
}
export interface IProductCategory {
  productCategoryId: number;
  code: string;
  name: string;
  isActive: boolean;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
  orderByColumn: number;
  productImage: string[];
  id: number;
  value: string;
}
export interface ISupplierProfileDetail {
  persnalDetails: IPersonalDetails;
  businessDetails: IBusinessProfile;
}
export interface IBusinessProfile {
  supplierId: number;
  categoryId: number;
  companyName: string;
  registrationNumber: string;
  primaryTrade: string;
  primaryTradeId: number;
  productsubCategory: IIdValue[];
  city: number;
  cityName: string;
  town: string;
  businessAddress: string;
  deliveryRadius: number;
  selectedSubCategory: IIdValue[];
  locationCoordinates: string;
}
export interface ITown {
  townId: number;
  name: string;
  isActive: boolean;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
  cityId: number;
}
export interface IManageAdsVMWithImage {
  supplierAdsId: number;
  adTitle: string;
  price: number;
  adsStatusId: number;
  subCategoryValue: string;
  activeFrom: string;
  activeTo: string;
  city: string;
  town: string;
  addres: string;
  adImage: string[];
  isActive: boolean;
  adViewCount: number;
  productCategoryId: number;
}
export interface IInactiveManageAdsVMWithImages {
  supplierAdsId: number;
  adTitle: string;
  price: number;
  activeFrom: string;
  activeTo: string;
  city: string;
  subCategoryValue: string;
  town: string;
  addres: string;
  fileName: string;
  adImage: string[];
  totalDay: number;
}
export interface ISkill {
  skillId: number;
  name: string;
  skillTitle: string;
  metaTags: string;
  description: string;
  createdOn: string;
  createdBy: string;
  modifiedOn: string;
  modifiedBy: string;
  isActive: boolean;
  orderByColumn: number;
  skillImage: string[];
  skillIconPath: string;
  imagePath: string;
  slug: string;
  seoPageTitle: string ;
  ogTitle: string ;
  ogDescription: string ;
}
export interface IActivePromotion {
  promotionId: number;
  name: string;
  description: string;
  image: string[];
  imageMobile: string[];
  base64Image: string;
  isAcitve: boolean;
  isMain: boolean;
  createdBy: string;
  createdOn: string;
  modifiedBy: string;
  modifiedOn: string;
  action: string;
  skillName: string;
  skillId: number;
  subSkillIds: string;
  userRoleId: string;
  promotionStartDate: string;
  promotionEndDate: string;
  promotionFor: string;
  campaignTypeId: number;
}
export interface IActiveCampaignType {
  campaignTypeId: number;
  name: string;
  description: string;
  image: string[];
  base64Image: string;
  isAcitve: boolean;
  isMain: boolean;
  createdBy: string;
  createdOn: string;
  modifiedBy: string;
  modifiedOn: string;
  action: string;
  skillName: string;
  skillId: number;
  subSkillIds: string;
  userRoleId: string;
  campaignStartDate: string;
  campaignEndDate: string;
  campaignFor: string;
  campaignTypeName: string;
}

export interface IPromotionDetails {
  name: string;
  description: string;
  skillName: string;
  skillId: number;
  image: string[];
}
export interface IActiveOrders {
  orderId: number;
  packageName: string;
  totalAds: number;
  expiryDate: string;
}
export interface ISupplierShopWeb {
  supplierId: number;
  supplierCompanyName: string;
  supplierAddress: string;
  latLong: string;
  contactNo: string;
  supplierImage: string[];
  email: string;
  supplierAds: IShopSupplierAd[];
  supplierFeedbacks: ISuppliersFeedback[];
}
export interface IAdDetails {
  productPrice: number;
  productName: string;
  supplierAdId: number;
  supplierAdReference: string;
  productBy: string;
  productDescription: string;
  deleiveryAvailable: boolean;
  collectionAvailable: boolean;
  supplierEmail: string;
  mobileNumber: string;
  adViews: number;
  productId: number;
  supplierId: number;
  isSaved: boolean;
  imageIds: [];
}
export interface IProductCategory_Home {
  supplierAd: ISupplierAd[];
  categoryList: ICategoryList[];
}
export interface ISupplierAd {
  supplierAdId: number;
  supplierAdTitle: string;
  supplierCompanyName: string;
  price: number;
  supplierAdImage: string;
  adImageId: number;
  imageName: string;
  imageLoaded: boolean;
  isSaved: boolean;
  adStatus: number;
  customerSavedAdsId: number;
  subCategoryId: number;
}
export interface ICategoryList {
  categoryId: number;
  categoryName: string;
}
export interface IDropDownListCat {
  Value: number;
  Name: string;
}
export interface ICategoryDropDownSettings {
  singleSelection: boolean;
  idField: string;
  textField: string;
  itemsShowLimit?: number;
  enableCheckAll?: boolean;
  closeDropDownOnSelection: boolean;
}
//export interface IFilterDropDownSettings {
//  singleSelection: boolean;
//  idField: string;
//  textField: string;
//  closeDropDownOnSelection: boolean;
//}
export interface IDropDownListForProduct {
  singleSelection: boolean;
  idField: string;
  textField: string;
  allowSearchFilter: boolean;
  itemsShowLimit: number;
  selectAllText: string;
  unSelectAllText: string;
  closeDropDownOnSelection: boolean;
  enableCheckAll: boolean;
}

export interface ISmsVM {
  mobileNumber: string;
  mobileNumberList: string[];
  message: string;
}

export interface IFacebookLeads {
  id: number;
  fullName: string;
  phoneNumber: string;
  budget: number;
  skillId: number;
  subSkillId: number ;
  startedDate: string;
}
