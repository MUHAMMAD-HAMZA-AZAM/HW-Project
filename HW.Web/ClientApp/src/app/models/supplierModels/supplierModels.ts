import { Data } from "@angular/router";
import { Images } from "../userModels/userModels";

export class SupplierProfile {
  persnalDetails: persnalDetails;
  businessDetails: businessDetails;
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
}
//export class SupplierBussinessDetail {
// // SupplierId: number;
//  CompanyName: string;
//  CompanyRegistrationNo: string;
//  PrimaryTradeId: number;
//  PrimaryTrade: string;
//  ProductIds: any;
//  DeliveryRadius: number;
//  CityId: number;
//  BusinessAddress: string;
//  LocationCoordinates: string;
//  EmailAddress: string;
//}
export class PostAdVM {
  supplierAdId: number;
  supplierId: number;
  productCategoryId: string;
  productSubcategoryId: string;
  productSubCategory: string;
  postTitle: string;
  postDiscription: string;
  price: number;
  town: string;
  address: string;
  cityId: string;
  statusId: number;
  imageVMs:Images[] = [];
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
