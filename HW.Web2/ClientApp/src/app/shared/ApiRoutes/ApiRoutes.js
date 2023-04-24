"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.apiRoute = {
    AnonymousToken: {
        token: "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJuYmYiOjE2MDkyNDUzMTUsImV4cCI6MTYwOTI0OTc0MywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDoxNTc2NiIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjE1NzY2L3Jlc291cmNlcyIsImFwaTEiXSwiY2xpZW50X2lkIjoiR2F0ZXdheSIsInN1YiI6Imhhc2VlYkBnbWFpbC5jb20iLCJhdXRoX3RpbWUiOjE2MDkyNDUzMTUsImlkcCI6ImxvY2FsIiwiSWQiOiIxNDAzNjEiLCJSb2xlIjoiQW5vbnltb3VzIiwiVXNlcklkIjoiY2E3N2Q0OGQtODU0Yi00ZjUxLWJjNzktODI0OTcwODYwYjYyIiwic2NvcGUiOlsiYXBpMSJdLCJhbXIiOlsiY3VzdG9tIl0sImp0aSI6IjBkZjgwNjVhLWZlZGQtNDhmNS1hY2NlLTJkNWY5YjUwYThmNyIsImlhdCI6MTYwOTI0NjEzMX0.wT19-UiuG7xdAF5e50kCHk5uQrteJN68DjSecjdRhQQ"
    },
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
        UpdateJobQuotation: "Customer/UpdateJobQuotation",
        WebLiveLeads: "Job/WebLiveLeads",
        WebLiveLeadsLatLong: "Job/WebLiveLeadsLatLong",
        GetJobMainImage: "Images/GetJobMainImages",
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
        CancelJob: "Customer/DeleteJobQuotation",
        PostTradesmanFeedback: "Job/PostTradesmanFeedback",
    },
    Login: {
        Login: "Identity/Login",
    },
    forgotPassword: {
        forgotPassword: "Identity/GetUserIdByEmailOrPhoneNumber",
        verifyOtpAndGetResetPasswordToken: "UserManagement/VerifyOtpAndGetPasswordResetToken"
    },
    Common: {
        getOtp: "UserManagement/GetwebOtp",
        getCityList: "Customer/GetCityList",
        getDistanceList: "UserManagement/GetDistances",
        VerifyOtp: "UserManagement/VerifyOtp"
    },
    registration: {
        CheckEmailandPhoneNumberAvailability: "Identity/CheckEmailandPhoneNumberAvailability",
        verifyOtpAndRegisterUser: "UserManagement/WebUserRegistration"
    },
    resetPassword: {
        resetPassword: "Identity/ResetPassword"
    },
    Supplier: {
        PostAd: "Supplier/PostAd/Create",
        GetAdBySubCategoryIdsWeb: "Supplier/GetAdBySubCategoryIdsWeb",
        GetActiveAds: "Supplier/SpGetActiveAdsWithImages",
        GetInActiveAds: "Supplier/SpGetInActiveAdsWithImages",
        GetEditAdDetail: "Supplier/GetEditAdDetail",
        GetBusinessAndPersnalProfileWeb: "Supplier/GetBusinessAndPersnalProfileWeb",
        GetSupplierList: "Supplier/GetSupplierList",
        GetSupplierImageList: "Supplier/GetSupplierImageList",
        GetAllProductCategory: "Supplier/GetAllProductCategory",
        GetCategoriesForListing: "Supplier/GetCategoriesForListing",
        GetMarkeetPlaceProducts: "Supplier/GetMarkeetPlaceProducts",
        GetMarkeetPlaceProductsImages: "Images/GetMarkeetPlaceProductsImages",
        AddAllProductSubCatgory: "Supplier/AddAllProductSubCatgory",
        GetDistances: "UserManagement/GetDistances",
        GetAllCities: "UserManagement/GetAllCities",
        GetDropDownOptionWeb: "UserManagement/GetDropDownOptionWeb",
        GetSuppliersFeedbackBySupplierId: "Supplier/GetSuppliersFeedbackBySupplierId",
        AddSupplierBusinessDetails: "UserManagement/AddSupplierBusinessDetails",
        UpdatePersonalDetails: "UserManagement/UpdatePersonalDetails",
        AddUpdateSupplierProfileImage: "Images/AddUpdateSupplierProfileImage",
        GetProductCatogory: "Supplier/GetProductCatogory",
        GetAllProductSubCatagories: "Supplier/GetAllProductSubCatagories",
        SaveAndUpdateAd: "Supplier/SaveAndUpdateAd",
        UpdateSupplierAdsstatus: "Supplier/UpdateSupplierAdsstatus",
        DeleteAd: "Supplier/DeleteAd",
        GetPersonalInformation: "Supplier/GetPersonalInformation",
        GetPostAdImagesList: "Supplier/GetPostAdImagesList",
        GetBusinessProfile: "Supplier/GetBusinessProfile",
        GetBusinessDetailsStatus: "Supplier/GetBusinessDetailsStatus",
        GetActiveProducts: "Supplier/GetCategoriesForListing",
        GetSupplierBusinessProfile: "Supplier/GetSupplierBusinessProfile",
        AllSubCategory: "Supplier/AllSubCategory",
    },
    Communication: {
      SendUsAMessage: "Communication/SendContactEmail",
        SendSmsAsync: "Communication/SendSmsAsync",
    },
    PackagesAndPayments: {
        GetPackgesByCategory: "AdminPackagesAndPayments/GetPackgesByCategory",
        GetPackgesTypeByRoleId: "AdminPackagesAndPayments/GetPackgesTypeByRoleId",
        AddOrderByPackageId: "AdminPackagesAndPayments/AddOrderByPackageId",
        UpdatePckagesAndOrderStatus: "AdminPackagesAndPayments/UpdatePckagesAndOrderStatus",
        GetActiveOrdersList: "AdminPackagesAndPayments/GetActiveOrdersList",
        GetExpiredOrdersList: "AdminPackagesAndPayments/GetExpiredOrdersList",
    },
    Tradesman: {
        GetSkillList: "Tradesman/GetSkillList",
        GetSkillTagsById: "Tradesman/GetSkillTagsBySkillId",
        GetTradesmanBySkillTown: "Tradesman/GetLAllTradesmanbySkillTown",
        GetTradesmanSkillsByParentId: "Tradesman/GetTradesmanSkillsByParentId",
        GetJobLeadsWebByTradesmanId: "Job/GetJobLeadsWebByTradesmanId",
        GetJobDetailWeb: "Job/GetJobDetailWeb",
        GetJobDetailsByIdWeb: "Customer/GetJobDetailsByIdWeb",
        NotificationCallRequest: "Notification/NotificationCallRequest",
        FeedbackRequest: "Notification/RequestFeedbackNotification",
        MarkAsFinishedJob: "Job/MarkAsFinishedJob",
        GetTradesmanProfile: "Tradesman/GetTradesmanProfile",
        GetPersonalDetails: "Tradesman/GetPersonalDetails",
        GetBusinessAndPersnalProfileWeb: "Tradesman/GetBusinessAndPersnalProfileWeb",
        GetTradesmanSkills: "Tradesman/GetTradesmanSkills",
        GetInvoiceJobReceipts: "Tradesman/GetInvoiceJobReceipts",
        AddEditTradesmanWithSkills: "Usermanagement/AddEditTradesmanWithSkills",
        UpdatePersonalDetails: "Tradesman/UpdatePersonalDetails",
        AddUpdateTradesmanProfileImage: "Images/AddUpdateTradesmanProfileImage",
        GetActiveBidsDetailsWeb: "Job/GetActiveBidsDetailsWeb",
        GetDeclinedBidsDetailsWeb: "Job/GetDeclinedBidsDetailsWeb",
        Dashboard: "Tradesman/LiveLeads/list",
        GetCustomerDetailsByIdWeb: "Customer/GetCustomerDetailsByIdWeb",
        GetJobDetailsById: "Customer/GetJobDetailsById",
        SubmitAndEditBid: "Tradesman/SubmitBid",
        GetCompletedJob: "Tradesman/GetCompletedJob",
        GetTradesmanBySkills: "AdminTradesman/GetTradesmanByCategoryReport",
        GetBusinessDetailsStatus: "Tradesman/GetBusinessDetailsStatus",
    },
    IdentityServer: {
        GetPhoneNumberVerificationStatus: "Identity/GetPhoneNumberVerificationStatus",
        GetUserByFacebookId: "Identity/GetUserByFacebookId",
        GetUserByGoogleId: "Identity/GetUserByGoogleId",
        GetUserBlockStatus: "Identity/GetUserBlockStatus",
        GetUserPinStatus: "Identity/GetUserPinStatus",
    },
};
exports.pagesUrls = {
    CommonRegistrationPages: {
        forgotPassword: "/resetpassword/app-forgot-password",
        forgotPasswordCode: "/resetpassword/app-forgot-password-code",
        resetPassword: "/resetpassword/app-reset-password",
        login: "/login/Customer",
        loginCustomer: "/login/Customer",
        loginTradesman: "/login/Tradesman",
        loginSupplier: "/login/Supplier",
        registrationCode: "/registration/appregistrationotpcode"
    }
};
//# sourceMappingURL=ApiRoutes.js.map
