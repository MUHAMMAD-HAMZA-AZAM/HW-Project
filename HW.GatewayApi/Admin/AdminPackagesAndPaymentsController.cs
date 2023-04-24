using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.GatewayApi.Admin;
using HW.GatewayApi.AdminServices;
using HW.GatewayApi.AuthO;
using HW.GatewayApi.Controllers;
using HW.GatewayApi.Services;
using HW.PackagesAndPaymentsModels;
using HW.PackagesAndPaymentsViewModels;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;

namespace HW.GatewayApi.AdminService
{
    [Produces("application/json")]
    public class AdminPackagesAndPaymentsController : BaseController
    {
        private readonly IAdminPackagesAndPaymentsService adminPackagesAndPaymentsService;
        public AdminPackagesAndPaymentsController(IAdminPackagesAndPaymentsService adminPackagesAndPaymentsService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.adminPackagesAndPaymentsService = adminPackagesAndPaymentsService;
        }
        [HttpGet]
        public async Task<List<PromotionsTypes>> GetPromoTypesList()
        {
            return await adminPackagesAndPaymentsService.GetPromoTypesList();
        }

        [HttpPost]
        public async Task<List<PromotionsVM>> GetPromotionList([FromBody] PromotionsVM promotionsVM)
        {
            return await adminPackagesAndPaymentsService.GetPromotionList(promotionsVM);
        }
        [HttpPost]
        public async Task<Response> AddOrUpdatePromotions([FromBody] PromotionsVM promotionsVM)
        {
            return await adminPackagesAndPaymentsService.AddOrUpdatePromotions(promotionsVM);
        }
        [HttpPost]
        public async Task<Response> DeletePromotion([FromBody] int id)
        {
            return await adminPackagesAndPaymentsService.DeletePromotion(id);
        }
        //[HttpGet]
        //public async Task<List<PromotionsTypes>> GetPromoTypesList()
        //{
        //    return await adminPackagesAndPaymentsService.GetPromoTypesList();
        //}
        [HttpPost]
        public async Task<Response> AddPackages([FromBody] Packages packages)
        {
            return await adminPackagesAndPaymentsService.AddPackages(packages);
        }
        [HttpPost]
        public async Task<List<PackagesVM>> GetAllPackages([FromBody] PackagesFiltersVM model)
        {
            return await adminPackagesAndPaymentsService.GetAllPackages(model);
        }
        [HttpPost]
        public async Task<Response> AddNewPromotionType([FromBody] PromotionsTypes model)
        {
            return await adminPackagesAndPaymentsService.AddNewPromotionType(model);
        }

        [HttpGet]
        public async Task<List<PromotionsTypes>> GetAllPromotiontype()
        {
            return await adminPackagesAndPaymentsService.GetAllPromotiontype();
        }
        [HttpGet]

        public async Task<List<PackageTypeVM>> GetAllPackagetype()
        {
            return await adminPackagesAndPaymentsService.GetAllPackagetype();
        }

        [HttpPost]
        public async Task<Response> AddNewCouponTypes([FromBody] CouponTypes model)
        {
            return await adminPackagesAndPaymentsService.AddNewCouponTypes(model);
        }
        [HttpGet]
        public async Task<List<CouponTypes>> GetAllCoupontype()
        {
            return await adminPackagesAndPaymentsService.GetAllCoupontype();
        }
        [HttpPost]
        public async Task<Response> AddCoupons([FromBody] Coupons model)
        {
            return await adminPackagesAndPaymentsService.AddCoupon(model);
        }

        [HttpPost]
        public async Task<List<CouponVM>> GetCoupons([FromBody] CouponVM model)
        {
            return await adminPackagesAndPaymentsService.GetCoupons(model);
        }
        [HttpPost]
        public async Task<Response> AddPromotionOnPackages([FromBody] PromotionOnPackages model)
        {
            return await adminPackagesAndPaymentsService.AddPromotionOnPackages(model);
        }
        [HttpPost]
        public async Task<List<PromotionOnPackagesCM>> GetAllPromotionOnPackages([FromBody] PromotionOnPackagesCM model)
        {
            return await adminPackagesAndPaymentsService.GetAllPromotionOnPackages(model);
        }
        [HttpPost]
        public async Task<Response> AddNewOrder([FromBody] OrderVm model)
        {
            return await adminPackagesAndPaymentsService.AddNewOrder(model);
        }
        [HttpPost]
        public async Task<List<OrderVm>> GetAllOrders([FromBody] OrderVm model)
        {
            return await adminPackagesAndPaymentsService.GetAllOrders(model);
        }
        [HttpPost]
        public async Task<Response> AddUpdatePackageTypes([FromBody] PackageTypeVM model)
        {
            return await adminPackagesAndPaymentsService.AddUpdatePackageTypes(model);
        }

        [HttpPost]
        public Task<List<GetPackgesByCategoryVM>> GetPackgesByCategory([FromBody] PackgesTypeForUserRolesVM packgesTypeForUserRolesVM)
        {
            return adminPackagesAndPaymentsService.GetPackgesByCategory(packgesTypeForUserRolesVM);
        }

        [HttpGet]
        public Task<List<PackgesTypeVM>> GetPackgesTypeByRoleId(int userRoleId)
        {
            return adminPackagesAndPaymentsService.GetPackgesTypeByRoleId(userRoleId);
        }
        [HttpPost]
        public Task<Response> AddOrderByPackageId([FromBody] OrderByPackageIdVM orderByPackageIds)
        {
            orderByPackageIds.UserId = DecodeTokenForUser()?.Id;

            return adminPackagesAndPaymentsService.AddOrderByPackageId(orderByPackageIds);
        }
        [HttpGet]
        public Task<Response> UpdatePckagesAndOrderStatus(long orderId)
        {
            return adminPackagesAndPaymentsService.UpdatePckagesAndOrderStatus(orderId);
        }
        [HttpGet]
        public async Task<List<ActiveOrders>> GetActiveOrdersList(int pageSize, int pageNumber)
        {
            //UserRegisterVM userVM = DecodeTokenForUser();
            string userId = DecodeTokenForUser()?.Id;
            return await adminPackagesAndPaymentsService.GetActiveOrdersList(userId, pageSize, pageNumber);
        }
        [HttpGet]
        public async Task<List<ActiveOrders>> GetExpiredOrdersList(int pageSize, int pageNumber)
        {
            //UserRegisterVM userVM = DecodeTokenForUser();
            string userId = DecodeTokenForUser()?.Id;
            return await adminPackagesAndPaymentsService.GetExpiredOrdersList(userId, pageSize, pageNumber);
        }
        [HttpGet]
        public async Task<List<PromotionsTypeVM>> GetPromotionTypes()
        {
            return await adminPackagesAndPaymentsService.GetPromotionTypes();
        }
        [HttpGet]
        public async Task<List<Voucher>> GetVoucherList()
        {
            return await adminPackagesAndPaymentsService.GetVoucherList();
        }
        [HttpPost]
        public async Task<Response> AddUpdateVoucherCategory([FromBody] VoucherCategoryVM categoryVM)
        {
            return await adminPackagesAndPaymentsService.AddUpdateVoucherCategory(categoryVM);
        }
        [HttpGet]
        public async Task<List<VoucherCategoryVM>> GetvoucherCategoryList()
        {
            return await adminPackagesAndPaymentsService.GetvoucherCategoryList();
        }
        [HttpGet]
        public async Task<Response> DeleteVoucherCategory(int voucherCategoryId)
        {
            return await adminPackagesAndPaymentsService.DeleteVoucherCategory(voucherCategoryId);
        }
        [HttpGet]
        public async Task<List<VoucherTypeVM>> GetVoucherTypeList()
        {
            return await adminPackagesAndPaymentsService.GetVoucherTypeList();
        }
        [HttpPost]
        public async Task<Response> AddUpdateVoucherType([FromBody] VoucherTypeVM voucherType)
        {
            return await adminPackagesAndPaymentsService.AddUpdateVoucherType(voucherType);
        }
        [HttpGet]
        public async Task<Response> DeleteVoucherType(int voucherTypeId)
        {
            return await adminPackagesAndPaymentsService.DeleteVoucherType(voucherTypeId);
        }
        [HttpGet]
        public async Task<List<VoucherBookVM>> GetVoucherBookList()
        {
            return await adminPackagesAndPaymentsService.GetVoucherBookList();
        }
        [HttpGet]
        public async Task<Response> DeleteVoucherBook(int voucherBookId)
        {
            return await adminPackagesAndPaymentsService.DeleteVoucherBook(voucherBookId);
        }
        [HttpPost]
        public async Task<Response> AddUpdateVoucherBook([FromBody] VoucherBookVM voucherBook)
        {
            return await adminPackagesAndPaymentsService.AddUpdateVoucherBook(voucherBook);
        }
        [HttpGet]
        public async Task<List<VoucherBookAllocationVM>> GetVouchrBookAllocation()
        {
            return await adminPackagesAndPaymentsService.GetVouchrBookAllocation();
        }
        [HttpGet]
        public async Task<Response> DeleteVoucherBookAllocation(int voucherBookAllocationId)
        {
            return await adminPackagesAndPaymentsService.DeleteVoucherBookAllocation(voucherBookAllocationId);
        }
        [HttpPost]
        public async Task<Response> AddUpdateVoucherBookAllocation([FromBody] VoucherBookAllocationVM voucherBookAllocation)
        {
            return await adminPackagesAndPaymentsService.AddUpdateVoucherBookAllocation(voucherBookAllocation);
        }
        [HttpGet]
        public async Task<List<VoucherBookPagesVM>> GetVoucherBookPagesList()
        {
            return await adminPackagesAndPaymentsService.GetVoucherBookPagesList();
        }
        [HttpPost]
        public async Task<List<VoucherBookLeavesVM>> GetVoucherBookLeavesList([FromBody] VoucherBookLeavesVM voucherBookLeavesVM)
        {
            return await adminPackagesAndPaymentsService.GetVoucherBookLeavesList(voucherBookLeavesVM);
        }
        [HttpGet]
        public async Task<List<CategoryCommissionSetupVM>> GetCategoryCommissionSetupList()
        {
            return await adminPackagesAndPaymentsService.GetCategoryCommissionSetupList();
        }
        [HttpPost]
        public async Task<Response> AddUpdateCategoryCommissionSetup([FromBody] CategoryCommissionSetupVM categoryCommissionSetup)
        {
            return await adminPackagesAndPaymentsService.AddUpdateCategoryCommissionSetup(categoryCommissionSetup);
        }
        [HttpGet]
        public async Task<List<CategoryCommissionSetupVM>> GetOverrideTradesmanCommissionList()
        {
            return await adminPackagesAndPaymentsService.GetOverrideTradesmanCommissionList();
        }
        [HttpPost]
        public async Task<Response> AddAndUpdateOverrideTradesmanCommission([FromBody] CategoryCommissionSetupVM categoryCommissionSetup)
        {
            return await adminPackagesAndPaymentsService.AddAndUpdateOverrideTradesmanCommission(categoryCommissionSetup);
        }
        [HttpGet]
        public async Task<Response> DeleteCategoryCommissionSetup(int categoryCommissionId)
        {
            return await adminPackagesAndPaymentsService.DeleteCategoryCommissionSetup(categoryCommissionId);
        }
        [HttpPost]
        public async Task<Response> AddAndUpdateAccountType([FromBody] AccountsVM accountsVM)
        {
            return await adminPackagesAndPaymentsService.AddAndUpdateAccountType(accountsVM);
        }
        [HttpGet]
        public async Task<List<AccountsVM>> GetAccountTypeList()
        {
            return await adminPackagesAndPaymentsService.GetAccountTypeList();
        }
        [HttpPost]
        public async Task<Response> AddAndUpdateAccount([FromBody] AccountsVM accountsVM)
        {
            return await adminPackagesAndPaymentsService.AddAndUpdateAccount(accountsVM);
        }
        [HttpGet]
        public async Task<List<AccountsVM>> GetAccountList()
        {
            return await adminPackagesAndPaymentsService.GetAccountList();
        }
        public async Task<Response> getSubAccountRecord(long id)
        {
          //long id = await GetEntityIdByUserId();
          return await adminPackagesAndPaymentsService.getSubAccountRecord(id);
        }
        [HttpPost]
        public async Task<Response> AddAndUpdateSubAccount([FromBody] AccountsVM accountsVM)
        {
            return await adminPackagesAndPaymentsService.AddAndUpdateSubAccount(accountsVM);
        }
        [HttpPost]
        public async Task<List<AccountsVM>> GetSubAccountList([FromBody] SearchSubAccountVM accountsVM)
        {
            return await adminPackagesAndPaymentsService.GetSubAccountList(accountsVM);
        }        
        [HttpPost]
        public async Task<Response> AddAndUpdateReferral([FromBody] ReferralVM referralVM)
        {
            return await adminPackagesAndPaymentsService.AddAndUpdateReferral(referralVM);
        }
        [HttpGet]
        public async Task<List<ReferralVM>> GetReferralList()
        {
            return await adminPackagesAndPaymentsService.GetReferralList();
        }
        [HttpGet]
        public async Task<List<PaymentWithdrawalRequestVM>> GetWithdrawalRequestList(int paymentStatus,long tradesmanId , string phoneNumber, string role)
        {
            return await adminPackagesAndPaymentsService.GetWithdrawalRequestList(paymentStatus, tradesmanId, phoneNumber, role);
        }
        [HttpGet]
        public Task<Response> UpdateWithdrawalRequestStatus(int withdrawalRequestId, string userId, decimal amount, long tradesmanId,string role)
        {
            return adminPackagesAndPaymentsService.UpdateWithdrawalRequestStatus(withdrawalRequestId, userId, amount, tradesmanId,role);
        }        
        [HttpGet]
        public Task<Response> DeclineWithdrawRequest(int withdrawalRequestId, string userId)
        {
            return adminPackagesAndPaymentsService.DeclineWithdrawRequest(withdrawalRequestId, userId);
        }

        [HttpPost]
        public Task<List<GetLeadgerReportVM>>GetLeaderReport([FromBody] GetLeadgerReportVM getLeadgerReportVM)
        {
            return adminPackagesAndPaymentsService.GetLeaderReport(getLeadgerReportVM);
        }

        [HttpPost]
        public Task<List<GeneralLedgerTransactionVM>> GetLedgerTransectionReportByAccountRef([FromBody] GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            return adminPackagesAndPaymentsService.GetLedgerTransectionReportByAccountRef(generalLedgerTransaction);
        }


        [HttpPost]
        public async Task<Response> AddAndUpdateGeneralLedgerTransection([FromBody] GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            return await adminPackagesAndPaymentsService.AddAndUpdateGeneralLedgerTransection(generalLedgerTransaction);
        }

        [HttpPost]
        public Task<List<GeneralLedgerTransactionVM>> GetDetailedLedgerTransectionReportByAccountRef([FromBody] GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            return adminPackagesAndPaymentsService.GetDetailedLedgerTransectionReportByAccountRef(generalLedgerTransaction);
        }

        [HttpPost]
        public async Task<Response> GetUserTransectionReport([FromBody] string data)
        {
            return await adminPackagesAndPaymentsService.GetUserTransectionReport(data);
        }
        [HttpGet]
        public async Task<Response> GetAccountUsersName(int accountType)
        {
            return await adminPackagesAndPaymentsService.GetAccountUsersName(accountType);
        }

        //For Supplier App
        //Promotion Types
        [HttpPost]
        public async Task<string> AddNewPromotionTypeForSupplier([FromBody] string data)
        {
            return await adminPackagesAndPaymentsService.AddNewPromotionTypeForSupplier(data);
        }

        [HttpGet]
        public async Task<string> GetPromotionTypeByIdForSupplier([FromQuery] int id)
        {
            return await adminPackagesAndPaymentsService.GetPromotionTypeByIdForSupplier(id);
        }
        [HttpPost]
        public async Task<string> GetAllPromotionTypesForSupplier([FromBody] PromotionsVM promotionsVM)
        {
            return await adminPackagesAndPaymentsService.GetAllPromotionTypesForSupplier(promotionsVM);
        }
        [HttpGet]
        public async Task<string> GetPromotionTypesListForSupplier(long ? supplierId)
        {
            return await adminPackagesAndPaymentsService.GetPromotionTypesListForSupplier(supplierId);
        }


        //For Promotions
        [HttpPost]
        public async Task<string> GetAllPromotionsForSupplier([FromBody] PromotionsVM promotionsVM)
        {
            return await adminPackagesAndPaymentsService.GetAllPromotionsForSupplier(promotionsVM);
        }
        [HttpGet]
        public async Task<string> GetPromotionByIdForSupplier([FromQuery] int id)
        {
            return await adminPackagesAndPaymentsService.GetPromotionByIdForSupplier(id);
        }        
        [HttpGet]
        public async Task<string> GetPromotionsBySuplierId(long supplierId)
        {
            return await adminPackagesAndPaymentsService.GetPromotionsBySuplierId(supplierId);
        }
        [HttpPost]
        public async Task<string> AddPromotionForSupplier([FromBody] string data)
        {
            return await adminPackagesAndPaymentsService.AddPromotionForSupplier(data);
        }
        [HttpGet]
        public async Task<string> DeletePromotionForSupplier([FromQuery] int id)
        {
            return await adminPackagesAndPaymentsService.DeletePromotionForSupplier(id);
        }
        [HttpPost]
        public async Task<Response> UpdateShippingChargesAndPaymentStatus([FromBody] string data)
        {
            return await adminPackagesAndPaymentsService.UpdateShippingChargesAndPaymentStatus(data);
        }
        [HttpPost]
        public async Task<Response> GetChartOfAccount([FromBody] string data)
        {
            return await adminPackagesAndPaymentsService.GetChartOfAccount(data);
        }

        [HttpGet]
        public async Task<string> GetChartOfAccounts()
        {
            return await adminPackagesAndPaymentsService.GetChartOfAccounts();
        }

        [HttpGet]
        public async Task<Response> GetSubAccountsLastLevel()
        {
            return await adminPackagesAndPaymentsService.GetSubAccountsLastLevel();
        }
        [HttpPost]
        public async Task<string> GetDetailedGLReport([FromBody] string data)
        {
            return await adminPackagesAndPaymentsService.GetDetailedGLReport(data);
        }
    }
}
