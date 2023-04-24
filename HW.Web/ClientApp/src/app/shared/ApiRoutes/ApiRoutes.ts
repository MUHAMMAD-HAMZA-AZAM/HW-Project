//let baseUrl: string = "http://192.168.100.22:15790/api/";
debugger;
var baseUrl: any; /*= "http://test.gateway.hoomwork.com/api/";*/
//let baseUrl: string ="https://www.hoomwork.com/gateway/api/";




//export var apiUrls = {
//  Customers: {
//    SpGetAdminDashBoard: "AdminCustomer/SpGetAdminDashBoard",
//    SpGetPrimaryUsersList: "AdminCustomer/SpGetPrimaryUsersList",
//    SpGetUserProfile: "AdminCustomer/SpGetUserProfile"
//  },
//  SpGetActiveJobList: {
//    SpGetActiveJobList: "AdminJob/GetActiveJobList"
//  },

//  InprogressJobList: {
//    InprogressJobList: "AdminJob/GetInprogressJobList"
//  },

//  Jobs: {
//    GetSupplierById: "",
//    GetAllJobsCount: "AdminJob/GetAllJobsCount",
//    GetJobDetailsList: "AdminJob/GetJobDetailsList",
//    GetReciceBids: "AdminJob/GetReciveBids",
//    GetReciceBidsDetails: "AdminJob/ReciveBidDetails",
//    jobdetails: "Jobmodule/AppJobDetails",
//    getqoutationId: "Jobmodule/app-recive-bids",
//    CompletedJobListAdmin: "Job/CompletedJobListAdmin",
//    url: "home/AdminHome",
//    getJobDetails: "Jobmodule/app-recive-bids-detail",
//  },

//  Users: {

//    SpGetSupplierStats: 'AdminSupplier/SpGetSupplierStats',
//    SpGetSupplierList: 'AdminSupplier/SpGetSupplierList',
//    SpGetTradesmanStats: 'AdminTradesman/SpGetTradesmanStats',
//    SpGetTradesmanList: 'AdminTradesman/SpGetTradesmanList',
//    UserProfile: "Usermodule/app-user-profile",
//    getBussinessProfile: 'AdminTradesman/SpGetBusinessDetails'
//  },
//  Login: {
//    Login: "AdminIdentityServer/Login",
//    loginurl: "login",
//    forgotPassword: "forgotPassword",
//    forgotPasswordUrl: 'AdminUserManagment/AdminForgotPasswordAuthentication',
//    resetPassword: "AdminIdentityServer/AdminResetPassword",
//  }


//}






export var apiRoute = {
  Home: {
    GetAllSubcategory: "Supplier/GetAllSubcategory"
  },
  Customers: {
    JobQuotationsWeb: "Customer/JobQuotationsWeb",
  },
  Notifications: {
    GetNotifications: "Notification/GetNotifications"
  },
  Jobs: {
    GetFinishedJobs: "Job/GetFinishedJobList",
    GetFinishedJobDetail: "Job/GetFinishedJobDetails",
    GetAlljobDetails: "Job/GetAlljobDetails",
    GetInprogressJobDetail: "Job/GetInprogressJobDetail",
    GetPostedJobsByCustomerId: "Job/GetPostedJobsByCustomerId",
    GetQuoteImagesByJobQuotationIdWeb: "Job/GetQuoteImagesByJobQuotationIdWeb",
    GetPostedJobDetailByJobQuotationId: "Job/GetJobQuotationByJobQuotationId",
    GetJobQuotationByJobQuotationId: "Job/GetJobQuotationByJobQuotationId",
    GetBidDetails: "Job/GetBidDetails",
    AddJobDetails: "Job/AddJobDetails",
    UpdateBidStatus: "Job/UpdateBidStatus",
    GetQuotationBids: "Job/GetQuotationBids",
    UpdateSelectedBid: "Job/UpdateSelectedBid",
    GetTradesmanSkillsByParentId: "Tradesman/GetTradesmanSkillsByParentId",
    UpdateJobQuotation: baseUrl +"Customer/UpdateJobQuotation"

  },
  Users: {
    MarketPlace: {
      ProductCategory: "Supplier/GetProductCategoryDetailWeb",
      SupplierAdDetail: "Supplier/GetProductDataByAdId",
      LoadImage: "Supplier/GetPostAdImagesList",
      SaveOrUnsave: "Customer/AddDeleteCustomerSavedAd",
      SupplierShop: "Supplier/GetSupplierShopWeb",

    },


    CustomerProfile: "Customer/GetPersonalDetails",
    CustomerProfileUpdate: "UserManagement/UpdatePersonalDetails",
    AddUpdateUserProfileImage: "Images/AddUpdateUserProfileImage",
    AddUpdateUserImage: "Images/AddUpdateUserImage",
    GetPaymentRecords: "Customer/getPaymentRecords",
    CancelJob: "Customer/DeleteJobQuotation"

    
  },
  Login: {
    Login: "Identity/Login",
  },
  forgotPassword: {
    forgotPassword: "Identity/GetUserIdByEmailOrPhoneNumber",
    verifyOtpAndGetResetPasswordToken: "UserManagement/VerifyOtpAndGetPasswordResetToken"
  },
  Common: {
    getOtp: "UserManagement/GetOtp",
    getCityList: "Customer/GetCityList",
    getDistanceList: "UserManagement/GetDistances"
  },
  registration: {
    CheckEmailandPhoneNumberAvailability: "Identity/CheckEmailandPhoneNumberAvailability",
    verifyOtpAndRegisterUser: "UserManagement/WebUserRegistration"
  },
  resetPassword: {
    resetPassword: "Identity/ResetPassword"
  },
  Supplier: {

    GetAdBySubCategoryIdsWeb: "Supplier/GetAdBySubCategoryIdsWeb",
    GetActiveAds: "Supplier/SpGetActiveAdsWithImages",
    GetInActiveAds: "Supplier/SpGetInActiveAdsWithImages",
    GetEditAdDetail: "Supplier/GetEditAdDetail",
    GetBusinessAndPersnalProfileWeb: "Supplier/GetBusinessAndPersnalProfileWeb",
    GetAllProductCategory: "Supplier/GetAllProductCategory",
    AddAllProductSubCatgory: "Supplier/AddAllProductSubCatgory",
    GetDistances: "UserManagement/GetDistances",
    GetAllCities: "UserManagement/GetAllCities",
    GetDropDownOptionWeb: "UserManagement/GetDropDownOptionWeb",
    GetSuppliersFeedbackBySupplierId: "Supplier/GetSuppliersFeedbackBySupplierId",
    AddSupplierBusinessDetails: "UserManagement/AddSupplierBusinessDetails",
    UpdatePersonalDetails: "UserManagement/UpdatePersonalDetails",
    AddUpdateSupplierProfileImage: "/Images/AddUpdateSupplierProfileImage",
    GetProductCatogory: "Supplier/GetProductCatogory",
    GetAllProductSubCatagories: "Supplier/GetAllProductSubCatagories",
    SaveAndUpdateAd: "Supplier/SaveAndUpdateAd",
    UpdateSupplierAdsstatus: "Supplier/UpdateSupplierAdsstatus",
    DeleteAd: "Supplier/DeleteAd",
    GetPersonalInformation: "Supplier/GetPersonalInformation",
    GetPostAdImagesList: "Supplier/GetPostAdImagesList",
  },
  Communication: {
    SendUsAMessage: "Communication/SendEmailAsync"
  },



  Tradesman: {
    GetTradesmanSkillsByParentId: "Tradesman/GetTradesmanSkillsByParentId",
    GetTradesmanProfile: "Tradesman/GetTradesmanProfile"
  },




}
export var pagesUrls = {
  CommonRegistrationPages: {
    forgotPassword: "/resetpassword/app-forgot-password",
    forgotPasswordCode: "/resetpassword/app-forgot-password-code",
    resetPassword: "/resetpassword/app-reset-password",
    login: "/login",
    registrationCode: "/registration/appregistrationotpcode"
  }
}
