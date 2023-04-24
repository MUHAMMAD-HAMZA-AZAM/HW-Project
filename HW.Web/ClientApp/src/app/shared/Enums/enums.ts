export var loginsecurity = {
  CRole: "Customer",
  TRole: "Tradesman",
  ORole: "Organization",
  SRole: "Supplier"
}
export var httpStatus = {
  Ok: "200",
  loginError:"invalid email address/phone number or password."
}
export var LoginValidation = {
  emailOrPhoneNumber: "Please enter email address or phone Number.",
  password: "Please enter password.",
  unauthorizedUser: "You are Un-Authorized User.",
  InvalidCredentials:"Invalid Credentials."
}
export var forgotPassword = { 
  forgotPasswordEmailError: "please enter email address.",
  forgotPasswordEmailOrPhoneError: "please enter valid email address or phone number.",
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
  text: T;
  value: U;
}


export var RegistrationErrorMessages = {
  firstNameErrorMessage: "Please enter first name.",
  lastNameErrorMessage: "Please enter last name.",
  dataOfBirth:"Please enter date of birth.",
  passwordErrorMessage:"Please enter password.",
  genderErrorMessage:"Please select your gender.",
  cityErrorMessage:"Please select city.",
  roleErrorMessage:"Please select your role.",
  termsAndConditionErrorMessage: "Please check Terms & Conditions.",
  verificationErrorMessage:"Please enter verification code."
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
  Pending: 5
}

export var AdsStatus = {
  Regular: 1,
  Featured: 2,
  SpotLight: 3,
  Pending:4
}

export var googleApiKey = {
  mapApiKey:'AIzaSyBXL-PpLFMRj9h1GhosFJ3eWCwL3r5MvGI'
}

export var JobQuotationErrors = {
  categoryId: "Please select category.",
  subcatgoryId: "Please select subcategory.",
  jobTitle: "Please enter job title.",
  jobDescription: "Please enter job description",
  cityError:"Please select city.",
  townError:"Please enter your town.",
  areaError:"Please enter area.",
  numberOfBidsError:"Please select number of bids.",
  startDateError:"Please select expected start date.",
  startTimeError:"Please select expected start time.",
  budgetError:"Please enter your budget.",
  streetAddressError:"Please enter location of work.",
  imageError:"Please select only four images.",
}

export var PostAdErrors = {
  categoryIdError: "Please select category id.",
  subCategoryIdError: "Plese select sub category id.",
  postTitleError: "Plese enter ad title.",
  productDescriptionError: "Plese enter information about your product.",
  priceError: "Plese product price.",
  cityError: "Plese select city.",
  townError: "Plese enter town.",
  addressError: "Plese enter address.",
  collectionError: "Plese check produc available.",
}
