export class PrimaryUserVM {
  public id: string;
  public customerId: number;
  public customerName: string;
  public customerMobile: string;
  public skillName: string;
  public jobsPosted: number;
  public noOfRecoards: number;
  public RecordNo: number;
  public customerCNIC: string;
  public sourceOfReg: string;
  public userId: string;
  public salesmanName: string;
  public isTestUser: boolean;
  public emailConfirmed: boolean;
  public phoneNumberConfirmed: boolean;
  public isselectedforexport: boolean;
  public createdOn: Date;
  public lastActive: Date;

}
export class Customer {

  public customerId: string;
  public userId: string;
  public firstName: string;
  public lastName: string;
  public cnic: number;
  public gender: number;
  public dateOfBirth: Date;
  public mobileNumber: string;
  public email: string;
  public profileImage: string;
  public entityId: string;
  public City: any;

}

export class SpUserProfileVM {
  public userId: number;
  public isActive: boolean;
  public firstName: string;
  public lastName: string;
  public cnic: string;
  public email: string;
  public dateofBirth: string;
  public mobileNo: string;
  public gender: number;
  public profileImage: string;
  public createdOn: Date;

}
export class SupplierProfileImage {
  public imageBase64;
}
export class SpBusinessProfileVM {
  public userId: number;
  public tradeName: string;
  public registrationNo: string;
  public primaryTrade: string;
  public travellingDistance: string;
  public skills: string;
  public latLong: string;
  public businessAddress: string;
  public town: string;
  public city: string;
  public firstName: string;
  public lastName: string;
  public cnic: string;
  public gender: number;
  public dob: Date;
  public dOB: Date;
  public createdOn: Date;
  public mobile: string;
  public email: string;
  public profileImage: string;

}
export class PersonalDetailsUpdate {
  UserId: number;
  EntityId: number;
  FirstName: string;
  LastName: string;
  Email: string;
  Cnic: string;
  Gender: number;
  Role: string;
  DateOfBirth: string;
  MobileNumber: string;
  ProfileImage: string;
  UserRole: string;
  CustomerId: string;
  FeaturedSupplier: boolean
  city: string;
  cityId: number;
}
export class BusinessDetailsupdatetrd {
  tradesmanId: number;
  travelingDistance: number;
  companyName: string;
  companyRegNo: string;
  cityId: number;
  town: number;
  businessAddress: string;
  skillIds: number[] = [];
  city: string;
  tradesmanSkills: object[] = [];
  isOrganization: boolean;
  locationCoordinates: string;
  userId: number;
}
export class PersonalDetailsUpdatetrd {
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
  tradesmanId: number;
}

export class BusinessDetailUpdate {
  CompanyName: string;
  CompanyRegistrationNo: string;
  PrimaryTradeId: number;
  PrimaryTrade: string;
  ProductIds: number[] = [];
  DeliveryRadius: number;
  CityId: number;
  BusinessAddress: string;
  LocationCoordinates: string;
  EmailAddress: string;
  SupplierId: number;
  FeaturedSupplier: boolean;
}
export class persnalDetails {
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
  featuredSupplier: string;
}
export class businessDetails {
  supplierId: number;
  companyName: string;
  registrationNumber: number;
  primaryTrade: number;
  productsubCategory: any;
  city: number;
  businessAddress: string;
  deliveryRadius: string;
  selectedSubCategory: any;
  locationCoordinates: string;
}

export class SupplierProfile {
  persnalDetails: persnalDetails;
  businessDetails: businessDetails;
}
export class tradesmanProfile {
  persnalDetails: persnalDetails;
  businessDetails: businessDetailstrd;
}
export class BusinessDetailsupdate {
  tradesmanId: number;
  travelingDistance: number;
  companyName: string;
  companyRegNo: string;
  cityId: number;
  town: number;
  businessAddress: string;
  skillIds: number[] = [];
  city: string;
  tradesmanSkills: any;
  isOrganization: boolean;
  locationCoordinates: string;
}
export class businessDetailstrd {
  addressLine: string;
  businessAddress: string;
  city: string;
  cityId: number;
  companyName: string;
  companyRegNo: string;
  isOrganization: boolean;
  latLng: string;
  locationCoordinates: string;
  skillIds;
  town: string;
  tradesmanId: number;
  tradesmanSkills: any;
  travelingDistance: number;

}
export class PostAdVM {
  supplierAdId: number;
  supplierId: number;
  productCategoryId: string;
  productSubcategoryId: number;
  productSubCategory: string;
  postTitle: string;
  postDiscription: string;
  price: number;
  town: string;
  address: string;
  cityId: string;
  statusId: number;
  imageVMs: Images[] = [];
  videoVM: any;
  deliveryAvailable: string;
  collectionAvailable: boolean;
  supplierStatusId: string;
  createdOn: Date;
  activeFrom: Date;
  activeTo: Date;
  createdBy: Date;
  videoPath: Date;
}
export class Images {
  id: number;
  filePath: string;
  imageBase64: string;
  IsMain: boolean;
  ImageContent: [];
}
export class ImageVM {
  id: number;
  filePath: string;
  imageBase64: string;
  IsMain: boolean;
  ImageContent: [];
  localUrl: any;
}

export interface IAnalyticsModal {
  
  name: string;
  id: number;
  ip: string;
  device: string;
  platform: string;
  os: string;
  osVersion: string;
  ipLocation: string;
  applicationType: string;
  createdBy: string;
  createdOn: string;  
  applicaitonVersion: string;
  mobileDevice: boolean;
  tabletDevice: boolean;
  desktopDevice: boolean;
  browser: string;
  browserVersion: string;
  country: string;
  countryCapital: string;
  city: string;
  district: string;

}

