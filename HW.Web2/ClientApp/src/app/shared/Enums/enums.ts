export var loginsecurity = {
  CRole: "Customer",
  TRole: "Tradesman",
  ORole: "Organization",
  SRole: "Supplier"
}
export var AspnetRoles = {
  CRole: "3",
  TRole: "1",
  ORole: "2",
  SRole: "4"
}

export var CampaignTypes = {
  Promotion: "1",
  Advertisement:"2"
}

export var CampaignTypesName = {
  PCampaign: "Promotion",
  ACampaign:"Advertisement"
}
export var httpStatus = {
  Ok: "200",
  Restricted:"403",
  loginError:"invalid email address/phone number or password."
}
export var LoginValidation = {
  emailOrPhoneNumber: "Please enter email address or phone Number.",
  password: "Please enter Pin.",
  roleErrorMessage: "Please select your role.",
  unauthorizedUser: "You are Un-Authorized User.",
  InvalidCredentials:"Invalid Credentials.",
  InvalidPin:"Invalid Pin."
}
export var forgotPassword = { 
  forgotPasswordEmailError: "please enter email address.",
  forgotPasswordEmailOrPhoneError: "please enter valid phone number.",
  roleErrorMessage: "Please select your role.",
  unauthorizedUser: "Can't Send Email.",
  unauthorizedErrormessage:"You are unauthorized user."
}
export var resetPassword = {
  password: "Please enter Password.",
  confirmPassword: "Please enter Confirm Password.",
  passwordMatch:"Passwords not match."
}
export var ReportDatesValidation = {
  startDateErrorMessage: "Please enter start date.",
  endDateErrorMessage: "Please enter end date.",
}

export var SortedType = {
  lowest: 1,
  Highest:2,
  Saved:3

}
export class keyValue<T, U> {
  text: T | undefined;
  value: U | undefined;
}


export var RegistrationErrorMessages = {
  firstNameErrorMessage: "Please enter first name.",
  lastNameErrorMessage: "Please enter last name.",
  dataOfBirth:"Please enter date of birth.",
  passwordErrorMessage:"Please enter pin code.",
  genderErrorMessage:"Please select your gender.",
  cityErrorMessage:"Please select city.",
  roleErrorMessage:"Please select your role.",
  termsAndConditionErrorMessage: "Please check Terms & Conditions.",
  verificationErrorMessage:"Please enter verification code.",
  RoleType:"Please Select Your Role.",
  verificationPhoneNumberErrorMessage:"Please Enter Your Phone Number.",
  emailErrorMessage:"Please Enter Your Email.",
}
export var RegistrationErrorMessagesForSupplier = {
  TradeNameErrorMessage: "Please enter trade name.",
  primaryTradeErrorMessage: "Please select atleast one trade.",
  subCategoryErrorMessage: "Please select atleast subcategory.",
  bussinessErrorMessage: "Please enter bussiness address."
}

export var BidStatus ={
  Active : 1,
  Declined : 2,
  Completed: 3,
  Accepted: 4,
  Pending: 5,
  Urgent: 6,
  Deleted:7,
  StandBy:8,
}

export var AdsStatus = {
  Regular: 1,
  Featured: 2,
  SpotLight: 3,
  Pending:4
}

export var googleApiKey = {
  mapApiKey:'AIzaSyBh_wL0Jb7M1m4xBLNWmOWkVPbE5vpBHck'
}

export var JobQuotationErrors = {
  categoryId: "Please select category.",
  subcatgoryId: "Please select subcategory.",
  jobTitle: "Please enter job title.",
  jobDescription: "Please enter job description",
  cityError:"Please select city.",
  townError:"Please enter your town.",
  areaError:"Please enter address.",
  numberOfBidsError:"Please select number of bids.",
  startDateError: "Please select expected start date.",
  startDatelessError: "Please don't select previous  start date.",
  startTimeError:"Please select expected start time.",
  budgetError:"Please enter your budget.",
  streetAddressError:"Please enter location of work.",
  imageError: "Please select only four images.",
  budgetPatternError: "Please Enter Valid Budget",
  userName: "Please Enter User Name",
  email: "Please Enter Email Address",
  mobileNumber: "Please Enter Phone Number",
  relationShip:"Please Enter Your Property Relatioship"
}

export var PostAdErrors = {
  categoryIdError: "Please select category.",
  subCategoryIdError: "Please select sub category.",
  postTitleError: "Please enter ad title.",
  productDescriptionError: "Please enter information about your product.",
  priceError: "Please product price.",
  cityError: "Please select city.",
  townError: "Please enter town.",
  addressError: "Please enter address.",
  collectionError: "Please check produc available.",
  ImagesError: "Please select only 10 images.",
}

export var CommonErrors = {
  commonErrorMessage:"Ohh! Something went wrong."
}

export var BidErrorsMessage = {
  AmountError:"Please enter your amount."
}
export var TargetDatabase = {
  Customer :1,
  Tradesman :2,
  Supplier :3
}
export var NotificationTitles = {
  NewJobPost : "New job post",
  JobUpdted : "Job Update",
  NewMessage : "New message received",
  NewBid : "New bid",
  BidUpdated : "Bid Updated",
  NewCallRequest :"New call request",
  BidAccepted : "Bid accepted",
  BidDeclined : "Bid declined",
  JobIsFinished :"Job is finished",
  FeedbackRequest : "Feedback Request",
  PromoteYourAd : "Promote Your Ad",
  YourAdIsPosted : "Your Ad is posted successfully",
  NewFeedbackReceived : "New feedback received",
  BidCostUpdated : "Job cost updated",
  ExpiredAd : "AD Expiry Notification",
  Credit : "Referral Credit",
  NewOrderPlace : "New Order Placed",
  OrderStatus : "Order Status",
}
