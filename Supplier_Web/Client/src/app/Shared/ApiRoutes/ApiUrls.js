"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ApiUrls = void 0;
exports.ApiUrls = {
    Supplier: {
        Registration: {
            CheckEmailandPhoneNumberAvailability: "Identity/CheckEmailandPhoneNumberAvailability",
            VerifyOtpAndRegisterUser: "UserManagement/WebUserRegistration",
            GetPersonalInformationBySupplierId: "Supplier/GetPersonalInformationBySupplierId",
            GetOtp: "UserManagement/GetwebOtp",
            verifyOtpAndRegisterUser: "UserManagement/WebUserRegistration",
            login: "Identity/Login",
            GetSupplierAcountsType: "Identity/GetSupplierAcountsType"
        },
        City: {
            getCityList: "Customer/GetCityList",
        },
        Product: {
            GetActiveProducts: "Supplier/GetProductCategories",
            GetOrderDetailById: "Supplier/GetOrderDetailById",
            GetProductSubCategoryById: "Supplier/GetProductSubCategoryById",
            GetProductCategoryGroupListById: "Supplier/GetCategoryGroupsById",
            GetHomeProductList: "Supplier/GetHomeProductList",
            GetTopFiveProductCategory: "Supplier/GetTopFiveProductCategory",
            GetSupplierProductListWeb: "Supplier/GetSupplierProductListWeb",
            GetProductDetailWeb: "Supplier/GetProductDetailWeb",
            GetProductsByCategory: "Supplier/GetProductsByCategory",
            GetProductsByName: "Supplier/GetProductsByName",
            PlaceOrder: "Supplier/PlaceOrder",
            GetPaymentHistory: "Supplier/GetPaymentHistory",
            GetSupplierProductById: "Supplier/GetSupplierProductById"
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
            GetUserDetailsByUserRole: "UserManagement/GetUserDetailsByUserRole",
        },
        Verification: {
            VerifyOtpWithoutToken: "UserManagement/VerifyOtpWithoutToken",
        },
        Login: {
            Login: "Identity/Login",
        },
        Order: {
            GetLoggedCustomerOrdersList: "Supplier/GetCustomerOrdersList",
            GetLoggedCustomerOrderedProductsList: "Supplier/GetCustomerOrderedProductsList"
        },
        SaveAndRemoveProductsInWishlist: "Customer/SaveAndRemoveProductsInWishlist",
        CheckProductStatusInWishList: "Customer/CheckProductStatusInWishList",
        GetCustomerWishListProducts: "Customer/GetCustomerWishListProducts",
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
    }
};
//# sourceMappingURL=ApiUrls.js.map