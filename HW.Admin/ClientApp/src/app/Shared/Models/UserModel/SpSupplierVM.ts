export class SpSupplierStatsVM {

  public supplierCount: number;
  public supplierAdsCount: number;
  public pageSize: number;
}

export class SpSupplierListVM {

  public id: string;
  public supplierId: number;
  public isActive: boolean;
  public companyName: string;
  public cNIC: string;
  public supplierCategory: string;
  public mobileNo: string;
  public supplierAddress: string;
  public supplierAdsCount: number;
  public noOfRecoards: number;
  public RecordNo: number;
  public userId: string;
  public salesmanName: string;
  public sourceOfReg: string;
  public isTestUser: boolean;
  public emailConfirmed: boolean;
  public phoneNumberConfirmed: boolean;
  public createdOn: Date;
  public lastActive: Date;
  public isselectedforexport: boolean;
  public featuredSupplier: boolean
}

export class BasicRegistration {
  role: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  cnic: number;
  emailAddress: string;
  password: string;
  gender: number;
  phoneNumber: number;
  city: string;
  cityId: number;
  termsAndcondition: boolean;
  verificationCode: string;
  emailOrPhoneNumber: string;
  facebookUserId: string;
  googleUserId: string;
  facebookClientId: string;
  googleClientId: string;
  email: string;
  mobileNumber: number;
}
export class CheckEmailandPhoneNumberAvailability {
  role: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  cnic: number;
  emailAddress: string;
  password: string;
  gender: number;
  phoneNumber: number;
  city: string;
  termsAndcondition: boolean;
  verificationCode: string;
  emailOrPhoneNumber: string;
  facebookUserId: string;
  googleUserId: string;
  email: string;
}
export class IdValueVm {
  id: number;
  value: string;
}
