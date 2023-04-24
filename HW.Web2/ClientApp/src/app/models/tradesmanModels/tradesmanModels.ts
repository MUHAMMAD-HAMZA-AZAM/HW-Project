export interface jobDetails {
  customerId: number;
  customerName: string;
  jobDetailId: number;
  jobQuotationId: number;
  customerEmail: string;
  jobTitle: string;
  jobDescription: string;
  jobImages: [];
  latLong: string;
  budget: number;
  tradesmanBid: number;
  expectedJobStartDate: Date;
  expectedJobStartTime: Date;
  tradesmanMessage: string;
  jobLocation: string;
  jobAddress: string;
  jobAddressLine: string;
  customerAddress: string;
  userAddress: string;
  bidId: number;
  video: []
  audioMessage: string;
  isFinish: string;
  isFinished: boolean;
}
export interface MarkAsFinished {
  JobDetailId: number
  CustomerId: number
  TradesmanId: number
  JobQuotationId: number
  StatusId: number
  StartDate: Date
  EndDate: Date
  BidId: number
  BidStatus: number
  isPaymentDirect: boolean
}
export interface tradesmanProfile {
  persnalDetails: persnalDetails;
  businessDetails: BusinessDetailsupdate;
}

export interface persnalDetails {
  userId: string;
  entityId: number;
  firstName: string;
  lastName: string;
  email: string;
  cnic: string;
  gender: number;
  role: string;
  profileImage: string;
  dateOfBirth: string;
  mobileNumber: string;
  isNumberConfirmed: boolean;
  isEmailConfirmed: boolean;
  firebaseClientId: string;

}
export interface businessDetails {
  addressLine: string;
  businessAddress: string;
  city: string;
  cityId: number;
  companyName: string;
  companyRegNo: string;
  isOrganization: boolean;
  latLng: string;
  locationCoordinates: string;
  skillIds: number;
  town: string;
  townId: number;
  tradesmanId: number;
  tradesmanSkills: any;
  travelingDistance: number;

}


export interface PersonalDetailsUpdate {
  userId: number;
  entityId: number;
  firstName: string;
  lastName: string;
  email: string;
  cnic: string;
  gender: number;
  role: string;
  dateOfBirth: string;
  mobileNumber: string;
  profileImage: string;
}

export interface BusinessDetailsupdate {
  tradesmanId: number;
  travelingDistance: number;
  companyName: string;
  companyRegNo: string;
  cityId: number;
  town: string;
  businessAddress: string;
  skillIds: number[];
  city: string;
  tradesmanSkills: string[];
  isOrganization: boolean;
  locationCoordinates: string;
}


export interface TradesmanProfileImage {
  imageBase64: string;
}
export interface TradesmanPayments {
  amount: number;
  paymentMode: string;
  paymentMonth: string;
  paymentStatus: number;
  discountedAmount: number;
  serviceCharges: number;
  otherCharges: number;
  commission: number;
  netPayableToTradesman: number;
  payableAmount: number;
  paidViaWallet: number;
}

export interface ActiveBidsVM {
  budget: number;
  customerName: string;
  date: string;
  bidImage: string;
  jobDate: string;
  jobQuotationid: number;
  lastName: string;
  workAddress: string;
  workTitle: string;
}

export interface BidVM {
  BidsId: number;
  JobQuotationId: number;
  //Audio { get; set; }
  TradesmanId: number;
  CustomerId: number;
  CustomerBudget: string;
  Comments: string;
  Amount: number;
  StatusId: number;
  CreatedBy: string;
  JobTitle: string;
  Base64String: string;
  Audio: AudioVM;
}

export interface AudioVM {
  AudioId: number;
  FileName: string;
  Base64String: string;
}



export interface TradesmanRatingsVM {
  tradesmanFeedbackId: number;
  jobDetailId: number | undefined;
  customerId: number;
  tradesmanId: number | undefined;
  comments: number
  overallRating: number
  communicationRating: number;
  qualityRating: number
}

export interface CompletedJobDetailsVM {
  jobDetailId: number;
  customerName: string;
  customerId: number;
  jobQuotationId: number
  jobTitle: string;
  jobStartedDate: Date;
  jobFinishDate: Date;
  payment: number;
  latLong: string;
  rating: number
  comment: string;
  jobAddress: string;
  mapAddress: string;

}
export interface skillsVM {
  skillId: number
  name  : string
  skillTitle: string
  metaTags: string
  description: string
  createdOn :Date
  createdBy: string
  modifiedOn : Date
  modifiedBy: string
  isActive: boolean
  orderByColumn: number
  skillImage: string
  imagePath: string
  slug: string

}
