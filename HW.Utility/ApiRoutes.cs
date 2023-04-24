
namespace HW.Utility
{
    public static class ApiRoutes
    {
        public static class Gateway
        {
            /// <summary>
            /// local URL
            /// </summary>
           //public const string BaseUrl = "http://172.16.1.185:15790/";
            //public const string BaseUrl = "http://172.16.2.111:15790/";

            /// <summary>
            /// Live URL
            /// </summary
            public static readonly string BaseUrl = "https://hoomwork.com/GatewayNet5/";
            //public static readonly string BaseUrl = "https://www.hoomwork.com/gateway/";

            /// <summary>
            /// Test Server URL
            /// </summary>
            //public const string BaseUrl = "http://185.15.244.235/gateway/"; 
            //public const string BaseUrl = "https://test2.hoomwork.com/gateway/"; 

            /// <summary>
            /// New UI Test Server URL
            /// </summary> 
            // public const string BaseUrl = "http://185.15.244.235/nugateway/";

        }


        public static class Elmah
        {
            public static readonly string BaseURL = "api/Elmah/";
            public static readonly string ElmahErrorlogList = $"{BaseURL}ElmahErrorlogList";
            public static readonly string ElmahErrorDetailsById = $"{BaseURL}ElmahErrorDetailsById";
            public static readonly string GetClientIpAddress = $"{BaseURL}GetClientIpAddress";
        }

        public static class Audio
        {
            //public static readonly string BaseUrl = "http://local.services.com/AudioApi";
            //public static readonly string BaseUrl = "http://localhost:15761";
            public static readonly string BaseUrl = "api/Audio/";
            public static readonly string GetAllAudio = $"{BaseUrl}GetAllAudio";
            public static readonly string GetByJobQuotationId = $"{BaseUrl}GetByJobQuotationId";
            public static readonly string SaveBidAudio = $"{BaseUrl}SaveBidAudio";
            public static readonly string GetAudioById = $"{BaseUrl}GetAudioById";
            public static readonly string SaveDisputeAudio = $"{BaseUrl}SaveDisputeAudio";
        }

        public static class PackagesAndPayments
        {
            //public static readonly string BaseUrl = "http://local.services.com/PackagesAndPaymentsApi";
            //public static readonly string BaseUrl = "http://localhost:15778";
            public static readonly string BaseUrl = "api/PackagesAndPayments/";
            public static readonly string GetPromoTypesList = $"{BaseUrl}GetPromoTypesList";
            public static readonly string GetReferalCodeByUserId = $"{BaseUrl}GetReferalCodeByUserId";
            public static readonly string GetRedeemRecordByJQID = $"{BaseUrl}GetRedeemRecordByJQID";
            public static readonly string GetProRecordByJQID = $"{BaseUrl}GetProRecordByJQID";
            public static readonly string GetPromotionList = $"{BaseUrl}GetPromotionList";
            public static readonly string GetSubAccount = $"{BaseUrl}GetSubAccount";
            public static readonly string GetPaymentWithdrawalRequestByTradesmanId = $"{BaseUrl}GetPaymentWithdrawalRequestByTradesmanId";
            public static readonly string GetLedgerTransaction = $"{BaseUrl}GetLedgerTransaction";
            public static readonly string GetLedgerTransactionCustomer = $"{BaseUrl}GetLedgerTransactionCustomer";
            public static readonly string GetSupplierWallet = $"{BaseUrl}GetSupplierWallet";
            public static readonly string AddLeadgerTransection = $"{BaseUrl}AddLeadgerTransection"; 
            public static readonly string AddUpdateAccountReceivable = $"{BaseUrl}AddUpdateAccountReceivable";
            public static readonly string AddOrUpdatePromotions = $"{BaseUrl}AddOrUpdatePromotions";
            public static readonly string FilterPromotions = $"{BaseUrl}FilterPromotions";
            public static readonly string AddPromotionRedemptions = $"{BaseUrl}AddPromotionRedemptions";
            public static readonly string GetPromotionRedeemRecordByRedeemUserID = $"{BaseUrl}GetPromotionRedeemRecordByRedeemUserID";
            public static readonly string AddSubAccount = $"{BaseUrl}AddSubAccount";
            public static readonly string DeletePromotion = $"{BaseUrl}DeletePromotion";
            public static readonly string AddNewPackages = $"{BaseUrl}AddNewPackages";
            public static readonly string AddNewPromotionType = $"{BaseUrl}AddNewPromotionType";
            public static readonly string AddCouponType = $"{BaseUrl}AddCouponType";
            public static readonly string AddCoupons = $"{BaseUrl}AddCoupons";
            public static readonly string GetAllCoupons = $"{BaseUrl}GetAllCoupons";
            public static readonly string GetAllPromotionOnPackages = $"{BaseUrl}GetAllPromotionOnPackages";
            public static readonly string GetAllPackage = $"{BaseUrl}GetAllPackages";
            public static readonly string GetAllPromotionTypesList = $"{BaseUrl}GetAllPromotionTypesList";
            public static readonly string GetAllPackagetype = $"{BaseUrl}GetAllPackagetype";
            public static readonly string GetAllCouponTypesList = $"{BaseUrl}GetAllCouponTypesList";
            public static readonly string AddPromotionOnPackage = $"{BaseUrl}AddPromotionOnPackages";
            public static readonly string AddNewOrder = $"{BaseUrl}AddNewOrder";
            public static readonly string GetOrder = $"{BaseUrl}GetAllOrders";
            public static readonly string AddUpdatePackageTypes = $"{BaseUrl}AddUpdatePackageType";
            public static readonly string GetPackgesByCategory = $"{BaseUrl}GetPackgesByCategory";
            public static readonly string GetPackgesTypeByRoleId = $"{BaseUrl}GetPackgesTypeByRoleId";
            public static readonly string AddOrderByPackageId = $"{BaseUrl}AddOrderByPackageId";
            public static readonly string UpdatePckagesAndOrderStatus = $"{BaseUrl}UpdatePckagesAndOrderStatus";
            public static readonly string GetActiveOrdersList = $"{BaseUrl}GetActiveOrdersList";
            public static readonly string GetExpiredOrdersList = $"{BaseUrl}GetExpiredOrdersList";
            public static readonly string GetPromotionTypes = $"{BaseUrl}GetPromotionTypes";
            public static readonly string AddUpdateVoucherCategory = $"{BaseUrl}AddUpdateVoucherCategory";
            public static readonly string GetvoucherCategoryList = $"{BaseUrl}GetvoucherCategoryList";
            public static readonly string DeleteVoucherCategory = $"{BaseUrl}deleteVoucherCategory";
            public static readonly string GetVoucherTypeList = $"{BaseUrl}GetVoucherTypeList";
            public static readonly string AddUpdateVoucherType = $"{BaseUrl}AddUpdateVoucherType";
            public static readonly string DeleteVoucherType = $"{BaseUrl}DeleteVoucherType";
            public static readonly string GetVoucherBookList = $"{BaseUrl}GetVoucherBookList";
            public static readonly string DeleteVoucherBook = $"{BaseUrl}DeleteVoucherBook";
            public static readonly string AddUpdateVoucherBook = $"{BaseUrl}AddUpdateVoucherBook";
            public static readonly string GetVouchrBookAllocation = $"{BaseUrl}GetVouchrBookAllocation";
            public static readonly string DeleteVoucherBookAllocation = $"{BaseUrl}DeleteVoucherBookAllocation";
            public static readonly string AddUpdateVoucherBookAllocation = $"{BaseUrl}AddUpdateVoucherBookAllocation";
            public static readonly string GetVoucherBookPagesList = $"{BaseUrl}GetVoucherBookPagesList";
            public static readonly string GetVoucherBookLeavesList = $"{BaseUrl}GetVoucherBookLeavesList";
            public static readonly string GetCategoryCommissionSetupList = $"{BaseUrl}GetCategoryCommissionSetupList";
            public static readonly string AddUpdateCategoryCommissionSetup = $"{BaseUrl}AddUpdateCategoryCommissionSetup";
            public static readonly string GetOverrideTradesmanCommissionList = $"{BaseUrl}GetOverrideTradesmanCommissionList";
            public static readonly string AddAndUpdateOverrideTradesmanCommission = $"{BaseUrl}AddAndUpdateOverrideTradesmanCommission";
            public static readonly string DeleteCategoryCommissionSetup = $"{BaseUrl}DeleteCategoryCommissionSetup";
            public static readonly string AddAndUpdateAccountType = $"{BaseUrl}AddAndUpdateAccountType";
            public static readonly string GetAccountTypeList = $"{BaseUrl}GetAccountTypeList";
            public static readonly string AddAndUpdateAccount = $"{BaseUrl}AddAndUpdateAccount";
            public static readonly string GetAccountList = $"{BaseUrl}GetAccountList";
            public static readonly string AddAndUpdateSubAccount = $"{BaseUrl}AddAndUpdateSubAccount";
            public static readonly string GetSubAccountList = $"{BaseUrl}GetSubAccountList";
            public static readonly string AddAndUpdateReferral = $"{BaseUrl}AddAndUpdateReferral";
            public static readonly string GetReferralList = $"{BaseUrl}GetReferralList";
            public static readonly string GetWithdrawalRequestList = $"{BaseUrl}GetWithdrawalRequestList";
            public static readonly string UpdateWithdrawalRequestStatus = $"{BaseUrl}UpdateWithdrawalRequestStatus";
            public static readonly string DeclineWithdrawRequest = $"{BaseUrl}DeclineWithdrawRequest";

            public static readonly string AddEditPromotionReferalCode = $"{BaseUrl}AddEditPromotionReferalCode";
            public static readonly string GetReferalRecordByreferalCode = $"{BaseUrl}GetReferalRecordByreferalCode";
            public static readonly string GetReferalRecordByreferalUserID = $"{BaseUrl}GetReferalRecordByreferalUserID";
            public static readonly string GetVoucherList = $"{BaseUrl}GetVoucherList";
            public static readonly string AddEditRedemptions = $"{BaseUrl}AddEditRedemptions";
            public static readonly string UpdateVoucherBookLeaves = $"{BaseUrl}UpdateVoucherBookLeaves";
            public static readonly string GetRedeemRecordByRedeemUserID = $"{BaseUrl}GetRedeemRecordByRedeemUserID";

            public static readonly string GetRedemptionById = $"{BaseUrl}GetRedemptionById";

            public static readonly string AddEditTradesmanJobReceipts = $"{BaseUrl}AddEditTradesmanJobReceipts";
            public static readonly string getPaymentRecords = $"{BaseUrl}getPaymentRecords";
            public static readonly string GetAllPaymentMethods = $"{BaseUrl}GetAllPaymentMethods";
            public static readonly string GetJobReceiptsByTradesmanId = $"{BaseUrl}GetJobReceiptsByTradesmanId";

            public static readonly string getCommissionByCategory = $"{BaseUrl}getCommissionByCategory";
            public static readonly string getTradesmanCommissionOverride = $"{BaseUrl}getTradesmanCommissionOverride";
            public static readonly string GetHoomWorkSubAccount = $"{BaseUrl}GetHoomWorkSubAccount";
            public static readonly string GetHoomWorkJZSubAccount = $"{BaseUrl}GetHoomWorkJZSubAccount";
            public static readonly string GetSubAccountByTradesmanId = $"{BaseUrl}GetSubAccountByTradesmanId";
            public static readonly string GetWalletHistory = $"{BaseUrl}GetWalletHistory";
            public static readonly string JazzCashAcknowledgementReceipt = $"{BaseUrl}JazzCashAcknowledgementReceipt";

            public static readonly string ProceedToJazzCash = $"{BaseUrl}ProceedToJazzCash";
            public static readonly string GetJazzCashMerchantDetails = $"{BaseUrl}GetJazzCashMerchantDetails";
            

            public static readonly string GetTradesmanJobReceiptsByTradesmanId = $"{BaseUrl}GetTradesmanJobReceiptsByTradesmanId";
            public static readonly string getRedemptionRecord = $"{BaseUrl}getRedemptionRecord";
            public static readonly string getSubAccountRecord = $"{BaseUrl}getSubAccountRecord";
            public static readonly string AddLeaderTransactionForCreditUser = $"{BaseUrl}AddLeaderTransactionForCreditUser";
            public static readonly string AddPaymentWithdrawalRequest = $"{BaseUrl}AddPaymentWithdrawalRequest";
            public static readonly string AddSupplierPaymentWithdrawalRequest = $"{BaseUrl}AddSupplierPaymentWithdrawalRequest";
            public static readonly string getuseofWalletValue = $"{BaseUrl}getuseofWalletValue";
            public static readonly string getJazzCashValue = $"{BaseUrl}getJazzCashValue";
            public static readonly string DeletePromotionEntry = $"{BaseUrl}DeletePromotionEntry";
            public static readonly string GetLeaderReport = $"{BaseUrl}GetLeaderReport";
            public static readonly string GetPromotionData = $"{BaseUrl}GetPromotionData";
            public static readonly string GetWalletValueByBidId = $"{BaseUrl}GetWalletValueByBidId";
            public static readonly string GetJazzCashByBidId = $"{BaseUrl}GetJazzCashByBidId";
            public static readonly string GetLedgerTransectionReportByAccountRef = $"{BaseUrl}GetLedgerTransectionReportByAccountRef";
            public static readonly string AddAndUpdateGeneralLedgerTransection = $"{BaseUrl}AddAndUpdateGeneralLedgerTransection";
            public static readonly string GetDetailedLedgerTransectionReportByAccountRef = $"{BaseUrl}GetDetailedLedgerTransectionReportByAccountRef";
            public static readonly string GetUserTransectionReport = $"{BaseUrl}GetUserTransectionReport";
            public static readonly string GetAccountUsersName = $"{BaseUrl}GetAccountUsersName";
            public static readonly string GetSubAccountsLastLevel = $"{BaseUrl}GetSubAccountsLastLevel";
            public static readonly string GetChartOfAccounts = $"{BaseUrl}GetChartOfAccounts";
            public static readonly string InsertChartOfAccounts = $"{BaseUrl}InsertChartOfAccounts";
            public static readonly string DeleteChartOfAccounts = $"{BaseUrl}DeleteChartOfAccounts";
            public static readonly string AddJournalEntry = $"{BaseUrl}AddJournalEntry";
            public static readonly string AddFiscalPeriod = $"{BaseUrl}AddFiscalPeriod";
            public static readonly string AddLeadgerTransactionEntry = $"{BaseUrl}AddLeadgerTransactionEntry";
            public static readonly string GetChartOfAccount = $"{BaseUrl}GetChartOfAccount";
            public static readonly string GetFiscalPeriodsByYear = $"{BaseUrl}GetFiscalPeriodsByYear";
            public static readonly string LeadgerTransectionEntries = $"{BaseUrl}LeadgerTransectionEntries";



            public static readonly string AddNewPromotionTypeForSupplier = $"{BaseUrl}AddNewPromotionTypeForSupplier";
            public static readonly string GetPromotionTypeByIdForSupplier = $"{BaseUrl}GetPromotionTypeByIdForSupplier?promotionTypeId=";
            public static readonly string GetAllPromotionTypesForSupplier = $"{BaseUrl}GetAllPromotionTypesForSupplier?supplierId=";
            public static readonly string GetPromotionTypesListForSupplier = $"{BaseUrl}GetPromotionTypesListForSupplier";
            public static readonly string AddPromotionForSupplier = $"{BaseUrl}AddPromotionForSupplier";
            public static readonly string GetAllPromotionsForSupplier = $"{BaseUrl}GetAllPromotionsForSupplier?supplierId=";
            public static readonly string GetPromotionByIdForSupplier = $"{BaseUrl}GetPromotionByIdForSupplier?promotionId=";
            public static readonly string GetPromotionsBySuplierId = $"{BaseUrl}GetPromotionsBySuplierId";
            public static readonly string DeletePromotionForSupplier = $"{BaseUrl}DeletePromotionForSupplier?promotionId=";
            public static readonly string SupplierLeadgerTransaction = $"{BaseUrl}SupplierLeadgerTransaction";
            public static readonly string UpdateShippingChargesAndPaymentStatus = $"{BaseUrl}UpdateShippingChargesAndPaymentStatus";
            public static readonly string GetDetailedGLReport = $"{BaseUrl}GetDetailedGLReport";
            public static readonly string GetWithdrawalListById = $"{BaseUrl}GetWithdrawalListById";
        }

        public static class Call
        {
            public static readonly string BaseUrl = "api/Call/";
            //public static readonly string BaseUrl = "http://localhost:15762";
            //public static readonly string BaseUrl = "http://local.services.com/CallApi";
            public static readonly string GetTradesmanCallLogs = $"{BaseUrl}GetTradesmanCallLogs";
            public static readonly string GetCallTypeById = $"{BaseUrl}GetCallTypeById";
            public static readonly string GetAllCallType = $"{BaseUrl}GetAllCallType";
            public static readonly string DeleteCallLogs = $"{BaseUrl}DeleteCallLogs";
            public static readonly string GetTradesmanCallLogByJobQuotationId = $"{BaseUrl}GetTradesmanCallLogByJobQuotationId";
            public static readonly string GetSuppliersCallLog = $"{BaseUrl}GetSuppliersCallLog";
            public static readonly string GetJobQuotationCallLogs = $"{BaseUrl}GetJobQuotationCallLogs";
            public static readonly string GetCallLogByBidId = $"{BaseUrl}GetCallLogByBidId";
            public static readonly string DeleteSuppliersCallLogs = $"{BaseUrl}DeleteSuppliersCallLogs";
            public static readonly string GetSuppliersCallLogs = $"{BaseUrl}GetSuppliersCallLogs";
            public static readonly string PostCallRequestLog = $"{BaseUrl}PostCallRequestLog";
            public static readonly string GetCallRequestLogs = $"{BaseUrl}GetCallRequestLogs";
        }

        public static class AdminPackagesAndPayments
        {
            public static readonly string BaseUrl = "api/AdminPackagesAndPayments/";

            public static readonly string GetPackgesByCategory = $"{BaseUrl}GetPackgesByCategory";
            public static readonly string GetPackgesTypeByRoleId = $"{BaseUrl}GetPackgesTypeByRoleId";
            public static readonly string AddOrderByPackageId = $"{BaseUrl}AddOrderByPackageId";
            public static readonly string GetPromotionTypes = $"{BaseUrl}GetPromotionTypes";
            public static readonly string GetVoucherList = $"{BaseUrl}GetVoucherList";
            public static readonly string UpdatePckagesAndOrderStatus = $"{BaseUrl}UpdatePckagesAndOrderStatus";
            public static readonly string GetActiveOrdersList = $"{BaseUrl}GetActiveOrdersList";
            public static readonly string GetExpiredOrdersList = $"{BaseUrl}GetExpiredOrdersList";
            public static readonly string GetPromotionsBySuplierId = $"{BaseUrl}GetPromotionsBySuplierId";

        }
        public static class CMS
        {
            //public static readonly string BaseUrl = "http://local.services.com/CMS";
            //public static readonly string BaseUrl = "http://localhost:15779";
            public static readonly string BaseUrl = "api/CMS/";
            public static readonly string InsertAndUpDateCategory = $"{BaseUrl}InsertAndUpDateCategory";
            public static readonly string InsertAndUpDateSubCategory = $"{BaseUrl}InsertAndUpDateSubCategory";
            public static readonly string DeleteCategory = $"{BaseUrl}DeleteCategory";
            public static readonly string GetCategoryList = $"{BaseUrl}GetCategoryList";
            public static readonly string getuser = $"{BaseUrl}GetUser";
            public static readonly string CreateUpdatePost = $"{BaseUrl}CreateUpdatePost";
            public static readonly string GetPostsList = $"{BaseUrl}GetPostsList";
            public static readonly string GetPostDetails = $"{BaseUrl}GetPostDetails";
            public static readonly string CreateUpdatePageSeo = $"{BaseUrl}CreateUpdatePageSeo";
            public static readonly string GetPagesList = $"{BaseUrl}GetPagesList";
            public static readonly string GetSitePagesList = $"{BaseUrl}GetSitePagesList";
            public static readonly string AddUpdateSitePage = $"{BaseUrl}AddUpdateSitePage";
            public static readonly string GetSeoPageById = $"{BaseUrl}GetSeoPageById";
            public static readonly string GetSitePagesListByPageId = $"{BaseUrl}GetSitePagesListByPageId";
 





        }

        public static class Analytics
        {
            public static readonly string BaseUrl = "api/Analytics/";
            public static readonly string SaveAnalytics = $"{BaseUrl}SaveAnalytics";
            public static readonly string GetUserAnalytics = $"{BaseUrl}GetUserAnalytics";
        }
        public static class Customer
        {
            public static readonly string BaseUrl = "api/Customer/";
            //public static readonly string BaseUrl = "http://localhost:15764";
            //public static readonly string BaseUrl = "http://local.services.com/CustomerApi";
            public static readonly string GetAll = $"{BaseUrl}GetAll";
            public static readonly string GetNotificationById = $"{BaseUrl}GetNotificationById";
            public static readonly string GetCustomerById = $"{BaseUrl}GetCustomerById";
            public static readonly string GetByIds = $"{BaseUrl}GetCustomerByIdList";
            public static readonly string Login = $"{BaseUrl}Login";
            public static readonly string GetByName = $"{BaseUrl}GetByName";
            public static readonly string Add = $"{BaseUrl}Add";
            public static readonly string AddEditCustomer = $"{BaseUrl}AddEditCustomer";
            public static readonly string AddAdViews = $"{BaseUrl}AddAdViews";
            public static readonly string Delete = $"{BaseUrl}Delete";
            public static readonly string GetCustomerDetailsById = $"{BaseUrl}GetCustomerDetailsById";
            public static readonly string GetCityList = $"{BaseUrl}GetCityList";
            public static readonly string GetFile = $"{BaseUrl}GetFile";
            public static readonly string GetJobDetailsById = $"{BaseUrl}GetJobDetailsById";
            public static readonly string GetCustomerByIdList = $"{BaseUrl}GetCustomerByIdList";
            public static readonly string GetCustomerFavoriteByCustomerId = $"{BaseUrl}GetCustomerFavoriteByCustomerId";
            public static readonly string getPaymentRecords = $"{BaseUrl}getPaymentRecords";
            public static readonly string GetTradesmanCallLogs = $"{BaseUrl}GetTradesmanCallLogs";
            public static readonly string GetJobQuotationCallLogs = $"{BaseUrl}GetJobQuotationCallLogs";
            public static readonly string GetJobQuotationCallInfo = $"{BaseUrl}GetJobQuotationCallInfo";
            public static readonly string AddDeleteCustomerSavedAd = $"{BaseUrl}AddDeleteCustomerSavedAd";
            public static readonly string AddDeleteCustomerLikedAd = $"{BaseUrl}AddDeleteCustomerLikedAd";
            public static readonly string AddCustomerProductRating = $"{BaseUrl}AddCustomerProductRating";
            public static readonly string AddDeleteCustomerSavedTradesman = $"{BaseUrl}AddDeleteCustomerSavedTradesman";
            public static readonly string SetTradesmanToCustomerFavorite = $"{BaseUrl}SetTradesmanToCustomerFavorite";
            public static readonly string GetCustomerSavedAds = $"{BaseUrl}GetCustomerSavedAds";
            public static readonly string JobQuotation = $"{BaseUrl}JobQuotation";
            public static readonly string GetEntityIdByUserId = $"{BaseUrl}GetEntityIdByUserId";
            public static readonly string UpdateJobQuotation = $"{BaseUrl}UpdateJobQuotation";
            public static readonly string DeleteJobQuotation = $"{BaseUrl}DeleteJobQuotation";
            public static readonly string GetCustomerByUserId = $"{BaseUrl}GetCustomerByUserId";
            public static readonly string GetTradesmanIsFavorite = $"{BaseUrl}GetTradesmanIsFavorite";
            public static readonly string CheckSavedTradesmanById = $"{BaseUrl}CheckSavedTradesmanById";
            public static readonly string GetPersonalDetails = $"{BaseUrl}GetPersonalDetails";
            public static readonly string UpdatePersonalDetails = $"{BaseUrl}UpdatePersonalDetails";
            public static readonly string AddVideo = $"{BaseUrl}AddVideo";
            public static readonly string CheckSavedAdsByadId = $"{BaseUrl}CheckSavedAdsByadId";
            public static readonly string CheckLikedAdsByadId = $"{BaseUrl}CheckLikedAdsByadId";
            public static readonly string CheckRatedAdsByadId = $"{BaseUrl}CheckRatedAdsByadId";
            public static readonly string PostJobQuotationWeb = $"{BaseUrl}PostJobQuotationWeb";
            public static readonly string SpGetAdminDashBoard = $"{BaseUrl}SpGetAdminDashBoard";
            public static readonly string SpGetPrimaryUsersList = $"{BaseUrl}SpGetPrimaryUsersList";
            public static readonly string SpGetUserProfile = $"{BaseUrl}SpGetUserProfile";
            public static readonly string GetAllCustomers = $"{BaseUrl}GetAllCustomers";
            public static readonly string GetCustomerReport = $"{BaseUrl}GetCustomerReport";
            public static readonly string GetAllCustomersDropdown = $"{BaseUrl}GetAllCustomersDropdown";
            public static readonly string Get_All_Customers_Yearly_Report = $"{BaseUrl}Get_All_Customers_Yearly_Report";
            public static readonly string Get_All_Customers_From_To_Report = $"{BaseUrl}Get_All_Customers_From_To_Report";
            public static readonly string Get_All_Customers = $"{BaseUrl}Get_All_Customers";
            public static readonly string UpdateCustomerPublicId = $"{BaseUrl}UpdateCustomerPublicId";
            public static readonly string GetCustomersFordaynamicReport = $"{BaseUrl}GetRegistredCustomerForDaynamicReport";
            public static readonly string GetCustomerAddressList = $"{BaseUrl}GetCustomerAddressList";
            public static readonly string BlockCustomer = $"{BaseUrl}BlockCustomer";
            public static readonly string AdViewsCount = $"{BaseUrl}AdViewsCount";
            public static readonly string GetAdViewerList = $"{BaseUrl}GetAdViewerList";
            public static readonly string GetCustomerFavoriteTradesman = $"{BaseUrl}GetCustomerFavoriteTradesman";
            public static readonly string GetTopTradesman = $"{BaseUrl}GetTopTradesman";
            public static readonly string GetAdLikerList = $"{BaseUrl}GetAdLikerList";
            public static readonly string GetAdRatedList = $"{BaseUrl}GetAdRatedList";
            public static readonly string AddLinkedSalesman = $"{BaseUrl}AddLinkedSalesman";
            public static readonly string UpdatePhoneNumberByUserId = $"{BaseUrl}UpdatePhoneNumberByUserId";
            public static readonly string GetCustomerByPublicId = $"{BaseUrl}GetCustomerByPublicId";
            public static readonly string GetCustomerDashBoardCount = $"{BaseUrl}GetCustomerDashBoardCount";
            public static readonly string AddUpdateSecurityRoleItem = $"{BaseUrl}AddUpdateSecurityRoleItem";
            public static readonly string UpdateJobQuotationStatus = $"{BaseUrl}UpdateJobQuotationStatus";
            public static readonly string SaveAndRemoveProductsInWishlist = $"{BaseUrl}SaveAndRemoveProductsInWishlist";
            public static readonly string CheckProductStatusInWishList = $"{BaseUrl}CheckProductStatusInWishList";
            public static readonly string GetCustomerWishListProducts = $"{BaseUrl}GetCustomerWishListProducts";
            public static readonly string GetCustomerWishListProductsMobile = $"{BaseUrl}GetCustomerWishListProductsMobile";
            public static readonly string GetUserDetailsByUserRole = $"{BaseUrl}GetUserDetailsByUserRole";
            public static readonly string GetProductImageByFileName = $"{BaseUrl}GetProductImageByFileName";
            public static readonly string GetCityListWithTraxCityId = $"AdminCustomer/GetCityListWithTraxCityId";
        }

        public static class EstateAgent
        {
            public static readonly string BaseUrl = "http://localhost:15765";
            //public static readonly string BaseUrl = "http://local.services.com/EstateAgentApi";
        }

        public static class IdentityServer
        {
            public static readonly string BaseUrl = "Identity/";
            //public static readonly string BaseUrl = "http://localhost:15766";
            //public static readonly string BaseUrl = "http://local.services.com/IdentityServerApi";
            public static readonly string Login = $"{BaseUrl}Login";
            public static readonly string AdminUserLoging = $"{BaseUrl}AdminUserLoging";
            public static readonly string Changeusertype = $"{BaseUrl}Changeusertype";
            public static readonly string UpdateUsersPhoneNumberByUserId = $"{BaseUrl}UpdateUsersPhoneNumberByUserId";
            public static readonly string GetPhoneNumberVerificationStatus = $"{BaseUrl}GetPhoneNumberVerificationStatus";
            public static readonly string CheckEmailandPhoneNumberAvailability = $"{BaseUrl}CheckEmailandPhoneNumberAvailability";
            public static readonly string RegisterUser = $"{BaseUrl}RegisterUser";
            public static readonly string GetUserByUserId = $"{BaseUrl}GetUserByUserId";
            public static readonly string ForgotPassword = $"{BaseUrl}ForgotPassword";
            public static readonly string ResetPassword = $"{BaseUrl}ResetPassword";
            public static readonly string DeleteUser = $"{BaseUrl}DeleteUser";
            public static readonly string GetSecurityRoleItem = $"{BaseUrl}GetSecurityRoleItem";
            public static readonly string GetSecurityRoles = $"{BaseUrl}GetSecurityRoles";
            public static readonly string GetSecurityRoleDetails = $"{BaseUrl}GetSecurityRoleDetails";
            public static readonly string AddSecurityRoleDetails = $"{BaseUrl}AddSecurityRoleDetails";
            public static readonly string getAllInActiveUser = $"{BaseUrl}getAllInActiveUser";
            public static readonly string GetUserIdByEmailOrPhoneNumber = $"{BaseUrl}GetUserIdByEmailOrPhoneNumber";
            public static readonly string GetPasswordResetToken = $"{BaseUrl}GetPasswordResetToken";
            public static readonly string UpdateUserFirebaseToken = $"{BaseUrl}UpdateUserFirebaseToken";
            public static readonly string DecodeToken = $"{BaseUrl}DecodeToken";
            public static readonly string IsUserVerified = $"{BaseUrl}IsUserVerified";
            public static readonly string GetUserByToken = $"{BaseUrl}GetUserByToken";
            public static readonly string GetCitybyToken = $"{BaseUrl}GetCitybyToken";
            public static readonly string GetUserEmailByPhoneNumber = $"{BaseUrl}GetUserEmailByPhoneNumber";
            public static readonly string ValidateFacebookToken = $"{BaseUrl}ValidateFacebookToken";
            public static readonly string GetFbUserAsync = $"{BaseUrl}GetFbUserAsync";
            public static readonly string FindUserFbRecord = $"{BaseUrl}FindUserFbRecord";
            public static readonly string UpdateUser = $"{BaseUrl}UpdateUser";
            public static readonly string GetAdminUserDetails = $"{BaseUrl}GetAdminUserDetails";
            public static readonly string GetUserListByUserIds = $"{BaseUrl}GetUserListByUserIds";
            public static readonly string GetUserNameByUserId = $"{BaseUrl}GetUserNameByUserId";
            public static readonly string GetFirebaseIdByUserId = $"{BaseUrl}GetFirebaseIdByUserId";
            public static readonly string AdminForgotPasswordAuthentication = $"{BaseUrl}AdminForgotPasswordAuthentication";
            public static readonly string AdminResetPassword = $"{BaseUrl}AdminResetPassword";
            public static readonly string ChangeAdminUserPassword = $"{BaseUrl}ChangeAdminUserPassword";
            public static readonly string ApplicationServerPing = $"{BaseUrl}ApplicationServerPing";
            public static readonly string UpdateAdminUserdetails = $"{BaseUrl}UpdateAdminUserdetails";
            public static readonly string DeleteAdminUser = $"{BaseUrl}DeleteAdminUser";
            public static readonly string GetUserPinStatus = $"{BaseUrl}GetUserPinStatus";
            public static readonly string GetUsersTypeByUserIds = $"{BaseUrl}GetUsersTypeByUserIds";
            public static readonly string GetUserByFacebookId = $"{BaseUrl}GetUserByFacebookId";
            public static readonly string GetUserByGoogleId = $"{BaseUrl}GetUserByGoogleId";
            public static readonly string GetPhoneNumberByUserId = $"{BaseUrl}GetPhoneNumberByUserId";
            public static readonly string GetUserBlockStatus = $"{BaseUrl}GetUserBlockStatus";
            public static readonly string GetUserByAppleId = $"{BaseUrl}GetUserByAppleId";
            public static readonly string DeleteUserInfo = $"{BaseUrl}DeleteUserInfo";
            public static readonly string GetDeleteUserInfo = $"{BaseUrl}GetDeleteUserInfo";
            public static readonly string GetAllActiveUsers = $"{BaseUrl}GetAllActiveUsers";
            public static readonly string AddUpdateSecurityRoleItem = $"{BaseUrl}AddUpdateSecurityRoleItem";
            public static readonly string DeleteSecurityRoleItem = $"{BaseUrl}DeleteSecurityRoleItem";
            public static readonly string BlockUser = $"{BaseUrl}BlockUser";
            public static readonly string AddUpdateMenuItem = $"{BaseUrl}AddUpdateMenuItem";
            public static readonly string GetMenuItemsList = $"{BaseUrl}GetMenuItemsList";            
            public static readonly string AddUpdateSubMenuItem = $"{BaseUrl}AddUpdateSubMenuItem";
            public static readonly string GetSubMenuItemsList = $"{BaseUrl}GetSubMenuItemsList";
            public static readonly string UpdateLastActiveLogin = $"{BaseUrl}UpdateLastActiveLogin";
            public static readonly string GetSupplierAcountsType = $"{BaseUrl}GetSupplierAcountsType";
        }

        public static class Image
        {
            public static readonly string BaseUrl = "api/Images/";
            //public static readonly string BaseUrl = "http://localhost:15767";
            //public static readonly string BaseUrl = "http://local.services.com/ImageApi";
            public static readonly string GetBidImage = $"{BaseUrl}GetImages";
            public static readonly string GetJobImagesByIds = $"{BaseUrl}GetJobImagesByIds";
            public static readonly string GetImageByCustomerId = $"{BaseUrl}GetImageByCustomerId";
            public static readonly string GetMarkeetPlaceProductsImages = $"{BaseUrl}GetMarkeetPlaceProductsImages";
            public static readonly string GetJobQuotationImages = $"{BaseUrl}GetJobQuotationImages";
            public static readonly string GetAllImages = $"{BaseUrl}GetAllImages";
            public static readonly string GetJobImageByUrl = $"{BaseUrl}GetJobImageByUrl";
            public static readonly string GetByJobQuotationId = $"{BaseUrl}GetByJobQuotationId";
            public static readonly string GetJobBidImages = $"{BaseUrl}GetJobBidImages";
            public static readonly string GetJobImages = $"{BaseUrl}GetJobImages";
            public static readonly string GetSupplierAdImageByUrl = $"{BaseUrl}GetSupplierAdImageByUrl";
            public static readonly string GetCustomerProfileImageList = $"{BaseUrl}GetCustomerProfileImageList";
            public static readonly string GetImageByJobDetailId = $"{BaseUrl}GetImageByJobDetailId";
            public static readonly string GetTradesmanProfileImages = $"{BaseUrl}GetTradesmanProfileImages";
            public static readonly string GetProfileImageBySupplierId = $"{BaseUrl}GetProfileImageBySupplierId";
            public static readonly string GetTradesmanImageById = $"{BaseUrl}GetTradesmanImageById";
            public static readonly string GetProductCategoryImages = $"{BaseUrl}GetProductCategoryImages";
            public static readonly string GetJobImagesListByJobQuotationId = $"{BaseUrl}GetJobImagesListByJobQuotationId";
            public static readonly string GetJobImagesListByJobQuotationIds = $"{BaseUrl}GetJobImagesListByJobQuotationIds";
            public static readonly string GetTradesmanProfileImageByTradesmanId = $"{BaseUrl}GetTradesmanProfileImageByTradesmanId";
            public static readonly string GetTradesmanProfileImageBySkillIds = $"{BaseUrl}GetTradesmanProfileImageBySkillIds";
            public static readonly string GetSupplierProductImageByProductCategoryId = $"{BaseUrl}GetSupplierProductImageByProductCategoryId";
            public static readonly string GetSupplierAdImagesByAdId = $"{BaseUrl}GetSupplierAdImagesByAdId";
            public static readonly string GetSupplierAdImagesBySupplierAdIds = $"{BaseUrl}GetSupplierAdImagesBySupplierAdIds";
            public static readonly string SaveJobImages = $"{BaseUrl}SaveJobImages";
            public static readonly string SaveDisputeImages = $"{BaseUrl}SaveDisputeImages";
            public static readonly string DeleteJobImages = $"{BaseUrl}DeleteJobImages";
            public static readonly string DeleteSupplierAdImages = $"{BaseUrl}DeleteSupplierAdImages";
            public static readonly string GetAdsImages = $"{BaseUrl}GetAdsImages";
            public static readonly string SubmitAndUpdateSupplierAdImages = $"{BaseUrl}SubmitAndUpdateSupplierAdImages";
            public static readonly string GetJobMainImage = $"{BaseUrl}GetJobMainImages";
            public static readonly string GetJobImage = $"{BaseUrl}GetJobImage";


            public static readonly string AddUpdateTradesmanProfileImage = $"{BaseUrl}AddUpdateTradesmanProfileImage";
            public static readonly string AddUpdateUserProfileImage = $"{BaseUrl}AddUpdateUserProfileImage";
            public static readonly string AddUpdateSupplierProfileImage = $"{BaseUrl}AddUpdateSupplierProfileImage";
            public static readonly string GetSupplierAdImageById = $"{BaseUrl}GetSupplierAdImageById";
            public static readonly string GetJobImageById = $"{BaseUrl}GetJobImageById";
            public static readonly string FeaturedSupplierImages = $"{BaseUrl}FeaturedSupplierImages";
            public static readonly string GetFeaturedSupplierImages = $"{BaseUrl}GetFeaturedSupplierImages";
            public static readonly string UpdateFeaturedSupplierImages = $"{BaseUrl}UpdateFeaturedSupplierImages";
            public static readonly string AddSupplierProductImages = $"{BaseUrl}AddSupplierProductImages";
            public static readonly string MarkProductImageAsMain = $"{BaseUrl}MarkProductImageAsMain";
            public static readonly string AddAndUpdateLogo = $"{BaseUrl}AddAndUpdateLogo";
            public static readonly string GetLogoData = $"{BaseUrl}GetLogoData";
            
        }

        public static class Job
        {
            public static readonly string BaseUrl = "api/Job/";
            //public static readonly string BaseUrl = "http://localhost:15768";
            //public static readonly string BaseUrl = "http://local.services.com/JobApi";

            public static readonly string GetAllJobs = $"{BaseUrl}GetAllJobs";
            public static readonly string GetByCustomerId = $"{BaseUrl}GetByCustomerId";
            public static readonly string GetById = $"{BaseUrl}GetById";
            public static readonly string GetActiveBids = $"{BaseUrl}GetActiveBids";
            public static readonly string GetByName = $"{BaseUrl}GetByName";
            public static readonly string GetInprogressJob = $"{BaseUrl}GetInprogressJob";
            public static readonly string GetDeclinedBidsDetails = $"{BaseUrl}GetDeclinedBidsDetails";
            public static readonly string GetDeclinedBidsDetailsWeb = $"{BaseUrl}GetDeclinedBidsDetailsWeb";
            public static readonly string GetAllBids = $"{BaseUrl}GetAllBids";
            public static readonly string CheckFeedBackStatus = $"{BaseUrl}CheckFeedBackStatus";
            public static readonly string GetJobQuotationsByIds = $"{BaseUrl}GetJobQuotationsByIds";
            public static readonly string GetAllJobsDetails = $"{BaseUrl}GetAllJobsDetails";
            public static readonly string GetJobsDetail = $"{BaseUrl}GetJobsDetail";
            public static readonly string GetJobDetail = $"{BaseUrl}GetJobDetail";
            public static readonly string GetJobDetailWeb = $"{BaseUrl}GetJobDetailWeb";
            public static readonly string GetJobLeadsByTradesmanId = $"{BaseUrl}GetJobLeadsByTradesmanId";
            public static readonly string GetJobQuotationsBySkillId = $"{BaseUrl}GetJobQuotationsBySkillId";
            public static readonly string GetJobQuotationsBySkill = $"{BaseUrl}GetJobQuotationsBySkill";
            public static readonly string GetJobQuotationsWebBySkill = $"{BaseUrl}GetJobQuotationsWebBySkill";
            public static readonly string GetUsertypeByTradesmanId = $"{BaseUrl}GetUsertypeByTradesmanId";
            public static readonly string GetCompletedJobById = $"{BaseUrl}GetCompletedJobById";
            public static readonly string GetAJobByJobDeatilId = $"{BaseUrl}GetAJobByJobDeatilId";
            public static readonly string GetFeedBack = $"{BaseUrl}GetFeedBack";
            public static readonly string GetTradesmanFeedBack = $"{BaseUrl}GetTradesmanFeedBack";
            public static readonly string GetJobQuotations = $"{BaseUrl}GetJobQuotations";
            public static readonly string SubmitBid = $"{BaseUrl}SubmitBid";
            public static readonly string GetFinishedJob = $"{BaseUrl}GetFinishedJob";
            public static readonly string GetJobDetailsById = $"{BaseUrl}GetJobDetailsById";
            public static readonly string GetJobQuotationById = $"{BaseUrl}GetJobQuotationById";
            public static readonly string GetJobsByTradesmanIds = $"{BaseUrl}GetJobsByTradesmanIds";
            public static readonly string GetBidCounts = $"{BaseUrl}GetBidCounts";
            public static readonly string GetBidCountsOnJob = $"{BaseUrl}GetBidCountsOnJob";
            public static readonly string GetBidCountByJobQuotationId = $"{BaseUrl}GetBidCountByJobQuotationId";
            public static readonly string GetQuotationBids = $"{BaseUrl}GetQuotationBids";
            public static readonly string UpdateSelectedBid = $"{BaseUrl}UpdateSelectedBid";
            public static readonly string GetPostedJobs = $"{BaseUrl}GetPostedJobs";
            public static readonly string UpdateBidStatus = $"{BaseUrl}UpdateBidStatus";
            public static readonly string GetBidById = $"{BaseUrl}GetBidById";
            public static readonly string GetBidsCountOnJobQuotation = $"{BaseUrl}GetBidsCountOnJobQuotation";
            public static readonly string GetBidDetails = $"{BaseUrl}GetBidDetails";
            public static readonly string AddJobDetails = $"{BaseUrl}AddJobDetails";
            public static readonly string GetJobDetailsByJobQuotationId = $"{BaseUrl}GetJobDetailsByJobQuotationId";
            public static readonly string AddEscalateIssue = $"{BaseUrl}AddEscalateIssue";
            public static readonly string GetActiveBidsDetails = $"{BaseUrl}GetActiveBidsDetails";
            public static readonly string GetActiveBidsDetailsWeb = $"{BaseUrl}GetActiveBidsDetailsWeb";
            public static readonly string GetCompletedJobDetailsByCustomerAndStatusIds = $"{BaseUrl}GetCompletedJobDetailsByCustomerAndStatusIds";
            public static readonly string GetDisputeRecord = $"{BaseUrl}GetDisputeRecord";
            public static readonly string GetDisputeStatusByStatusIds = $"{BaseUrl}GetDisputeStatusByStatusIds";
            public static readonly string updateStatuse = $"{BaseUrl}updateStatuse";
            public static readonly string GetJobDetails = $"{BaseUrl}GetJobDetails";
            public static readonly string GetPostedJobsByCustomerId = $"{BaseUrl}GetPostedJobsByCustomerId";
            public static readonly string GetJobQuotationByJobQuotationId = $"{BaseUrl}GetJobQuotationByJobQuotationId";
            public static readonly string GetJobAddressByJobQuotationId = $"{BaseUrl}GetJobAddressByJobQuotationId";
            public static readonly string GetFavoriteTradesmenByJobQuotationId = $"{BaseUrl}GetFavoriteTradesmenByJobQuotationId";
            public static readonly string GetJobStatusByCustomerId = $"{BaseUrl}GetJobStatusByCustomerId";
            public static readonly string GetJobAddressByJobQuotationIds = $"{BaseUrl}GetJobAddressByJobQuotationIds";
            public static readonly string GetTradesmanFeedbackByCustomerIds = $"{BaseUrl}GetTradesmanFeedbackByCustomerIds";
            public static readonly string GetFinishedJobList = $"{BaseUrl}GetFinishedJobList";
            public static readonly string GetFinishedJobDetails = $"{BaseUrl}GetFinishedJobDetails";
            public static readonly string PostTradesmanFeedback = $"{BaseUrl}PostTradesmanFeedback";
            public static readonly string GetSupplierFeedbackBySupplierId = $"{BaseUrl}GetSupplierFeedbackBySupplierId";
            public static readonly string SetSupplierRating = $"{BaseUrl}SetSupplierRating";
            public static readonly string JobQuotation = $"{BaseUrl}JobQuotation";
            public static readonly string SaveJobAddress = $"{BaseUrl}SaveJobAddress";
            public static readonly string GetAlljobDetails = $"{BaseUrl}GetAlljobDetails";
            public static readonly string GetBidListByCustomerId = $"{BaseUrl}GetBidListByCustomerId";
            public static readonly string GetInprogressJobDetail = $"{BaseUrl}GetInprogressJobDetail";
            public static readonly string UpdateJobQuotation = $"{BaseUrl}UpdateJobQuotation";
            public static readonly string DeleteJobQuotation = $"{BaseUrl}DeleteJobQuotation";
            public static readonly string DeleteJobAddressByJobQuotationId = $"{BaseUrl}DeleteJobAddressByJobQuotationId";
            public static readonly string GetBidJobQuotaionId = $"{BaseUrl}GetBidJobQuotaionId";

            public static readonly string GetJobAddress = $"{BaseUrl}GetJobAddress";
            public static readonly string MarkAsFinishedJob = $"{BaseUrl}MarkAsFinishedJob";
            public static readonly string UpdateJobDetailStatus = $"{BaseUrl}UpdateJobDetailStatus";
            public static readonly string AddJobQuotationFavoriteTradesmen = $"{BaseUrl}AddJobQuotationFavoriteTradesmen";
            public static readonly string DeleteEscalateIssue = $"{BaseUrl}DeleteEscalateIssue";

            public static readonly string GetQuotationIdByBidId = $"{BaseUrl}GetQuotationIdByBidId";
            public static readonly string GetJobDetailIdByQuotationId = $"{BaseUrl}GetJobDetailIdByQuotationId";
            public static readonly string GetJobQuotationBidsByJobQuotatationIds = $"{BaseUrl}GetJobQuotationBidsByJobQuotatationIds";
            public static readonly string DeleteJobDetailsByJobQuotationId = $"{BaseUrl}DeleteJobDetailsByJobQuotationId";
            public static readonly string UpdateJobCost = $"{BaseUrl}UpdateJobCost";


            public static readonly string SpGetPostedJobsByCustomerId = $"{BaseUrl}SpGetPostedJobsByCustomerId";
            public static readonly string SpGetJobsByCustomerId = $"{BaseUrl}SpGetJobsByCustomerId";
            public static readonly string GetJobQuotationDataByQuotationId = $"{BaseUrl}GetJobQuotationDataByQuotationId";
            public static readonly string GetJobQuotationMediaByQuotationId = $"{BaseUrl}GetJobQuotationMediaByQuotationId";
            public static readonly string GetQuoteVideoByJobQuotationId = $"{BaseUrl}GetQuoteVideoByJobQuotationId";
            public static readonly string GetQuoteImageById = $"{BaseUrl}GetQuoteImageById";
            public static readonly string GetQuoteAudioByJobQuotationId = $"{BaseUrl}GetQuoteAudioByJobQuotationId";

            public static readonly string GetTradesmanByBidId = $"{BaseUrl}GetTradesmanByBidId";
            public static readonly string Sp_GetTradesmanFirebaseClientId = $"{BaseUrl}Sp_GetTradesmanFirebaseClientId";
            public static readonly string UpdateJobQuotationStatus = $"{BaseUrl}UpdateJobQuotationStatus";
            public static readonly string WebLiveLeads = $"{BaseUrl}WebLiveLeads";
            public static readonly string WebLiveLeadsLatLong = $"{BaseUrl}WebLiveLeadsLatLong";
            public static readonly string WebLiveLeadsPanel = $"{BaseUrl}WebLiveLeadsPanel";
            public static readonly string getEscalateOptions = $"{BaseUrl}getEscalateOptions";
            public static readonly string submitIssue = $"{BaseUrl}submitIssue";
            public static readonly string getEscalateIssueByJQID = $"{BaseUrl}getEscalateIssueByJQID";
            public static readonly string UpdateBidByStatusId = $"{BaseUrl}UpdateBidByStatusId";
            public static readonly string GetAcceptedBidsList = $"{BaseUrl}GetAcceptedBidsList";
            public static readonly string GetInprogressJobsMobile = $"{BaseUrl}GetInprogressJobsMobile";
            public static readonly string JobStartNotification = $"{BaseUrl}JobStartNotification";
            public static readonly string JobFifnishedNotification = $"{BaseUrl}JobFifnishedNotification";
            public static readonly string JobStartNotificationForTradesman = $"{BaseUrl}JobStartNotificationForTradesman";
            public static readonly string StartOrFinishJob = $"{BaseUrl}StartOrFinishJob";
            public static readonly string UpdateJobAdditionalCharges = $"{BaseUrl}UpdateJobAdditionalCharges";
            public static readonly string JobPostByFacebookLeads = $"{BaseUrl}JobPostByFacebookLeads";
            public static readonly string GetUserFromFacebookLeads = $"{BaseUrl}GetUserFromFacebookLeads";

            //Admin side
            public static readonly string GetAllJobsCount = $"{BaseUrl}GetAllJobsCount";
            public static readonly string GetActiveJobList = $"{BaseUrl}GetActiveJobList";
            public static readonly string GetPendingJobList = $"{BaseUrl}GetPendingJobList";
            public static readonly string GetInprogressJobList = $"{BaseUrl}GetInprogressJobList";
            public static readonly string GetDeletedJobList = $"{BaseUrl}GetDeletedJobList";
            public static readonly string GetJobDetailsList = $"{BaseUrl}GetJobDetailsList";
            public static readonly string CompletedJobListAdmin = $"{BaseUrl}CompletedJobListAdmin";
            public static readonly string GetReciveBids = $"{BaseUrl}GetReciveBids";
            public static readonly string GetReciveBidDetails = $"{BaseUrl}GetReciveBidDetails";
            public static readonly string GetJobStatusDropdown = $"{BaseUrl}JobStatusForDropDown";
            public static readonly string GetCsJobStatusDropdown = $"{BaseUrl}GetCsJobStatusDropdown";
            public static readonly string GetJobsForReport = $"{BaseUrl}GetJobsForReport";
            public static readonly string GEtlatestJobsForOneDayReport = $"{BaseUrl}GEtlatestJobsForOneDayReport";
            public static readonly string latestJobsForOneDayReport = $"{BaseUrl}latestJobsForOneDayReport";
            public static readonly string GetlatestBidsForOneDayReport = $"{BaseUrl}latestBidsForOneDayReport";
            public static readonly string GetPostedJobsForDynamicReport = $"{BaseUrl}GetPostedJobsForDynamicReport";
            public static readonly string GetBidsForDynamicReport = $"{BaseUrl}GetBidsForDynamicReport";
            public static readonly string GetPostedJobsAddressList = $"{BaseUrl}GetPostedJobsAddressList";
            public static readonly string DeleteJobByQuotationId = $"{BaseUrl}DeleteJobByQuotationId";
            public static readonly string GetTownsList = $"{BaseUrl}GetTownsList";
            public static readonly string PostJobContactInfo = $"{BaseUrl}PostJobContactInfo";
            public static readonly string GetJobsCount = $"{BaseUrl}GetJobsCount";
            public static readonly string ApproveJob = $"{BaseUrl}ApproveJob";
            public static readonly string JobAuthorizer = $"{BaseUrl}JobAuthorizer";
            public static readonly string JobAuthorizerList = $"{BaseUrl}JobAuthorizerList";
            public static readonly string AdminJobAuthorizerList = $"{BaseUrl}AdminJobAuthorizerList";
            public static readonly string JobDetailsByJQID = $"{BaseUrl}JobDetailsByJQID";
            public static readonly string UpdateJobBudget = $"{BaseUrl}UpdateJobBudget";
            public static readonly string UpdateJobExtraCharges = $"{BaseUrl}UpdateJobExtraCharges";
            public static readonly string AssignJobToTradesman = $"{BaseUrl}AssignJobToTradesman";
            public static readonly string GetUrgentJobsList = $"{BaseUrl}GetUrgentJobsList";
            public static readonly string GetJobsListByCategory = $"{BaseUrl}GetJobsListByCategory";
            public static readonly string GetBidCountOnJob = $"{BaseUrl}GetBidCountOnJob";
            public static readonly string GetBidCountByJobId = $"{BaseUrl}GetBidCountByJobId";
            public static readonly string GetBidByJQID = $"{BaseUrl}GetBidByJQID";
            public static readonly string GetTradesmanByName = $"{BaseUrl}GetTradesmanByName";
            public static readonly string ChangeJobStatus = $"{BaseUrl}ChangeJobStatus";
            public static readonly string GetBidStatusForTradesmanId = $"{BaseUrl}GetBidStatusForTradesmanId";
            public static readonly string EscalateIssuesRequestList =$"{BaseUrl}EscalateIssuesRequestList";
            public static readonly string AuthorizeEscalateIssueRequest = $"{BaseUrl}AuthorizeEscalateIssueRequest";
            public static readonly string AuthorizeEscalateIssuesList = $"{BaseUrl}AuthorizeEscalateIssuesList";
            public static readonly string GetEscalateOptionsList = $"{BaseUrl}GetEscalateOptionsList";
            public static readonly string InsertAndUpdateEscalateOption = $"{BaseUrl}InsertAndUpdateEscalateOption";
        }

        public static class Promotion
        {
            public static readonly string BaseUrl = "api/Promotion/";
            //public static readonly string BaseUrl = "http://localhost:15769";
            //public static readonly string BaseUrl = "http://local.services.com/LoggingApi";

            public static readonly string AddEditPromotionReferalCode = $"{BaseUrl}AddEditPromotionReferalCode";
            public static readonly string GetReferalRecordByreferalCode = $"{BaseUrl}GetReferalRecordByreferalCode";
            public static readonly string GetReferalRecordByreferalUserID = $"{BaseUrl}GetReferalRecordByreferalUserID";
            public static readonly string GetVoucherList = $"{BaseUrl}GetVoucherList";
            public static readonly string AddEditRedemptions = $"{BaseUrl}AddEditRedemptions";
            public static readonly string AddPromotionRedemptions = $"{BaseUrl}AddPromotionRedemptions";
            public static readonly string GetRedeemRecordByRedeemUserID = $"{BaseUrl}GetRedeemRecordByRedeemUserID";
            public static readonly string GetRedeemRecordByJQID = $"{BaseUrl}GetRedeemRecordByJQID";
            public static readonly string GetProRecordByJQID = $"{BaseUrl}GetProRecordByJQID";
            public static readonly string GetPromotionRedeemRecordByRedeemUserID = $"{BaseUrl}GetPromotionRedeemRecordByRedeemUserID";
            public static readonly string GetReferalCodeByUserId = $"{BaseUrl}GetReferalCodeByUserId";
            public static readonly string GetRedemptionById = $"{BaseUrl}GetRedemptionById";
        }
        public static class Logging
        {
            public static readonly string BaseUrl = "api/Logging/";
            //public static readonly string BaseUrl = "http://localhost:15769";
            //public static readonly string BaseUrl = "http://local.services.com/LoggingApi";

            // Gateway level routes
            public static readonly string LogException = $"{BaseUrl}LogException";
        }

        public static class Notification
        {
            public static readonly string BaseUrl = "api/Notification/";
            //public static readonly string BaseUrl = "http://localhost:15770";
            //public static readonly string BaseUrl = "http://local.services.com/NotificationApi";

            // Gateway Level Routes
            public static readonly string SendBidNotification = $"{BaseUrl}SendBidNotification";
            public static readonly string UpdateNotificationStatus = $"{BaseUrl}UpdateNotificationStatus";
            public static readonly string GetNotificationById = $"{BaseUrl}GetNotificationById";
            public static readonly string NotificationCallRequest = $"{BaseUrl}NotificationCallRequest";
            public static readonly string PostBidAcceptanceNotification = $"{BaseUrl}PostBidAcceptanceNotification";
            public static readonly string RequestFeedbackNotification = $"{BaseUrl}RequestFeedbackNotification";
            public static readonly string PromoteAdNotification = $"{BaseUrl}PromoteAdNotification";
            public static readonly string NotificationRateSupplier = $"{BaseUrl}NotificationRateSupplier";
            public static readonly string NotificationRatingTradesman = $"{BaseUrl}NotificationRatingTradesman";
            public static readonly string GetNotifications = $"{BaseUrl}GetNotifications";
            public static readonly string GetNotificationsForOrders = $"{BaseUrl}GetNotificationsForOrders";
            public static readonly string GetAdminNotifications = $"{BaseUrl}GetAdminNotifications";
            public static readonly string GetNotificationsCount = $"{BaseUrl}GetNotificationsCount";
            public static readonly string GetHWMallNotificationsCount = $"{BaseUrl}GetHWMallNotificationsCount";
            public static readonly string NotificationJobCostUpdate = $"{BaseUrl}NotificationJobCostUpdate";
            public static readonly string NotificationBidDecline = $"{BaseUrl}NotificationBidDecline";
            public static readonly string NotificationJobUpdate = $"{BaseUrl}NotificationJobUpdate";
            public static readonly string GetNotificationsByUserId = $"{BaseUrl}GetNotificationsByUserId";
            public static readonly string MarkNotificationAsRead = $"{BaseUrl}MarkNotificationAsRead";


            // API Level Routes
            public static readonly string PostTopicNotification = $"{BaseUrl}PostTopicNotification";
            public static readonly string PostDataNotification = $"{BaseUrl}PostDataNotification";
            public static readonly string SaveNotificationDataWeb = $"{BaseUrl}SaveNotificationDataWeb";
            public static readonly string GetNotificationLogByNotificationId = $"{BaseUrl}GetNotificationLogByNotificationId";
            public static readonly string UpdateNotificationLogByNotificationId = $"{BaseUrl}UpdateNotificationLogByNotificationId";
        }

        public static class Payment
        {

            public static readonly string BaseUrl = "api/Payment/";
            //public static readonly string BaseUrl = "http://localhost:15772";
            //public static readonly string BaseUrl = "http://local.services.com/PaymentApi";

            public static readonly string GetMembershipListById = $"{BaseUrl}GetMembershipListById";
            public static readonly string GetAllPaymentMethods = $"{BaseUrl}GetAllPaymentMethods";
            public static readonly string GetJobReceiptsByTradesmanId = $"{BaseUrl}GetJobReceiptsByTradesmanId";
            public static readonly string AddEditTradesmanJobReceipts = $"{BaseUrl}AddEditTradesmanJobReceipts";
            public static readonly string AddTradesmanJobReceipts = $"{BaseUrl}AddTradesmanJobReceipts";
            public static readonly string GetTradesmanJobReceiptsByTradesmanId = $"{BaseUrl}GetTradesmanJobReceiptsByTradesmanId";
            public static readonly string getPaymentRecords = $"{BaseUrl}getPaymentRecords";
            public static readonly string GetadvertismentPlans = $"{BaseUrl}GetadvertismentPlans";
            public static readonly string sportLightadvertismentsPlan = $"{BaseUrl}sportLightadvertismentsPlan";
            public static readonly string FeaturedadvertismentsPlan = $"{BaseUrl}FeaturedadvertismentsPlan";
            public static readonly string GetPaymentsHistory = $"{BaseUrl}GetPaymentsHistory";
            public static readonly string GetPromotionPaymentHistory = $"{BaseUrl}GetPromotionPaymentHistory";
            public static readonly string AddEditPaymentHistory = $"{BaseUrl}AddEditPaymentHistory";
            public static readonly string GetpaymentRecord = $"{BaseUrl}GetpaymentRecord";
            public static readonly string GetPaymentsPlan = $"{BaseUrl}GetPaymentsPlan";
            public static readonly string GetPaymentBill = $"{BaseUrl}GetPaymentBill";
            public static readonly string GetMethodCodeById = $"{BaseUrl}GetMethodCodeById";
            public static readonly string UpdateJobReceiptCost = $"{BaseUrl}UpdateJobReceiptCost";
            public static readonly string AddEditTransationDetail = $"{BaseUrl}AddEditTransationDetail";
            public static readonly string ProceedToJazzCash = $"{BaseUrl}ProceedToJazzCash";
            public static readonly string JazzCashCallBack = $"{BaseUrl}JazzCashCallBack";
            public static readonly string GetJazzCashMerchantDetails = $"{BaseUrl}GetJazzCashMerchantDetails";
            
        }

        //public static class Organization
        //{
        //    public static readonly string BaseUrl = "http://localhost:15771";
        //    public static readonly string AddEdit = $"{BaseUrl}Organization/AddEdit";
        //    public static readonly string SetSkills = $"{BaseUrl}Organization/SetSkills";
        //    public static readonly string Delete = $"{BaseUrl}Organization/Delete";
        //    public static readonly string GetPersonalDetails = $"{BaseUrl}Organization/GetPersonalDetails";
        //    public static readonly string GetEntityIdByUserId = $"{BaseUrl}Organization/GetEntityIdByUserId";
        //    public static readonly string GetBusinessDetails = $"{BaseUrl}Organization/GetBusinessDetails";
        //    public static readonly string GetByUserId = $"{BaseUrl}Organization/GetByUserId";
        //    public static readonly string GetSkillIds = $"{BaseUrl}Organization/GetSkillIds";
        //}

        public static class Purchase
        {
            public static readonly string BaseUrl = "http://localhost:15773";
            //public static readonly string BaseUrl = "http://local.services.com/PurchaseApi";
        }

        public static class Supplier
        {
            public static readonly string BaseUrl = "api/Supplier/";
            //public static readonly string BaseUrl = "http://localhost:15774";
            //public static readonly string BaseUrl = "http://local.services.com/SupplierApi";
            public static readonly string GetAllProductCategory = $"{BaseUrl}GetAllProductCategory";
            public static readonly string UpdateStockLevel= $"{BaseUrl}UpdateStockLevel"; 
            public static readonly string AddCustomerFeedBack = $"{BaseUrl}AddCustomerFeedBack";
            public static readonly string GetCustomerFeedBackList = $"{BaseUrl}GetCustomerFeedBackList";
            public static readonly string GetProfileVerification = $"{BaseUrl}GetProfileVerification"; 
            public static readonly string GetOrderDetailById = $"{BaseUrl}GetOrderDetailById";
            public static readonly string GetPaymentHistory = $"{BaseUrl}GetPaymentHistory"; 
            public static readonly string GetTransactionHistory = $"{BaseUrl}GetTransactionHistory"; 
            public static readonly string GetPaymentDetail = $"{BaseUrl}GetPaymentDetail";
            public static readonly string GetCategoriesForAdminListing = $"{BaseUrl}GetCategoriesForAdminListing";
            public static readonly string GetCategoriesNameWithId = $"{BaseUrl}GetCategoriesNameWithId";
            public static readonly string getProductSubCategoryById = $"{BaseUrl}getProductSubCategoryById";
            public static readonly string DeleteSuppliersCallLogs = $"{BaseUrl}DeleteSuppliersCallLogs";
            public static readonly string AddEditSupplier = $"{BaseUrl}AddEditSupplier";
            public static readonly string GetSupplierAdsById = $"{BaseUrl}GetSupplierAdsById";
            public static readonly string GetSuppliersOrderlistById = $"{BaseUrl}GetSuppliersOrderlistById";
            public static readonly string GetSupplierReport = $"{BaseUrl}GetSupplierReport";
            public static readonly string GetAllSubProducts = $"{BaseUrl}GetAllSubProducts";
            public static readonly string GetSuppliersOrderlist = $"{BaseUrl}GetSuppliersOrderlist";
            public static readonly string GetSuppliersLeadgerlist = $"{BaseUrl}GetSuppliersLeadgerlist";
            public static readonly string BlockProduct = $"{BaseUrl}BlockProduct";
            public static readonly string GetAllProductVariant = $"{BaseUrl}GetAllProductVariant";
            public static readonly string RelistAd = $"{BaseUrl}RelistAd";
            public static readonly string GetProductCategory = $"{BaseUrl}GetProductCategory";
            public static readonly string GetCategoriesForListing = $"{BaseUrl}GetCategoriesForListing";
            public static readonly string GetSupplierList = $"{BaseUrl}GetSupplierList";
            public static readonly string GetSupplierImageList = $"{BaseUrl}GetSupplierImageList";
            public static readonly string GetAllProductCatagories = $"{BaseUrl}GetAllProductCatagories";
            public static readonly string GetSupplierBySupplierId = $"{BaseUrl}GetSupplierBySupplierId";
            public static readonly string GetSuppliersFeedbackBySupplierId = $"{BaseUrl}GetSuppliersFeedbackBySupplierId";
            public static readonly string GetSupplierAdByAdId = $"{BaseUrl}GetSupplierAdByAdId";
            public static readonly string GetProductDetailsByAdId = $"{BaseUrl}GetProductDetailsByAdId";
            public static readonly string GetSubCategoriesByProductCategoryId = $"{BaseUrl}GetSubCategoriesByProductCategoryId";
            public static readonly string GetSupplierAdsByProductSubCategoryIds = $"{BaseUrl}GetSupplierAdsByProductSubCategoryIds";
            public static readonly string GetProductCategoryDetails = $"{BaseUrl}GetProductCategoryDetails";
            public static readonly string GetSuppliersByIds = $"{BaseUrl}GetSuppliersByIds";
            public static readonly string GetSupplierById = $"{BaseUrl}GetSupplierById";
            public static readonly string GetSupplierAdsBySupplierId = $"{BaseUrl}GetSupplierAdsBySupplierId";
            public static readonly string GetSupplierShop = $"{BaseUrl}GetSupplierShop";
            public static readonly string GetSupplierAdsByStatusId = $"{BaseUrl}GetSupplierAdsByStatusId";
            public static readonly string GetEntityIdByUserId = $"{BaseUrl}GetEntityIdByUserId";
            public static readonly string GetSuppliersCallLog = $"{BaseUrl}GetSuppliersCallLog";
            public static readonly string GetAllProductSubCatagories = $"{BaseUrl}GetAllProductSubCatagories";
            public static readonly string AddRegistrationDetail = $"{BaseUrl}AddRegistrationDetail";
            public static readonly string AddAllProductSubCatgory = $"{BaseUrl}AddAllProductSubCatgory";
            public static readonly string GetAllSupplierAds = $"{BaseUrl}GetAllSupplierAds";
            public static readonly string GetAllSupplier = $"{BaseUrl}GetAllSupplier";
            public static readonly string GetManageAds = $"{BaseUrl}GetManageAds";
            public static readonly string GetInactiveManageAds = $"{BaseUrl}GetInactiveManageAds";
            public static readonly string GetProductImages = $"{BaseUrl}GetProductImages";
            public static readonly string productCategory = $"{BaseUrl}productCategory";
            public static readonly string GetProductCatogory = $"{BaseUrl}GetProductCatogory";
            public static readonly string ProductNamesByCatId = $"{BaseUrl}ProductNamesByCatId";
            public static readonly string postsupplierAds = $"{BaseUrl}postsupplierAds";
            public static readonly string postsupplieradsdetail = $"{BaseUrl}postsupplieradsdetail";
            public static readonly string UpdatePersonalDetail = $"{BaseUrl}UpdatePersonalDetail";
            public static readonly string AllSubCategory = $"{BaseUrl}AllSubCategory";
            public static readonly string SaveAndUpdateAd = $"{BaseUrl}SaveAndUpdateAd";
            public static readonly string GetEditAdDetail = $"{BaseUrl}GetEditAdDetail";
            public static readonly string GetProductCategoryIdBySupplierID = $"{BaseUrl}GetProductCategoryIdBySupplierID";
            public static readonly string UpdateAdcStatus = $"{BaseUrl}UpdateAdcStatus";
            public static readonly string UpdateSupplierAdsstatus = $"{BaseUrl}UpdateSupplierAdsstatus";
            public static readonly string DeleteCallLogs = $"{BaseUrl}DeleteCallLogs";
            public static readonly string DeleteAdVideo = $"{BaseUrl}DeleteAdVideo";
            public static readonly string UpdateAd = $"{BaseUrl}UpdateAd";
            public static readonly string GetSupplierByUserId = $"{BaseUrl}GetSupplierByUserId";
            public static readonly string SetSupplierSubCategories = $"{BaseUrl}SetSupplierSubCategories";
            public static readonly string GetSupplierTradeNmae = $"{BaseUrl}GetSupplierTradeNmae";
            public static readonly string GetProductSubCategoryById = $"{BaseUrl}GetProductSubCategoryById";
            public static readonly string GetBusinessProfile = $"{BaseUrl}GetBusinessProfile";
            public static readonly string GetPersonalInformation = $"{BaseUrl}GetPersonalInformation";
            public static readonly string PostPersonalInformation = $"{BaseUrl}PostPersonalInformation";
            public static readonly string DeleteAd = $"{BaseUrl}DeleteAd";
            public static readonly string UpdateSupplierAdViewCount = $"{BaseUrl}UpdateSupplierAdViewCount";
            public static readonly string GetSelectedSupplierSubCategory = $"{BaseUrl}GetSelectedSupplierSubCategory";
            public static readonly string getProductSubCategoriesById = $"{BaseUrl}getProductSubCategoriesById";
            public static readonly string GetProductDataByAdId = $"{BaseUrl}GetProductDataByAdId";
            public static readonly string GetProductMediaByAdId = $"{BaseUrl}GetProductMediaByAdId";
            public static readonly string GetSupplierAdImageById = $"{BaseUrl}GetSupplierAdImageById";
            public static readonly string GetCategoryImageById = $"{BaseUrl}GetCategoryImageById";
            public static readonly string GetProductVideoByAdId = $"{BaseUrl}GetProductVideoByAdId";
            public static readonly string SpGetActiveJobs = $"{BaseUrl}SpGetActiveJobs";
            public static readonly string GetCategoryName = $"{BaseUrl}GetCategoryName";
            public static readonly string SpGetActiveAds = $"{BaseUrl}SpGetActiveAds";
            public static readonly string SpGetInActiveAds = $"{BaseUrl}SpGetInActiveAds";
            public static readonly string GetPostAdImagesList = $"{BaseUrl}GetPostAdImagesList";
            public static readonly string GetSupplierAdVideoVM = $"{BaseUrl}GetSupplierAdVideoVM";
            public static readonly string GetProductCategoryDetailWeb = $"{BaseUrl}GetProductCategoryDetailWeb";
            public static readonly string GetAdBySubCategoryIdsWeb = $"{BaseUrl}GetAdBySubCategoryIdsWeb";
            public static readonly string GetAdBySearchWeb = $"{BaseUrl}GetAdBySearchWeb";
            public static readonly string MarketSimilarProductsVMs = $"{BaseUrl}MarketSimilarProductsVMs";
            public static readonly string GetSupplierAdsByProductCategoryId = $"{BaseUrl}GetSupplierAdsByProductCategoryId";
            public static readonly string AllSupplier = $"{BaseUrl}AllSupplier";
            public static readonly string GetAllAds = $"{BaseUrl}GetAllAds";
            public static readonly string GetAllSubcategory = $"{BaseUrl}GetAllSubcategory";
            public static readonly string SpGetSupplierList = $"{BaseUrl}SpGetSupplierList";
            public static readonly string SpGetHoomWorkSupplierList = $"{BaseUrl}SpGetHoomWorkSupplierList";
            public static readonly string SpGetLocalSupplierList = $"{BaseUrl}SpGetLocalSupplierList";
            public static readonly string SpGetSupplierStats = $"{BaseUrl}SpGetSupplierStats";
            public static readonly string GetAllSuppliers = $"{BaseUrl}GetAllSuppliers"; 
            public static readonly string GetAllProduCatcategoryGroup = $"{BaseUrl}GetAllProduCatcategoryGroup";
            public static readonly string GetOrderStatusList = $"{BaseUrl}GetOrderStatusList";
            public static readonly string GetAllSuppliersYearlyReport = $"{BaseUrl}GetAllSuppliersYearlyReport";
            public static readonly string GetAllSuppliersFromToReport = $"{BaseUrl}GetAllSuppliersFromToReport";
            public static readonly string SpGetActiveAdsWithImages = $"{BaseUrl}SpGetActiveAdsWithImages";
            public static readonly string SpGetInActiveAdsWithImages = $"{BaseUrl}SpGetInActiveAdsWithImages";
            public static readonly string GetCategoriesForDropDown = $"{BaseUrl}GetCategoriesForDropDown"; 
            public static readonly string GetSubCategoriesForDropDown = $"{BaseUrl}GetSubCategoriesForDropDown";
            public static readonly string GetSupplierForReport = $"{BaseUrl}GetSupplierForReport";
            public static readonly string UpdateSupplierPublicId = $"{BaseUrl}UpdateSupplierPublicId";
            public static readonly string GetSupplierLast24HourRegistred = $"{BaseUrl}GetSupplierLast24HourRegistred"; //
            public static readonly string GetPostedadsLastDay = $"{BaseUrl}GetPostedadsLastDay";
            public static readonly string GetPostedAdsForDynamicReport = $"{BaseUrl}GetPostedAdsForDynamicReport";
            public static readonly string GetSupplierAddressList = $"{BaseUrl}GetSupplierAddressList";
            public static readonly string GetSupplierAdsAddressList = $"{BaseUrl}GetSupplierAdsAddressList";
            public static readonly string BlockSupplier = $"{BaseUrl}BlockSupplier";
            public static readonly string SupplierByCategory = $"{BaseUrl}SupplierByCategory";
            public static readonly string CheckProductAvailability = $"{BaseUrl}CheckProductAvailability";
            public static readonly string AddNewProduct = $"{BaseUrl}AddNewProducts";
            public static readonly string AddUpdateProductsCategoryGroup = $"{BaseUrl}AddUpdateProductsCategoryGroup";
            public static readonly string AddNewSubProduct = $"{BaseUrl}AddNewSubProducts";
            public static readonly string AddUpdateNewVariant = $"{BaseUrl}AddUpdateNewVariant";
            public static readonly string FeaturedSupplier = $"{BaseUrl}FeaturedSupplier";
            public static readonly string GetBusinessDetailsStatus = $"{BaseUrl}GetBusinessDetailsStatus";
            public static readonly string GetMarkeetPlaceProducts = $"{BaseUrl}GetMarkeetPlaceProducts";
            public static readonly string GetMarkeetPlaceTopRatedProducts = $"{BaseUrl}GetMarkeetPlaceTopRatedProducts";
            public static readonly string GetMarkeetPlaceTopRatedProductsforWeb = $"{BaseUrl}GetMarkeetPlaceTopRatedProductsforWeb";
            public static readonly string GetSupplierAdDetails = $"{BaseUrl}GetSupplierAdDetails";
            public static readonly string DeleteSelectedSupplierAdId = $"{BaseUrl}DeleteSelectedSupplierAdId";
            public static readonly string AddLinkedSalesman = $"{BaseUrl}AddLinkedSalesman";
            public static readonly string UpdatePhoneNumberByUserId = $"{BaseUrl}UpdatePhoneNumberByUserId";
            public static readonly string GetTradesmanByName = $"{BaseUrl}GetTradesmanByName";
            public static readonly string GetMarketPlaceAds = $"{BaseUrl}GetMarketPlaceAds";
            public static readonly string AddUpdateProductAttribute = $"{BaseUrl}AddUpdateProductAttribute";
            public static readonly string AddUpdateSupplierSlider = $"{BaseUrl}AddUpdateSupplierSlider";
            public static readonly string AddUpdateProductCategoryAttribute = $"{BaseUrl}AddUpdateProductCategoryAttribute";
            public static readonly string GetProductAttributeList = $"{BaseUrl}GetProductAttributeList";
            public static readonly string GetSupplierSliderList = $"{BaseUrl}GetSupplierSliderList";
            public static readonly string GetSupplierProductList = $"{BaseUrl}GetSupplierProductList";
            public static readonly string GetSupplierProductDetail = $"{BaseUrl}GetSupplierProductDetail";
            public static readonly string GetProductDetailWeb = $"{BaseUrl}GetProductDetailWeb"; 
            public static readonly string GetProductDetailMob = $"{BaseUrl}GetProductDetailMob";
            public static readonly string GetProductCategoryAttributeList = $"{BaseUrl}GetProductCategoryAttributeList";
            public static readonly string GetProductAttributeListByCategoryId = $"{BaseUrl}GetProductAttributeListByCategoryId";
            public static readonly string GetProductCategoryGroupListById = $"{BaseUrl}GetProductCategoryGroupListById";
            public static readonly string AddNewSupplierProduct = $"{BaseUrl}AddNewSupplierProduct"; 
            public static readonly string GetTopFiveProductCategory = $"{BaseUrl}GetTopFiveProductCategory";
            public static readonly string GetSupplierWithDetails = $"{BaseUrl}GetSupplierWithDetails";
            public static readonly string AddAndUpdateSellerAccount = $"{BaseUrl}AddAndUpdateSellerAccount";
            public static readonly string AddAndUpdateBusinessAccount = $"{BaseUrl}AddAndUpdateBusinessAccount";
            public static readonly string UpdateSupplierProduct = $"{BaseUrl}UpdateSupplierProduct";
            public static readonly string GetHomeProductList = $"{BaseUrl}GetHomeProductList"; 
            public static readonly string GetSupplierProductListWeb = $"{BaseUrl}GetSupplierProductListWeb";
            public static readonly string GetProductsByCategory = $"{BaseUrl}GetProductsByCategory"; 
            public static readonly string GetProductsByName = $"{BaseUrl}GetProductsByName";
            public static readonly string GetProductCategories = $"{BaseUrl}GetProductCategories";
            public static readonly string GetCategoryGroupsById = $"{BaseUrl}GetCategoryGroupsById?subCategoryId=";
            public static readonly string GetCountryList = $"{BaseUrl}GetCountryList";
            public static readonly string GetProductSearchTagsList = $"{BaseUrl}GetProductSearchTagsList";
            public static readonly string GetSateList = $"{BaseUrl}GetSateList";
            public static readonly string GetAreaList = $"{BaseUrl}GetAreaList";
            public static readonly string GetLocationList = $"{BaseUrl}GetLocationList";
            public static readonly string GetBanksList = $"{BaseUrl}GetBanksList";
            public static readonly string SaveAndUpdateBankAccountData = $"{BaseUrl}SaveAndUpdateBankAccountData";
            
            public static readonly string GetBankAccountData = $"{BaseUrl}GetBankAccountData";
            
            public static readonly string SaveAndUpdateWhareHouseAddress = $"{BaseUrl}SaveAndUpdateWhareHouseAddress";
            public static readonly string GetWareHouseAddress = $"{BaseUrl}GetWareHouseAddress";
            public static readonly string SaveAndUpdateReturnAddress = $"{BaseUrl}SaveAndUpdateReturnAddress";
            public static readonly string SaveAndUpdateSocialLinks = $"{BaseUrl}SaveAndUpdateSocialLinks";
            public static readonly string GetReturnAddress = $"{BaseUrl}GetReturnAddress";
            public static readonly string GetSocialLinks = $"{BaseUrl}GetSocialLinks";
            public static readonly string GetOrdersList = $"{BaseUrl}GetOrdersList";
            public static readonly string GetSalesSummary = $"{BaseUrl}GetSalesSummary"; 
            public static readonly string PlaceOrder = $"{BaseUrl}PlaceOrder"; 
            public static readonly string GetOrderDetailsById = $"{BaseUrl}GetOrderDetailsById";
            public static readonly string UpdateOrderStatus = $"{BaseUrl}UpdateOrderStatus";
            public static readonly string GetSupplierProductById = $"{BaseUrl}GetSupplierProductById";
            public static readonly string GetVariantsByProductId = $"{BaseUrl}GetVariantsByProductId";
            public static readonly string GetCustomerOrdersList = $"{BaseUrl}GetCustomerOrdersList";
            public static readonly string GetCustomerOrderedProductsList = $"{BaseUrl}GetCustomerOrderedProductsList";
            public static readonly string GetProfile = $"{BaseUrl}GetProfile";
            public static readonly string GetSuppliersProductsListForApproval = $"{BaseUrl}GetSuppliersProductsListForApproval";
            public static readonly string ApproveSupplierProduct = $"{BaseUrl}ApproveSupplierProduct";
            public static readonly string UpdateSupplierAllGoodStatus = $"{BaseUrl}UpdateSupplierAllGoodStatus";
            public static readonly string GetSupplierProductImagesbyProductId = $"{BaseUrl}GetSupplierProductImagesbyProductId";
             public static readonly string GetShippingCost = $"{BaseUrl}GetShippingCost";
            public static readonly string GetShippingChargesList = $"{BaseUrl}GetShippingChargesList";
            public static readonly string AddUpdateShippingCost = $"{BaseUrl}AddUpdateShippingCost";
            public static readonly string CancelCustomerOrder = $"{BaseUrl}CancelCustomerOrder";
            public static readonly string OrderCancelByAdmin = $"{BaseUrl}OrderCancelByAdmin";
            public static readonly string GetCanellationReasonsListForAdmin = $"{BaseUrl}GetCanellationReasonsListForAdmin";
            public static readonly string GetCustomerCanclledOrdersList = $"{BaseUrl}GetCustomerCanclledOrdersList";
            public static readonly string AddSupplierLeadgerEntry = $"{BaseUrl}AddSupplierLeadgerEntry";
            public static readonly string GetOrderCancellationReasonsList = $"{BaseUrl}GetOrderCancellationReasonsList";
            public static readonly string InsertAndUpdateCancellationReason = $"{BaseUrl}InsertAndUpdateCancellationReason";
            public static readonly string GetOrderByOrderId = $"{BaseUrl}GetOrderByOrderId";

            public static readonly string GetSupplierProfileDetails = $"{BaseUrl}GetSupplierProfileDetails";

            public static readonly string GetCountryListForAdmin = $"{BaseUrl}GetCountryListForAdmin";
            public static readonly string GetProductsByTag = $"{BaseUrl}GetProductsByTag";



            public static readonly string AddUpdateCountry = $"{BaseUrl}AddUpdateCountry";
            public static readonly string deleteCountryStatus = $"{BaseUrl}deleteCountryStatus";
            public static readonly string GetStateListForAdmin = $"{BaseUrl}GetStateListForAdmin";
            public static readonly string AddUpdateState = $"{BaseUrl}AddUpdateState";
            public static readonly string deletestateStatus = $"{BaseUrl}deletestateStatus";
            public static readonly string GetBanksListForAdmin = $"{BaseUrl}GetBanksListForAdmin";
            public static readonly string AddUpdatebank = $"{BaseUrl}AddUpdatebank";
            public static readonly string deletebankStatus = $"{BaseUrl}deletebankStatus";
            public static readonly string GetAreaListForAdmin = $"{BaseUrl}GetAreaListForAdmin";
            public static readonly string deleteareaStatus = $"{BaseUrl}deleteareaStatus";
            public static readonly string saveAndUpdateArea = $"{BaseUrl}saveAndUpdateArea";
            public static readonly string GetLocationListForAdmin = $"{BaseUrl}GetLocationListForAdmin";
            public static readonly string AddUpdateLocation = $"{BaseUrl}AddUpdateLocation";
            public static readonly string deletelocationStatus = $"{BaseUrl}deletelocationStatus";
            public static readonly string GetLoggedSupplierCanelledOrdersList = $"{BaseUrl}GetLoggedSupplierCanelledOrdersList";
            public static readonly string GetSupplierShopUrl = $"{BaseUrl}GetSupplierShopUrl";
            public static readonly string GetSupplierShopDetails = $"{BaseUrl}GetSupplierShopDetails";
            public static readonly string AddUpdateFreeShipping = $"{BaseUrl}AddUpdateFreeShipping";
            public static readonly string GetFreeShippingList = $"{BaseUrl}GetFreeShippingList";
            public static readonly string DeleteFreeShipping = $"{BaseUrl}DeleteFreeShipping";
        
            public static readonly string GetProductsList = $"{BaseUrl}GetProductsList";
        

            public static readonly string GetOrderTracking = $"{BaseUrl}GetOrderTracking";
            public static readonly string GetOrderedItemTracking = $"{BaseUrl}GetOrderedItemTracking";
            public static readonly string GetCityList = $"{BaseUrl}GetCityList";

            public static readonly string GetSupplierOrderBySupplierId = $"{BaseUrl}GetSupplierOrderBySupplierId";
            public static readonly string deleteLinkStatus = $"{BaseUrl}deleteLinkStatus";
            public static readonly string GetAllCatSubCatGroupCategories = $"{BaseUrl}GetAllCatSubCatGroupCategories";
            public static readonly string SupplierList = $"{BaseUrl}SupplierList";
            public static readonly string AddUpdateSupplierCommission = $"{BaseUrl}AddUpdateSupplierCommission";
            public static readonly string GetSupplierCommissionList = $"{BaseUrl}GetSupplierCommissionList";
      




        }


        public static class Shipping
        {
            public static readonly string BaseUrl = "api/ShippingApi/";
            public static readonly string AddPickupAddres = $"{BaseUrl}AddPickupAddres";
        }

        public static class Shipment
        {
            public static readonly string BaseUrl = "https://sonic.pk/api/shipment/";
            public static readonly string TrackShipment = $"{BaseUrl}track";
            public static readonly string OrderTrackShipment = $"{BaseUrl}track/order_id";
        }

        public static class Tradesman
        {
            public static readonly string BaseUrl = "api/Tradesman/";
            //public static readonly string BaseUrl = "http://localhost:15775";
            //public static readonly string BaseUrl = "http://local.services.com/TradesmanApi";
            public static readonly string GetAllSkills = $"{BaseUrl}GetAllSkills";
            public static readonly string GetSkillTagsBySkill = $"{BaseUrl}GetSkillTagsBySkillId";
            public static readonly string GetSubSkillTagsById = $"{BaseUrl}GetSubSkillTagsById";
            public static readonly string GetMetaTags = $"{BaseUrl}GetCommonMetaTags";
            public static readonly string GetSkillListAdmin = $"{BaseUrl}GetSkillListAdmin";
            public static readonly string GetActiveBids = $"{BaseUrl}GetActiveBids";
            public static readonly string GetDeclinedBids = $"{BaseUrl}GetDeclinedBids";
            public static readonly string CheckFeedBackStatus = $"{BaseUrl}CheckFeedBackStatus";
            public static readonly string GetLAllTradesman = $"{BaseUrl}GetLAllTradesman";
            public static readonly string GetLAllActiveTradesman = $"{BaseUrl}GetLAllActiveTradesman";
            public static readonly string GetJobsDetail = $"{BaseUrl}GetJobsDetails";
            public static readonly string GetTradesmanSkillReport = $"{BaseUrl}GetLAllTradesmanbyCategoryReport";
            public static readonly string GetCompletedJob = $"{BaseUrl}GetCompletedJob";
            public static readonly string GetJobLeadsByTradesmanId = $"{BaseUrl}GetJobLeadsByTradesmanId";
            public static readonly string GetCallInfo = $"{BaseUrl}GetCallInfo";
            public static readonly string GetTradesmanSkillIds = $"{BaseUrl}GetTradesmanSkillIds";
            public static readonly string GetInvoiceMembership = $"{BaseUrl}GetInvoiceMembership";
            public static readonly string GetInvoiceJobReceipts = $"{BaseUrl}GetInvoiceJobReceipts";
            public static readonly string GetPersonalDetails = $"{BaseUrl}GetPersonalDetails";
            public static readonly string GetTradesmanSkills = $"{BaseUrl}GetTradesmanSkills";
            public static readonly string GetTradesmanSkillsByParentId = $"{BaseUrl}GetTradesmanSkillsByParentId";
            public static readonly string GetTradesmanSubSkills = $"{BaseUrl}GetTradesmanSubSkills";
            public static readonly string UpdatePersonalDetails = $"{BaseUrl}UpdatePersonalDetails";
            public static readonly string GetFeedBack = $"{BaseUrl}Job/GetFeedBack";
            public static readonly string SubmitBid = $"{BaseUrl}SubmitBid";
            public static readonly string SubmitBidsVoice = $"{BaseUrl}SubmitBidsVoice";
            public static readonly string GetJobBidImages = $"{BaseUrl}GetJobBidImages";
            public static readonly string GetTradesmanCallLogs = $"{BaseUrl}GetTradesmanCallLogs";
            public static readonly string DeleteCallLogs = $"{BaseUrl}DeleteCallLogs";
            public static readonly string GetTradesmanDetailsByTradesmanIds = $"{BaseUrl}GetTradesmanDetailsByTradesmanIds";
            public static readonly string GetTradesmanById = $"{BaseUrl}GetTradesmanById";
            public static readonly string GetSkillSetByTradesmanId = $"{BaseUrl}GetSkillSetByTradesmanId";
            public static readonly string GetTradesmanProfile = $"{BaseUrl}GetTradesmanProfile";
            public static readonly string GetSkillBySkillId = $"{BaseUrl}GetSkillBySkillId";
            public static readonly string GetSubSkillBySkillId = $"{BaseUrl}GetSubSkillBySkillId";
            public static readonly string GetSubSkillsBySkillId = $"{BaseUrl}GetSubSkillsBySkillId";
            public static readonly string GetSubSkillsWithSkill = $"{BaseUrl}GetSubSkillsWithSkill";



            public static readonly string GetSubSkillbySubSkillId = $"{BaseUrl}GetSubSkillbySubSkillId";
            public static readonly string GetTradesmanByTradesmanId = $"{BaseUrl}GetTradesmanByTradesmanId";
            public static readonly string GetLocalProfessionalImages = $"{BaseUrl}GetLocalProfessionalImages";
            public static readonly string RateTradesmanByJobQuotationId = $"{BaseUrl}RateTradesmanByJobQuotationId";
            public static readonly string GetTradmanDetails = $"{BaseUrl}GetTradmanDetails";
            public static readonly string GetSubSkillBySkillIds = $"{BaseUrl}GetSubSkillBySkillIds";
            public static readonly string GetSubSkillById = $"{BaseUrl}GetSubSkillById";
            public static readonly string AddEditTradesman = $"{BaseUrl}AddEditTradesman";
            public static readonly string DeleteTradesman = $"{BaseUrl}DeleteTradesman";
            public static readonly string SetTradesmanSkills = $"{BaseUrl}SetTradesmanSkills";
            public static readonly string GetEntityIdByUserId = $"{BaseUrl}GetEntityIdByUserId";
            public static readonly string GetSkillSetByTradesmanIds = $"{BaseUrl}GetSkillSetByTradesmanIds";
            public static readonly string GetSkillsByIds = $"{BaseUrl}GetSkillsByIds";
            public static readonly string GetBusinessDetail = $"{BaseUrl}GetBusinessDetail";
            public static readonly string GetTradesmanByUserId = $"{BaseUrl}GetTradesmanByUserId";
            public static readonly string GetSubSkill = $"{BaseUrl}GetSubSkill";
            public static readonly string GetSubSkillList = $"{BaseUrl}GetSubSkillList";
            public static readonly string GetTradesmanSkillName = $"{BaseUrl}GetTradesmanSkillName";
            public static readonly string GetTradesmenListBySkillIdAndCityId = $"{BaseUrl}GetTradesmenListBySkillIdAndCityId";

            public static readonly string SP_GetTradesmanWithSkillIdAndCityId = $"{BaseUrl}SP_GetTradesmanWithSkillIdAndCityId";
            public static readonly string SpGetTradesmanList = $"{BaseUrl}SpGetTradesmanList";
            public static readonly string SpGetTradesmanStats = $"{BaseUrl}SpGetTradesmanStats";
            public static readonly string SpGetBusinessDetails = $"{BaseUrl}SpGetBusinessDetails";
            public static readonly string GetLAllTradesmanFromToReport = $"{BaseUrl}GetLAllTradesmanFromToReport";
            public static readonly string GetLAllTradesmanYearlyReport = $"{BaseUrl}GetLAllTradesmanYearlyReport";
            public static readonly string GetTradesmanReport = $"{BaseUrl}GetTradesmanReport";
            public static readonly string GetTradesmanSkillsForDropdown = $"{BaseUrl}GetTradesmanSkillsForDropdown";
            public static readonly string GetSkillList = $"{BaseUrl}GetSkillList";
            public static readonly string getAllInActiveFromToReport = $"{BaseUrl}getAllInActiveFromToReport";
            public static readonly string GetLAllTradesmanbySkillTown = $"{BaseUrl}GetLAllTradesmanbySkillTown";
            public static readonly string UpdateTradesmanPublicId = $"{BaseUrl}UpdateTradesmanPublicId";
            public static readonly string GetLAllTradesmanLast24Hours = $"{BaseUrl}GetLAllTradesmanLast24Hours";
            public static readonly string GetTradesmanAddressList = $"{BaseUrl}GetTradesmanAddressList";
            public static readonly string BlockTradesMan = $"{BaseUrl}BlockTradesMan";
            public static readonly string CheckSkillAvailability = $"{BaseUrl}CheckSkillAvailability";
            public static readonly string CheckOrderAvailability = $"{BaseUrl}CheckOrderAvailability";
            public static readonly string AddNewSkill = $"{BaseUrl}AddNewSkill";
            public static readonly string AddOrUpdateSubSkill = $"{BaseUrl}AddOrUpdateSubSkill";
            public static readonly string UpdateSkill = $"{BaseUrl}UpdateSkill";
            public static readonly string DeleteSkill = $"{BaseUrl}DeleteSkill";
            public static readonly string TradesmanByCategory = $"{BaseUrl}TradesmanByCategory";
            public static readonly string GetInactiveUserReport = $"{BaseUrl}GetInactiveUserReport";
            public static readonly string GetBusinessDetailsStatus = $"{BaseUrl}GetBusinessDetailsStatus";
            public static readonly string AddLinkedSalesman = $"{BaseUrl}AddLinkedSalesman";
            public static readonly string GetTradesmanByName = $"{BaseUrl}GetTradesmanByName";
            public static readonly string UpdatePhoneNumberByUserId = $"{BaseUrl}UpdatePhoneNumberByUserId";
            public static readonly string BlockTradesman = $"{BaseUrl}BlockTradesman";
            public static readonly string GetTradesmanFirebaseIdListBySkillAndCity = $"{BaseUrl}GetTradesmanFirebaseIdListBySkillAndCity";


        }
        public static class UserManagement
        {
            public static readonly string BaseUrl = "api/UserManagement/";
            //public static readonly string BaseUrl = "http://localhost:15776";
            //public static readonly string BaseUrl = "http://local.services.com/UsemanagementApi";
            public static readonly string GetCityList = $"{BaseUrl}GetCityList";
            public static readonly string GetCitiesList = $"{BaseUrl}GetCitiesList";
            public static readonly string GetCityById = $"{BaseUrl}GetCityById";
            public static readonly string GetUserDetailsByUserRole = $"{BaseUrl}GetUserDetailsByUserRole";
            public static readonly string GetAllCities = $"{BaseUrl}GetAllCities";
            public static readonly string GetDistances = $"{BaseUrl}GetDistances";
            public static readonly string GetOtp = $"{BaseUrl}GetOtp";
            public static readonly string VerifyOtp = $"{BaseUrl}VerifyOtp";
            public static readonly string GetCityNameByCityId = $"{BaseUrl}GetCityNameByCityId";
            public static readonly string VerifyOtpAndRegisterUser = $"{BaseUrl}VerifyOtpAndRegisterUser";
            public static readonly string VerifyOtpAndRegisterUserMobile = $"{BaseUrl}VerifyOtpAndRegisterUserMobile";
            public static readonly string VerifyOtpAndGetPasswordResetToken = $"{BaseUrl}VerifyOtpAndGetPasswordResetToken";
            public static readonly string CreateRoleBasedEntity = $"{BaseUrl}CreateRoleBasedEntity";
            public static readonly string AddEditTradesmanWithSkills = $"{BaseUrl}AddEditTradesmanWithSkills";
            public static readonly string AddSupplierBusinessDetails = $"{BaseUrl}AddSupplierBusinessDetails";
            public static readonly string GetCityIdByName = $"{BaseUrl}GetCityIdByName";
            public static readonly string GetCityByName = $"{BaseUrl}GetCityByName";
            public static readonly string GetCityIdbyName = $"{BaseUrl}GetCityIdbyName";
            public static readonly string GetDistanceBuName = $"{BaseUrl}GetDistanceBuName";
            public static readonly string GetAllCitiesById = $"{BaseUrl}GetAllCitiesById";
            public static readonly string UpdatePersonalDetails = $"{BaseUrl}UpdatePersonalDetails";
            public static readonly string AdminForgotPasswordAuthentication = $"Admin{BaseUrl}AdminForgotPasswordAuthentication";
            public static readonly string CheckCityAvailability = $"{BaseUrl}CheckCityAvailability";
            public static readonly string AddNewCity = $"{BaseUrl}AddNewCity";
            public static readonly string AddUpdateTown = $"{BaseUrl}AddUpdateTown";
            public static readonly string UpdateCity = $"{BaseUrl}UpdateCity";
            public static readonly string DeleteCity = $"{BaseUrl}DeleteCity";
            public static readonly string GetStatesList = $"{BaseUrl}GetStatesList";
            public static readonly string InsertAndUpDateFAQs = $"{BaseUrl}InsertAndUpDateFAQs";
            public static readonly string GetFAQsList = $"{BaseUrl}GetFAQsList";
            public static readonly string InsertAndUpDateActivePromotion = $"{BaseUrl}InsertAndUpDateActivePromotion";
            public static readonly string AddUpdateToolTipForm = $"{BaseUrl}AddUpdateToolTipForm";
            public static readonly string AddUpdateToolTipFormDetails = $"{BaseUrl}AddUpdateToolTipFormDetails";
            public static readonly string GetPromotionList = $"{BaseUrl}GetPromotionList";
            public static readonly string GetPromotionListForWeb = $"{BaseUrl}GetPromotionListForWeb";
            public static readonly string GetCampaignsList = $"{BaseUrl}GetCampaignsList";
            public static readonly string GetCompaignsTypeList = $"{BaseUrl}GetCompaignsTypeList";
            public static readonly string GetPromotionListIdValue = $"{BaseUrl}GetPromotionListIdValue";
            public static readonly string GetToolTipFormsList = $"{BaseUrl}GetToolTipFormsList";
            public static readonly string GetToolTipFormsDetailsList = $"{BaseUrl}GetToolTipFormsDetailsList";
            public static readonly string GetSingleFormDetails = $"{BaseUrl}GetSingleFormDetails";
            public static readonly string GetAgreementsList = $"{BaseUrl}GetAgreementsList";
            public static readonly string GetCompaignsTypesListForAdmin = $"{BaseUrl}GetCompaignsTypesListForAdmin";
            public static readonly string GetAllSalesman = $"{BaseUrl}GetAllSalesman";
            public static readonly string LinkedSalesManList = $"{BaseUrl}LinkedSalesManList";
            public static readonly string InsertAndUpDateAgreement = $"{BaseUrl}InsertAndUpDateAgreement";
            public static readonly string AddAndUpdateCampaigns = $"{BaseUrl}AddAndUpdateCampaigns";
            public static readonly string AddSalesman = $"{BaseUrl}AddUpdateSalesman";
            public static readonly string UnlinkSalesman = $"{BaseUrl}UnlinkSalesman";
            public static readonly string GetTownsByCityId = $"{BaseUrl}GetTownsByCityId";
            public static readonly string GetAllTown = $"{BaseUrl}GetAllTown";
            public static readonly string GetActivePromotionList = $"{BaseUrl}GetActivePromotionList";
            public static readonly string AddAndUpdateApplicationSetting = $"{BaseUrl}AddAndUpdateApplicationSetting";
            public static readonly string GetSettingList = $"{BaseUrl}GetSettingList";
            public static readonly string AddAndUpdateApplicationSettingDetails = $"{BaseUrl}AddAndUpdateApplicationSettingDetails";
            public static readonly string GetSettingDetailsList = $"{BaseUrl}GetSettingDetailsList";
            public static readonly string GetPromotionImageById = $"{BaseUrl}GetPromotionImageById";
            public static readonly string CheckUserExist = $"{BaseUrl}CheckUserExist";
            public static readonly string GetCityListWithTraxCityId = $"{BaseUrl}GetCityListWithTraxCityId";
            public static readonly string SendMessage = $"{BaseUrl}SendMessage";
            public static readonly string AddUpdateState = $"{BaseUrl}AddUpdateState";
            public static readonly string GetStateListForAdmin = $"{BaseUrl}GetStateListForAdmin";
            public static readonly string deletestateStatus = $"{BaseUrl}deletestateStatus";
            public static readonly string GetSateList = $"{BaseUrl}GetSateList";
            public static readonly string GetCityListByReleventState = $"{BaseUrl}GetCityListByReleventState";
            public static readonly string GetTimonialsTypesListForAdmin = $"{BaseUrl}GetTimonialsTypesListForAdmin";
            public static readonly string AddUpdateTestinomials = $"{BaseUrl}AddUpdateTestinomials";
            public static readonly string GetTestimonialsListForAdmin = $"{BaseUrl}GetTestimonialsListForAdmin";
            public static readonly string DeleteTesimoaialsStatus = $"{BaseUrl}DeleteTesimoaialsStatus";




        }

        public static class Video
        {
            public static readonly string BaseUrl = "api/Video/";
            //public static readonly string BaseUrl = "http://localhost:15777";
            //public static readonly string BaseUrl = "http://local.services.com/VideoApi";
            public static readonly string GetAllVideo = $"{BaseUrl}GetAllVideo";
            public static readonly string GetByJobQuotationId = $"{BaseUrl}GetByJobQuotationId";
            public static readonly string GetSupplierAdVideoByAdId = $"{BaseUrl}GetSupplierAdVideoByAdId";
            public static readonly string AddVideo = $"{BaseUrl}AddVideo";
            public static readonly string GetAdVideo = $"{BaseUrl}GetAdVideo";
            public static readonly string DeleteJobQuotationVideo = $"{BaseUrl}DeleteJobQuotationVideo";
            public static readonly string DeleteAdVideo = $"{BaseUrl}DeleteAdVideo";
            public static readonly string SubmitAndUpdateAdVideo = $"{BaseUrl}SubmitAndUpdateAdVideo";
            public static readonly string GetSupplierAdVideoNameByAdId = $"{BaseUrl}GetSupplierAdVideoNameByAdId";
            public static readonly string AddJobVideo = $"{BaseUrl}AddJobVideo";
            public static readonly string UpdateJobVideoStatus = $"{BaseUrl}UpdateJobVideoStatus";

        }

        public static class Communication
        {
            public static readonly string BaseUrl = "api/Communication/";
            //public static readonly string BaseUrl = "http://localhost:15763";
            //public static readonly string BaseUrl = "http://local.services.com/CommunicationApi";
            public static readonly string SendSms = $"{BaseUrl}SendSms";
            public static readonly string SendRegisterUserSms = $"{BaseUrl}SendRegisterUserSms";
            public static readonly string SendEmail = $"{BaseUrl}SendEmail";
            public static readonly string SendEmailJobPost = $"{BaseUrl}SendEmailJobPost";
            public static readonly string SendWellcomeEmail = $"{BaseUrl}SendWellcomeEmail";
            public static readonly string SendWellcomeEmailTradesman = $"{BaseUrl}SendWellcomeEmailTradesman";
            public static readonly string SendOtpEmail = $"{BaseUrl}SendOtpEmail";
            public static readonly string TradesmanBidEmail = $"{BaseUrl}TradesmanBidEmail";
            public static readonly string SupplierWelcomeEmail = $"{BaseUrl}SupplierWelcomeEmail";
            public static readonly string SupplierHowItWorksEmail = $"{BaseUrl}SupplierHowItWorksEmail";
            public static readonly string PostAdEmail = $"{BaseUrl}PostAdEmail";
            public static readonly string NewJobPosted = $"{BaseUrl}NewJobPosted";
            public static readonly string SendContactEmail = $"{BaseUrl}SendContactEmail"; 
            public static readonly string SaveAndUpdateEmail = $"{BaseUrl}SaveAndUpdateEmail";
            public static readonly string SendFeedBackEmail = $"{BaseUrl}SendFeedBackEmail";
            public static readonly string PostRequestCallBack = $"{BaseUrl}PostRequestCallBack";
            public static readonly string NumberOfRetries = $"{BaseUrl}NumberOfRetries";
            public static readonly string AdminForgotPasswordAuthenticationEmail = $"{BaseUrl}AdminForgotPasswordAuthenticationEmail";
            public static readonly string SendCrashSMS = $"{BaseUrl}SendCrashSMS";
            public static readonly string SaveUpdateInappChat = $"{BaseUrl}SaveUpdateInappChat";
            public static readonly string SendLoginConfirmationEmail = $"{BaseUrl}SendLoginConfirmationEmail";
            public static readonly string RequestCallBacksList = $"{BaseUrl}RequestCallBacksList";
            public static readonly string DeleteRequestCaller = $"{BaseUrl}DeleteRequestCaller";
            public static readonly string SendTestEmail = $"{BaseUrl}SendTestEmail";
        }

        public static class Admin
        {
            public static readonly string BaseUrl = "http://localhost:2018";
            //public static readonly string BaseUrl = "http://local.services.com/AdminApi";
            public static readonly string GetEntityIdByUserId = $"{BaseUrl}Admin/GetEntityIdByUserId";
        }

        public static class StaticUrls
        {
            public static readonly string TermsAndConditions = "https://www.hoomwork.com/User/Agrements/UserAgreement";
            public static readonly string UserAppBlog = "https://www.hoomwork.com/HWUser/Home/Blogs";
            public static readonly string Faqs = "https://www.hoomwork.com/HWUser/Home/UserFAQs";
            public static readonly string AboutApp = "https://www.hoomwork.com/HWUser/AboutUs";
            public static readonly string PrivacyPolicy = "https://www.hoomwork.com/Common/PrivacyPolicy";
            public static readonly string HowItworks = "http://www.youtube.com/watch";
            public static readonly string UserAgreement = "https://www.hoomwork.com/User/Agrements/UserAgreement";
        }
        public static class SupplierStaticUrls
        {
            public static readonly string TermsAndConditions = "https://www.hoomwork.com/User/Agrements/SupplierAgreement";
            public static readonly string Faqs = "https://www.hoomwork.com/HWUser/Home/SupplierFAQs";
            public static readonly string AboutApp = "https://www.hoomwork.com/HWUser/AboutUs";
        }
        public static class TradesmanStaticUrls
        {
            public const string TermsAndConditions = "https://www.hoomwork.com/User/Agrements/TradesmanAgreement";
            public const string Faqs = "https://www.hoomwork.com/HWUser/Home/TradesmanFAQs";
            public const string AboutApp = "https://www.hoomwork.com/HWUser/AboutUs";
        }
    }
}

