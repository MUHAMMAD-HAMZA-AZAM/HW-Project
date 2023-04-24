using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.PackagesAndPaymentsApi.Services;
using HW.PackagesAndPaymentsModels;
using HW.PackagesAndPaymentsViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;

namespace HW.PackagesAndPaymentsApi.Controllers
{
    public class PackagesAndPaymentsController : BaseController
    {
        private readonly IPackagesAndPaymentsService packagesAndPaymentsService;

        public PackagesAndPaymentsController(IPackagesAndPaymentsService packagesAndPaymentsService)
        {
            this.packagesAndPaymentsService = packagesAndPaymentsService;
        }
        [HttpGet]
        public async Task<Response> AddUpdateAccountReceivable(long orderId, int paymentMethodId,string userName,bool isPaymentRecived)
        {
          return await packagesAndPaymentsService.AddUpdateAccountReceivable(orderId, paymentMethodId, userName, isPaymentRecived);
        }
        [HttpGet]
        public List<PromotionsTypes> GetPromoTypesList()
        {
            return packagesAndPaymentsService.GetPromoTypesList();
        }
        [HttpPost]
        public List<PromotionsVM> GetPromotionList([FromBody] PromotionsVM promotionsVM)
        {
            return packagesAndPaymentsService.GetPromotionList(promotionsVM);
        }
        [HttpPost]
        public Response AddOrUpdatePromotions([FromBody] PromotionsVM promotionsVM)
        {
            return packagesAndPaymentsService.AddOrUpdatePromotions(promotionsVM);
        }
        [HttpPost]
        public Response DeletePromotion([FromBody] int id)
        {
            return packagesAndPaymentsService.DeletePromotion(id);
        }

        [HttpPost]
        public Response AddNewPackages([FromBody] Packages packages)
        {
            return packagesAndPaymentsService.AddNewPackages(packages);
        }
        [HttpPost]
        public List<PackagesVM> GetAllPackages([FromBody] PackagesFiltersVM packagesFiltersVM)
        {
            return packagesAndPaymentsService.GetAllPackages(packagesFiltersVM);
        }
        [HttpPost]

        public Response AddNewPromotionType([FromBody] PromotionsTypes promotionsTypes)
        {
            return packagesAndPaymentsService.AddNewPromotionType(promotionsTypes);
        }
        [HttpGet]
        public List<PromotionsTypes> GetAllPromotionTypesList()
        {
            return packagesAndPaymentsService.GetAllPromotionTypesList();
        }
        [HttpPost]

        public Response AddCouponType([FromBody] CouponTypes couponTypes)
        {
            return packagesAndPaymentsService.AddCouponType(couponTypes);
        }
        [HttpGet]
        public List<CouponTypes> GetAllCouponTypesList()
        {
            return packagesAndPaymentsService.GetAllCouponTypesList();
        }

        [HttpPost]
        public Response AddCoupons([FromBody] Coupons Coupons)
        {
            return packagesAndPaymentsService.AddCoupons(Coupons);
        }
        [HttpPost]
        public List<CouponVM> GetAllCoupons([FromBody] CouponVM couponVM)
        {
            return packagesAndPaymentsService.GetAllCoupons(couponVM);
        }
        [HttpPost]
        public Response AddPromotionOnPackages([FromBody] PromotionOnPackages promotionOnPackages)
        {
            return packagesAndPaymentsService.AddPromotionOnPackages(promotionOnPackages);
        }
        [HttpPost]
        public List<PromotionOnPackagesCM> GetAllPromotionOnPackages([FromBody] PromotionOnPackagesCM promotionOnPackagesCM)
        {
            return packagesAndPaymentsService.GetAllPromotionOnPackages(promotionOnPackagesCM);
        }

        [HttpPost]
        public Response AddNewOrder([FromBody] OrderVm orders)
        {
            return packagesAndPaymentsService.AddNewOrder(orders);
        }

        [HttpPost]
        public List<OrderVm> GetAllOrders([FromBody] OrderVm orderVm)
        {
            return packagesAndPaymentsService.GetAllOrders(orderVm);
        }
        [HttpGet]
        public List<PackageTypeVM> GetAllPackagetype()
        {
            return packagesAndPaymentsService.GetAllPackagetype();
        }
        [HttpPost]
        public Response AddUpdatePackageType([FromBody] PackageTypeVM packageTypeVM)
        {
            return packagesAndPaymentsService.AddUpdatePackageType(packageTypeVM);
        }

        [HttpPost]
        public List<GetPackgesByCategoryVM> GetPackgesByCategory([FromBody] PackgesTypeForUserRolesVM packgesTypeForUserRolesVM)
        {
            return packagesAndPaymentsService.GetPackgesByCategory(packgesTypeForUserRolesVM);
        }
        [HttpGet]
        public List<PackgesTypeVM> GetPackgesTypeByRoleId(int userRoleId)
        {
            return packagesAndPaymentsService.GetPackgesTypeByRoleId(userRoleId);
        }
        [HttpPost]
        public Task<Response> AddOrderByPackageId([FromBody] OrderByPackageIdVM orderByPackageIds)
        {
            return packagesAndPaymentsService.AddOrderByPackageId(orderByPackageIds);
        }

        [HttpPost]
        public Task<Response> AddSubAccount([FromBody] SubAccount subAccount)
        {
            return packagesAndPaymentsService.AddSubAccount(subAccount);
        }

        [HttpPost]
        public Task<Response> AddLeadgerTransection([FromBody] LeadgerTransection leadgerTransection)
        {
            return packagesAndPaymentsService.AddLeadgerTransection(leadgerTransection);
        }

        [HttpGet]
        public Task<Response> UpdatePckagesAndOrderStatus(long orderId)
        {
            return packagesAndPaymentsService.UpdatePckagesAndOrderStatus(orderId);
        }

        [HttpGet]
        public SubAccount GetSubAccount(long customerId)
        {
            return packagesAndPaymentsService.GetSubAccount(customerId);
        }
        [HttpGet]
        public Response getuseofWalletValue(long customerId, long jqId)
        {
            return packagesAndPaymentsService.getuseofWalletValue(customerId,jqId);
        }
        [HttpGet]
        public Response GetWalletValueByBidId(long customerId, long bidId)
        {
            return packagesAndPaymentsService.GetWalletValueByBidId(customerId, bidId);
        }
        [HttpGet]
        public Response getJazzCashValue(long customerId, long jqId)
        {
            return packagesAndPaymentsService.getJazzCashValue(customerId, jqId);
        }
        [HttpGet]
        public Response GetJazzCashByBidId(long customerId, long bidId)
        {
            return packagesAndPaymentsService.GetJazzCashByBidId(customerId, bidId);
        }
        [HttpGet]
        public Response DeletePromotionEntry(long jqId)
        {
            return packagesAndPaymentsService.DeletePromotionEntry(jqId);
        }
        [HttpGet]
        public SubAccount GetSubAccountByTradesmanId(long tradesmanId,string role)
        {
            return packagesAndPaymentsService.GetSubAccountByTradesmanId(tradesmanId,role);
        }

        [HttpGet]
        public WithdrawalRequest GetPaymentWithdrawalRequestByTradesmanId(long tradesmanId,string role)
        {
            return packagesAndPaymentsService.GetPaymentWithdrawalRequestByTradesmanId(tradesmanId,role);
        }

        [HttpGet]
        public Response GetLedgerTransaction(long reftradesmanId)
        {
            return packagesAndPaymentsService.GetLedgerTransaction(reftradesmanId);
        }

        [HttpGet]
        public Response GetLedgerTransactionCustomer(long refcustomerId)
        {
            return packagesAndPaymentsService.GetLedgerTransactionCustomer(refcustomerId);
        }
        [HttpGet]
        public Response GetSupplierWallet(long refSupplierId)
        {
            return packagesAndPaymentsService.GetSupplierWallet(refSupplierId);
        }

        [HttpGet]
        public List<ActiveOrders> GetActiveOrdersList(string userId, int pageSize, int pageNumber)
        {
            return packagesAndPaymentsService.GetActiveOrdersList(userId, pageSize, pageNumber);
        }
        [HttpGet]
        public List<ActiveOrders> GetExpiredOrdersList(string userId, int pageSize, int pageNumber)
        {
            return packagesAndPaymentsService.GetExpiredOrdersList(userId, pageSize, pageNumber);
        }
        [HttpGet]
        public List<PromotionsTypeVM> GetPromotionTypes()
        {
            return packagesAndPaymentsService.GetPromotionTypes();
        }
        [HttpGet]
        public List<PromotionsTypeVM> GetPromotionData()
        {
            return packagesAndPaymentsService.GetPromotionData();
        }
        [HttpPost]
        public async Task<Response> AddEditTradesmanJobReceipts([FromBody] TradesmanJobReceipts data)
        {
            return await packagesAndPaymentsService.AddEditTradesmanJobReceipts(data);
        }
        [HttpPost]
        public async Task<Response> AddEditPromotionReferalCode([FromBody] ReferalCode referalCode)
        {
            return await packagesAndPaymentsService.AddEditPromotionReferalCode(referalCode);
        }
        [HttpPost]
        public async Task<Response> AddEditRedemptions([FromBody] Redemptions redemptions)
        {
            return await packagesAndPaymentsService.AddEditRedemptions(redemptions);
        }
        [HttpPost]
        public async Task<Response> AddPromotionRedemptions([FromBody] PromotionRedemptions proRedemptions)
        {
            return await packagesAndPaymentsService.AddPromotionRedemptions(proRedemptions);
        }
        [HttpPost]
        public async Task<Response> UpdateVoucherBookLeaves([FromBody] VoucherBookLeaves voucherBookLeaves)
        {
            return await packagesAndPaymentsService.UpdateVoucherBookLeaves(voucherBookLeaves);
        }
        public List<ReferalCode> GetReferalRecordByreferalCode(string referalCode)
        {
            return packagesAndPaymentsService.GetReferalRecordByreferalCode(referalCode);
        }
        public ReferalCode GetReferalRecordByreferalUserID(string referalUserID)
        {
            return packagesAndPaymentsService.GetReferalRecordByreferalUserID(referalUserID);
        }
        public Redemptions GetRedeemRecordByJQID(long redeemVoucherByJQID)
        {
            return packagesAndPaymentsService.GetRedeemRecordByJQID(redeemVoucherByJQID);
        }
        public PromotionRedemptions GetProRecordByJQID(long redeemPromotionByJQID)
        {
            return packagesAndPaymentsService.GetProRecordByJQID(redeemPromotionByJQID);
        }
        public Redemptions GetRedeemRecordByRedeemUserID(string redeemByUserID, long voucherID)
        {
            return packagesAndPaymentsService.GetRedeemRecordByRedeemUserID(redeemByUserID, voucherID);
        }
        public PromotionRedemptions GetPromotionRedeemRecordByRedeemUserID(string redeemByUserID, long promotionTypeID,long voucherId)
        {
            return packagesAndPaymentsService.GetPromotionRedeemRecordByRedeemUserID(redeemByUserID, promotionTypeID,voucherId);
        }
        [HttpGet]
        public List<VoucherBookLeaves> GetVoucherList()
        {
            return packagesAndPaymentsService.GetVoucherList();
        }

        public ReferalCode GetReferalCodeByUserId(long jobQuotationId)
        {
            return packagesAndPaymentsService.GetReferalCodeByUserId(jobQuotationId);
        }
        [HttpPost]
        public List<TradesmanJobReceipts> getPaymentRecords([FromBody] List<long> selectedColumn)
        {
            return packagesAndPaymentsService.getPaymentRecords(selectedColumn);
        }
        [HttpGet]
        public IQueryable<PaymentMethod> GetAllPaymentMethods()
        {
            return packagesAndPaymentsService.GetAllPaymentMethods();
        }
        [HttpGet]
        public List<TradesmanJobReceiptsVM> GetJobReceiptsByTradesmanId(long tradesmanId)
        {
            return packagesAndPaymentsService.GetJobReceiptsByTradesmanId(tradesmanId);
        }
        [HttpGet]
        public CategoryCommissionSetup getCommissionByCategory(long categoryId)
        {
            return packagesAndPaymentsService.getCommissionByCategory(categoryId);
        }
        [HttpGet]
        public TradesmanCommissionOverride getTradesmanCommissionOverride(long trademanId,long categoryId)
        {
            return packagesAndPaymentsService.getTradesmanCommissionOverride(trademanId,categoryId);
        }
        [HttpGet]
        public SubAccount GetHoomWorkSubAccount()
        {
            return packagesAndPaymentsService.GetHoomWorkSubAccount();
        }
        [HttpGet]
        public SubAccount GetHoomWorkJZSubAccount()
        {
            return packagesAndPaymentsService.GetHoomWorkJZSubAccount();
        }
        [HttpPost]
        public async Task<Response> AddUpdateVoucherCategory([FromBody] VoucherCategoryVM categoryVM)
        {
            return await packagesAndPaymentsService.AddUpdateVoucherCategory(categoryVM);
        }
        [HttpGet]
        public async Task<List<VoucherCategoryVM>> GetvoucherCategoryList()
        {
            return await packagesAndPaymentsService.GetvoucherCategoryList();
        }
        [HttpGet]
        public async Task<Response> DeleteVoucherCategory(int voucherCategoryId)
        {
            return await packagesAndPaymentsService.DeleteVoucherCategory(voucherCategoryId);

        }
        [HttpGet]
        public List<VoucherTypeVM> GetVoucherTypeList()
        {
            return  packagesAndPaymentsService.GetVoucherTypeList();
        }
        [HttpPost]
        public async Task<Response> AddUpdateVoucherType([FromBody] VoucherTypeVM voucherType)
        {
            return await packagesAndPaymentsService.AddUpdateVoucherType(voucherType);
        }
        [HttpGet]
        public async Task<Response> DeleteVoucherType(int voucherTypeId)
        {
            return await packagesAndPaymentsService.DeleteVoucherType(voucherTypeId);

        }
        [HttpGet]
        public List<VoucherBookVM> GetVoucherBookList()
        {
            return packagesAndPaymentsService.GetVoucherBookList();
        }
        [HttpGet]
        public async Task<Response> DeleteVoucherBook(int voucherBookId)
        {
            return await packagesAndPaymentsService.DeleteVoucherBook(voucherBookId);

        }
        [HttpPost]
        public Response AddUpdateVoucherBook([FromBody] VoucherBookVM voucherBook)
        {
            return  packagesAndPaymentsService.AddUpdateVoucherBook(voucherBook);
        }
        [HttpGet]
        public List<VoucherBookAllocationVM> GetVouchrBookAllocation()
        {
            return packagesAndPaymentsService.GetVouchrBookAllocation();
        }
        [HttpGet]
        public async Task<Response> DeleteVoucherBookAllocation(int voucherBookAllocationId)
        {
            return await packagesAndPaymentsService.DeleteVoucherBookAllocation(voucherBookAllocationId);

        }
        [HttpPost]
        public Response AddUpdateVoucherBookAllocation([FromBody] VoucherBookAllocationVM voucherBookAllocation)
        {
            return packagesAndPaymentsService.AddUpdateVoucherBookAllocation(voucherBookAllocation);
        }
        [HttpGet]
        public List<VoucherBookPagesVM> GetVoucherBookPagesList()
        {
            return packagesAndPaymentsService.GetVoucherBookPagesList();
        }
        
        [HttpPost]
        public List<VoucherBookLeavesVM> GetVoucherBookLeavesList([FromBody] VoucherBookLeavesVM voucherBookLeavesVM)
        {
            return packagesAndPaymentsService.GetVoucherBookLeavesList(voucherBookLeavesVM);
        }

        [HttpGet]
        public List<CategoryCommissionSetupVM> GetCategoryCommissionSetupList()
        {
            return packagesAndPaymentsService.GetCategoryCommissionSetupList();
        }
        [HttpPost]
        public Response AddUpdateCategoryCommissionSetup([FromBody] CategoryCommissionSetupVM categoryCommissionSetup)
        {
            return packagesAndPaymentsService.AddUpdateCategoryCommissionSetup(categoryCommissionSetup);
        }
        [HttpGet]
        public List<CategoryCommissionSetupVM> GetOverrideTradesmanCommissionList()
        {
            return packagesAndPaymentsService.GetOverrideTradesmanCommissionList();
        }
        [HttpPost]
        public Response AddAndUpdateOverrideTradesmanCommission([FromBody] CategoryCommissionSetupVM categoryCommissionSetup)
        {
            return packagesAndPaymentsService.AddAndUpdateOverrideTradesmanCommission(categoryCommissionSetup);
        }
        [HttpGet]
        public async Task<Response> DeleteCategoryCommissionSetup(int categoryCommissionId)
        {
            return await packagesAndPaymentsService.DeleteCategoryCommissionSetup(categoryCommissionId);

        }

        [HttpGet]
        public List<LeadgerTransection> GetWalletHistory(long subAccountId)
        {
            return packagesAndPaymentsService.GetWalletHistory(subAccountId);
        }

        [HttpGet]
        public TradesmanJobReceipts GetTradesmanJobReceiptsByTradesmanId(long tradesmanId, long jobDetailId)
        {
            return packagesAndPaymentsService.GetTradesmanJobReceiptsByTradesmanId(tradesmanId, jobDetailId);
        }       
        [HttpPost]
        public Response JazzCashAcknowledgementReceipt([FromBody] JazzCashAcknowledgementReceiptVM txnDetail)
        {
            return packagesAndPaymentsService.JazzCashAcknowledgementReceipt(txnDetail);
        }
        [HttpPost]
        public Response AddAndUpdateAccountType([FromBody] AccountsVM accountsVM)
        {
            return packagesAndPaymentsService.AddAndUpdateAccountType(accountsVM);
        }
        [HttpGet]
        public List<AccountsVM> GetAccountTypeList()
        {
            return packagesAndPaymentsService.GetAccountTypeList();
        }       
        [HttpPost]
        public Response AddAndUpdateAccount([FromBody] AccountsVM accountsVM)
        {
            return packagesAndPaymentsService.AddAndUpdateAccount(accountsVM);
        }
        [HttpGet]
        public List<AccountsVM> GetAccountList()
        {
            return packagesAndPaymentsService.GetAccountList();
        }
        [HttpPost]
        public Response AddAndUpdateSubAccount([FromBody] AccountsVM accountsVM)
        {
            return packagesAndPaymentsService.AddAndUpdateSubAccount(accountsVM);
        }

        [HttpPost]
        public List<AccountsVM> GetSubAccountList([FromBody] SearchSubAccountVM accountsVM)
        {
            return packagesAndPaymentsService.GetSubAccountList(accountsVM);
        }
        [HttpPost]
        public Response AddAndUpdateReferral([FromBody] ReferralVM referralVM)
        {
            return packagesAndPaymentsService.AddAndUpdateReferral(referralVM);
        }
        [HttpGet]
        public List<ReferralVM> GetReferralList()
        {
            return packagesAndPaymentsService.GetReferralList();
        }
        [HttpGet]
        public  Response getSubAccountRecord(long id)
        {
            return  packagesAndPaymentsService.getSubAccountRecord(id);

        }
        [HttpPost]
        public Task<Response> AddPaymentWithdrawalRequest([FromBody] PaymentWithdrawalRequestVM paymentWithdrawalRequestVM)
        {
            return packagesAndPaymentsService.AddPaymentWithdrawalRequest(paymentWithdrawalRequestVM);
        }
        [HttpPost]
        public Task<Response> AddSupplierPaymentWithdrawalRequest([FromBody] PaymentWithdrawalRequestVM paymentWithdrawalRequestVM)
        {
            return packagesAndPaymentsService.AddSupplierPaymentWithdrawalRequest(paymentWithdrawalRequestVM);
        }
        [HttpGet]
        public List<PaymentWithdrawalRequestVM> GetWithdrawalRequestList(int paymentStatus , long tradesmanId,string phoneNumber, string role)
        {
            return packagesAndPaymentsService.GetWithdrawalRequestList(paymentStatus , tradesmanId, phoneNumber,role);

        }
        [HttpGet]
        public Task<Response> GetWithdrawalListById(long Id)
        {
            return packagesAndPaymentsService.GetWithdrawalListById(Id);

        }
        [HttpGet]
        public Task<Response> UpdateWithdrawalRequestStatus(int withdrawalRequestId, string id)
        {
            return packagesAndPaymentsService.UpdateWithdrawalRequestStatus(withdrawalRequestId,id);

        }        
        [HttpGet]
        public Task<Response> DeclineWithdrawRequest(int withdrawalRequestId, string id)
        {
            return packagesAndPaymentsService.DeclineWithdrawRequest(withdrawalRequestId,id);

        }
        [HttpPost]
        public List<GetLeadgerReportVM> GetLeaderReport([FromBody] GetLeadgerReportVM getLeadgerReportVM)
        {
            return packagesAndPaymentsService.GetLeaderReport(getLeadgerReportVM);
        }

        [HttpPost]
        public List<GeneralLedgerTransactionVM> GetLedgerTransectionReportByAccountRef([FromBody] GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            return packagesAndPaymentsService.GetLedgerTransectionReportByAccountRef(generalLedgerTransaction);
        }

        [HttpPost]
        public Response AddAndUpdateGeneralLedgerTransection([FromBody] GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            return packagesAndPaymentsService.AddAndUpdateGeneralLedgerTransection(generalLedgerTransaction);
        }

        [HttpPost]
        public List<GeneralLedgerTransactionVM> GetDetailedLedgerTransectionReportByAccountRef([FromBody] GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            return packagesAndPaymentsService.GetDetailedLedgerTransectionReportByAccountRef(generalLedgerTransaction);
        }

        [HttpPost]
        public async Task<Response> GetUserTransectionReport([FromBody] string data)
        {
            return await packagesAndPaymentsService.GetUserTransectionReport(data);
        }
        [HttpGet]
        public async Task<Response> GetAccountUsersName(int accountType)
        {
            return await packagesAndPaymentsService.GetAccountUsersName(accountType);
        }
        [HttpPost]
        public Task<Response> ProceedToJazzCash([FromBody] JazzCashPaymentDetailVM txnDetail)
        {
            return packagesAndPaymentsService.ProceedToJazzCash(txnDetail);
        }
        [HttpGet]
        public Task<JazzCashPaymentDetailVM> GetJazzCashMerchantDetails(string key)
        {
            return packagesAndPaymentsService.GetJazzCashMerchantDetails(key);
        }

        [HttpPost]
        public async Task<Response> AddNewPromotionTypeForSupplier([FromBody] string data)
        {
            return await packagesAndPaymentsService.AddNewPromotionTypeForSupplier(data);
        }
        [HttpGet]
        public async Task<Response> GetPromotionTypeByIdForSupplier([FromQuery] int promotionTypeId)
        {
            return await packagesAndPaymentsService.GetPromotionTypeByIdForSupplier(promotionTypeId);
        }
        [HttpPost]
        public async Task<Response> GetAllPromotionTypesForSupplier([FromBody] PromotionsVM promotionsVM)
        {
            return await packagesAndPaymentsService.GetAllPromotionTypesForSupplier(promotionsVM);
        }
        [HttpGet]
        public async Task<Response> GetPromotionTypesListForSupplier(long? supplierId)
        {
            return await packagesAndPaymentsService.GetPromotionTypesListForSupplier(supplierId);
        }
        [HttpPost]
        public async Task<Response> GetAllPromotionsForSupplier([FromBody] PromotionsVM promotionsVM)
        {
            return await packagesAndPaymentsService.GetAllPromotionsForSupplier(promotionsVM);
        }
        [HttpPost]
        public async Task<Response> AddPromotionForSupplier([FromBody] string data)
        {
            return await packagesAndPaymentsService.AddPromotionForSupplier(data);
        }        
        [HttpGet]
        public async Task<Response> GetPromotionByIdForSupplier([FromQuery] int promotionId)
        {
            return await packagesAndPaymentsService.GetPromotionByIdForSupplier(promotionId);
        }        
        [HttpGet]
        public async Task<Response> GetPromotionsBySuplierId(long supplierId)
        {
            return await packagesAndPaymentsService.GetPromotionsBySuplierId(supplierId);
        }
        [HttpGet]
        public async Task<Response> DeletePromotionForSupplier([FromQuery] int promotionId)
        {
            return await packagesAndPaymentsService.DeletePromotionForSupplier(promotionId);
        }
        [HttpPost]
        public async Task<Response> SupplierLeadgerTransaction(long? supplierId)
        {
            return await packagesAndPaymentsService.SupplierLeadgerTransaction(supplierId);
        }
        [HttpPost]
        public async Task<Response> UpdateShippingChargesAndPaymentStatus([FromBody] string data)
        {
            return await packagesAndPaymentsService.UpdateShippingChargesAndPaymentStatus(data);
        }


        [HttpPost]
        public async Task<Response> GetChartOfAccount([FromBody] string data)
        {
            return await packagesAndPaymentsService.GetChartOfAccount(data);
        }
        [HttpGet]
        public async Task<Response> GetSubAccountsLastLevel()
        {
            return await packagesAndPaymentsService.GetSubAccountsLastLevel();
        }
        [HttpGet]
        public async Task<Response> GetChartOfAccounts()
        {
            return await packagesAndPaymentsService.GetChartOfAccounts();
        }
        [HttpPost]
        public async Task<Response> InsertChartOfAccounts([FromBody] string data)
        {
            return await packagesAndPaymentsService.InsertChartOfAccounts(data);
        }
        [HttpPost]
        public async Task<Response> DeleteChartOfAccounts([FromBody] string data)
        {
            return await packagesAndPaymentsService.DeleteChartOfAccounts(data);
        }
        [HttpPost]
        public async Task<Response> AddJournalEntry([FromBody] string data)
        {
            return await packagesAndPaymentsService.AddJournalEntry(data);
        }
        [HttpPost]
        public async Task<Response> AddFiscalPeriod([FromBody] string data)
        {
            return await packagesAndPaymentsService.AddFiscalPeriod(data);
        }
        [HttpPost]
        public async Task<Response> AddLeadgerTransactionEntry([FromBody] string data)
        {
            return await packagesAndPaymentsService.AddLeadgerTransactionEntry(data);
        }
        [HttpGet]
        public async Task<Response> GetFiscalPeriodsByYear(int fiscalYear)
        {
            return await packagesAndPaymentsService.GetFiscalPeriodsByYear(fiscalYear);
        }
        [HttpPost]
        public async Task<Response> GetDetailedGLReport([FromBody] string data)
        {
            return await packagesAndPaymentsService.GetDetailedGLReport(data);
        }
        [HttpGet]
        public Task<bool> LeadgerTransectionEntries(long jobQuotationId)
        {
            return packagesAndPaymentsService.LeadgerTransectionEntries(jobQuotationId);
        }
        
    }

}
