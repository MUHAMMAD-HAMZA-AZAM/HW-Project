export interface LoginVM {
  emailOrPhoneNumber: string;
  rememberMe: boolean,
  password: string;
  role: string;
  facebookClientId: string;
  googleClientId: string;
}

export interface ResponseVm
{
  status: any;
  message: string;
  resultData: any;
}

export interface RequestCallVm {
  name: string;
  phoneNumber: string;
  isActive: boolean;
}

export interface IfindTradesmanSearch {
  selectedskill: object;
  selectedtown: object;
}

export interface forgotPasswrodVm {
  id: string;
  email: string;
  password: string;
  confirmPassword: string;
  phoneNumber: string;
  role: string;
  firebaseClientId: string;
  verificationCode: string;
  clientId: string;
}
export interface ResetPassword {
  userId: string;
  passwordResetToken: string;
  password: string;
  confirmPassword: string;
}
export interface ResetPasswordVm {
  password: string;
  confirmPassword: string;
}

export interface BasicRegistration {
  role: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  cnic: number;
  emailAddress: string;
  password: string;
  gender: number;
  phoneNumber: string;
  city: string;
  termsAndcondition: boolean;
  verificationCode: string;
  emailOrPhoneNumber: string;
  facebookUserId: string;
  googleUserId: string;
  facebookClientId: string;
  googleClientId: string;
  email: string;
}
export interface CheckEmailandPhoneNumberAvailability {
  role: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  cnic: number;
  emailAddress: string;
  password: string;
  gender: number;
  phoneNumber: string;
  city: string;
  termsAndcondition: boolean;
  verificationCode: string;
  emailOrPhoneNumber: string;
  facebookUserId: string;
  googleUserId: string;
  email: string;
}
export interface IdValueVm {
  id: number;
  value: string;
}


export declare interface SocialUserInfo {
  provider: string;
  id: string;
  emailAddress: string;
  name: string;
  photoUrl: string;
  firstName: string;
  lastName: string;
  authToken: string;
  idToken: string;
  mobileNumber: number;
  gender: string;
  password: string;
  authorizationCode: string;
  facebook: any;
  linkedIn: any;
}

export interface AdsParameterVM {
  customerId: number;
  productCategoryIds: number[];
  subCategoryId: number;
  sortBy: string;
  pageNumber: number;
  pageSize: number;
}

export interface AdPackageDetailsVM {
  userId: number;
  packageIds: any;
  totalAmount: number;
  roleId: number;
}

export interface webLiveLeadsPanel {
  activeLiveLeads: any;
  inProgressLiveLeads: any;
  completedLiveLeads: any;
  activeLiveLeadsCount: number;
  inProgressLiveLeadsCount: number;
  completedLiveLeadsCount: number;
}
