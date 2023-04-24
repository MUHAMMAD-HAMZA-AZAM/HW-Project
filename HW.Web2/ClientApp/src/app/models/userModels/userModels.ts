import { DecimalPipe } from "@angular/common";
import { IdValueVm } from "../commonModels/commonModels";

export interface Customer {
  customerId: number;
  entityId: number;
  firstName: string;
  lastName: string;
  cnic: number;
  gender: number;
  dateOfBirth: Date;
  mobileNumber: string;
  phoneNumber: string;
  role: string;
  fromPersonalDetails: boolean;
  facebookUserId: string;
  googleUserId: string;
  email: string;
  profileImage: string;
  isNumberConfirmed: boolean;
  isEmailConfirmed: boolean;
  cityId: number;
  city: string;
  dob: string;
}

export interface ProfileImage {
   imageBase64: string;
}

export interface SupplierProfileImage {
   imageBase64: string;
}

export interface FinishedJobList {
   city: string;
   fileName: string;
   jobDetailId: number;
   jobEndTime: Date;
   jobImage: string;
   jobTitle: string;
   lastName: string;
   rating: number;
   streetAddress: string;
   town: string;
   tradesmanId: number;
   tradesmanName: string;

}

export interface FinishedJobDetails {

   customerId: number
   directPayment: number
   feedbackComments: string
   isFavorite: number;
   jobAddress: string;
   jobDetailId: number;
   jobEndingDateTime: Date;
   jobStartingDateTime: Date;
   jobTitle: string;
   latLng: string
   overallRating: number;
   payment: number;
   tradesmanId: number;
   tradesmanName: string;
   tradesmanProfileImage: string;
   jobQuotationId: number;
}
export interface SupplierAdVM {
   SupplierAdId: number;
   SupplierAdTitle: string;
   SupplierCompanyName: string;
   Price: number
   SupplierAdImage: string;
   AdImageId: number
   ImageName: string;
   ImageLoaded: boolean;
   IsSaved: boolean;
   AdStatus: number;
   CustomerSavedAdsId: number;
   SubCategoryId: number;
}

export interface AdDetailsVM {
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
  imageIds: string[];
}

export interface SupplierShopVM {

  supplierId: number;
  supplierCompanyName: number
  supplierAddress: string;
  latLong: string;
  contactNo: string;
  email: string;
  supplierImage: string;
  supplierAds: [];
  supplierFeedbacks: [];
}



export interface InProgressJobList {
   address: string;
   area: string;
   city: string;
   createdOn: Date;
   getcount: number;
   jobQuotationId: number;
   lastName: string;
   title: string;
   tradesmanId: number;
   tradesmanName: string;
  jobImage: string;
  isFinished: boolean;
  BidsId: number;
  TradesmanOffer: DecimalPipe;
  jobDetailId: number;
}

export interface InProgressJobDetails {
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
  workStartDate: Date
  workTitle: string;
  imageList: [];
}

export interface PostedJobDetails {
   address: string;
   area: string;
   budget: number;
   catagory: string;
   cityId: number;
   jobDescription: string;
   jobQuotationId: 40531
   jobStartingDate: Date;
   jobStartingTime: Date;
   quotesQuantity: number;
   selectiveTradesman: false
   startingDateTime: Date;
   subSkillId: number;
   title: string;
}




export interface ImageList {
  filePath: string;
  id: number;
  imageContent: string;
  isMain: boolean;
}

export interface UserPaymentInformation {
  amount: number;
  createdOn: Date;
  payableAmount: number;
  discountedAmount: number;
  paymentStatus: number;
  paidViaWallet: number;
}

export interface GetPostedJobs {
  bidCount: number;
  callCount: string;
  jobQuotationId: number;
  postedDate: string;
  workTitle: string;
  workingAddress: string;
  workStartDate: Date;
   jobImage: string;
}

export interface Notifications {

  body: string;
  createdOn: Date;
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

export interface TrademanProfileVm {
  email: string;
  gpsCoordinates: string;
  markerOptionsAddress: string;
  mobileNumber: string;
  rating: number;
  tradesmanAddress: string;
  tradesmanId: number;
  tradesmanName: string;
  tradesmanProfileImg: string;
  tradesmanUserId: string;
  skillsSet: [];
  feedbacks: feedbacks[];
}
export interface feedbacks {
  customerAddress: string;
  customerComment: string;
  customerName: string;
  rating: number;
}


export interface ReceivedBidVM {

  bidAudioFileName: string;
  bidAudioMessage: string;
  bidId: number;
  customerName: string;
  bidPostedOn: Date;
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
export interface PromotionTypeVM {
  promotionTypeId: number;
  promotionTypeName: string;
  promotionTypeCode: string;
  amount: number;
  categoryId: number;
}
export interface VoucherVM {
  voucherBookLeavesId: number;
  voucherBookId: number;
  voucherTypeId: number;
  pageNumber: number;
  voucherNo: string;
  validFrom: Date;
  validTo: Date;
  isUsed: boolean;
  discountedAmount: number;
  persentageDiscount: number;
  active: boolean;
  createdOn: Date;
  createdBy: string;
  modifiedBy: string;
  modifiedOn: Date;
}
export interface PromotionRedemptionsVM {
  promotionRedemptionsId: number;
  promotionId: number;
  redeemBy: string;
  redeemOn: Date;
  totalDiscount: number;
  jobQuotationId: number;
  customerId: number;
  tradesmanId: number;
  supplierId: number;
  jobDetailId: number;
  isVoucher: boolean;
  voucherBookLeavesId: number;
  categoryId: number;
    }

export interface FilterDropDown {
  Value: number;
  Name: string;
}


export interface ReceivedBids {
  bidBy: string;
  bidId: number;
  bidImage: string;
  bidImageName: string;
  bidOn: Date;
  isSelected: boolean;
  jobQuotationTitle: string;
  tradesmanAddress: string;
  tradesmanId: number;
}

export interface GetQuotes {
  userId: number;
  jobQuotationId: number;
  workTitle: string;
  jobDescription: string;
  budget: number;
  town: string;
  townId: number;
  addressLine: string;
  jobStartTime: string;
  locationCoordinates: string;
  area: string;
  streetAddress: string;
  categoryId: string;
  subCategoryId: number | undefined;
  subCategoryName: string;
  startedDate: Date;
  cityId: number;
  jobstartDateTime: Date;
  numberOfBids: number;
  createdBy: string;
  imageVMs: Images[];
  fireBaseIds: [];
  skillName: string;
  cityName: string;
  selectiveTradesman: boolean;
  statusId: number;
  email: string;
  phoneNumber: string;
  relationship: string;
  city: any;
  firstName: string;
  address: string;
  name: string;
  visitCharges: number;
  serviceCharges: number;
  priceReview: string;
}

export interface GetQuotesVM {
  workTitle: string;
  jobDescription: string;
  categoryId: string;
  subCategoryId: number;
  jobstartDateTime: Date;
  startedDate: Date;
  jobStartTime: string;
  budget: number;
  imageVMs: Images[];
  visitCharges: number;
  serviceCharges: number;
}

export interface ImageVM  {
  id: number;
  filePath: any;
  imageBase64: string;
  IsMain: boolean;
  ImageContent: string;
  localUrl: string;
  thumbImage: string;
}

export interface Images {
  id: number;
  filePath: string;
  imageBase64: string;
  IsMain: boolean;
  ImageContent: string;
  localUrl: any;
  thumbImage: string;
  //isMain: boolean;
  //imageContent: [];
}

export interface VideoVm {
  VideoId: number;
  FilePath: string;
  VideoContent: [];
  JobQuotationId: number;
  IsActive: boolean;
}

export interface SupplierRatingVM {

  supplierFeedbackId: number;
  customerName: string;
  comments: string;
  rating: number;
  createdOn: Date;




}

export interface SmsVM {
   mobileNumberList: any;
   message: string
}

export interface PostedJobDetailVM {
   address: string;
   area: string;
   budget: number;
   catagory: string;
   cityId: number;
   jobDescription: string;
   jobQuotationId: number;
   jobStartingDate: string;
   jobStartingTime: string;
   quotesQuantity: number
   selectiveTradesman: false
   startingDateTime: Date;
   subSkillId: number;
   title: string;
}


export interface PostedJobsVM {
   jobQuotationId: number;
   catagory: string;
   subCatagory: number[];
   title: string;
   jobDescription: string;
  //  images: string[] = [];
   imageVMs: ImageVM[];
   video: number;
   videoFileName: string;
   videoUpdated: boolean;
   budget: number;
   startingDateTime: Date
   jobStartingDate: string;
   jobStartingTime: string;
   cityId: number;
   area: string;
   address: string;
   quotesQuantity: number;
   selectiveTradesman: boolean;
   subSkillId: number;
   tradesmanList: number[];
   citiesList: number[];

}

export interface customerDashBoardCountVM {

   activeJobsCount: number;
   completedJobsCount: number;
   blogsCount: number;
   notificationsCount: number;

}
