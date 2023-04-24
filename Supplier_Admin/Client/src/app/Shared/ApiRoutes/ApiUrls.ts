
export var ApiUrls = {
  Supplier: {
    MarketPlace: {
      ProductCategory: "Supplier/GetProductCategoryDetailWeb",
      SupplierAdDetail: "Supplier/GetProductDataByAdId",
      GetProductByCategory: "/product",
      LoadImage: "Supplier/GetPostAdImagesList",
      SaveOrUnsave: "Customer/AddDeleteCustomerSavedAd",
      SupplierShop: "Supplier/GetSupplierShopWeb",
      GetAllProductCategory: "Supplier/GetAllProductCategory",
    },
    Product:
    {
      GetActiveProducts: "Supplier/GetProductCategories",
      GetProductSubCategoryById: "Supplier/GetProductSubCategoryById",
      AddUpdateProductAttribute: "Supplier/AddUpdateProductAttribute",
      GetProductAttributeList: "Supplier/GetProductAttributeList",
      GetProductAttributeListByCategoryId: "Supplier/GetProductAttributeListByCategoryId",
      GetProductCategoryGroupListById: "Supplier/GetProductCategoryGroupListById",
      GetAllProductVariant: "Supplier/GetAllProductVariant",
      AddNewSupplierProduct: "Supplier/AddNewSupplierProduct",
      UpdateSupplierProduct: "Supplier/UpdateSupplierProduct",
      GetSupplierProductList: "Supplier/GetSupplierProductList",
      UpdateStockLevel: "Supplier/UpdateStockLevel",
      GetSupplierProductDetail: "Supplier/GetSupplierProductDetail",
      GetProductSearchTagsList: "Supplier/GetProductSearchTagsList",
      GetProductCategories: "Supplier/GetProductCategories",
      GetCategoryGroupsById: "Supplier/GetCategoryGroupsById",
      GetVariantsByProductId: "Supplier/GetVariantsByProductId",
      AddUpdateFreeShipping: "Supplier/AddUpdateFreeShipping",
      GetFreeShippingList: "Supplier/GetFreeShippingList",
      DeleteFreeShipping: "Supplier/DeleteFreeShipping",
      GetAllCatSubCatGroupCategories: "Supplier/GetAllCatSubCatGroupCategories"

    },
    Registration: {
      CheckEmailandPhoneNumberAvailability: "Identity/CheckEmailandPhoneNumberAvailability",
      VerifyOtpAndRegisterUser: "UserManagement/WebUserRegistration",
      GetOtp: "UserManagement/GetwebOtp",
      VerifyOtpWithoutToken: "UserManagement/VerifyOtpWithoutToken",
      Login: "Identity/Login",
      GetPersonalInformationBySupplierId: "Supplier/GetPersonalInformationBySupplierId",
      GetSupplierAcountsType: "Identity/GetSupplierAcountsType",
      GetUserBlockStatus: "Identity/GetUserBlockStatus"

    },
    City: {
      getCityList: "Customer/GetCityList",
    },
    ForgetPassword: {
      GetUserIdByEmailOrPhoneNumber: "Identity/GetUserIdByEmailOrPhoneNumber",
      VerifyOtpAndGetPasswordResetToken: "UserManagement/VerifyOtpAndGetPasswordResetToken",
      ResetPassword: "Identity/ResetPassword"
    },
    Profile: {
      GetSupplierDetails: "supplier/GetSupplierWithDetails",
      AddAndUpdateSellerAccount: "supplier/AddAndUpdateSellerAccount",
      AddAndUpdateBusinessAccount: "supplier/AddAndUpdateBusinessAccount",
      GetCountryList: "supplier/GetCountryList",
      GetSateList: "supplier/GetSateList",
      GetAreaList: "supplier/GetAreaList",
      GetLocationList: "supplier/GetLocationList",
      GetBanksList: "supplier/GetBanksList",
      SaveAndUpdateBankAccountData: "supplier/SaveAndUpdateBankAccountData",
      GetBankAccountData: "supplier/GetBankAccountData",
      GetLogoData: "supplier/GetLogoData",
      GetProfile: "supplier/GetProfile",
      SaveAndUpdateWhareHouseAddress: "supplier/SaveAndUpdateWhareHouseAddress",
      GetWareHouseAddress: "supplier/GetWareHouseAddress",
      SaveAndUpdateReturnAddress: "supplier/SaveAndUpdateReturnAddress",
      SaveAndUpdateSocialLinks: "supplier/SaveAndUpdateSocialLinks",
      AddAndUpdateLogo: "supplier/AddAndUpdateLogo",
      GetReturnAddress: "supplier/GetReturnAddress",
      GetSocialLinks: "supplier/GetSocialLinks",
      GetProfileVerification: "supplier/GetProfileVerification",
      GetSupplierWallet: "PackagesAndPayments/GetSupplierWallet",
      GetUserDetailsByUserRole: "Customer/GetUserDetailsByUserRole",
      GetSupplierShopUrl: "supplier/GetSupplierShopUrl",
      GetCityListByReleventState: "UserManagement/GetCityListByReleventState",
      CheckUserExist: "AdminUserManagment/CheckUserExist"
    },
    PackagesAndPayments: {
      AddNewPromotionTypeForSupplier: "AdminPackagesAndPayments/AddNewPromotionTypeForSupplier",
      GetPromotionTypeByIdForSupplier: "AdminPackagesAndPayments/GetPromotionTypeByIdForSupplier?id=",
      GetAllPromotionTypesForSupplier: "AdminPackagesAndPayments/GetAllPromotionTypesForSupplier",
      GetPromotionTypesListForSupplier: "AdminPackagesAndPayments/GetPromotionTypesListForSupplier",
      AddPromotionForSupplier: "AdminPackagesAndPayments/AddPromotionForSupplier",
      GetAllPromotionsForSupplier: "AdminPackagesAndPayments/GetAllPromotionsForSupplier",
      GetPromotionByIdForSupplier: "AdminPackagesAndPayments/GetPromotionByIdForSupplier?id=",
      DeletePromotionForSupplier: "AdminPackagesAndPayments/DeletePromotionForSupplier?id=",
      AddSubAccountWithoutToken: "PackagesAndPayments/AddSubAccountWithoutToken",
      getSubAccountRecordWithoutToken: "PackagesAndPayments/getSubAccountRecordWithoutToken",
      SupplierLeadgerTransaction: "PackagesAndPayments/SupplierLeadgerTransaction",
      GetPaymentWithdrawalRequestByTradesmanId: "PackagesAndPayments/GetPaymentWithdrawalRequestByTradesmanId",
      addSupplierPaymentWithdrawalRequest: "PackagesAndPayments/AddSupplierPaymentWithdrawalRequest",
    },
    Image: {
      AddSupplierProductImages: "Images/AddSupplierProductImages",
      MarkProductImageAsMain: "Images/MarkProductImageAsMain",
    },
    Orders: {
      GetOrdersList: "Supplier/GetOrdersList",
      GetSalesSummary: "Supplier/GetSalesSummary",
      GetOrderDetailsById: "Supplier/GetOrderDetailsById",
      UpdateOrderStatus: "Supplier/UpdateOrderStatus",
      GetLoggedSupplierCanelledOrdersList: "Supplier/GetLoggedSupplierCanelledOrdersList",
      GetOrderTrackingForSupplier:"Supplier/GetOrderTracking",
      getOrderedItemTrackingForSupplier:"Supplier/getOrderedItemTracking"
    },
    Payment: {
      GetPaymentHistory: "Supplier/GetPaymentHistory",
      GetPaymentDetail:"Supplier/GetPaymentDetail",
      GetWithdrawalListById:"Supplier/GetWithdrawalListById"
    },
    Notifications: {
      GetAdminNotifications: "AdminNotfications/GetAdminNotifications",
      GetNotificationsByUserId: "AdminNotfications/GetNotificationsByUserId",
      MarkNotificationAsRead: "AdminNotfications/MarkNotificationAsRead",
    },
    IdentityServer: {
      UpdateUserFirebaseToken: "Identity/UpdateUserFirebaseToken",
    }
  },
}
