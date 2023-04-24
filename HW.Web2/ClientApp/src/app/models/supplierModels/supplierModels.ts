import { Data } from "@angular/router";
import { Images } from "../userModels/userModels";

export interface SupplierProfile {
  persnalDetails: persnalDetails;
  businessDetails: businessDetails;
}

export class persnalDetails {
  userId?: string;
  entityId?: number;
  firstName?: string;
  lastName?: string;
  email?: string;
  cnic?: string;
  gender?: number;
  role?: string;
  profileImage?: string;
  dateOfBirth?: string;
  mobileNumber?: string;
  isEmailConfirmed?: boolean;
  isNumberConfirmed?: boolean;

}
export class businessDetails {
  supplierId?: number;
  companyName?: string;
  registrationNumber?: number;
  primaryTrade?: number;
  productsubCategory?: any;
  city?: number;
  cityName?: string;
  businessAddress?: string;
  deliveryRadius?: string;
  selectedSubCategory?: any;
  locationCoordinates?: string;
}

export class BusinessDetailUpdate {
  CompanyName?: string;
  CompanyRegistrationNo?: string;
  PrimaryTradeId?: number;
  PrimaryTrade?: string;
  ProductIds?: number[] = [];
  DeliveryRadius?: number;
  CityId?: number;
  BusinessAddress?: string;
  LocationCoordinates?: string;
  EmailAddress?: string;
}

export class PersonalDetailsUpdate {
  UserId?: number;
  EntityId?: number;
  FirstName?: string;
  LastName?: string;
  Email?: string;
  Cnic?: string;
  Gender?: number;
  Role?: string;
  DateOfBirth?: string;
  MobileNumber?: string;
  ProfileImage?: string;
}
//export class SupplierBussinessDetail {
// // SupplierId?: number;
//  CompanyName?: string;
//  CompanyRegistrationNo?: string;
//  PrimaryTradeId?: number;
//  PrimaryTrade?: string;
//  ProductIds?: any;
//  DeliveryRadius?: number;
//  CityId?: number;
//  BusinessAddress?: string;
//  LocationCoordinates?: string;
//  EmailAddress?: string;
//}
export class PostAdVM {
  supplierAdId?: number;
  supplierId?: number;
  productCategoryId?: number;
  productSubcategoryId?: number;
  productSubCategory?: string;
  postTitle?: string;
  postDiscription?: string;
  price?: number;
  town?: string;
  address?: string;
  cityId?: string;
  discount?: string;
  statusId?: number;
  imageVMs?: Images[] = [];
  videoVM?: any;
  deliveryAvailable?: string;
  collectionAvailable?: boolean;
  Active?: boolean;
  supplierStatusId?: string;
  createdOn?: Date;
  activeFrom?: Date;
  activeTo?: Date;
  createdBy?: Date;
  videoPath?: Date;
}


export class ContactUsVm {
  emailAddresses?: string[] = [];
  bcc?: string[] = [];
  subject?: string;
  body?: string;
  phone?: string;
  name?: string;
}
