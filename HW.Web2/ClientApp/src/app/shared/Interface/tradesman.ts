export interface Tradesman {
}


export interface IIdValue {
  id: number;
  value: string;
}

export interface IGetCitiesAndDistanceVM {
  cities: Object;
  distances: Object;
  productCategories: Object;
}

export interface IDropdownSettings {
  singleSelection: boolean;
  idField: string;
  textField: string;
  allowSearchFilter: boolean;
  itemsShowLimit: number;
  selectAllText: string;
  unSelectAllText: string;
  enableCheckAll: boolean;
}

export interface ITownListVM {
  name: string;
  townId: number;
}

export interface IApplicationSetting {
  applicationSettingId: number;
  settingName: string;
  action: string;
  userId: string;
  applictaionSettingDetailId: number;
  settingKeyName: string;
  settingKeyValue: number;
  isActive: boolean;
  createdBy: string;
  modifiedBy: string;
  createdOn: string;
  modifiedOn: string;
}
export interface IPersonalDetails {
  userId: string;
  entityId: number;
  shopName: string;
  jobQuotationId: number;
  firstName: string;
  lastName: string;
  email: string;
  cnic: string;
  isNumberConfirmed: boolean;
  isEmailConfirmed: boolean;
  gender: number;
  role: string;
  profileImage: number[];
  dateOfBirth: Date;
  mobileNumber: string;
  latLong: string;
  town: string;
  townId: number;
  cityId: number;
  address: string;
  relationship: string;
  userRole: string;
  supplierId: number;
  customerId: number;
  accountType: number;
  featuredSupplier: boolean;
  city: string;
}

export interface ISkill {
  skillId: number;
  id: number;
  productCategoryId?: number;
  name: string;
  value: string;
  skillTitle: string;
  metaTags: string;
  description: string;
  createdOn: Date;
  createdBy: string;
  modifiedOn: Date;
  modifiedBy: string;
  isActive: boolean;
  orderByColumn: number;
  skillImage: number[];
  skillIconPath: string;
  imagePath: string;
  slug: string;
}
export interface ISkillIdValue {
  skillId: number;
  name: string;
}

export interface IActivePromotionVM {
  promotionId: number;
  name: string;
  description: string;
  image: number[];
  base64Image: string;
  isAcitve: boolean;
  isMain: boolean;
  createdBy: string;
  createdOn: Date;
  modifiedBy: string;
  modifiedOn: Date;
  action: string;
  skillName: string;
  skillId: number;
  subSkillIds: string;
  userRoleId: number;
  promotionStartDate: Date;
  promotionEndDate: Date;
  promotionFor: string;
}
export interface ITownListVM {
  name: string;
  townId: number;
}

export interface IErrorMessage {
  jobTitleError?: string;
  jobDiscriptionError?: string;
  cityIdError?: string;
  townError?: string;
  areaError?: string;
  startedDateError?: string;
  budgetError?: string;
  budgetPatternError?: string;
  userNameError?: string;
  emailerror?: string;
  phoneNumberError?: string;
  propertyRelationShipError?: string;
}

export interface IFileResult {
  result: object;
}

export interface IJobDetailWebVM {
  jobDetailId: number;
  bidImage: string[];
  workTitle: string;
  jobQuotationId: number;
  workAddress: string;
  jobStartedDate: string;
  date: Date;
  customerName: string;
  lastName: string;
  rating: number;
  jobCreater: string;
  workBudget: number;
  customerId: number;
}

export interface IEsclateOption {
  id: number;
  name: string;
  active: boolean;
  createdOn: Date;
  createdBy: string;
  modifiedOn: Date;
  modifiedBy: string;
  userRole: number;
}

export interface IJobLeadsWeb{
  jobTitle: string;
  postedOn: string;
  address: string;
  city: string;
  budget: number;
  bidImage: string[];
  bidCount: number;
  jobQuotesId: number;
}

export interface IBidWeb {
  jobQuotationid: number;
  bidImage: number[];
  fileName: string;
  workTitle: string;
  jobDate: string;
  customerName: string;
  lastName: string;
  workAddress: string;
  budget: number;
  date: Date;
  customerBudget: number;
}

export interface IBidDetails {
  bidId: number;
  tradesmanUserId: string;
  tradesmanId: number;
  customerId: number;
  tradesmanName: string;
  tradesmanProfileImage: number[];
  jobImage: number[];
  jobTitle: string;
  customerBudget: number;
  tradesmanOffer: number;
  jobDescription: string;
  bidAudioMessage: number[];
  bidAudioFileName: string;
  mobileNumber: string;
  email: string;
  jobQuotationId: number;
  bidPostedOn: Date;
  tradesmanAddress: string;
  customerName: string;
  bidStatusId: number;
  jobDetailsId: number;
  visitCharges: number;
  serviceCharges: number;
  otherCharges: number;
  skillId: number;
  workStartDate: Date;
  isStarted: boolean;
  isFinished: boolean;
  action: string;
  paymentMethod: number;
  pageNumber: number;
  pageSize: number;
}

export interface ITradesmanObj {
  tradesmanId: number;
  obQuotationId: number;
}

export interface ISubSkill {
  subSkillId: number;
  name: string;
  skillId: number;
  createdOn: Date;
  createdBy: string;
  modifiedOn: Date;
  modifiedBy: string;
  isActive: boolean;
  orderByColumn: number;
  subSkillImage: number[];
  metaTags: string;
  imagePath: string;
  description: string;
  slug: string;
  subSkillTitle: string;
  subSkillPrice: number;
  visitCharges: number;
}

export interface IPostVM {
  categoryId: number;
  postId: number;
  postTitle: string;
  postContent: string;
  summary: string;
  postStatus: number;
  commentStatus: boolean;
  createdBy: string;
  modifiedBy: string;
  isActive: boolean;
  imageBase64: string;
  headerImage: number[];
  categoryName: string;
  postStatusName: string;
  userName: string;
  postAction: string;
  createdOn: Date;
  metaTags: string;
  slug: string;
  pageNumber: number;
  pageSize: number;
  postsCount: number;
}
export interface IWebLiveLeads {
  workDescription: string;
  workTitle: string;
  workBudget: number;
  cityName: string;
  createdOn: Date;
  area: string;
  gpsCoordinates: string;
  jobImage: number[];
  jobQuotationId: number;
  totalJobs: number;
}

export interface ITradesman {
  tradesmanId: number;
  userId: string;
  publicId: string;
  emailAddress: string;
  mobileNumber: string;
  firstName: string;
  lastName: string;
  cnic: string;
  gender: number;
  dob: Date;
  travellingDistance: number;
  isFavorite: boolean;
  isOrganization: boolean;
  companyName: string;
  companyRegNo: string;
  gpsCoordinates: string;
  city: string;
  area: string;
  shopAddress: string;
  addressLine: string;
  createdOn: Date;
  createdBy: string;
  modifiedOn: Date;
  modifiedBy: string;
  isActive: boolean;
}

export interface ITradesmanReportbySkill extends Tradesman {
  name: string;
  profileImage: number[];
  reviews: number;
  rating: number;
  skillId: number;
}

export interface IProductCategory {
  productCategoryId: number;
  code: string;
  productImage: number[];
  imagebase64: string;
  name: string;
  isActive: boolean;
  orderByColumn: number;
  createdOn: Date;
  createdBy: string;
  modifiedOn: Date;
  modifiedBy: string;
}

