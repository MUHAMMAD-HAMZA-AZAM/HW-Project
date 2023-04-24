
export var ApiUrls = {
  Supplier: {
    Registration: {
      CheckEmailandPhoneNumberAvailability: "Identity/CheckEmailandPhoneNumberAvailability",
      VerifyOtpAndRegisterUser: "UserManagement/WebUserRegistration",
      GetPersonalInformationBySupplierId: "Supplier/GetPersonalInformationBySupplierId",
      GetOtp: "UserManagement/GetwebOtp",
      verifyOtpAndRegisterUser: "UserManagement/WebUserRegistration",
      login: "Identity/Login",
      GetSupplierAcountsType: "Identity/GetSupplierAcountsType",
      GetSupplierShopDetails: "Supplier/GetSupplierShopDetails",
      
    },
    City: {
      getCityList: "Customer/GetCityList",
      GetCityListWithTraxCityId: "AdminCustomer/GetCityListWithTraxCityId",
    },
    GetSocialLinks: "supplier/GetSocialLinks",
    Product: {
      GetActiveProducts: "Supplier/GetProductCategories",
      GetOrderDetailById: "Supplier/GetOrderDetailById",
      GetProductSubCategoryById: "Supplier/GetProductSubCategoryById",
      GetProductCategoryGroupListById: "Supplier/GetCategoryGroupsById",
      GetHomeProductList: "Supplier/GetHomeProductList",
      GetTopFiveProductCategory: "Supplier/GetTopFiveProductCategory",
      GetSupplierProductListWeb: "Supplier/GetSupplierProductListWeb",
      GetProductDetailWeb: "Supplier/GetProductDetailWeb",
      AddCustomerFeedBack: "Supplier/AddCustomerFeedBack",
      GetCustomerFeedBackList: "Supplier/GetCustomerFeedBackList",
      GetProductsByCategory: "Supplier/GetProductsByCategory",
      GetProductsByName:"Supplier/GetProductsByName",
      PlaceOrder: "Supplier/PlaceOrder",
      GetPaymentHistory:"Supplier/GetPaymentHistory",
      GetSupplierProductById: "Supplier/GetSupplierProductById",
      SendMessage: "UserManagement/SendMessage",
      GetProductsList: "Supplier/GetProductsList",
      GetProductsByTag: "Supplier/GetProductsByTag",
    },
    Payments: {
      AddSupplierLeadgerEntry:"Supplier/AddSupplierLeadgerEntry"
    },
    ShippingCost: {
      GetShippingCost: "Supplier/GetShippingCost",
    },
    Slider: {
      GetSupplierSliderList: "Supplier/GetSupplierSliderList",
    }
   
  },
  Customer: {
    Registration: {
      CheckEmailandPhoneNumberAvailability: "Identity/CheckEmailandPhoneNumberAvailability",
      VerifyOtpAndRegisterUser: "UserManagement/WebUserRegistration",
      GetPersonalDetailsByCustomerId: "Customer/GetPersonalDetailsByCustomerId",
      GetPersonalDetails: "Customer/GetPersonalDetails",
      login: "Identity/Login",
      GetOtp: "UserManagement/GetwebOtp",
    },
    LoggedUserDetails: {
      GetUserDetailsByUserRole: "Customer/GetUserDetailsByUserRole",
    },
    Verification: {
      VerifyOtpWithoutToken: "UserManagement/VerifyOtpWithoutToken",
    },
    Login: {
      Login: "Identity/Login",
    },
    Order: {

      GetLoggedCustomerOrdersList: "Supplier/GetCustomerOrdersList",
      GetLoggedCustomerOrderedProductsList :"Supplier/GetCustomerOrderedProductsList",
      CancelCustomerOrder : "Supplier/CancelCustomerOrder",
      GetLoggedCustomerCanelledOrdersList: "Supplier/GetCustomerCanclledOrdersList",
      GetOrderCancellationReasonsList : "Supplier/GetOrderCancellationReasonsList",
      GetCityShippingCost: "Supplier/GetShippingCost",
      GetOrderTrackingForCustomer:"Supplier/GetOrderTracking",
      GetOrderedItemTrackingForCustomer: "Supplier/getOrderedItemTracking",
    },
    SaveAndRemoveProductsInWishlist: "Customer/SaveAndRemoveProductsInWishlist",
    CheckProductStatusInWishList: "Customer/CheckProductStatusInWishList",
    GetCustomerWishListProducts:"Customer/GetCustomerWishListProducts",
    GetUserDetailsByUserRole: "Customer/GetUserDetailsByUserRole",
    GetUserIdByEmailOrPhoneNumber: "Identity/GetUserIdByEmailOrPhoneNumber",
  },
  ForgetPassword: { 
    VerifyOtpAndGetPasswordResetToken: "UserManagement/VerifyOtpAndGetPasswordResetToken",
    ResetPassword: "Identity/ResetPassword",
    GetOtp: "UserManagement/GetwebOtp",
  },
  ResetPassword: {
    resetPassword: "Identity/ResetPassword"
  },
  Promotion: {
    GetPromotionsBySuplierId: "AdminPackagesAndPayments/GetPromotionsBySuplierId",
    AddUpdateAccountReceivable: "PackagesAndPayments/AddUpdateAccountReceivable"
  },
  Identity: {
    GetUserByFacebookId: "Identity/GetUserByFacebookId",
    GetUserByGoogleId: "Identity/GetUserByFacebookId",
  },
  PackagesAndPayments: {
    AddSubAccountWithoutToken: "PackagesAndPayments/AddSubAccountWithoutToken",
    getSubAccountRecordWithoutToken: "PackagesAndPayments/getSubAccountRecordWithoutToken",
  },
  IdentityServer: {
    UpdateUserFirebaseToken: "Identity/UpdateUserFirebaseToken",
  },
  Notifications: {
    GetNotificationsByUserId: "AdminNotfications/GetNotificationsByUserId",
    MarkNotificationAsRead: "AdminNotfications/MarkNotificationAsRead",
  },
  CMS: {
    GetSeoPageById: "AdminCMS/GetSeoPageById",
  },
}
