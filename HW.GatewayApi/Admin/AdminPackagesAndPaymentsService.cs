using HW.Http;
using HW.PackagesAndPaymentsModels;
using HW.PackagesAndPaymentsViewModels;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.AdminServices
{
    public interface IAdminPackagesAndPaymentsService
    {
        Task<List<PromotionsTypes>> GetPromoTypesList();
        Task<List<PromotionsVM>> GetPromotionList(PromotionsVM promotionsVM);
        Task<Response> AddOrUpdatePromotions(PromotionsVM promotionsVM);
        Task<Response> DeletePromotion(int id);
        Task<List<PackagesVM>> GetAllPackages(PackagesFiltersVM model);
        Task<Response> AddPackages(Packages packages);
        Task<Response> AddNewPromotionType(PromotionsTypes model);
        Task<List<PromotionsTypes>> GetAllPromotiontype();
        Task<List<PackageTypeVM>> GetAllPackagetype();
        Task<Response> AddNewCouponTypes(CouponTypes model);
        Task<List<CouponTypes>> GetAllCoupontype();
        Task<Response> AddCoupon(Coupons model);
        Task<List<CouponVM>> GetCoupons(CouponVM model);
        Task<Response> AddPromotionOnPackages(PromotionOnPackages model);
        Task<List<PromotionOnPackagesCM>> GetAllPromotionOnPackages(PromotionOnPackagesCM model);


        Task<Response> AddNewOrder(OrderVm model);
        Task<Response> AddUpdatePackageTypes(PackageTypeVM model);
        Task<List<OrderVm>> GetAllOrders(OrderVm model);
        Task<List<GetPackgesByCategoryVM>> GetPackgesByCategory(PackgesTypeForUserRolesVM packgesTypeForUserRolesVM);
        Task<List<PackgesTypeVM>> GetPackgesTypeByRoleId(int userRoleId);
        Task<Response> AddOrderByPackageId(OrderByPackageIdVM orderByPackageIds);
        Task<Response> UpdatePckagesAndOrderStatus(long orderId);
        Task<List<ActiveOrders>> GetActiveOrdersList(string userId, int pageSize, int pageNumber);
        Task<List<ActiveOrders>> GetExpiredOrdersList(string userId, int pageSize, int pageNumber);
        Task<List<PromotionsTypeVM>> GetPromotionTypes();
        Task<List<Voucher>> GetVoucherList();
        Task<Response> AddUpdateVoucherCategory(VoucherCategoryVM categoryVM);
        Task<List<VoucherCategoryVM>> GetvoucherCategoryList();
        Task<Response> DeleteVoucherCategory(int voucherCategoryId);
        Task<List<VoucherTypeVM>> GetVoucherTypeList();
        Task<Response> AddUpdateVoucherType(VoucherTypeVM voucherType);
        Task<Response> DeleteVoucherType(int voucherTypeId);
        Task<List<VoucherBookVM>> GetVoucherBookList();
        Task<Response> DeleteVoucherBook(int voucherBookId);
        Task<Response> AddUpdateVoucherBook(VoucherBookVM voucherBook);
        Task<List<VoucherBookAllocationVM>> GetVouchrBookAllocation();
        Task<Response> DeleteVoucherBookAllocation(int voucherBookAllocationId);
        Task<Response> AddUpdateVoucherBookAllocation(VoucherBookAllocationVM voucherBookAllocation);
        Task<List<VoucherBookPagesVM>> GetVoucherBookPagesList();
        Task<List<VoucherBookLeavesVM>> GetVoucherBookLeavesList(VoucherBookLeavesVM voucherBookLeavesVM);
        Task<List<CategoryCommissionSetupVM>> GetCategoryCommissionSetupList();
        Task<Response> AddUpdateCategoryCommissionSetup(CategoryCommissionSetupVM categoryCommissionSetup);
        Task<List<CategoryCommissionSetupVM>> GetOverrideTradesmanCommissionList();
        Task<Response> AddAndUpdateOverrideTradesmanCommission(CategoryCommissionSetupVM categoryCommissionSetup);
        Task<Response> DeleteCategoryCommissionSetup(int categoryCommissionId);
        Task<Response> AddAndUpdateAccountType(AccountsVM accountsVM);
        Task<List<AccountsVM>> GetAccountTypeList();
        Task<Response> getSubAccountRecord(long id);
        Task<Response> AddAndUpdateAccount(AccountsVM accountsVM);
        Task<List<AccountsVM>> GetAccountList();
        Task<Response> AddAndUpdateSubAccount(AccountsVM accountsVM);
        Task<List<AccountsVM>> GetSubAccountList(SearchSubAccountVM accountsVM);
        Task<Response> AddAndUpdateReferral(ReferralVM referralVM);
        Task<List<ReferralVM>> GetReferralList();
        Task<List<PaymentWithdrawalRequestVM>> GetWithdrawalRequestList(int paymentStatus, long tradesmanId, string phoneNumber, string role);
        Task<Response> UpdateWithdrawalRequestStatus(int withdrawalRequestId, string id, decimal amount, long tradesmanId,string role);
        Task<Response> DeclineWithdrawRequest(int withdrawalRequestId, string id);
        Task<List<GetLeadgerReportVM>> GetLeaderReport(GetLeadgerReportVM getLeadgerReportVM);
        Task<List<GeneralLedgerTransactionVM>> GetLedgerTransectionReportByAccountRef(GeneralLedgerTransactionVM generalLedgerTransaction);

        Task<Response> AddAndUpdateGeneralLedgerTransection(GeneralLedgerTransactionVM generalLedgerTransaction);

        Task<List<GeneralLedgerTransactionVM>> GetDetailedLedgerTransectionReportByAccountRef(GeneralLedgerTransactionVM generalLedgerTransaction);

        Task<Response> GetUserTransectionReport(string data);
        Task<Response> GetAccountUsersName(int accountType);
        Task<string> AddNewPromotionTypeForSupplier(string data);
        Task<string> GetPromotionTypeByIdForSupplier(int id);
        Task<string> GetAllPromotionTypesForSupplier(PromotionsVM promotionsVM);
        Task<string> GetPromotionTypesListForSupplier(long? supplierId);

        Task<string> GetAllPromotionsForSupplier(PromotionsVM promotionsVM);
        Task<string> AddPromotionForSupplier(string data);
        Task<string> GetPromotionByIdForSupplier(int id);
        Task<string> GetPromotionsBySuplierId(long supplierId);
        Task<string> DeletePromotionForSupplier(int id);
        Task<Response> UpdateShippingChargesAndPaymentStatus(string data);
        Task<Response> GetChartOfAccount(string data);
        Task<string> GetChartOfAccounts();
        Task<Response> GetSubAccountsLastLevel();
        Task<string> GetDetailedGLReport(string data);
    }
    public class AdminPackagesAndPaymentsService : IAdminPackagesAndPaymentsService
    {
        private readonly IHttpClientService httpClient;
        private readonly IExceptionService exc;
        private readonly ApiConfig apiConfig;

        public AdminPackagesAndPaymentsService(IHttpClientService httpClientService, IExceptionService exceptionService, ApiConfig apiConfig)
        {
            this.apiConfig = apiConfig;
            exc = exceptionService;
            httpClient = httpClientService;
        }


        public async Task<Response> AddPackages(Packages packages)
        {
            Response res = new Response();
            try
            {
                var promoJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddNewPackages}", packages);
                return JsonConvert.DeserializeObject<Response>(promoJson);
                //return res;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return res;
            }
        }
        public async Task<Response> getSubAccountRecord(long id)
        {
          Response response = new Response();
          var jsonResponse = await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.getSubAccountRecord}?id={id}");
          response = JsonConvert.DeserializeObject<Response>(jsonResponse);
          return response;
        }
        public async Task<Response> AddNewPromotionType(PromotionsTypes promotionsTypes)
        {
            Response res = new Response();
            try
            {
                var promoJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddNewPromotionType}", promotionsTypes);
                return JsonConvert.DeserializeObject<Response>(promoJson);
                //return res;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return res;
            }
        }
        public async Task<Response> AddNewCouponTypes(CouponTypes couponTypes)
        {
            Response res = new Response();
            try
            {
                var promoJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddCouponType}", couponTypes);
                return JsonConvert.DeserializeObject<Response>(promoJson);
                //return res;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return res;
            }
        }

        public async Task<List<PackagesVM>> GetAllPackages(PackagesFiltersVM model)
        {
            List<PackagesVM> res = new List<PackagesVM>();
            try
            {
                var jobsJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAllPackage}", model);
                return JsonConvert.DeserializeObject<List<PackagesVM>>(jobsJson);
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<PackagesVM>();
            }
        }
        public async Task<List<PromotionsTypes>> GetPromoTypesList()
        {
            try
            {
                var promoJson = await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPromoTypesList}");
                return JsonConvert.DeserializeObject<List<PromotionsTypes>>(promoJson);
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return null;
            }
        }
        public async Task<List<PromotionsVM>> GetPromotionList(PromotionsVM promotionsVM)
        {
            var promoJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPromotionList}", promotionsVM);
            return JsonConvert.DeserializeObject<List<PromotionsVM>>(promoJson);
        }
        public async Task<Response> AddOrUpdatePromotions(PromotionsVM promotionsVM)
        {
            string response = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddOrUpdatePromotions}", promotionsVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> DeletePromotion(int id)
        {
            string response = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.DeletePromotion}", id);
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<List<PromotionsTypes>> GetAllPromotiontype()
        {
            try
            {
                var promoJson = await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAllPromotionTypesList}");
                return JsonConvert.DeserializeObject<List<PromotionsTypes>>(promoJson);
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return null;
            }
        }

        public async Task<List<PackageTypeVM>> GetAllPackagetype()
        {
            try
            {
                var promoJson = await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAllPackagetype}");
                return JsonConvert.DeserializeObject<List<PackageTypeVM>>(promoJson);
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return null;
            }
        }

        public async Task<List<CouponTypes>> GetAllCoupontype()
        {
            try
            {
                var promoJson = await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAllCouponTypesList}");
                return JsonConvert.DeserializeObject<List<CouponTypes>>(promoJson);
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return null;
            }
        }
        public async Task<Response> AddCoupon(Coupons coupon)
        {
            Response res = new Response();
            try
            {
                var promoJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddCoupons}", coupon);
                return JsonConvert.DeserializeObject<Response>(promoJson);
                //return res;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return res;
            }
        }
        public async Task<Response> AddPromotionOnPackages(PromotionOnPackages promotionOnPackages)
        {
            Response res = new Response();
            try
            {
                var promoJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddPromotionOnPackage}", promotionOnPackages);
                return JsonConvert.DeserializeObject<Response>(promoJson);
                //return res;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return res;
            }
        }
        public async Task<List<CouponVM>> GetCoupons(CouponVM coupon)
        {
            List<CouponVM> res = new List<CouponVM>();
            try
            {
                var promoJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAllCoupons}", coupon);
                return JsonConvert.DeserializeObject<List<CouponVM>>(promoJson);
                //return res;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<CouponVM>();
            }
        }
        public async Task<List<PromotionOnPackagesCM>> GetAllPromotionOnPackages(PromotionOnPackagesCM coupon)
        {
            List<PromotionOnPackagesCM> res = new List<PromotionOnPackagesCM>();
            try
            {
                var promoJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAllPromotionOnPackages}", coupon);
                return JsonConvert.DeserializeObject<List<PromotionOnPackagesCM>>(promoJson);
                //return res;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<PromotionOnPackagesCM>();
            }
        }


        public async Task<Response> AddNewOrder(OrderVm orders)
        {
            Response res = new Response();
            try
            {
                var promoJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddNewOrder}", orders);
                return JsonConvert.DeserializeObject<Response>(promoJson);
                //return res;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return res;
            }
        }
        public async Task<List<OrderVm>> GetAllOrders(OrderVm orderVm)
        {
            List<OrderVm> res = new List<OrderVm>();
            try
            {
                var promoJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetOrder}", orderVm);
                return JsonConvert.DeserializeObject<List<OrderVm>>(promoJson);
                //return res;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<OrderVm>();
            }
        }
        public async Task<Response> AddUpdatePackageTypes(PackageTypeVM model)
        {
            Response res = new Response();
            try
            {
                var promoJson = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddUpdatePackageTypes}", model);
                return JsonConvert.DeserializeObject<Response>(promoJson);
                //return res;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return res;
            }
        }

        public async Task<List<GetPackgesByCategoryVM>> GetPackgesByCategory([FromBody] PackgesTypeForUserRolesVM packgesTypeForUserRolesVM)
        {
            try
            {
                List<GetPackgesByCategoryVM> getPackgesByCategory = new List<GetPackgesByCategoryVM>();
                var packages = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPackgesByCategory}", packgesTypeForUserRolesVM);
                getPackgesByCategory = JsonConvert.DeserializeObject<List<GetPackgesByCategoryVM>>(packages);
                return getPackgesByCategory;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<GetPackgesByCategoryVM>();
            }
        }


        public async Task<List<PackgesTypeVM>> GetPackgesTypeByRoleId(int userRoleId)
        {
            try
            {
                List<PackgesTypeVM> getPackgestype = new List<PackgesTypeVM>();
                var packages = await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPackgesTypeByRoleId}?userRoleId={userRoleId}");
                getPackgestype = JsonConvert.DeserializeObject<List<PackgesTypeVM>>(packages);
                return getPackgestype;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<PackgesTypeVM>();
            }
        }

        public async Task<Response> AddOrderByPackageId([FromBody]OrderByPackageIdVM orderByPackageIds)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddOrderByPackageId}", orderByPackageIds));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> UpdatePckagesAndOrderStatus(long orderId)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.UpdatePckagesAndOrderStatus}?orderId={orderId}"));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<List<ActiveOrders>> GetActiveOrdersList(string userId, int pageSize, int pageNumber)
        {
            return JsonConvert.DeserializeObject<List<ActiveOrders>>
                (await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetActiveOrdersList}?userId={userId}&pageSize={pageSize}&pageNumber={pageNumber}", ""));
        }
        public async Task<List<ActiveOrders>> GetExpiredOrdersList(string userId, int pageSize, int pageNumber)
        {
            return JsonConvert.DeserializeObject<List<ActiveOrders>>
                (await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetExpiredOrdersList}?userId={userId}&pageSize={pageSize}&pageNumber={pageNumber}", ""));
        }

        public async Task<List<PromotionsTypeVM>> GetPromotionTypes()
        {
            List<PromotionsTypeVM> getPromotionsType = new List<PromotionsTypeVM>();
            var getPromotions = await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPromotionTypes}");
            return getPromotionsType = JsonConvert.DeserializeObject<List<PromotionsTypeVM>>(getPromotions);

        }
        public async Task<List<Voucher>> GetVoucherList()
        {
            List<Voucher> getVoucher = new List<Voucher>();
            var getVouchersList = await httpClient.GetAsync($"{apiConfig.PromotionApiUrl}{ApiRoutes.PackagesAndPayments.GetVoucherList}");
            return getVoucher = JsonConvert.DeserializeObject<List<Voucher>>(getVouchersList);

        }

        public async Task<Response> AddUpdateVoucherCategory(VoucherCategoryVM categoryVM)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddUpdateVoucherCategory}", categoryVM));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<List<VoucherCategoryVM>> GetvoucherCategoryList()
        {
            try
            {
                List<VoucherCategoryVM> voucherCategory = new List<VoucherCategoryVM>();
                voucherCategory = JsonConvert.DeserializeObject<List<VoucherCategoryVM>>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetvoucherCategoryList}"));
                return voucherCategory;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<VoucherCategoryVM>();
            }
        }

        public async Task<Response> DeleteVoucherCategory(int voucherCategoryId)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.DeleteVoucherCategory}?voucherCategoryId={voucherCategoryId}"));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<List<VoucherTypeVM>> GetVoucherTypeList()
        {
            try
            {
               return JsonConvert.DeserializeObject<List<VoucherTypeVM>>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetVoucherTypeList}"));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<VoucherTypeVM>();
            }
        }

        public async Task<Response> AddUpdateVoucherType(VoucherTypeVM voucherType)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddUpdateVoucherType}", voucherType));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> DeleteVoucherType(int voucherTypeId)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.DeleteVoucherType}?voucherTypeId={voucherTypeId}"));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<List<VoucherBookVM>> GetVoucherBookList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<VoucherBookVM>>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetVoucherBookList}"));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<VoucherBookVM>();
            }
        }

        public async Task<Response> DeleteVoucherBook(int voucherBookId)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.DeleteVoucherBook}?voucherBookId={voucherBookId}"));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> AddUpdateVoucherBook(VoucherBookVM voucherBook)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddUpdateVoucherBook}", voucherBook));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<List<VoucherBookAllocationVM>> GetVouchrBookAllocation()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<VoucherBookAllocationVM>>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetVouchrBookAllocation}"));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<VoucherBookAllocationVM>();
            }
        }

        public async Task<Response> DeleteVoucherBookAllocation(int voucherBookAllocationId)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.DeleteVoucherBookAllocation}?voucherBookAllocationId={voucherBookAllocationId}"));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> AddUpdateVoucherBookAllocation(VoucherBookAllocationVM voucherBookAllocation)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddUpdateVoucherBookAllocation}", voucherBookAllocation));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<List<VoucherBookPagesVM>> GetVoucherBookPagesList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<VoucherBookPagesVM>>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetVoucherBookPagesList}"));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<VoucherBookPagesVM>();
            }
        }

        public async Task<List<VoucherBookLeavesVM>> GetVoucherBookLeavesList(VoucherBookLeavesVM voucherBookLeavesVM)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<VoucherBookLeavesVM>>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetVoucherBookLeavesList}",voucherBookLeavesVM));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<VoucherBookLeavesVM>();
            }
        }

        public async Task<List<CategoryCommissionSetupVM>> GetCategoryCommissionSetupList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<CategoryCommissionSetupVM>>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetCategoryCommissionSetupList}"));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<CategoryCommissionSetupVM>();
            }
        }
               

        public async Task<Response> AddUpdateCategoryCommissionSetup(CategoryCommissionSetupVM categoryCommissionSetup)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddUpdateCategoryCommissionSetup}", categoryCommissionSetup));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }

        }        
        public async Task<List<CategoryCommissionSetupVM>> GetOverrideTradesmanCommissionList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<CategoryCommissionSetupVM>>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetOverrideTradesmanCommissionList}"));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<CategoryCommissionSetupVM>();
            }
        }
               

        public async Task<Response> AddAndUpdateOverrideTradesmanCommission(CategoryCommissionSetupVM categoryCommissionSetup)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddAndUpdateOverrideTradesmanCommission}", categoryCommissionSetup));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }

        }        
        public async Task<Response> DeleteCategoryCommissionSetup(int categoryCommissionId)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.DeleteCategoryCommissionSetup}?categoryCommissionId={categoryCommissionId}"));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<Response> AddAndUpdateAccountType(AccountsVM accountsVM)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddAndUpdateAccountType}", accountsVM));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }        
        public async Task<List<AccountsVM>> GetAccountTypeList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<AccountsVM>>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAccountTypeList}"));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<AccountsVM>();
            }
        }
        public async Task<Response> AddAndUpdateAccount(AccountsVM accountsVM)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddAndUpdateAccount}", accountsVM));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<List<AccountsVM>> GetAccountList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<AccountsVM>>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAccountList}"));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<AccountsVM>();
            }
        }        
        public async Task<Response> AddAndUpdateSubAccount(AccountsVM accountsVM)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddAndUpdateSubAccount}", accountsVM));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<List<AccountsVM>> GetSubAccountList(SearchSubAccountVM accountsVM)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<AccountsVM>>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccountList}",accountsVM));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<AccountsVM>();
            }
        }        
        public async Task<Response> AddAndUpdateReferral(ReferralVM referralVM)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddAndUpdateReferral}", referralVM));
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<List<ReferralVM>> GetReferralList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<ReferralVM>>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetReferralList}"));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<ReferralVM>();
            }
        }

        public async Task<List<PaymentWithdrawalRequestVM>> GetWithdrawalRequestList(int paymentStatus, long tradesmanId, string phoneNumber, string role)
        {
            try
            {
                return  JsonConvert.DeserializeObject<List<PaymentWithdrawalRequestVM>>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetWithdrawalRequestList}?paymentStatus={paymentStatus}&tradesmanId={tradesmanId}&phoneNumber={phoneNumber}&role={role}"));
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<PaymentWithdrawalRequestVM>();
            }
        }

        public async Task<Response> UpdateWithdrawalRequestStatus(int withdrawalRequestId, string id, decimal amount, long tradesmanId,string role)
        {
            try
            {
                Response response = new Response();
                response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.UpdateWithdrawalRequestStatus}?withdrawalRequestId={withdrawalRequestId}&id={id}"));
                if(response.Status == ResponseStatus.OK)
                {

                    ///////Tradesman WithDrawal Credit Request///////////
                    
                    SubAccount subAccount = new SubAccount();
                    if (role== Utility.UserRoles.Tradesman)
                    {
                    subAccount = JsonConvert.DeserializeObject<HW.PackagesAndPaymentsModels.SubAccount>(
                        await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccountByTradesmanId}?tradesmanId={tradesmanId}&role={role}", ""));
                    }
                    else if (role == Utility.UserRoles.Customer)
                    {
                         subAccount = JsonConvert.DeserializeObject<SubAccount>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccount}?customerId={tradesmanId}", ""));

                    }
                    else if (role == Utility.UserRoles.Supplier)
                    {
                        subAccount = JsonConvert.DeserializeObject<HW.PackagesAndPaymentsModels.SubAccount>(
                            await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccountByTradesmanId}?tradesmanId={tradesmanId}&role={role}", ""));
                    }

                    PackagesAndPaymentsModels.LeadgerTransection withdrawalTransactionTradesman = new PackagesAndPaymentsModels.LeadgerTransection()
                    {
                        AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountPayables),
                        SubAccountId = subAccount.SubAccountId,
                        Debit = amount,
                        Credit = 0,
                        Active = true,
                        RefTradesmanSubAccountId = subAccount?.TradesmanId??0 ,
                        ReffrenceDocumentType = "WithDraw",
                        CreatedOn = DateTime.Now,
                        CreatedBy = id
                    };
                    if (role==Utility.UserRoles.Customer)
                    {
                        withdrawalTransactionTradesman.RefTradesmanSubAccountId = 0;
                        withdrawalTransactionTradesman.RefCustomerSubAccountId = subAccount.CustomerId;
                    }
                    response = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", withdrawalTransactionTradesman)
                    );

                    ////////// Hoomwork Debit Request //////////

                    subAccount = JsonConvert.DeserializeObject<SubAccount>(
                        await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetHoomWorkJZSubAccount}", "")
                    );

                    LeadgerTransection leadgerTransctn = new LeadgerTransection()
                    {
                        AccountId = Convert.ToInt64(HW.Utility.AccountType.Assets),
                        SubAccountId = subAccount.SubAccountId,
                        Debit = 0,
                        Credit = amount,
                        Active = true,
                        ReffrenceDocumentType = "WithDraw",
                        CreatedOn = DateTime.Now,
                        CreatedBy = id
                    };

                    response = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransctn)
                    );
                }
                
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<Response> DeclineWithdrawRequest(int withdrawalRequestId, string id)
        {
            try
            {
                Response response = new Response();
                response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.DeclineWithdrawRequest}?withdrawalRequestId={withdrawalRequestId}&id={id}"));                
                return response;
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<List<GetLeadgerReportVM>> GetLeaderReport(GetLeadgerReportVM getLeadgerReportVM)
        {
           return JsonConvert.DeserializeObject<List<GetLeadgerReportVM>>(
                     await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetLeaderReport}", getLeadgerReportVM)
                 );
        }
        
        public async Task<List<GeneralLedgerTransactionVM>> GetLedgerTransectionReportByAccountRef(GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<GeneralLedgerTransactionVM>>
              (await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetLedgerTransectionReportByAccountRef}", generalLedgerTransaction));
            }

            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<GeneralLedgerTransactionVM>();
            }
          
        }

        public async Task<Response> AddAndUpdateGeneralLedgerTransection(GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            Response response = new Response();
            try
            {
                string result = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddAndUpdateGeneralLedgerTransection}", generalLedgerTransaction);
                return JsonConvert.DeserializeObject<Response>(result);
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return response;
            }
        }

        public async Task<List<GeneralLedgerTransactionVM>> GetDetailedLedgerTransectionReportByAccountRef(GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<GeneralLedgerTransactionVM>>
              (await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetDetailedLedgerTransectionReportByAccountRef}", generalLedgerTransaction));
            }

            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return new List<GeneralLedgerTransactionVM>();
            }

        }

        public async Task<string> AddNewPromotionTypeForSupplier(string data)
        {
            return await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddNewPromotionTypeForSupplier}", data);
        }
        public async Task<string> GetPromotionTypeByIdForSupplier(int id)
        {
            return await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPromotionTypeByIdForSupplier}{id}");
        }
        public async Task<string> GetAllPromotionTypesForSupplier(PromotionsVM promotionsVM)
        {
            return await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAllPromotionTypesForSupplier}", promotionsVM);
        }
        public async Task<string> GetPromotionTypesListForSupplier(long? supplierId)
        {
            return await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPromotionTypesListForSupplier}?supplierId={supplierId}");
        }
        public async Task<string> GetAllPromotionsForSupplier(PromotionsVM promotionsVM)
        {
            return await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAllPromotionsForSupplier}", promotionsVM);
        }
        
        public async Task<string> AddPromotionForSupplier(string data)
        {
            return await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddPromotionForSupplier}", data);
        }        
        public async Task<string> GetPromotionByIdForSupplier(int id)
        {
            return await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPromotionByIdForSupplier}{id}");
        }        
        public async Task<string> GetPromotionsBySuplierId(long supplierId)
        {
            return await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPromotionsBySuplierId}?supplierId={supplierId}");
        }
        public async Task<string> DeletePromotionForSupplier(int id)
        {
            return await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.DeletePromotionForSupplier}{id}");
        }

        public async Task<Response> GetUserTransectionReport(string data)
        {
            Response response = new Response();
            try
            {
                var result = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetUserTransectionReport}", data);
                return JsonConvert.DeserializeObject<Response>(result);
            }
            catch (Exception ex)
            {
                exc.AddErrorLog(ex);
                return response;
            }

           }

        public async Task<Response> GetAccountUsersName(int accountType)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAccountUsersName}?accountType={accountType}"));
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
        public async Task<Response> UpdateShippingChargesAndPaymentStatus(string data)
        {
          string shippingData=await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.UpdateShippingChargesAndPaymentStatus}", data);
            return  JsonConvert.DeserializeObject<Response>(shippingData);
        }

        public async Task<Response> GetChartOfAccount(string data)
        {
            Response response = new Response();
            try
            {
                var result = await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetChartOfAccount}", data);
                return JsonConvert.DeserializeObject<Response>(result);
            }
            catch (Exception ex)
            {
                return response;
            }
        }
        public async Task<string> GetChartOfAccounts()
        {
            return await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetChartOfAccounts}");
        }

        public async Task<Response> GetSubAccountsLastLevel()
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccountsLastLevel}"));
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
        public async Task<string> GetDetailedGLReport(string data)
        {
            //return await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetDetailedGLReport}", data);
            return await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{string.Format(ApiRoutes.PackagesAndPayments.GetDetailedGLReport)}", data);
        }
    }
    }
