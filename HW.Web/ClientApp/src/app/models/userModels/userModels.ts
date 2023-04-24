export class Customer {
  public firstName: string;
  public lastName: string;
  public cnic: number;
  public gender: number;
  public dateOfBirth: Date;
  public mobileNumber: string;
  public email: string;
  public profileImage: string;

}
export class ProfileImage {
  public imageBase64;
}

export class SupplierProfileImage {
  public imageBase64;
}

export class FinishedJobList {
  public city: string;
  public fileName: string;
  public jobDetailId: number;
  public jobEndTime: Date;
  public jobImage: string;
  public jobTitle: string;
  public lastName: string;
  public rating: number;
  public streetAddress: string;
  public town: string;
  public tradesmanId: number;
  public tradesmanName: string;

}

export class FinishedJobDetails {

  public customerId: number
  public directPayment: number
  public feedbackComments: string
  public isFavorite: number;
  public jobAddress: string;
  public jobDetailId: number;
  public jobEndingDateTime: Date;
  public jobStartingDateTime: Date;
  public jobTitle: string;
  public latLng: string
  public overallRating: number;
  public payment: number;
  public tradesmanId: number;
  public tradesmanName: string;
  public tradesmanProfileImage: string;
}
export class SupplierAdVM {
  public SupplierAdId: number;
  public SupplierAdTitle: string;
  public SupplierCompanyName: string;
  public Price: number
  public SupplierAdImage;
  public AdImageId: number
  public ImageName: string;
  public ImageLoaded: boolean;
  public IsSaved: boolean;
  public AdStatus: number;
  public CustomerSavedAdsId: number;
  public SubCategoryId: number;
}


export class InProgressJobList {
  public address: string;
  public area: string;
  public city: string;
  public createdOn: Date;
  public getcount: number;
  public jobQuotationId: number;
  public lastName: string;
  public title: string;
  public tradesmanId: number;
  public tradesmanName: string;
}

export class InProgressJobDetails {
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

export class PostedJobDetails {
  public address: string;
  public area: string;
  public budget: number;
  public catagory: string;
  public cityId: number;
  public jobDescription: string;
  public jobQuotationId: 40531
  public jobStartingDate: Date;
  public jobStartingTime: Date;
  public quotesQuantity: number;
  public selectiveTradesman: false
  public startingDateTime: Date;
  public subSkillId: number;
  public title: string;
}




export class ImageList {
  filePath: string;
  id: number;
  imageContent: string;
  isMain: boolean;
}

export class UserPaymentInformation {
  amount: number;
  createdOn: Date;
}

export class GetPostedJobs {
  bidCount: number;
  callCount: string;
  jobQuotationId: number;
  postedDate: Date;
  workTitle: string;
  workingAddress: string;
}

export class Notifications {

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

export class TrademanProfileVm {
  email: string;
  gpsCoordinates: string;
  markerOptionsAddress: string;
  mobileNumber: string;
  tradesmanAddress: string;
  tradesmanId: number;
  tradesmanName: string;
  tradesmanProfileImg: string;
  tradesmanUserId: string;
  skillsSet: [];
  feedbacks: [];
}
export class feedbacks {
  customerAddress: string;
  customerComment: string;
  customerName: string;
  rating: number;
}


export class ReceivedBidVM {

  bidAudioFileName: string;
  bidAudioMessage: string;
  bidId: number;
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

}

export class FilterDropDown {
  Value: number;
  Name: string;
}


export class ReceivedBids {
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

export class GetQuotes {
  userId: number;
  jobQuotationId: number;
  workTitle: string;
  jobDescription: string;
  budget: number;
  addressLine: string;
  town: string;
  area: string;
  streetAddress: string;
  categoryId: string;
  subCategoryId: number;
  cityId: number;
  jobstartDateTime: Date;
  numberOfBids: number;
  startedDate: Date;
  startTime: string;
  locationCoordinates: string;
  createdBy: string;
  imageVMs: Images[]=[];
  favoriteIds: number[];
  videoVM: VideoVm;
  skillName: string;
  cityName: string;
  selectiveTradesman: boolean;
  statusId: number;
  addressArea: string;
}

export class ImageVM {
  id: number;
  filePath: string;
  imageBase64: string;
  IsMain: boolean;
  ImageContent: [];
  localUrl: any;
}

export class Images {
  id: number;
  filePath: string;
  imageBase64: string;
  IsMain: boolean;
  ImageContent: [];
}

export class VideoVm {
  VideoId: number;
  FilePath: string;
  VideoContent: [];
  JobQuotationId: number;
  IsActive: boolean;
}

export class SupplierRatingVM {

  supplierFeedbackId: number;
  customerName: string;
  comments: string;
  rating: number;
  createdOn: Date;




}

export class PostedJobDetailVM {
  public address: string;
  public area: string;
  public budget: number;
  public catagory: string;
  public cityId: number;
  public jobDescription: string;
  public jobQuotationId: number;
  public jobStartingDate: string;
  public jobStartingTime: string;
  public quotesQuantity: number
  public selectiveTradesman: false
  public startingDateTime: Date;
  public subSkillId: number;
  public title: string;
}


export class PostedJobsVM {
  public jobQuotationId: number;
  public catagory: string;
  public subCatagory: number[] = [];
  public title: string;
  public jobDescription: string;
  // public images: string[] = [];
  public imageVMs: ImageVM[] = [];
  public video: number;
  public videoFileName: string;
  public videoUpdated: boolean;
  public budget: number;
  public startingDateTime: Date
  public jobStartingDate: string;
  public jobStartingTime: string;
  public cityId: number;
  public area: string;
  public address: string;
  public quotesQuantity: number;
  public selectiveTradesman: boolean;
  public subSkillId: number;
  public tradesmanList: number[] = [];
  public citiesList: number[] = [];

}
