
using HW.PackagesAndPaymentsViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SqlParameterHelper;
using HW.PackagesAndPaymentsModels;
using HW.PackagesAndPaymentsApi.DTO;
using HW.SupplierModels.DTOs;
using Microsoft.EntityFrameworkCore;
using HW.PackagesAndPaymentsModels.DTOs;
using HW.PackagesAndPaymentsViewModels.DTO;

namespace HW.PackagesAndPaymentsApi.Services
{
    public interface IPackagesAndPaymentsService
    {
        List<PromotionsTypes> GetPromoTypesList();
        List<PromotionsVM> GetPromotionList(PromotionsVM promotionsVM);
        Response AddOrUpdatePromotions(PromotionsVM promotionsVM);
        Response DeletePromotion(int id);
        Task<Response> AddUpdateAccountReceivable(long orderId, int paymentMethodId, string userName, bool isPaymentRecived);
        Task<Response> AddSubAccount(SubAccount subAccount);
        Task<Response> AddLeadgerTransection(LeadgerTransection leadgerTransection);
        //List<PromotionsTypes> GetPromoTypesList();
        Response AddNewPackages(Packages packages);
        List<PackagesVM> GetAllPackages(PackagesFiltersVM packagesFiltersVMs);
        Response AddNewPromotionType(PromotionsTypes promotionsTypes);
        List<PromotionsTypes> GetAllPromotionTypesList();
        Response AddCouponType(CouponTypes couponTypes);
        List<CouponTypes> GetAllCouponTypesList();
        Response AddCoupons(Coupons Coupons);
        SubAccount GetSubAccount(long customerId);
        WithdrawalRequest GetPaymentWithdrawalRequestByTradesmanId(long tradesmanID, string role);
        SubAccount GetSubAccountByTradesmanId(long tradesmanId, string role);
        Response GetLedgerTransaction(long reftradesmanId);
        Response GetLedgerTransactionCustomer(long refcustomerId);
        List<CouponVM> GetAllCoupons(CouponVM couponVM);
        Response AddPromotionOnPackages(PromotionOnPackages promotionOnPackages);
        List<PromotionOnPackagesCM> GetAllPromotionOnPackages(PromotionOnPackagesCM promotionOnPackages);
        Response AddNewOrder(OrderVm orders);
        List<OrderVm> GetAllOrders(OrderVm orderVm);
        List<PackageTypeVM> GetAllPackagetype();
        Response AddUpdatePackageType(PackageTypeVM packageTypeVM);
        List<GetPackgesByCategoryVM> GetPackgesByCategory(PackgesTypeForUserRolesVM packgesTypeForUserRolesVM);
        List<PackgesTypeVM> GetPackgesTypeByRoleId(int userRoleId);
        Task<Response> AddOrderByPackageId(OrderByPackageIdVM orderByPackageIds);
        Task<Response> UpdatePckagesAndOrderStatus(long orderId);
        List<ActiveOrders> GetActiveOrdersList(string userId, int pageSize, int pageNumber);
        List<ActiveOrders> GetExpiredOrdersList(string userId, int pageSize, int pageNumber);
        List<PromotionsTypeVM> GetPromotionTypes();
        List<PromotionsTypeVM> GetPromotionData();
        Task<Response> AddEditTradesmanJobReceipts(TradesmanJobReceipts data);

        Task<Response> AddEditPromotionReferalCode(ReferalCode referalCode);
        Task<Response> AddEditRedemptions(Redemptions redemptions);
        Task<Response> AddPromotionRedemptions(PromotionRedemptions proRedemptions);
        Task<Response> UpdateVoucherBookLeaves(VoucherBookLeaves voucherBookLeaves);
        List<ReferalCode> GetReferalRecordByreferalCode(string referalCode);
        ReferalCode GetReferalRecordByreferalUserID(string referalUserID);
        Redemptions GetRedeemRecordByJQID(long redeemVoucherByJQID);
        PromotionRedemptions GetProRecordByJQID(long redeemPromotionByJQID);
        Redemptions GetRedeemRecordByRedeemUserID(string redeemByUserID, long voucherID);
        PromotionRedemptions GetPromotionRedeemRecordByRedeemUserID(string redeemByUserID, long promotionTypeID, long voucherId);
        List<VoucherBookLeaves> GetVoucherList();
        ReferalCode GetReferalCodeByUserId(long jobqoutationId);
        Redemptions GetRedemptionById(string userId);
        List<TradesmanJobReceipts> getPaymentRecords(List<long> selectedColumn);
        IQueryable<PaymentMethod> GetAllPaymentMethods();
        List<TradesmanJobReceiptsVM> GetJobReceiptsByTradesmanId(long tradesmanId);
        CategoryCommissionSetup getCommissionByCategory(long categoryId);
        TradesmanCommissionOverride getTradesmanCommissionOverride(long trademanId, long categoryId);
        SubAccount GetHoomWorkSubAccount();
        SubAccount GetHoomWorkJZSubAccount();

        Task<Response> AddUpdateVoucherCategory(VoucherCategoryVM categoryVM);
        Task<List<VoucherCategoryVM>> GetvoucherCategoryList();
        Task<Response> DeleteVoucherCategory(int voucherCategoryId);
        List<VoucherTypeVM> GetVoucherTypeList();
        Task<Response> AddUpdateVoucherType(VoucherTypeVM voucherType);
        Task<Response> DeleteVoucherType(int voucherTypeId);
        List<VoucherBookVM> GetVoucherBookList();
        Task<Response> DeleteVoucherBook(int voucherBookId);
        Response AddUpdateVoucherBook(VoucherBookVM voucherBook);
        List<VoucherBookAllocationVM> GetVouchrBookAllocation();
        Task<Response> DeleteVoucherBookAllocation(int voucherBookAllocationId);
        Response AddUpdateVoucherBookAllocation(VoucherBookAllocationVM voucherBookAllocation);
        List<VoucherBookPagesVM> GetVoucherBookPagesList();
        List<VoucherBookLeavesVM> GetVoucherBookLeavesList(VoucherBookLeavesVM voucherBookLeavesVM);
        List<CategoryCommissionSetupVM> GetCategoryCommissionSetupList();
        Response AddUpdateCategoryCommissionSetup(CategoryCommissionSetupVM categoryCommissionSetup);
        List<CategoryCommissionSetupVM> GetOverrideTradesmanCommissionList();
        Response AddAndUpdateOverrideTradesmanCommission(CategoryCommissionSetupVM categoryCommissionSetup);
        Task<Response> DeleteCategoryCommissionSetup(int categoryCommissionId);
        List<LeadgerTransection> GetWalletHistory(long subAccountId);
        TradesmanJobReceipts GetTradesmanJobReceiptsByTradesmanId(long tradesmanId, long jobDetailId);
        Response JazzCashAcknowledgementReceipt(JazzCashAcknowledgementReceiptVM txnDetail);
        Response getuseofWalletValue(long customerId, long jqId);
        Response GetWalletValueByBidId(long customerId, long bidId);
        Response AddAndUpdateAccountType(AccountsVM accountsVM);
        List<AccountsVM> GetAccountTypeList();
        Response getJazzCashValue(long customerId, long jqId);
        Response GetJazzCashByBidId(long customerId, long bidId);
        Response AddAndUpdateAccount(AccountsVM accountsVM);
        List<AccountsVM> GetAccountList();
        Response DeletePromotionEntry(long jqId);
        Response AddAndUpdateSubAccount(AccountsVM accountsVM);
        List<AccountsVM> GetSubAccountList(SearchSubAccountVM accountsVM);
        Response AddAndUpdateReferral(ReferralVM referralVM);
        List<ReferralVM> GetReferralList();
        Response getSubAccountRecord(long id);
        Task<Response> AddPaymentWithdrawalRequest(PaymentWithdrawalRequestVM paymentWithdrawalRequestVM);
        List<PaymentWithdrawalRequestVM> GetWithdrawalRequestList(int paymentStatus, long tradesmanId, string phoneNumber , string role);
        Task<Response> UpdateWithdrawalRequestStatus(int withdrawalRequestId, string id);
        Task<Response> DeclineWithdrawRequest(int withdrawalRequestId, string id);
        List<GetLeadgerReportVM> GetLeaderReport(GetLeadgerReportVM getLeadgerReportVM);
        List<GeneralLedgerTransactionVM> GetLedgerTransectionReportByAccountRef(GeneralLedgerTransactionVM generalLedgerTransaction);
        Response AddAndUpdateGeneralLedgerTransection(GeneralLedgerTransactionVM generalLedgerTransaction);

        List<GeneralLedgerTransactionVM> GetDetailedLedgerTransectionReportByAccountRef(GeneralLedgerTransactionVM generalLedgerTransaction);
        Response GetSupplierWallet(long refSupplierId);
        Task<Response> GetUserTransectionReport(string data);
        Task<Response> GetAccountUsersName(int accountType);

        Task<Response> ProceedToJazzCash(JazzCashPaymentDetailVM txnDetail);
        Task<JazzCashPaymentDetailVM> GetJazzCashMerchantDetails(string key);


        Task<Response> AddNewPromotionTypeForSupplier(string data);
        Task<Response> GetPromotionTypeByIdForSupplier(int promotionTypeId);
        Task<Response> GetAllPromotionTypesForSupplier(PromotionsVM promotionsVM);
        Task<Response> GetPromotionTypesListForSupplier(long? supplierId);
        Task<Response> AddPromotionForSupplier(string data);
        Task<Response> GetAllPromotionsForSupplier(PromotionsVM promotionsVM);
        Task<Response> GetPromotionByIdForSupplier(int promotionId);
        Task<Response> GetPromotionsBySuplierId(long supplierId);
        Task<Response> DeletePromotionForSupplier(int promotionId);
        Task<Response> SupplierLeadgerTransaction(long? supplierId);
        Task<Response> UpdateShippingChargesAndPaymentStatus(string data);
        Task<Response> GetChartOfAccount(string data);

        Task<Response> GetSubAccountsLastLevel();
        Task<Response> GetChartOfAccounts();
        Task<Response> InsertChartOfAccounts(string data);
        Task<Response> DeleteChartOfAccounts(string data);
        Task<Response> AddJournalEntry(string data);
        Task<Response> AddLeadgerTransactionEntry(string data);
        Task<Response> AddFiscalPeriod(string data);
        Task<Response> GetFiscalPeriodsByYear(int fiscalYear);
        Task<Response> GetDetailedGLReport(string data);
        Task<bool> LeadgerTransectionEntries(long jobQuotationId);



        Task<Response> AddSupplierPaymentWithdrawalRequest(PaymentWithdrawalRequestVM paymentWithdrawalRequestVM);
        Task<Response> GetWithdrawalListById(long id);
    }
    public class PackagesAndPaymentsService : IPackagesAndPaymentsService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;

        public PackagesAndPaymentsService(IUnitOfWork uow, IExceptionService Exc)
        {
            this.uow = uow;
            this.Exc = Exc;
        }

        public async Task<Response> AddUpdateAccountReceivable(long orderId, int paymentMethodId, string userName, bool isPaymentRecived)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                      new SqlParameter("@orderId", orderId),
                      new SqlParameter("@paymentMethodId",paymentMethodId),
                      new SqlParameter("@userName",userName),
                      new SqlParameter("@isPaymentRecived",isPaymentRecived)
                      };
               var result = await uow.ExecuteReaderSingleDSNew<Response>("Sp_AddUpdateAccountReceivable", sqlParameters);
               response.Message = result.Select(x => x.Message).FirstOrDefault();
                response.Status = ResponseStatus.OK;
                SqlParameter[] sqlParameter =
                   {
                      new SqlParameter("@orderId", orderId),
                      };
               await uow.ExecuteReaderSingleDSNew<Response>("Sp_SupplierLeadgerEntry", sqlParameter);
                response.Status = ResponseStatus.OK;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public List<PromotionsTypes> GetPromoTypesList()
        {
            List<PromotionsTypes> dataList = new List<PromotionsTypes>();
            try
            {
                //dataList = uow.Repository<PromotionsTypes>()
                dataList = uow.Repository<PromotionsTypes>().Get(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return dataList;
        }
        public List<PromotionsVM> GetPromotionList(PromotionsVM promotionsVM)
        {
            List<PromotionsVM> dataList = new List<PromotionsVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                new SqlParameter("@PromotionName" , promotionsVM.PromotionName),
                new SqlParameter("@PromotionCode" , promotionsVM.PromotionCode),
                new SqlParameter("@PromoStartDate" , promotionsVM.PromoStartDate),
                new SqlParameter("@PromotionEndDate" , promotionsVM.PromotionEndDate),
                new SqlParameter("@PromotionIdTypeId" , promotionsVM.PromotionIdTypeId),
                new SqlParameter("@EntityStatus" , promotionsVM.EntityStatus),

                };
                dataList = uow.ExecuteReaderSingleDS<PromotionsVM>("SP_GetPromotionList", sqlParameters).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return dataList;
        }

        public Response AddOrUpdatePromotions(PromotionsVM promotionsVM)
        {
            Response response = new Response();
            Promotions promotion = new Promotions();
            try
            {
                if (promotionsVM.PromotionId > 0)
                {
                    Promotions getPromotionById = uow.Repository<Promotions>().GetById(promotionsVM.PromotionId);
                    if (getPromotionById != null)
                    {
                        getPromotionById.PromotionId = promotionsVM.PromotionId;
                        getPromotionById.PromotionIdTypeId = promotionsVM?.PromotionIdTypeId;
                        getPromotionById.PromotionName = promotionsVM.PromotionName;
                        getPromotionById.PromotionCode = promotionsVM.PromotionCode;
                        getPromotionById.PromoStartDate = promotionsVM?.PromoStartDate;
                        getPromotionById.PromotionEndDate = promotionsVM?.PromotionEndDate;
                        //getPromotionById.DiscountPercentPrice = promotionsVM?.DiscountPercentPrice;
                        //getPromotionById.DiscountDays = promotionsVM?.DiscountDays;
                        //getPromotionById.DiscountJobsApplied = promotionsVM?.DiscountJobsApplied;
                        //getPromotionById.DiscountCategories = promotionsVM?.DiscountCategories;
                        getPromotionById.UpdatedBy = promotionsVM.UpdatedBy;
                        getPromotionById.UpdatedDate = DateTime.Now;
                        getPromotionById.EntityStatus = promotionsVM.EntityStatus;
                        getPromotionById.Amount = promotionsVM?.Amount;
                        getPromotionById.CategoryId = promotionsVM?.CategoryId;
                        //getPromotionById.PermotionsForAll = promotionsVM?.PermotionsForAll;
                        //getPromotionById.PermotionsForNew = promotionsVM?.PermotionsForNew;
                        //getPromotionById.PermotionsForOld = promotionsVM?.PermotionsForOld;
                        promotion.IsActive = promotionsVM.IsActive;
                        //promotion.EntityStatus = promotionsVM.EntityStatus;
                        uow.Repository<Promotions>().Update(getPromotionById);
                        uow.Save();
                        response.Message = "Data Updated Successfully!";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "Something went wrong";
                        response.Status = ResponseStatus.Error;
                    }

                }
                else
                {
                    promotion.PromotionIdTypeId = promotionsVM.PromotionIdTypeId;
                    promotion.PromotionName = promotionsVM.PromotionName;
                    promotion.PromotionCode = promotionsVM.PromotionCode;
                    promotion.PromoStartDate = promotionsVM.PromoStartDate;
                    promotion.PromotionEndDate = promotionsVM.PromotionEndDate;
                    //promotion.DiscountPercentPrice = promotionsVM.DiscountPercentPrice;
                    //promotion.DiscountDays = promotionsVM.DiscountDays;
                    //promotion.DiscountJobsApplied = promotionsVM.DiscountJobsApplied;
                    //promotion.DiscountCategories = promotionsVM.DiscountCategories;
                    promotion.CreatedBy = promotionsVM.CreatedBy;
                    promotion.CreatedDate = DateTime.Now;
                    promotion.IsActive = promotionsVM.IsActive;
                    promotion.EntityStatus = promotionsVM.EntityStatus;
                    promotion.Amount = promotionsVM.Amount;
                    promotion.CategoryId = promotionsVM.CategoryId;
                    //promotion.PermotionsForAll = promotionsVM.PermotionsForAll;
                    //promotion.PermotionsForNew = promotionsVM.PermotionsForNew;
                    //promotion.PermotionsForOld = promotionsVM.PermotionsForOld;
                    uow.Repository<Promotions>().Add(promotion);
                    uow.Save();
                    response.Message = "Data Saved Successfully!";
                    response.Status = ResponseStatus.OK;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }

            return response;
        }
        public Response DeletePromotion(int id)
        {
            Response response = new Response();
            Promotions promotion = new Promotions();
            try
            {
                if (id > 0)
                {
                    Promotions getPromotionById = uow.Repository<Promotions>().GetById(id);
                    if (getPromotionById != null)
                    {
                        if (getPromotionById.IsActive == true)
                            getPromotionById.IsActive = false;
                        else
                            getPromotionById.IsActive = true;

                        uow.Repository<Promotions>().Update(getPromotionById);
                        uow.Save();
                        response.Message = "Data Deleted Successfully!";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "Something went wrong";
                        response.Status = ResponseStatus.Error;
                    }

                }
                else
                {
                    response.Message = "Something went wrong";
                    response.Status = ResponseStatus.Error;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public Response AddNewPackages(Packages packages)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@PackageId", packages.PackageId),
                new SqlParameter("@PackageName",packages.PackageName),
                new SqlParameter("@SalePrice",packages.SalePrice),
                new SqlParameter("@TotalApplicableJobs",packages.TotalApplicableJobs),
                new SqlParameter("@TotalCategories", packages.TotalCategories),
                new SqlParameter("@TradePrice",packages.TradePrice),
                new SqlParameter("@ValidityDays",packages.ValidityDays),
                new SqlParameter("@UserRoleId",packages.UserRoleId),
                new SqlParameter("@PackageCode", packages.PackageCode),
                new SqlParameter("@IsActive",packages.IsActive),
                new SqlParameter("@EntityStatus",packages.EntityStatus),
                new SqlParameter("@PackageTypeId",packages.PackageTypeId),
                new SqlParameter("@CreatedBy",packages.CreatedBy),
                new SqlParameter("@CreatedDate", packages.CreatedDate),
                new SqlParameter("@UpdatedBy",packages.UpdatedBy),
                new SqlParameter("@UpdatedDate",packages.UpdatedDate)

            };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_AddUpdatePackages", sqlParameters);
                res.Message = result.Select(x => x.Message).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<PackagesVM> GetAllPackages(PackagesFiltersVM packagesFiltersVM)
        {
            List<PackagesVM> packages = new List<PackagesVM>();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@PackageName", packagesFiltersVM.PackageName),
                new SqlParameter("@PackageCode", packagesFiltersVM.PackageCode),
                new SqlParameter("@UserRoleId", packagesFiltersVM.UserRoleId),
                new SqlParameter("@Entity", packagesFiltersVM.Entity),
                new SqlParameter("@pageNumber", packagesFiltersVM.pageNumber),
                new SqlParameter("@pageSize", packagesFiltersVM.pageSize),
                new SqlParameter("@OrderByColumn", packagesFiltersVM.OrderByColumn)

                };
                packages = uow.ExecuteReaderSingleDS<PackagesVM>("GetAllPackages", sqlParameters);

                return packages;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<PackagesVM>();
            }
        }
        public Response AddNewPromotionType(PromotionsTypes promotionsTypes)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@PromotionTypeId", promotionsTypes.PromotionTypeId),
                new SqlParameter("@PromotionTypeName",promotionsTypes.PromotionTypeName),

                new SqlParameter("@PromotionTypeCode", promotionsTypes.PromotionTypeCode),
                new SqlParameter("@PromotionOn", promotionsTypes.PromotionOn),
                new SqlParameter("@IsActive",promotionsTypes.IsActive),
                new SqlParameter("@EntityStatus",promotionsTypes.EntityStatus),
                new SqlParameter("@CreatedBy",promotionsTypes.CreatedBy),
               // new SqlParameter("@CreatedDate", promotionsTypes.CreatedDate),
                new SqlParameter("@UpdatedBy",promotionsTypes.UpdatedBy)
               // new SqlParameter("@UpdatedDate",promotionsTypes.UpdatedDate)

            };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_AddUpdatePromotionTypes", sqlParameters);
                res.Message = result.Select(x => x.Message).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<PromotionsTypes> GetAllPromotionTypesList()
        {
            List<PromotionsTypes> dataList = new List<PromotionsTypes>();
            try
            {
                SqlParameter[] sqlParameters = { };
                dataList = uow.ExecuteReaderSingleDS<PromotionsTypes>("SP_GetPromotionTypeList", sqlParameters).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return dataList;
        }

        public Response AddCouponType(CouponTypes couponTypes)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@CouponTypeId", couponTypes.CouponTypeId),
                new SqlParameter("@CouponTypeName",couponTypes.CouponTypeName),
                new SqlParameter("@CouponTypeCode", couponTypes.CouponTypeCode),
                new SqlParameter("@IsActive",couponTypes.IsActive),
                new SqlParameter("@EntityStatus",couponTypes.EntityStatus),
                new SqlParameter("@CreatedBy",couponTypes.CreatedBy),
                new SqlParameter("@UpdatedBy",couponTypes.UpdatedBy)
            };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_AddUpdateCouponTypes", sqlParameters);
                res.Message = result.Select(x => x.Message).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<CouponTypes> GetAllCouponTypesList()
        {
            List<CouponTypes> dataList = new List<CouponTypes>();
            try
            {
                SqlParameter[] sqlParameters = { };
                dataList = uow.ExecuteReaderSingleDS<CouponTypes>("SP_GetCouponTypeList", sqlParameters).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return dataList;
        }

        public Response AddCoupons(Coupons coupon)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@CouponId", coupon.CouponId),
                new SqlParameter("@CouponName",coupon.CouponeName),
                new SqlParameter("@DiscountAmount", coupon.DiscountAmount),
                new SqlParameter("@DiscountDays",coupon.DiscountDays),
                new SqlParameter("@DiscountCategories",coupon.DiscountCategories),
                new SqlParameter("@DiscountJobsApplied", coupon.DiscountJobsApplied),
                new SqlParameter("@CouponTypeId",coupon.CouponTypeId),
                new SqlParameter("@CouponCode", coupon.CouponCode),
                new SqlParameter("@IsActive",coupon.IsActive),
                new SqlParameter("@EntityStatus",coupon.EntityStatus),
                new SqlParameter("@CreatedBy",coupon.CreatedBy),
                new SqlParameter("@UpdatedBy",coupon.UpdatedBy)
            };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_AddUpdateCoupons", sqlParameters);
                res.Message = result.Select(x => x.Message).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<CouponVM> GetAllCoupons(CouponVM couponVM)
        {
            List<CouponVM> couponVM1 = new List<CouponVM>();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@CouponName", couponVM.CouponeName),
                new SqlParameter("@CouponCode", couponVM.CouponCode),
                new SqlParameter("@CouponTypeId", couponVM.CouponTypeId),
                new SqlParameter("@entityStatus", couponVM.EntityStatus),
                new SqlParameter("@pageNumber", couponVM.pageNumber),
                new SqlParameter("@pageSize", couponVM.pageSize),
                new SqlParameter("@OrderByColumn", couponVM.OrderByColumn)

                };
                couponVM1 = uow.ExecuteReaderSingleDS<CouponVM>("GetAllCoupons", sqlParameters);

                return couponVM1;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<CouponVM>();
            }
        }

        public Response AddPromotionOnPackages(PromotionOnPackages promotionOnPackages)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@PromotionOnPackagesId", promotionOnPackages.PromotionOnPackagesId),
                new SqlParameter("@PackageId",promotionOnPackages.PackageId),
                new SqlParameter("@PromotionId", promotionOnPackages.PromotionId),
                new SqlParameter("@UserRoleId",promotionOnPackages.UserRoleId),
                new SqlParameter("@OriginalSalePrice",promotionOnPackages.OriginalSalePrice),
                new SqlParameter("@DiscountPercentPrice", promotionOnPackages.DiscountPercentPrice),
                new SqlParameter("@PriceAfterDiscount",promotionOnPackages.PriceAfterDiscount),
                new SqlParameter("@ValidityDays", promotionOnPackages.ValidityDays),

                 new SqlParameter("@DiscountDays",promotionOnPackages.DiscountDays),
                new SqlParameter("@DiscountedValidityDays", promotionOnPackages.DiscountedValidityDays),
                new SqlParameter("@TotalApplicableJobs",promotionOnPackages.TotalApplicableJobs),
                new SqlParameter("@DiscountJobsApplied",promotionOnPackages.DiscountJobsApplied),
                new SqlParameter("@DiscountedTotalApplicableJobs", promotionOnPackages.DiscountedTotalApplicableJobs),
                new SqlParameter("@TotalCategories",promotionOnPackages.TotalCategories),
                new SqlParameter("@DiscountCategories", promotionOnPackages.DiscountCategories),
                new SqlParameter("@DiscountedTotalCategories", promotionOnPackages.DiscountedTotalCategories),
                new SqlParameter("@EntityStatus", promotionOnPackages.EntityStatus),
                new SqlParameter("@IsActive",promotionOnPackages.IsActive),
                new SqlParameter("@CreatedBy",promotionOnPackages.CreatedBy),
                new SqlParameter("@UpdatedBy",promotionOnPackages.UpdatedBy)

            };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_AddUpdatePromotionOnPackages", sqlParameters);
                res.Message = result.Select(x => x.Message).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<PromotionOnPackagesCM> GetAllPromotionOnPackages(PromotionOnPackagesCM promotionOnPackages)
        {
            List<PromotionOnPackagesCM> couponVM1 = new List<PromotionOnPackagesCM>();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@PackageId", promotionOnPackages.PackageId),
                new SqlParameter("@PromotionId", promotionOnPackages.PromotionId),
                new SqlParameter("@UserRoleId", promotionOnPackages.UserRoleId),
                new SqlParameter("@entityStatus", promotionOnPackages.EntityStatus),
                new SqlParameter("@pageNumber", promotionOnPackages.pageNumber),
                new SqlParameter("@pageSize", promotionOnPackages.pageSize),
                new SqlParameter("@OrderByColumn", promotionOnPackages.OrderByColumn)

                };
                couponVM1 = uow.ExecuteReaderSingleDS<PromotionOnPackagesCM>("Sp_GetAllPromotionsOnPackages", sqlParameters);

                return couponVM1;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<PromotionOnPackagesCM>();
            }
        }

        public SubAccount GetSubAccount(long customerId)
        {
            try
            {
                return uow.Repository<SubAccount>().Get(x => x.CustomerId == customerId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SubAccount();
            }
        }
        public SubAccount GetSubAccountByTradesmanId(long tradesmanId, string role)
        {
            try
            {
                if (role == "Tradesman")
                {
                    return uow.Repository<SubAccount>().Get(x => x.TradesmanId == tradesmanId).FirstOrDefault();
                }
                else
                {
                    return uow.Repository<SubAccount>().Get(x => x.SupplierId == tradesmanId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SubAccount();
            }
        }

        public Response GetLedgerTransaction(long refertradesmanId)
        {
            try
            {
                SqlParameter[] sqlParameters =
               {
                new SqlParameter("@reftradesmanId",refertradesmanId)
            };

                SpTransactionAmountVM sPClass = uow.ExecuteReaderSingleDS<SpTransactionAmountVM>("GetTrdesmanWalletAmount", sqlParameters)?.FirstOrDefault();
                Response response = new Response
                {
                    Status = ResponseStatus.OK,
                    Message = "Ok.",
                    ResultData = sPClass?.amount ?? 0
                };
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public Response GetLedgerTransactionCustomer(long refercustomerId)
        {
            try
            {
                SqlParameter[] sqlParameters =
               {
                new SqlParameter("@refcustomerId",refercustomerId)
            };

                SpTransactionAmountVM sPClass = uow.ExecuteReaderSingleDS<SpTransactionAmountVM>("GetCustomerWalletAmount", sqlParameters)?.FirstOrDefault();
                Response response = new Response
                {
                    Status = ResponseStatus.OK,
                    Message = "Ok.",
                    ResultData = sPClass?.amount ?? 0
                };
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public Response AddNewOrder(OrderVm orders)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@OrderId", orders.OrderId),
                new SqlParameter("@PromotionOnPackagesId", orders.PromotionOnPackagesId),
                new SqlParameter("@PackageId",orders.PackageId),

                new SqlParameter("@UserRoleId",orders.UserRoleId),
                new SqlParameter("@UserId",orders.AspnetUserId),
                new SqlParameter("@OriginalSalePrice",orders.OriginalSalePrice),
                new SqlParameter("@DiscountPercentPrice", orders.DiscountPercentPrice),
                new SqlParameter("@PriceAfterDiscount",orders.PriceAfterDiscount),
                new SqlParameter("@ValidityDays", orders.ValidityDays),

                 new SqlParameter("@DiscountDays",orders.DiscountDays),
                new SqlParameter("@DiscountedValidityDays", orders.DiscountedValidityDays),
                new SqlParameter("@TotalApplicableJobs",orders.TotalApplicableJobs),
                new SqlParameter("@DiscountJobsApplied",orders.DiscountJobsApplied),
                new SqlParameter("@DiscountedTotalApplicableJobs", orders.DiscountedTotalApplicableJobs),
                new SqlParameter("@TotalCategories",orders.TotalCategories),
                new SqlParameter("@DiscountCategories", orders.DiscountCategories),
                new SqlParameter("@DiscountedTotalCategories", orders.DiscountedTotalCategories),
                new SqlParameter("@OrderTotal", orders.OrderTotal),
                new SqlParameter("@Skills", orders.SelectedSkills),
                new SqlParameter("@EntityStatus", orders.EntityStatus),
                new SqlParameter("@IsActive",orders.IsActive),
                new SqlParameter("@CreatedBy",orders.CreatedBy),
                new SqlParameter("@UpdatedBy",orders.UpdatedBy)

            };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_AddUpdateOrders", sqlParameters);
                res.Message = result.Select(x => x.Message).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<OrderVm> GetAllOrders(OrderVm promotionOnPackages)
        {
            List<OrderVm> couponVM1 = new List<OrderVm>();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@PackageId", promotionOnPackages.PackageId),
                new SqlParameter("@PromotionOnPackagesId", promotionOnPackages.PromotionOnPackagesId),
                new SqlParameter("@UserRoleId", promotionOnPackages.UserRoleId),
                new SqlParameter("@AspnetUserId", promotionOnPackages.AspnetUserId),
                new SqlParameter("@pageNumber", promotionOnPackages.pageNumber),
                new SqlParameter("@pageSize", promotionOnPackages.pageSize),
                new SqlParameter("@OrderByColumn", promotionOnPackages.OrderByColumn)

                };
                couponVM1 = uow.ExecuteReaderSingleDS<OrderVm>("Sp_GetAllOrders", sqlParameters);

                return couponVM1;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<OrderVm>();
            }
        }
        public List<PackageTypeVM> GetAllPackagetype()
        {
            List<PackageTypeVM> data = new List<PackageTypeVM>();
            try
            {
                SqlParameter[] sqlParameters =
                  {};
                data = uow.ExecuteReaderSingleDS<PackageTypeVM>("Sp_GetAllPackageType", sqlParameters);

                return data;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<PackageTypeVM>();
            }
        }
        public Response AddUpdatePackageType(PackageTypeVM packageTypeVM)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
              {
                new SqlParameter("@PackageTypeId", packageTypeVM.PackageTypeId),
                new SqlParameter("@PackageTypeName", packageTypeVM.PackageTypeName),
                new SqlParameter("@PackageTypeCode",packageTypeVM.PackageTypeCode),
                new SqlParameter("@CreatedBy",packageTypeVM.CreatedBy),
                new SqlParameter("@CreatedOn",packageTypeVM.CreatedOn),
                new SqlParameter("@IsActive",packageTypeVM.IsActive),
                new SqlParameter("@Status", packageTypeVM.EntityStatus),
                new SqlParameter("@userRoleId", packageTypeVM.UserRoleId)
            };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_AddupdatePackageType", sqlParameters);
                res.Message = result.Select(x => x.Message).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public List<GetPackgesByCategoryVM> GetPackgesByCategory(PackgesTypeForUserRolesVM packgesTypeForUserRolesVM)
        {
            try
            {
                List<GetPackgesByCategoryVM> getPackgesByCategories = new List<GetPackgesByCategoryVM>();

                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(long));

                packgesTypeForUserRolesVM.packageTypeIds.ForEach(x => dt.Rows.Add(x));

                SqlParameter[] sqlParameters =
                  {
                 new SqlParameter("@typeIds", SqlDbType.Structured) { TypeName = "dbo.IdsList", Value = dt },
                 new SqlParameter("@userRole",packgesTypeForUserRolesVM.userRoleId)

            };
                getPackgesByCategories = uow.ExecuteReaderSingleDS<GetPackgesByCategoryVM>("GetPackgesByCategory", sqlParameters);
                return getPackgesByCategories;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<GetPackgesByCategoryVM>();
            }
        }

        public List<PackgesTypeVM> GetPackgesTypeByRoleId(int userRoleId)
        {
            try
            {
                List<PackgesTypeVM> getPackgestype = new List<PackgesTypeVM>();
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@userRoleId",userRoleId),
            };
                getPackgestype = uow.ExecuteReaderSingleDS<PackgesTypeVM>("GetPackgesTypeByRoleId", sqlParameters);// pkg Types Available 4 this Role
                return getPackgestype;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<PackgesTypeVM>();
            }
        }

        public async Task<Response> AddOrderByPackageId(OrderByPackageIdVM orderByPackageIds)
        {
            try
            {
                Response response = new Response();
                if (orderByPackageIds != null)
                {
                    Orders orders = new Orders()
                    {
                        AspnetUserId = orderByPackageIds.UserId,
                        UserRoleId = orderByPackageIds.RoleId,
                        OrderTotal = orderByPackageIds.TotalAmount,
                        CreatedDate = DateTime.Now,
                    };

                    await uow.Repository<Orders>().AddAsync(orders);
                    await uow.SaveAsync();
                    if (orders.OrderId > 0)
                    {
                        foreach (var item in orderByPackageIds.PackageIds)
                        {
                            OrderItem orderItem = new OrderItem()
                            {
                                OrderId = orders.OrderId,
                                PackageId = item,
                                CreatedOn = DateTime.Now
                            };
                            await uow.Repository<OrderItem>().AddAsync(orderItem);

                        }
                        await uow.SaveAsync();
                        response.ResultData = orders.OrderId;
                        response.Status = ResponseStatus.OK;
                        response.Message = "Successfully Updated";

                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Error";
                    }
                }


                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> AddSubAccount(SubAccount subAccount)
        {
            Response response = new Response();
            try
            {
                if (subAccount != null)
                {
                    await uow.Repository<SubAccount>().AddAsync(subAccount);
                    await uow.SaveAsync();

                    response.Status = ResponseStatus.OK;
                    response.Message = "Successfully Added";
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = "Error";
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> AddLeadgerTransection(LeadgerTransection leadgerTransection)
        {
            Response response = new Response();
            try
            {
                if (leadgerTransection != null)
                {
                    await uow.Repository<LeadgerTransection>().AddAsync(leadgerTransection);
                    await uow.SaveAsync();

                    response.Status = ResponseStatus.OK;
                    response.Message = "Successfully Added";
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = "Error";
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<Response> UpdatePckagesAndOrderStatus(long orderId)
        {
            try
            {
                Response response = new Response();
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@orderId",orderId),
            };
                uow.ExecuteReaderSingleDS<Response>("Sp_UpdatePackageAndOrderStatus", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Saved";
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<ActiveOrders> GetActiveOrdersList(string userId, int pageSize, int pageNumber)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@pageNumber",pageNumber)

            };
                List<ActiveOrders> result = uow.ExecuteReaderSingleDS<ActiveOrders>("SP_GetActiveOrdersList", sqlParameters);

                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }
        public List<ActiveOrders> GetExpiredOrdersList(string userId, int pageSize, int pageNumber)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@pageNumber",pageNumber)

            };
                List<ActiveOrders> result = uow.ExecuteReaderSingleDS<ActiveOrders>("SP_GetExpiredOrdersList", sqlParameters);

                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }

        public List<PromotionsTypeVM> GetPromotionTypes()
        {
            List<PromotionsTypeVM> promotionsTypeVM = new List<PromotionsTypeVM>();
            try
            {
                SqlParameter[] sqlParameters = { };
                promotionsTypeVM = uow.ExecuteReaderSingleDS<PromotionsTypeVM>("GetPromotionsType", sqlParameters);
            }
            catch (Exception ex)
            {

            }
            return promotionsTypeVM;
        }
        public List<PromotionsTypeVM> GetPromotionData()
        {
            List<PromotionsTypeVM> promotionsTypeVM = new List<PromotionsTypeVM>();
            try
            {
                SqlParameter[] sqlParameters = { };
                promotionsTypeVM = uow.ExecuteReaderSingleDS<PromotionsTypeVM>("GetPromotionData", sqlParameters);
            }
            catch (Exception ex)
            {

            }
            return promotionsTypeVM;
        }

        public async Task<Response> AddEditTradesmanJobReceipts(TradesmanJobReceipts data)
        {

            Response response = new Response();
            try
            {
                TradesmanJobReceipts existingData = await uow.Repository<TradesmanJobReceipts>().GetByIdAsync(data.TradesmanJobReceiptsId);
                if (existingData != null)
                {
                    var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    var jsonValues = JsonConvert.SerializeObject(data, settings);
                    JsonConvert.PopulateObject(jsonValues, existingData);
                    uow.Repository<TradesmanJobReceipts>().Update(existingData);
                }
                else
                {
                    await uow.Repository<TradesmanJobReceipts>().AddAsync(data);
                }
                await uow.SaveAsync();

                response.Message = "Successfull Created.";
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;
        }

        //////////////////////////////////Promotions//////////////////////////////////////////////
        public async Task<Response> AddEditPromotionReferalCode(ReferalCode referalCode)
        {

            Response response = new Response();
            try
            {
                ReferalCode existingData = await uow.Repository<ReferalCode>().GetByIdAsync(referalCode.ReferralId);
                if (existingData != null)
                {
                    var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    var jsonValues = JsonConvert.SerializeObject(referalCode, settings);
                    JsonConvert.PopulateObject(jsonValues, existingData);
                    uow.Repository<ReferalCode>().Update(existingData);
                }
                else
                {
                    
                    await uow.Repository<ReferalCode>().AddAsync(referalCode);
                }

                await uow.SaveAsync();

                response.Message = "Successfull Created.";
                response.ResultData = referalCode.UserId;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;
        }
        public async Task<Response> AddEditRedemptions(Redemptions redemptions)
        {

            Response response = new Response();
            try
            {
                Redemptions existingData = await uow.Repository<Redemptions>().GetByIdAsync(redemptions.RedemptionsId);
                if (existingData != null)
                {
                    var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    var jsonValues = JsonConvert.SerializeObject(redemptions, settings);
                    JsonConvert.PopulateObject(jsonValues, existingData);
                    uow.Repository<Redemptions>().Update(existingData);
                }
                else
                {
                    await uow.Repository<Redemptions>().AddAsync(redemptions);
                }

                await uow.SaveAsync();

                response.Message = "Successfull Created.";
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;
        }
        public async Task<Response> AddPromotionRedemptions(PromotionRedemptions proRedemptions)
        {

            Response response = new Response();
            try
            {
                PromotionRedemptions existingData = await uow.Repository<PromotionRedemptions>().GetByIdAsync(proRedemptions.PromotionRedemptionsId);
                if (existingData != null)
                {
                    var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    var jsonValues = JsonConvert.SerializeObject(proRedemptions, settings);
                    JsonConvert.PopulateObject(jsonValues, existingData);
                    uow.Repository<PromotionRedemptions>().Update(existingData);
                }
                else
                {
                    await uow.Repository<PromotionRedemptions>().AddAsync(proRedemptions);
                }

                await uow.SaveAsync();

                response.Message = "Successfull Created.";
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;
        }
        public async Task<Response> UpdateVoucherBookLeaves(VoucherBookLeaves voucherBookLeaves)
        {

            Response response = new Response();
            try
            {
                VoucherBookLeaves existingData = new VoucherBookLeaves();
                existingData = uow.Repository<VoucherBookLeaves>().GetAll().Where(a => a.VoucherBookLeavesId == voucherBookLeaves.VoucherBookLeavesId).FirstOrDefault();

                if (existingData != null)
                {
                    existingData.IsUsed = voucherBookLeaves.IsUsed;
                    existingData.ModifiedOn = DateTime.Now;
                    uow.Repository<VoucherBookLeaves>().Update(existingData);
                    await uow.SaveAsync();
                    response.Message = "Successfull Updated.";
                    response.Status = ResponseStatus.OK;
                }


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public List<ReferalCode> GetReferalRecordByreferalCode(string referalCode)
        {
            List<ReferalCode> referallist = new List<ReferalCode>();
            try
            {
                referallist = uow.Repository<ReferalCode>().GetAll().Where(a => a.ReferralCode == referalCode).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
            return referallist;
        }
        public ReferalCode GetReferalRecordByreferalUserID(string referalUserID)
        {
            ReferalCode referallist = new ReferalCode();
            try
            {
                referallist = uow.Repository<ReferalCode>().GetAll().Where(a => a.ReferredUser == referalUserID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
            return referallist;
        }
        public Redemptions GetRedeemRecordByJQID(long redeemVoucherByJQID)
        {
            Redemptions voucherRecord = new Redemptions();
            try
            {
                voucherRecord = uow.Repository<Redemptions>().GetAll().Where(a => a.JobQuotationId == redeemVoucherByJQID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
            return voucherRecord;
        }
        public PromotionRedemptions GetProRecordByJQID(long redeemPromotionByJQID)
        {
            PromotionRedemptions referallist = new PromotionRedemptions();
            try
            {
                referallist = uow.Repository<PromotionRedemptions>().GetAll().Where(a => a.JobQuotationId == redeemPromotionByJQID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
            return referallist;
        }
        public Redemptions GetRedeemRecordByRedeemUserID(string redeemByUserID, long voucherID)
        {
            Redemptions referallist = new Redemptions();
            try
            {
                referallist = uow.Repository<Redemptions>().GetAll().Where(a => a.RedeemBy == redeemByUserID && a.VoucherId == voucherID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
            return referallist;
        }
        public PromotionRedemptions GetPromotionRedeemRecordByRedeemUserID(string redeemByUserID, long promotionTypeID, long voucherId)
        {
            PromotionRedemptions getrecord = new PromotionRedemptions();
            try
            {
                getrecord = uow.Repository<PromotionRedemptions>().GetAll().Where(a => a.RedeemBy == redeemByUserID && a.PromotionId == promotionTypeID && a.VoucherBookLeavesId == voucherId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
            return getrecord;
        }
        public List<VoucherBookLeaves> GetVoucherList()
        {
            List<VoucherBookLeaves> voucherList = new List<VoucherBookLeaves>();
            try
            {
                voucherList = uow.Repository<VoucherBookLeaves>().GetAll().Where(a => a.IsUsed != true && a.Active == true && a.ValidFrom <= DateTime.Now && a.ValidTo >= DateTime.Now).ToList();
            }
            catch (Exception ex)
            {

            }
            return voucherList;
        }
        public ReferalCode GetReferalCodeByUserId(long jobqoutationId)
        {
            try
            {
                return uow.Repository<ReferalCode>().Get(x => x.JobQuotationId == jobqoutationId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new ReferalCode();
            }
        }
        public Redemptions GetRedemptionById(string userID)
        {
            try
            {
                return uow.Repository<Redemptions>().Get(x => x.RedeemBy == userID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Redemptions();
            }
        }
        public List<TradesmanJobReceipts> getPaymentRecords(List<long> selectedColumn)
        {
            try
            {
                return uow.Repository<TradesmanJobReceipts>().GetAll().Where(x => selectedColumn.Contains(x.JobDetailId)).OrderByDescending(o => o.CreatedOn).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<TradesmanJobReceipts>();
            }
        }
        public IQueryable<PaymentMethod> GetAllPaymentMethods()
        {
            try
            {
                return uow.Repository<PaymentMethod>().GetAll();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<PaymentMethod>().AsQueryable();
            }
        }
        public List<TradesmanJobReceiptsVM> GetJobReceiptsByTradesmanId(long tradesmanId)
        {
            List<TradesmanJobReceiptsVM> tradesmanJobReceipts = new List<TradesmanJobReceiptsVM>();
            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@tradesmanId", tradesmanId)
                };
                tradesmanJobReceipts = uow.ExecuteReaderSingleDS<TradesmanJobReceiptsVM>("sp_GetJobReceiptsByTradesmanId", sqlParameters);
                return tradesmanJobReceipts;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<TradesmanJobReceiptsVM>();
            }
        }
        public CategoryCommissionSetup getCommissionByCategory(long categoryId)
        {
            try
            {
                return uow.Repository<CategoryCommissionSetup>().Get(x => x.CategoryId == categoryId && x.Active == true && x.EntityStatus == "a").FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new CategoryCommissionSetup();

            }
        }
        public TradesmanCommissionOverride getTradesmanCommissionOverride(long trademanId, long categoryId)
        {
            try
            {
                return uow.Repository<TradesmanCommissionOverride>().Get(x => x.TradesmanId == trademanId && x.CategoryId == categoryId && x.Active == true && x.EntityStatus == "a").FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new TradesmanCommissionOverride();

                //public async Task<Response> AddSubAccount(SubAccount subAccount)
                //{
                //    Response response = new Response();
                //    try
                //    {
                //        SubAccount existingData = await uow.Repository<SubAccount>().GetByIdAsync(subAccount.SubAccountId);
                //        if (existingData != null)
                //        {
                //            var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                //            var jsonValues = JsonConvert.SerializeObject(subAccount, settings);
                //            JsonConvert.PopulateObject(jsonValues, existingData);
                //            uow.Repository<SubAccount>().Update(existingData);
                //        }
                //        else
                //        {
                //            await uow.Repository<SubAccount>().AddAsync(subAccount);
                //        }

                //        await uow.SaveAsync();

                //        response.Message = "Successfull Created.";
                //        response.Status = ResponseStatus.OK;
                //    }
                //    catch (Exception ex)
                //    {
                //        response.Message = ex.Message;
                //        response.Status = ResponseStatus.Error;

                //        Exc.AddErrorLog(ex);
                //    }
                //    return response;
                //}
            }
        }
        public SubAccount GetHoomWorkSubAccount()
        {
            try
            {
                return uow.Repository<SubAccount>().Get(s => s.SubAccountName == "HoomWork").FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SubAccount();
            }
        }
        public SubAccount GetHoomWorkJZSubAccount()
        {
            try
            {
                return uow.Repository<SubAccount>().Get(s => s.SubAccountName == "Homework Jazz Cash").FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SubAccount();
            }
        }

        public async Task<Response> AddUpdateVoucherCategory(VoucherCategoryVM categoryVM)
        {
            try
            {
                Response response = new Response();
                if (categoryVM.VoucherCategoryId == 0)
                {
                    VoucherCategory category = new VoucherCategory()
                    {
                        VoucherCategoryCode = categoryVM.VoucherCategoryCode,
                        VoucherCategoryName = categoryVM.VoucherCategoryName,
                        Active = categoryVM.Active,
                        CreatedOn = DateTime.Now,
                        CreatedBy = categoryVM.CreatedBy
                    };
                    await uow.Repository<VoucherCategory>().AddAsync(category);
                    await uow.SaveAsync();
                    response.Message = "Voucher Category Successfully Save";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    VoucherCategory existingCategory = new VoucherCategory();
                    existingCategory = uow.Repository<VoucherCategory>().Get(x => x.VoucherCategoryId == categoryVM.VoucherCategoryId).FirstOrDefault();
                    if (existingCategory != null)
                    {
                        existingCategory.VoucherCategoryCode = categoryVM.VoucherCategoryCode;
                        existingCategory.VoucherCategoryName = categoryVM.VoucherCategoryName;
                        existingCategory.Active = categoryVM.Active;
                        existingCategory.ModifiedOn = DateTime.Now;
                        existingCategory.ModifiedBy = categoryVM.ModifiedBy;
                        uow.Repository<VoucherCategory>().Update(existingCategory);
                        await uow.SaveAsync();
                        response.Message = "Voucher Category Successfully Updated";
                        response.Status = ResponseStatus.OK;
                    }

                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<List<VoucherCategoryVM>> GetvoucherCategoryList()
        {
            List<VoucherCategoryVM> voucherCategories = new List<VoucherCategoryVM>();
            try
            {
                SqlParameter[] sqlParameters = {

                };
                voucherCategories = uow.ExecuteReaderSingleDS<VoucherCategoryVM>("GetVoucherCategoryList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return voucherCategories;
        }

        public async Task<Response> DeleteVoucherCategory(int voucherCategoryId)
        {
            try
            {
                Response response = new Response();

                if (voucherCategoryId > 0)
                {
                    await uow.Repository<VoucherCategory>().DeleteAsync(voucherCategoryId);
                    await uow.SaveAsync();

                    response.Message = "Data Successfully Deleted";
                    response.Status = ResponseStatus.OK;
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();

            }
        }

        public List<VoucherTypeVM> GetVoucherTypeList()
        {
            List<VoucherTypeVM> voucherCategories = new List<VoucherTypeVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                };
                voucherCategories = uow.ExecuteReaderSingleDS<VoucherTypeVM>("GetVoucherTypeList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return voucherCategories;
        }

        public async Task<Response> AddUpdateVoucherType(VoucherTypeVM voucherType)
        {
            try
            {
                Response response = new Response();
                if (voucherType.VoucherTypeId == 0)
                {
                    VoucherType type = new VoucherType()
                    {
                        VoucherTypeCode = voucherType.VoucherTypeCode,
                        VoucherTypeName = voucherType.VoucherTypeName,
                        VoucherCategoryId = voucherType.VoucherCategoryId,
                        Active = voucherType.Active,
                        CreatedOn = DateTime.Now,
                        CreatedBy = voucherType.CreatedBy
                    };
                    await uow.Repository<VoucherType>().AddAsync(type);
                    await uow.SaveAsync();
                    response.Message = "Voucher Type Successfully Save";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    VoucherType existingType = new VoucherType();
                    existingType = uow.Repository<VoucherType>().Get(x => x.VoucherTypeId == voucherType.VoucherTypeId).FirstOrDefault();
                    if (existingType != null)
                    {
                        existingType.VoucherTypeCode = voucherType.VoucherTypeCode;
                        existingType.VoucherTypeName = voucherType.VoucherTypeName;
                        existingType.VoucherCategoryId = voucherType.VoucherCategoryId;
                        existingType.Active = voucherType.Active;
                        existingType.ModifiedOn = DateTime.Now;
                        existingType.ModifiedBy = voucherType.ModifiedBy;
                        uow.Repository<VoucherType>().Update(existingType);
                        await uow.SaveAsync();
                        response.Message = "Voucher Type Successfully Updated";
                        response.Status = ResponseStatus.OK;
                    }

                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> DeleteVoucherType(int voucherTypeId)
        {
            try
            {
                Response response = new Response();

                if (voucherTypeId > 0)
                {
                    await uow.Repository<VoucherType>().DeleteAsync(voucherTypeId);
                    await uow.SaveAsync();

                    response.Message = "Data Successfully Deleted";
                    response.Status = ResponseStatus.OK;
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();

            }
        }

        public List<VoucherBookVM> GetVoucherBookList()
        {
            List<VoucherBookVM> voucherBookVMs = new List<VoucherBookVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                };
                voucherBookVMs = uow.ExecuteReaderSingleDS<VoucherBookVM>("GetVoucherBookList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return voucherBookVMs;
        }

        public async Task<Response> DeleteVoucherBook(int voucherBookId)
        {
            try
            {
                Response response = new Response();

                if (voucherBookId > 0)
                {
                    await uow.Repository<VoucherBook>().DeleteAsync(voucherBookId);
                    await uow.SaveAsync();

                    response.Message = "Data Successfully Deleted";
                    response.Status = ResponseStatus.OK;
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();

            }
        }

        public Response AddUpdateVoucherBook(VoucherBookVM voucherBook)
        {
            try
            {
                Response response = new Response();
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@VoucherBookId",voucherBook.VoucherBookId),
                new SqlParameter("@VoucherBookName",voucherBook.VoucherBookName),
                new SqlParameter("@VoucherTypeId",voucherBook.VoucherTypeId),
                new SqlParameter("@NoOfLeaves",voucherBook.NoOfLeaves),
                new SqlParameter("@NoOfPages",voucherBook.NoOfPages),
                new SqlParameter("@BookLevelAmountDiscount",voucherBook.BookLevelAmountDiscount),
                new SqlParameter("@BookLevelPersentageDiscount",voucherBook.BookLevelPersentageDiscount),
                new SqlParameter("@ValidFrom",voucherBook.ValidFrom),
                new SqlParameter("@ValidTo",voucherBook.ValidTo),
                new SqlParameter("@IsAssigned",voucherBook.IsAssigned),
                new SqlParameter("@Active",voucherBook.Active),
                new SqlParameter("@CreatedBy",voucherBook.CreatedBy),
                new SqlParameter("@ModifiedBy",voucherBook.ModifiedBy),
            };
                uow.ExecuteReaderSingleDS<Response>("Sp_UpdateAddVoucherBook_test", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Saved";
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public List<VoucherBookAllocationVM> GetVouchrBookAllocation()
        {
            List<VoucherBookAllocationVM> VoucherBookAllocation = new List<VoucherBookAllocationVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                };
                VoucherBookAllocation = uow.ExecuteReaderSingleDS<VoucherBookAllocationVM>("Sp_GetVoucherBookAllocationList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return VoucherBookAllocation;
        }

        public async Task<Response> DeleteVoucherBookAllocation(int voucherBookAllocationId)
        {
            try
            {
                Response response = new Response();

                if (voucherBookAllocationId > 0)
                {
                    await uow.Repository<VoucherBookAllocation>().DeleteAsync(voucherBookAllocationId);
                    await uow.SaveAsync();

                    response.Message = "Data Successfully Deleted";
                    response.Status = ResponseStatus.OK;
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();

            }
        }

        public Response AddUpdateVoucherBookAllocation(VoucherBookAllocationVM voucherBookAllocation)
        {
            try
            {
                Response response = new Response();
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@VoucherBookAllocationId",voucherBookAllocation.VoucherBookAllocationId),
                new SqlParameter("@VoucherBookId",voucherBookAllocation.VoucherBookId),
                new SqlParameter("@AssigneeFirstName",voucherBookAllocation.AssigneeFirstName),
                new SqlParameter("@AssigneeLastName",voucherBookAllocation.AssigneeLastName),
                new SqlParameter("@ContactNo",voucherBookAllocation.ContactNo),
                new SqlParameter("@IsInternalPerson",voucherBookAllocation.IsInternalPerson),
                new SqlParameter("@EmployDesignation",voucherBookAllocation.EmployDesignation),
                new SqlParameter("@ExternalPersonOccupation",voucherBookAllocation.ExternalPersonOccupation),
                new SqlParameter("@ExternalDesignation",voucherBookAllocation.ExternalDesignation),
                new SqlParameter("@Company",voucherBookAllocation.Company),
                new SqlParameter("@NopagesAssigned",voucherBookAllocation.NopagesAssigned),
                new SqlParameter("@NoOfLeavesAssigned",voucherBookAllocation.NoOfLeavesAssigned),
                new SqlParameter("@Active",voucherBookAllocation.Active),
                new SqlParameter("@CreatedBy",voucherBookAllocation.CreatedBy),
                new SqlParameter("@ModifiedBy",voucherBookAllocation.ModifiedBy),

            };
                uow.ExecuteReaderSingleDS<Response>("Sp_UpdateAddVoucherBookAllocation", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Saved";
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public List<VoucherBookPagesVM> GetVoucherBookPagesList()
        {
            List<VoucherBookPagesVM> voucherBookpages = new List<VoucherBookPagesVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                };
                voucherBookpages = uow.ExecuteReaderSingleDS<VoucherBookPagesVM>("Sp_GetVoucherBookPages", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return voucherBookpages;
        }

        public List<VoucherBookLeavesVM> GetVoucherBookLeavesList(VoucherBookLeavesVM voucherBookLeavesVM)
        {
            List<VoucherBookLeavesVM> voucherBookLeaves = new List<VoucherBookLeavesVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                };
                voucherBookLeaves = uow.ExecuteReaderSingleDS<VoucherBookLeavesVM>("Sp_GetVoucherBookLeavesList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return voucherBookLeaves;
        }

        public List<CategoryCommissionSetupVM> GetCategoryCommissionSetupList()
        {
            List<CategoryCommissionSetupVM> categoryCommission = new List<CategoryCommissionSetupVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                };
                categoryCommission = uow.ExecuteReaderSingleDS<CategoryCommissionSetupVM>("SP_GetCategoryCommissionSetup", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return categoryCommission;
        }

        public Response AddUpdateCategoryCommissionSetup(CategoryCommissionSetupVM categoryCommissionSetup)
        {
            try
            {
                Response response = new Response();
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@CategoryCommissionSetupId",categoryCommissionSetup.CategoryCommissionSetupId),
                new SqlParameter("@CategoryId",categoryCommissionSetup.CategoryId),
                new SqlParameter("@CommissionAmount",categoryCommissionSetup.CommisionAmount),
                new SqlParameter("@CommissionPercentage",categoryCommissionSetup.CommissionPercentage),
                new SqlParameter("@EntityStatus",categoryCommissionSetup.EntityStatus),
                new SqlParameter("@CreatedBy",categoryCommissionSetup.CreatedBy),
                new SqlParameter("@ModifiedBy",categoryCommissionSetup.ModifiedBy),
                new SqlParameter("@CommissionStartDate",categoryCommissionSetup.CommissionStartDate),
                new SqlParameter("@CommissionEndDate",categoryCommissionSetup.CommissionEndDate)
            };
                uow.ExecuteReaderSingleDS<Response>("Sp_AddUpdateCategoryCommissionSetup", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Saved";
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }

        public List<CategoryCommissionSetupVM> GetOverrideTradesmanCommissionList()
        {
            List<CategoryCommissionSetupVM> categoryCommission = new List<CategoryCommissionSetupVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                };
                categoryCommission = uow.ExecuteReaderSingleDS<CategoryCommissionSetupVM>("SP_GetOverrideTradesmanCommissionList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return categoryCommission;
        }

        public Response AddAndUpdateOverrideTradesmanCommission(CategoryCommissionSetupVM categoryCommissionSetup)
        {
            try
            {
                TradesmanCommissionOverride tradesmanCommissionOverride = new TradesmanCommissionOverride();
                Response response = new Response();
                if (categoryCommissionSetup.id <= 0 && categoryCommissionSetup.Action == "add")
                {
                    tradesmanCommissionOverride.CategoryCommissionSetupId = categoryCommissionSetup.CategoryCommissionSetupId;
                    tradesmanCommissionOverride.CommissionOverrideAmount = categoryCommissionSetup.CommisionAmount;
                    tradesmanCommissionOverride.CommissionOverridePercentage = categoryCommissionSetup.CommissionPercentage;
                    tradesmanCommissionOverride.CategoryId = categoryCommissionSetup.CategoryId;
                    tradesmanCommissionOverride.TradesmanId = categoryCommissionSetup.TradesmanId;
                    tradesmanCommissionOverride.ReferencePerson = categoryCommissionSetup.ReferencePerson;
                    tradesmanCommissionOverride.Active = categoryCommissionSetup.Active;
                    tradesmanCommissionOverride.CreatedBy = categoryCommissionSetup.CreatedBy;
                    tradesmanCommissionOverride.CreatedOn = DateTime.Now;
                    uow.Repository<TradesmanCommissionOverride>().Add(tradesmanCommissionOverride);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data saved successfully!";
                }
                else if (categoryCommissionSetup.id > 0 && categoryCommissionSetup.Action == "update")
                {
                    tradesmanCommissionOverride = uow.Repository<TradesmanCommissionOverride>().Get(x => x.TradesmanCommissionOverrideId == categoryCommissionSetup.id).FirstOrDefault();
                    if (tradesmanCommissionOverride != null)
                    {
                        tradesmanCommissionOverride.CategoryCommissionSetupId = categoryCommissionSetup.CategoryCommissionSetupId;
                        tradesmanCommissionOverride.CommissionOverrideAmount = categoryCommissionSetup.CommisionAmount;
                        tradesmanCommissionOverride.CommissionOverridePercentage = categoryCommissionSetup.CommissionPercentage;
                        tradesmanCommissionOverride.CategoryId = categoryCommissionSetup.CategoryId;
                        tradesmanCommissionOverride.TradesmanId = categoryCommissionSetup.TradesmanId;
                        tradesmanCommissionOverride.ReferencePerson = categoryCommissionSetup.ReferencePerson;
                        tradesmanCommissionOverride.Active = categoryCommissionSetup.Active;
                        tradesmanCommissionOverride.ModifiedBy = categoryCommissionSetup.ModifiedBy;
                        tradesmanCommissionOverride.ModifiedOn = DateTime.Now;
                        uow.Repository<TradesmanCommissionOverride>().Update(tradesmanCommissionOverride);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Data saved successfully!";

                    }
                }
                else if (categoryCommissionSetup.id > 0 && categoryCommissionSetup.Action == "delete")
                {
                    tradesmanCommissionOverride = uow.Repository<TradesmanCommissionOverride>().Get(x => x.TradesmanCommissionOverrideId == categoryCommissionSetup.id).FirstOrDefault();
                    if (tradesmanCommissionOverride != null)
                    {
                        tradesmanCommissionOverride.Active = tradesmanCommissionOverride.Active == true ? false : true;
                        tradesmanCommissionOverride.ModifiedBy = categoryCommissionSetup.ModifiedBy;
                        tradesmanCommissionOverride.ModifiedOn = DateTime.Now;
                        uow.Repository<TradesmanCommissionOverride>().Update(tradesmanCommissionOverride);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Data saved successfully!";
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }

        public List<LeadgerTransection> GetWalletHistory(long subAccountId)
        {
            List<LeadgerTransection> leadgerTransections = new List<LeadgerTransection>();
            try
            {
                leadgerTransections = uow.Repository<LeadgerTransection>().GetAll().Where(x => x.SubAccountId == subAccountId && x.ReffrenceDocumentType != "Order").ToList();

                return leadgerTransections;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<LeadgerTransection>();
            }
        }

        public async Task<Response> DeleteCategoryCommissionSetup(int categoryCommissionId)
        {
            try
            {
                Response response = new Response();

                if (categoryCommissionId > 0)
                {
                    await uow.Repository<CategoryCommissionSetup>().DeleteAsync(categoryCommissionId);
                    await uow.SaveAsync();

                    response.Message = "Data Successfully Deleted";
                    response.Status = ResponseStatus.OK;
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public TradesmanJobReceipts GetTradesmanJobReceiptsByTradesmanId(long tradesmanId, long jobDetailId)
        {
            try
            {
                var result = uow.Repository<TradesmanJobReceipts>().Get(x => x.TradesmanId == tradesmanId && x.JobDetailId == jobDetailId).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new TradesmanJobReceipts();
            }

        }

        public Response JazzCashAcknowledgementReceipt(JazzCashAcknowledgementReceiptVM txnDetail)
        {
            try
            {
                Response response = new Response();
                uow.ExecuteReaderSingleDS<Response>("SP_AddJazzCashAcknowledgementReceipt", txnDetail.ToSqlParamsArray());
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Saved";
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public Response GetWalletValueByBidId(long customerId, long bidId)
        {
            Response response = new Response();
            try
            {
                LeadgerTransection leadgerTransection = new LeadgerTransection();

                leadgerTransection = uow.Repository<LeadgerTransection>().Get(x => x.RefCustomerSubAccountId == customerId && x.ReffrenceDocumentId == bidId && x.ReffrenceDocumentType == "Paid By Wallet" && x.SubAccountId != 3).OrderByDescending(a => a.CreatedOn).FirstOrDefault();
                if (leadgerTransection != null)
                {
                    response.Status = ResponseStatus.OK;
                    response.ResultData = leadgerTransection.Credit;
                    response.Message = leadgerTransection.LeadgerTransectionId.ToString();
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public Response AddAndUpdateAccountType(AccountsVM accountsVM)
        {
            try
            {
                PackagesAndPaymentsModels.AccountType accountType = new PackagesAndPaymentsModels.AccountType();
                Response response = new Response();
                if (accountsVM.id <= 0 && accountsVM.Action == "add")
                {
                    accountType.AccountTypeCode = accountsVM.AccountTypeCode;
                    accountType.AccounTypeName = accountsVM.AccounTypeName;
                    accountType.Active = accountsVM.Active;
                    accountType.CreatedBy = accountsVM.CreatedBy;
                    accountType.CreatedOn = DateTime.Now;
                    uow.Repository<PackagesAndPaymentsModels.AccountType>().Add(accountType);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data saved successfully!";
                }
                else if (accountsVM.id > 0 && accountsVM.Action == "update")
                {
                    var getSingleRecord = uow.Repository<PackagesAndPaymentsModels.AccountType>().GetById(accountsVM.id);
                    if (getSingleRecord != null)
                    {
                        getSingleRecord.AccountTypeCode = accountsVM.AccountTypeCode;
                        getSingleRecord.AccounTypeName = accountsVM.AccounTypeName;
                        getSingleRecord.Active = accountsVM.Active;
                        getSingleRecord.ModifiedBy = accountsVM.ModifiedBy;
                        getSingleRecord.ModifiedOn = DateTime.Now;
                        uow.Repository<PackagesAndPaymentsModels.AccountType>().Update(getSingleRecord);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Data saved successfully!";

                    }
                }
                else if (accountsVM.id > 0 && accountsVM.Action == "delete")
                {
                    var getSingleRecord = uow.Repository<PackagesAndPaymentsModels.AccountType>().GetById(accountsVM.id);
                    if (getSingleRecord != null)
                    {
                        getSingleRecord.Active = getSingleRecord.Active == true ? false : true;
                        getSingleRecord.ModifiedBy = accountsVM.ModifiedBy;
                        getSingleRecord.ModifiedOn = DateTime.Now;
                        uow.Repository<PackagesAndPaymentsModels.AccountType>().Update(getSingleRecord);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Data saved successfully!";
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<AccountsVM> GetAccountTypeList()
        {
            List<AccountsVM> accountsVM = new List<AccountsVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                };
                accountsVM = uow.ExecuteReaderSingleDS<AccountsVM>("SP_GetAccountType", sqlParameters).ToList();
                return accountsVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<AccountsVM>();
            }
        }
        public Response AddAndUpdateAccount(AccountsVM accountsVM)
        {
            try
            {
                Account account = new Account();
                Response response = new Response();
                if (accountsVM.id <= 0 && accountsVM.Action == "add")
                {
                    account.AccountNo = accountsVM.AccountNo.ToString();
                    account.AccountName = accountsVM.AccountName;
                    account.AccountTypeId = Convert.ToInt32(accountsVM.AccountTypeId);
                    account.Active = accountsVM.Active;
                    account.CreatedBy = accountsVM.CreatedBy;
                    account.CreatedOn = DateTime.Now;
                    uow.Repository<Account>().Add(account);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data saved successfully!";
                }
                else if (accountsVM.id > 0 && accountsVM.Action == "update")
                {
                    var getSingleRecord = uow.Repository<Account>().GetById(accountsVM.id);
                    if (getSingleRecord != null)
                    {
                        getSingleRecord.AccountNo = accountsVM.AccountNo.ToString();
                        getSingleRecord.AccountName = accountsVM.AccountName;
                        getSingleRecord.AccountTypeId = Convert.ToInt32(accountsVM.AccountTypeId);
                        getSingleRecord.Active = accountsVM.Active;
                        getSingleRecord.ModifiedBy = accountsVM.ModifiedBy;
                        getSingleRecord.ModifiedOn = DateTime.Now;
                        uow.Repository<Account>().Update(getSingleRecord);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Data saved successfully!";

                    }
                }
                else if (accountsVM.id > 0 && accountsVM.Action == "delete")
                {
                    var getSingleRecord = uow.Repository<Account>().GetById(accountsVM.id);
                    if (getSingleRecord != null)
                    {
                        getSingleRecord.Active = getSingleRecord.Active == true ? false : true;
                        getSingleRecord.ModifiedBy = accountsVM.ModifiedBy;
                        getSingleRecord.ModifiedOn = DateTime.Now;
                        uow.Repository<Account>().Update(getSingleRecord);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Data saved successfully!";
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<AccountsVM> GetAccountList()
        {
            List<AccountsVM> accountsVM = new List<AccountsVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                };
                accountsVM = uow.ExecuteReaderSingleDS<AccountsVM>("SP_GetAccountList", sqlParameters).ToList();
                return accountsVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<AccountsVM>();
            }
        }
        public Response AddAndUpdateSubAccount(AccountsVM accountsVM)
        {
            try
            {
                SubAccount subAccount = new SubAccount();
                Response response = new Response();
                if (accountsVM.id <= 0 && accountsVM.Action == "add")
                {
                    subAccount.AccountId = accountsVM.AccountId;
                    subAccount.SubAccountName = accountsVM.SubAccountName;
                    subAccount.SubAccountNo = accountsVM.SubAccountNo;
                    subAccount.CustomerId = accountsVM.CustomerId;
                    subAccount.CustomerName = accountsVM.CustomerName;
                    subAccount.TradesmanId = accountsVM.TradesmanId;
                    subAccount.TradesmanName = accountsVM.TradesmanName;
                    subAccount.SupplierId = accountsVM.SupplierId;
                    subAccount.SupplierName = accountsVM.SupplierName;
                    subAccount.Active = accountsVM.Active;
                    subAccount.CreatedBy = accountsVM.CreatedBy;
                    subAccount.CreatedOn = DateTime.Now;
                    uow.Repository<SubAccount>().Add(subAccount);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data saved successfully!";
                }
                else if (accountsVM.id > 0 && accountsVM.Action == "update")
                {
                    var getSingleRecord = uow.Repository<SubAccount>().GetById(accountsVM.id);
                    if (getSingleRecord != null)
                    {
                        getSingleRecord.AccountId = accountsVM.AccountId;
                        getSingleRecord.SubAccountName = accountsVM.SubAccountName;
                        getSingleRecord.SubAccountNo = accountsVM.SubAccountNo;
                        getSingleRecord.CustomerId = accountsVM.CustomerId;
                        getSingleRecord.CustomerName = accountsVM.CustomerName;
                        getSingleRecord.TradesmanId = accountsVM.TradesmanId;
                        getSingleRecord.TradesmanName = accountsVM.TradesmanName;
                        getSingleRecord.SupplierId = accountsVM.SupplierId;
                        getSingleRecord.SupplierName = accountsVM.SupplierName;
                        getSingleRecord.Active = accountsVM.Active;
                        getSingleRecord.ModifiedBy = accountsVM.ModifiedBy;
                        getSingleRecord.ModifiedOn = DateTime.Now;
                        uow.Repository<SubAccount>().Update(getSingleRecord);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Data saved successfully!";

                    }
                }
                else if (accountsVM.id > 0 && accountsVM.Action == "delete")
                {
                    var getSingleRecord = uow.Repository<SubAccount>().GetById(accountsVM.id);
                    if (getSingleRecord != null)
                    {
                        getSingleRecord.Active = getSingleRecord.Active == true ? false : true;
                        getSingleRecord.ModifiedBy = accountsVM.ModifiedBy;
                        getSingleRecord.ModifiedOn = DateTime.Now;
                        uow.Repository<SubAccount>().Update(getSingleRecord);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Data saved successfully!";
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<AccountsVM> GetSubAccountList(SearchSubAccountVM accountsVMs)
        {
            List<AccountsVM> accountsVM = new List<AccountsVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@pageSize",accountsVMs.pageSize),
                    new SqlParameter("@pageNumber",accountsVMs.pageNumber),
                    new SqlParameter("@subAccountName",accountsVMs?.SubAccountName ?? ""),
                    new SqlParameter("@startDate",accountsVMs?.startDate ?? ""),
                    new SqlParameter("@endDate",accountsVMs?.endDate ?? ""),
                    new SqlParameter("@userId",accountsVMs.UserId),
                    new SqlParameter("@userName",accountsVMs?.UserName ?? ""),
                    new SqlParameter("@subAccountNo",accountsVMs?.SubAccountNo),
                };
                accountsVM = uow.ExecuteReaderSingleDS<AccountsVM>("SP_GetSubAccountList", sqlParameters).ToList();
                return accountsVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<AccountsVM>();
            }
        }
        public Response AddAndUpdateReferral(ReferralVM referralVM)
        {
            try
            {
                Referral referral = new Referral();
                Response response = new Response();
                if (referralVM.id <= 0 && referralVM.Action == "add")
                {
                    referral.Role = referralVM.Role;
                    referral.Amount = referralVM.Amount;
                    referral.Limit = referralVM.Limit;
                    referral.StartingFrom = referralVM.StartingFrom;
                    referral.EndedAt = referralVM.EndedAt;
                    referral.Active = referralVM.Active;
                    referral.CreatedBy = referralVM.CreatedBy;
                    referral.CreateOn = DateTime.Now;
                    uow.Repository<Referral>().Add(referral);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data saved successfully!";
                }
                else if (referralVM.id > 0 && referralVM.Action == "update")
                {
                    referral = uow.Repository<Referral>().GetById(referralVM.id);
                    if (referral != null)
                    {
                        referral.Role = referralVM.Role;
                        referral.Amount = referralVM.Amount;
                        referral.Limit = referralVM.Limit;
                        referral.StartingFrom = referralVM.StartingFrom;
                        referral.EndedAt = referralVM.EndedAt;
                        referral.Active = referralVM.Active;
                        referral.ModifiedBy = referralVM.ModifiedBy;
                        referral.ModifiedOn = DateTime.Now;
                        uow.Repository<Referral>().Update(referral);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Data saved successfully!";

                    }
                }
                else if (referralVM.id > 0 && referralVM.Action == "delete")
                {
                    referral = uow.Repository<Referral>().GetById(referralVM.id);
                    if (referral != null)
                    {
                        referral.Active = referral.Active == true ? false : true;
                        referral.ModifiedBy = referralVM.ModifiedBy;
                        referral.ModifiedOn = DateTime.Now;
                        uow.Repository<Referral>().Update(referral);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Data saved successfully!";
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<ReferralVM> GetReferralList()
        {
            List<ReferralVM> accountsVM = new List<ReferralVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                };
                accountsVM = uow.ExecuteReaderSingleDS<ReferralVM>("SP_GetReferralList", sqlParameters).ToList();
                return accountsVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ReferralVM>();
            }
        }

        public Response getSubAccountRecord(long id)
        {
            try
            {
                Response response = new Response();
                SubAccount subAccount = new SubAccount();
                string subId = Convert.ToString(id);
                subAccount = uow.Repository<SubAccount>().Get(x => x.SubAccountNo == subId).FirstOrDefault();

                if (subAccount == null)
                {
                    response.Status = ResponseStatus.OK;
                    response.Message = "You can create new Sub Account.";
                }
                else
                {
                    response.Status = ResponseStatus.Restrected;
                    response.Message = "Sub Account is already Created.";
                }
                return response;
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> AddPaymentWithdrawalRequest(PaymentWithdrawalRequestVM paymentWithdrawalRequestVM)
        {
            try
            {
                Response response = new Response();
                WithdrawalRequest withdrawalRequest = new WithdrawalRequest();

                if (paymentWithdrawalRequestVM != null)
                {
                    if (paymentWithdrawalRequestVM.Role == Utility.UserRoles.Customer)
                    {
                        withdrawalRequest.CustomerId = paymentWithdrawalRequestVM.TradesmanId;
                        withdrawalRequest.CustomerName = paymentWithdrawalRequestVM.TradesmanName;
                        withdrawalRequest.PhoneNumber = paymentWithdrawalRequestVM.PhoneNumber;
                        withdrawalRequest.Cnic = paymentWithdrawalRequestVM.Cnic;
                        withdrawalRequest.Amount = paymentWithdrawalRequestVM.Amount;
                        withdrawalRequest.CreatedOn = DateTime.Now;
                        withdrawalRequest.CreatedBy = paymentWithdrawalRequestVM.CreatedBy;
                        withdrawalRequest.PaymentStatusId = (int)WithdravalPaymentStatus.Pending;
                        withdrawalRequest.Role = paymentWithdrawalRequestVM.Role;
                    }
                    else if (paymentWithdrawalRequestVM.Role == Utility.UserRoles.Tradesman)
                    {
                        withdrawalRequest.TradesmanId = paymentWithdrawalRequestVM.TradesmanId;
                        withdrawalRequest.TradesmanName = paymentWithdrawalRequestVM.TradesmanName;
                        withdrawalRequest.PhoneNumber = paymentWithdrawalRequestVM.PhoneNumber;
                        withdrawalRequest.Cnic = paymentWithdrawalRequestVM.Cnic;
                        withdrawalRequest.Amount = paymentWithdrawalRequestVM.Amount;
                        withdrawalRequest.CreatedOn = DateTime.Now;
                        withdrawalRequest.CreatedBy = paymentWithdrawalRequestVM.CreatedBy;
                        withdrawalRequest.PaymentStatusId = (int)WithdravalPaymentStatus.Pending;
                        withdrawalRequest.Role = paymentWithdrawalRequestVM.Role;
                    }
                    else if (paymentWithdrawalRequestVM.Role == Utility.UserRoles.Supplier)
                    {
                        withdrawalRequest.SupplierId = paymentWithdrawalRequestVM.TradesmanId;
                        withdrawalRequest.SupplierName = paymentWithdrawalRequestVM.TradesmanName;
                        withdrawalRequest.PhoneNumber = paymentWithdrawalRequestVM.PhoneNumber;
                        withdrawalRequest.Cnic = paymentWithdrawalRequestVM.Cnic;
                        withdrawalRequest.Amount = paymentWithdrawalRequestVM.Amount;
                        withdrawalRequest.CreatedOn = DateTime.Now;
                        withdrawalRequest.CreatedBy = paymentWithdrawalRequestVM.CreatedBy;
                        withdrawalRequest.PaymentStatusId = (int)WithdravalPaymentStatus.Pending;
                        withdrawalRequest.Role = paymentWithdrawalRequestVM.Role;
                    }
                    await uow.Repository<WithdrawalRequest>().AddAsync(withdrawalRequest);
                    await uow.SaveAsync();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data added Successfully Saved !!";
                }
                return response;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public List<PaymentWithdrawalRequestVM> GetWithdrawalRequestList(int paymentStatus, long tradesmanId, string phoneNumber,string role)
        {
            List<PaymentWithdrawalRequestVM> paymentWithdrawal = new List<PaymentWithdrawalRequestVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                     new SqlParameter("@paymentStatus",paymentStatus),
                     new SqlParameter("@tradesmanId",tradesmanId),
                     new SqlParameter("@phoneNumber",phoneNumber),
                     new SqlParameter("@role",role)
                };
                paymentWithdrawal = uow.ExecuteReaderSingleDS<PaymentWithdrawalRequestVM>("SP_GetWithdrawalRequestList", sqlParameters).ToList();
                return paymentWithdrawal;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<PaymentWithdrawalRequestVM>();
            }
        }

        public async Task<Response> UpdateWithdrawalRequestStatus(int withdrawalRequestId, string id)
        {
            try
            {
                Response response = new Response();
                WithdrawalRequest withdrawalRequest = new WithdrawalRequest();

                if (withdrawalRequestId > 0 && !string.IsNullOrWhiteSpace(id))
                {

                    withdrawalRequest = uow.Repository<WithdrawalRequest>().Get(x => x.WithdrawalRequestId == withdrawalRequestId).FirstOrDefault();

                    if (withdrawalRequest != null)
                    {
                        withdrawalRequest.PaymentStatusId = (int)WithdravalPaymentStatus.Completed;
                        withdrawalRequest.ModifiedOn = DateTime.Now;
                        withdrawalRequest.ModifiedBy = id;
                    }

                    uow.Repository<WithdrawalRequest>().Update(withdrawalRequest);
                    await uow.SaveAsync();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Successfully Updated";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "null value";
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<Response> DeclineWithdrawRequest(int withdrawalRequestId, string id)
        {
            try
            {
                Response response = new Response();
                if (withdrawalRequestId > 0 && !string.IsNullOrWhiteSpace(id))
                {
                    WithdrawalRequest withdrawalRequest = new WithdrawalRequest();
                    withdrawalRequest = uow.Repository<WithdrawalRequest>().Get(x => x.WithdrawalRequestId == withdrawalRequestId).FirstOrDefault();
                    if (withdrawalRequest != null)
                    {
                        withdrawalRequest.PaymentStatusId = (int)WithdravalPaymentStatus.Declined;
                        withdrawalRequest.ModifiedOn = DateTime.Now;
                        withdrawalRequest.ModifiedBy = id;
                    }
                    uow.Repository<WithdrawalRequest>().Update(withdrawalRequest);
                    await uow.SaveAsync();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Successfully declined";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Something went wrong!";
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public WithdrawalRequest GetPaymentWithdrawalRequestByTradesmanId(long tradesmanId, string role)
        {
            try
            {
                WithdrawalRequest withdrawalRequest = new WithdrawalRequest();
                if (role == "Tradesman")
                {
                    withdrawalRequest = uow.Repository<WithdrawalRequest>().Get(x => x.TradesmanId == tradesmanId && x.PaymentStatusId == 1).FirstOrDefault();
                }
                else if (role == "Customer")
                {
                    withdrawalRequest = uow.Repository<WithdrawalRequest>().Get(x => x.CustomerId == tradesmanId && x.PaymentStatusId == 1).FirstOrDefault();
                }
                else
                {
                    withdrawalRequest = uow.Repository<WithdrawalRequest>().Get(x => x.SupplierId == tradesmanId && x.PaymentStatusId == 1).FirstOrDefault();
                }
                return withdrawalRequest;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new WithdrawalRequest();
            }

        }

        public Response getuseofWalletValue(long customerId, long jqId)
        {
            Response response = new Response();
            try
            {
                LeadgerTransection leadgerTransection = new LeadgerTransection();

                leadgerTransection = uow.Repository<LeadgerTransection>().Get(x => x.RefCustomerSubAccountId == customerId && x.ReffrenceDocumentNo == jqId && x.ReffrenceDocumentType == "Paid By Wallet").FirstOrDefault();
                if (leadgerTransection != null)
                {
                    response.Status = ResponseStatus.OK;
                    response.ResultData = leadgerTransection.Credit;
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public Response getJazzCashValue(long customerId, long jqId)
        {
            Response response = new Response();
            try
            {
                LeadgerTransection leadgerTransection = new LeadgerTransection();

                leadgerTransection = uow.Repository<LeadgerTransection>().Get(x => x.RefCustomerSubAccountId == customerId && x.ReffrenceDocumentNo == jqId && x.ReffrenceDocumentType == "JazzCash Deposit").FirstOrDefault();
                if (leadgerTransection != null)
                {
                    response.Status = ResponseStatus.OK;
                    response.ResultData = leadgerTransection.Debit;
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public Response GetJazzCashByBidId(long customerId, long bidId)
        {
            Response response = new Response();
            try
            {
                LeadgerTransection leadgerTransection = new LeadgerTransection();

                leadgerTransection = uow.Repository<LeadgerTransection>().Get(x => x.RefCustomerSubAccountId == customerId && x.ReffrenceDocumentId == bidId && x.ReffrenceDocumentType == "JazzCash Deposit" && x.SubAccountId != 3).OrderByDescending(a => a.CreatedOn).FirstOrDefault();
                if (leadgerTransection != null)
                {
                    response.Status = ResponseStatus.OK;
                    response.ResultData = leadgerTransection.Debit;
                    response.Message = leadgerTransection.LeadgerTransectionId.ToString();
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }


        public Response DeletePromotionEntry(long jqId)
        {
            Response response = new Response();
            PromotionRedemptions promotionRedeem = new PromotionRedemptions();
            try
            {
                if (jqId > 0)
                {
                    PromotionRedemptions getPromotionById = uow.Repository<PromotionRedemptions>().GetAll().Where(a => a.JobQuotationId == jqId).FirstOrDefault();
                    if (getPromotionById != null)
                    {

                        uow.Repository<PromotionRedemptions>().Delete(getPromotionById);
                        uow.Save();
                        response.Message = "Data Deleted Successfully!";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "Something went wrong";
                        response.Status = ResponseStatus.Error;
                    }

                }
                else
                {
                    response.Message = "Something went wrong";
                    response.Status = ResponseStatus.Error;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public List<GetLeadgerReportVM> GetLeaderReport(GetLeadgerReportVM getLeadgerReportVM)
        {
            List<GetLeadgerReportVM> leadgerReport = new List<GetLeadgerReportVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                     new SqlParameter("@customerId",getLeadgerReportVM.CustomerId),
                     new SqlParameter("@tradesmanId",getLeadgerReportVM.TradesmanId),
                     new SqlParameter("@jobQuotationId",getLeadgerReportVM.JobQuotationId),
                     new SqlParameter("@jobDetailId",getLeadgerReportVM.JobDetailId),
                     new SqlParameter("@fromDate",getLeadgerReportVM?.StartDate),
                     new SqlParameter("@toDate",getLeadgerReportVM?.EndDate),
                };
                leadgerReport = uow.ExecuteReaderSingleDS<GetLeadgerReportVM>("SP_GetLeadger", sqlParameters).ToList();
                return leadgerReport;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<GetLeadgerReportVM>();
            }
        }

        public List<GeneralLedgerTransactionVM> GetLedgerTransectionReportByAccountRef(GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            List<GeneralLedgerTransactionVM> generalLedgerTransactions = new List<GeneralLedgerTransactionVM>();
            try
            {
                SqlParameter[] sqlParameters = {

                  new SqlParameter("@accountId",generalLedgerTransaction.AccountId),
                  new SqlParameter("@refCustomerSubAccountId",generalLedgerTransaction.RefCustomerSubAccountId),
                  new SqlParameter("@refTradesmanSubAccountId",generalLedgerTransaction.RefTradesmanSubAccountId),

                };

                generalLedgerTransactions = uow.ExecuteReaderSingleDS<GeneralLedgerTransactionVM>("Sp_GetLeadertransectionByAccountandRef", sqlParameters).ToList();

                return generalLedgerTransactions;
            }

            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<GeneralLedgerTransactionVM>();
            }

        }

        public Response AddAndUpdateGeneralLedgerTransection(GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            Response response = new Response();

            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@accountId",generalLedgerTransaction.AccountId),
                    new SqlParameter("@subAccountId ",generalLedgerTransaction.SubAccountId),
                  new SqlParameter("@refCustomerSubAccountId",generalLedgerTransaction.RefCustomerSubAccountId),
                  new SqlParameter("@refTradesmanSubAccountId",generalLedgerTransaction.RefTradesmanSubAccountId),
                   new SqlParameter("@refSupplierSubAccountId",generalLedgerTransaction.RefSupplierSubAccountId),
                  new SqlParameter("@debit",generalLedgerTransaction.Debit),
                  new SqlParameter("@credit",generalLedgerTransaction.Credit),
                  new SqlParameter("@reffrenceDocumentNo",generalLedgerTransaction.ReffrenceDocumentNo),
                  new SqlParameter("@reffrenceDocumentId",generalLedgerTransaction.ReffrenceDocumentId),
                  new SqlParameter("@reffrenceDocumentType",generalLedgerTransaction.ReffrenceDocumentType),
                   new SqlParameter("@active",generalLedgerTransaction.Active),
                  new SqlParameter("@createdOn",generalLedgerTransaction.CreatedOn),
                  new SqlParameter("@createdBy",generalLedgerTransaction.CreatedBy),
                  new SqlParameter("@modifiedOn",generalLedgerTransaction.ModifiedOn),
                   new SqlParameter("@modifiedBy",generalLedgerTransaction.ModifiedBy),
                };

                uow.ExecuteReaderSingleDS<GeneralLedgerTransactionVM>("Sp_AddUpdateLeadgertransection", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Ledger Transection added Successfully !!";

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }

        public List<GeneralLedgerTransactionVM> GetDetailedLedgerTransectionReportByAccountRef(GeneralLedgerTransactionVM generalLedgerTransaction)
        {
            List<GeneralLedgerTransactionVM> generalLedgerTransactions = new List<GeneralLedgerTransactionVM>();
            try
            {
                SqlParameter[] sqlParameters = {

                  new SqlParameter("@accountId",generalLedgerTransaction.AccountId),
                  new SqlParameter("@fromDate",generalLedgerTransaction.FromDate),
                  new SqlParameter("@toDate",generalLedgerTransaction.ToDate),
                     new SqlParameter("@userId",generalLedgerTransaction.userId),
                };

                generalLedgerTransactions = uow.ExecuteReaderSingleDS<GeneralLedgerTransactionVM>("Sp_GetGLLeadger", sqlParameters).ToList();

                return generalLedgerTransactions;
            }

            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<GeneralLedgerTransactionVM>();
            }

        }

        public async Task<Response> GetUserTransectionReport(string data)
        {
            Response response = new Response();

            try
            {
                var entity = JsonConvert.DeserializeObject<TransectionReportVM>(data);
                SqlParameter[] sqlParameters =
           {
                new SqlParameter("@UserType",entity.UserTypeId),
                 new SqlParameter("@UserId",entity.UserId),
                 new SqlParameter("@TransectionType", entity.TransectionType)
                };
                var userTransectionReportsList = uow.ExecuteReaderSingleDS<TransectionReportVM>("Sp_TransectionReport", sqlParameters).ToList();
                response.ResultData = userTransectionReportsList;
                response.Status = ResponseStatus.OK;

            }
            catch (Exception ex)
            {
                //  Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetAccountUsersName(int accountType)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@AccountType",accountType)
                };
                response.ResultData = uow.ExecuteReaderSingleDS<AccountsUsersNameVm>("Sp_GetAccountUsersName", sqlParameters).ToList();
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> ProceedToJazzCash(JazzCashPaymentDetailVM txnDetail)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@Version",txnDetail.pp_Version),
                    new SqlParameter("@TxnType",txnDetail.pp_TxnType),
                    new SqlParameter("@Language",txnDetail.pp_Language),
                    new SqlParameter("@MerchantID",txnDetail.pp_MerchantID),
                    new SqlParameter("@SubMerchantId",txnDetail.pp_SubMerchantID),
                    new SqlParameter("@Password",txnDetail.pp_Password),
                    new SqlParameter("@BankID",txnDetail.pp_BankID),
                    new SqlParameter("@ProductID",txnDetail.pp_ProductID),
                    new SqlParameter("@TxnCurrency",txnDetail.pp_TxnCurrency),
                    new SqlParameter("@TxnDateTime",txnDetail.pp_TxnDateTime),
                    new SqlParameter("@TxnExpiryDateTime",txnDetail.pp_TxnExpiryDateTime),
                    new SqlParameter("@SecureHash",txnDetail.pp_SecureHash),
                    new SqlParameter("@ppmpf_1",txnDetail.ppmpf_1),
                    new SqlParameter("@ppmpf_2",txnDetail.ppmpf_2),
                    new SqlParameter("@ppmpf_3",txnDetail.ppmpf_3),
                    new SqlParameter("@ppmpf_4",txnDetail.ppmpf_4),
                    new SqlParameter("@ppmpf_5",txnDetail.ppmpf_5),
                    new SqlParameter("@TxnRefNo",txnDetail.pp_TxnRefNo),
                    new SqlParameter("@Amount",txnDetail.pp_Amount),
                    new SqlParameter("@BillReference",txnDetail.pp_BillReference),
                    new SqlParameter("@Description",txnDetail.pp_Description),
                    new SqlParameter("@ReturnURL",txnDetail.pp_ReturnURL),
                    new SqlParameter("@CreatedBy",txnDetail.UserId)

                };
                await uow.ExecuteReaderSingleDSNew<Response>("SP_AddJazzCashMerchantDetails", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Saved";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = "Error";
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<JazzCashPaymentDetailVM> GetJazzCashMerchantDetails(string key)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@SecurityKey",key)
                };
                var merchantDetails = await uow.ExecuteReaderSingleDSNew<JazzCashPaymentDetailVM>("SP_GetJazzCashMerchantDetails", sqlParameters);
                return merchantDetails.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JazzCashPaymentDetailVM();
            }
        }

        public async Task<Response> AddNewPromotionTypeForSupplier(string data)
        {
            Response res = new Response();
            try
            {
                var promotionsTypes = JsonConvert.DeserializeObject<SupplierPromotionsTypes>(data);
                SqlParameter[] sqlParameters =
                  {
                    new SqlParameter("@PromotionTypeId", promotionsTypes.PromotionTypeId),
                    new SqlParameter("@PromotionTypeName",promotionsTypes.PromotionTypeName),
                    new SqlParameter("@PromotionTypeCode", promotionsTypes.PromotionTypeCode),
                    new SqlParameter("@IsActive",promotionsTypes.IsActive),
                    new SqlParameter("@EntityStatus",promotionsTypes.EntityStatus),
                    new SqlParameter("@CreatedBy",promotionsTypes.CreatedBy),
                    new SqlParameter("@UpdatedBy",promotionsTypes.UpdatedBy)
                 };
                var result = await uow.ExecuteReaderSingleDSNew<Response>("Sp_SupplierAddUpdatePromotionTypes", sqlParameters);
                res.Message = result.Select(x => x.Message).FirstOrDefault();
                res.Status = ResponseStatus.OK;
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                res.Status = ResponseStatus.Error;
                return res;
            }
        }
        public async Task<Response> GetPromotionTypeByIdForSupplier(int promotionTypeId)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                    new SqlParameter("@PromotionTypeId", promotionTypeId)
                 };
                var result = await uow.ExecuteReaderSingleDSNew<PromotionTypeDTO>("Sp_GetSupplierPromotionTypes", sqlParameters);
                res.Status = ResponseStatus.OK;
                res.ResultData = result.FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                res.Status = ResponseStatus.Error;
                return new Response();
            }
        }
        public async Task<Response> GetAllPromotionTypesForSupplier(PromotionsVM promotionsVM)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@supplierId",promotionsVM.SupplierId),
                    new SqlParameter("@promotionTypeName",promotionsVM.PromotionName),
                    new SqlParameter("@promotionTypeCode",promotionsVM.PromotionCode),
                    new SqlParameter("@entity",promotionsVM.EntityStatus)
                };
                var result = await uow.ExecuteReaderSingleDSNew<PromotionTypeDTO>("SP_GetSupplierPromotionTypeList", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.ResultData = result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }

            return response;
        }
        public async Task<Response> GetPromotionTypesListForSupplier(long? supplierId)
        {
            Response response = new Response();
            try
            {
                var result = await uow.Repository<SupplierPromotionsTypes>().Get(x => x.IsActive == true && x.EntityStatus == "1").Where(a => a.SupplierId == supplierId)
                                .Select(p => new IdValueDTO { Id = p.PromotionTypeId, Text = p.PromotionTypeName }).ToListAsync();
                response.Status = ResponseStatus.OK;
                response.ResultData = result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetAllPromotionsForSupplier(PromotionsVM promotionsVM)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@supplierId",promotionsVM.SupplierId),
                    new SqlParameter("@promotionName",promotionsVM.PromotionName),
                    new SqlParameter("@categoryId",promotionsVM.CategoryId),

                    new SqlParameter("@entity",promotionsVM.EntityStatus)
                };
                var dataList = await uow.ExecuteReaderSingleDSNew<SupplierPromotionsDTO>("SP_GetSupplierPromotionList", sqlParameters);
                response.ResultData = dataList;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }

            return response;
        }

        public async Task<Response> AddPromotionForSupplier(string data)
        {
            Response response = new Response();
            var promotion = JsonConvert.DeserializeObject<SupplierPromotionsDTO>(data);
            try
            {
                SqlParameter[] sqlParameters =
                  {
                    new SqlParameter("@PromotionId", promotion.PromotionId),
                    new SqlParameter("@PromotionTypeId", promotion.PromotionTypeId),
                    new SqlParameter("@PromotionName",promotion.PromotionName),
                    new SqlParameter("@IsActive",promotion.IsActive),
                    new SqlParameter("@EntityStatus",promotion.EntityStatus),
                    new SqlParameter("@CreatedBy", promotion.CreatedBy),
                    new SqlParameter("@UpdatedBy",promotion.UpdatedBy),
                    new SqlParameter("@PromotionStartDate",promotion.PromotionStartDate),
                    new SqlParameter("@PromotionEndDate",promotion.PromotionEndDate),
                    new SqlParameter("@CategoryId",promotion.CategoryId),
                    new SqlParameter("@SubCategoryId",promotion.SubCategoryId),
                    new SqlParameter("@CategoryGroupId",promotion.CategoryGroupId),
                    new SqlParameter("@Amount",promotion.Amount),
                    new SqlParameter("@AmountType",promotion.AmountType)

                 };
                var result = await uow.ExecuteReaderSingleDSNew<Response>("Sp_SupplierAddUpdatePromotions", sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
                Exc.AddErrorLog(ex);
            }

            return response;
        }
        public async Task<Response> GetPromotionByIdForSupplier(int promotionId)
        {
            Response res = new Response();
            try
            {
                var result = await uow.Repository<SupplierPromotions>().Get(x => x.PromotionId == promotionId).FirstOrDefaultAsync();
                res.Status = ResponseStatus.OK;
                res.ResultData = result;
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                res.Status = ResponseStatus.Error;
                return new Response();
            }
        }
        public async Task<Response> GetPromotionsBySuplierId(long supplierId)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@supplierId",supplierId)
                };
                var result = await uow.ExecuteReaderSingleDSNew<PromotionsVM>("GetPromotionBySuplierId", sqlParameters);
                res.Status = ResponseStatus.OK;
                res.ResultData = result;
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                res.Status = ResponseStatus.Error;
                return new Response();
            }
        }
        public async Task<Response> DeletePromotionForSupplier(int promotionId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                    new SqlParameter("@PromotionId", promotionId)
                 };
                var result = await uow.ExecuteReaderSingleDSNew<Response>("Sp_DeleteSupplierPromotions", sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<Response> SupplierLeadgerTransaction(long? supplierId)
        {
            throw new NotImplementedException();
        }



        public async Task<Response> UpdateShippingChargesAndPaymentStatus(string data)
        {
            Response res = new Response();
            try
            {
                var shippingData = JsonConvert.DeserializeObject<List<UpdateShippingChargesAndPaymentStatusDTO>>(data);

                foreach (var item in shippingData)
                {
                    SqlParameter[] sqlParameters =
                  {
                    new SqlParameter("@ShippingAmount", item.ShippingAmount),
                    new SqlParameter("@PaymentReceivedStatus",item.PaymentReceivedStatus),
                    new SqlParameter("@DispatchPaymentStatus",item.DispatchPaymentStatus),
                    new SqlParameter("@ItemId",item.ItemId),
                    new SqlParameter("@VariantId",item.VariantId),
                    new SqlParameter("@SupplierId",item.SupplierId),
                    new SqlParameter("@orderDetailId",item.OrderDetailId),
                 };
                    var result = await uow.ExecuteReaderSingleDSNew<Response>("Sp_UpdateShippingChargesAndPaymentStatus", sqlParameters);
                }
                //res.Message = result.Select(x => x.Message).FirstOrDefault();
                res.Status = ResponseStatus.OK;
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                res.Status = ResponseStatus.Error;
                return new Response();
            }
            //}
            //   return response;
        }

        public async Task<Response> GetChartOfAccount(string data)
        {
            Response response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<GetChartOfAccountVM>(data);
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@accountId",entity.AccountId),
                    new SqlParameter("@userId",entity.UserId),
                    new SqlParameter("@jobQuotationId",entity.JobQuotationId),
                    new SqlParameter("@userName",entity.UserName),
                    new SqlParameter("@startDate",entity.FromDate),
                    new SqlParameter("@endDate",entity.ToDate),
                    new SqlParameter("@ReffrenceDocumentType",entity.DocumentType),
                };
                response.ResultData = uow.ExecuteReaderSingleDS<GetChartOfAccountVM>("SP_GetChartOfAccount", sqlParameters);
                if (response.ResultData != null)
                    response.Status = ResponseStatus.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                return response;
            }

        }
        public async Task<Response> GetSubAccountsLastLevel()
        {
            Response response = new Response();
            try
            {
                response.ResultData = uow.ExecuteCommand<GetSubAccountsLastLevelDTO>("GetSubAccountsLastLevel").ToList();
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetChartOfAccounts()
        {
            Response response = new Response();
            try
            {
                var coaNodes = new List<COANode>();
                var chartOfAccounts = new ChartOfAccountDTO();
                var accountsData = await uow.ExecuteReaderMultipleDS<AccountDetailData>("GetChartOfAccounts");
                var accountCategories = accountsData.AccountCategory;
                foreach (var accountCategory in accountCategories)
                {
                    var node1 = new COANode();
                    node1.Id = accountCategory.AccountCategoryId;
                    node1.Name = accountCategory.AccountCategoryName;
                    node1.Code = accountCategory.AccountCategoryCode;
                    node1.IsControlAccount = true;
                    if (accountsData.AccountSubCategory != null)
                    {
                        var subCate = accountsData.AccountSubCategory.Where(p => p.AccountCategoryId == accountCategory.AccountCategoryId).ToList();
                        var ChildsOfNode1 = new List<COANode>();
                        foreach (var accountSubCategory in subCate)
                        {
                            var node2 = new COANode();
                            node2.Id = accountSubCategory.AccountSubCategoryId;
                            node2.Name = accountSubCategory.AccountSubCategoryName;
                            node2.Code = accountSubCategory.AccountSubCategoryCode;
                            node2.IsControlAccount = (bool)accountSubCategory.IsControlAccount ? accountSubCategory.IsControlAccount : false;
                            if (accountsData.Accounts != null)
                            {
                                var accounts = accountsData.Accounts.Where(p => p.AccountCategoryId == accountCategory.AccountCategoryId
                                        && p.AccountSubCategoryId == accountSubCategory.AccountSubCategoryId).ToList();
                                var ChildsOfNode2 = new List<COANode>();
                                foreach (var account in accounts)
                                {
                                    var node3 = new COANode();
                                    node3.Id = Convert.ToInt32(account.AccountId);
                                    node3.Name = account.AccountName;
                                    node3.Code = account.AccountNo.ToString();
                                    node3.IsControlAccount = (bool)account.IsControlAccount ? account.IsControlAccount : false;
                                    if (accountsData.SubAccounts != null)
                                    {
                                        var subAccounts = accountsData.SubAccounts.Where(p => p.AccountCategoryId == account.AccountCategoryId
                                                && p.AccountSubCategoryId == account.AccountSubCategoryId
                                                && p.AccountId == account.AccountId).ToList();

                                        var ChildsOfNode3 = new List<COANode>();
                                        foreach (var subAccount in subAccounts)
                                        {
                                            var node4 = new COANode();
                                            node4.Id = Convert.ToInt32(subAccount.SubAccountId);
                                            node4.Name = subAccount.SubAccountName;
                                            node4.Code = subAccount.SubAccountNo;
                                            node4.IsControlAccount = (bool)subAccount.IsControlAccount ? subAccount.IsControlAccount : false;
                                            ChildsOfNode3.Add(node4);
                                        }
                                        //level 4
                                        node3.Children = ChildsOfNode3;
                                    }
                                    //level 3
                                    ChildsOfNode2.Add(node3);
                                }
                                node2.Children = ChildsOfNode2;
                            }
                            //level 2
                            ChildsOfNode1.Add(node2);
                        }
                        node1.Children = ChildsOfNode1;
                    }
                    //level 1
                    coaNodes.Add(node1);
                }
                response.Status = ResponseStatus.OK;
                response.ResultData = coaNodes;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> InsertChartOfAccounts(string data)
        {
            Response response = new Response();

            try
            {
                var entity = JsonConvert.DeserializeObject<ChartOfAccountVm>(data);
                SqlParameter[] sqlParameters =
               {
                    new SqlParameter("@id",entity.id??0),
                    new SqlParameter("@subAccountName",entity.name),
                    new SqlParameter("@subAccountCode",entity.code),
                    new SqlParameter("@parentLevel",entity.parentLevel),
                    new SqlParameter("@isControlAccount",entity.isControlAccount??false),
                    new SqlParameter("@parentIdLevel1",entity.parentIdLevel1),
                    new SqlParameter("@parentIdLevel2",entity.parentIdLevel2??0),
                    new SqlParameter("@parentIdLevel3",entity.parentIdLevel3??0),
                };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_InsertSubAccount", sqlParameters);
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
            }
            return response;

        }

        public async Task<Response> DeleteChartOfAccounts(string data)
        {
            Response response = new Response();

            try
            {
                var entity = JsonConvert.DeserializeObject<COATNodeVM>(data);
                SqlParameter[] sqlParameters =
               {
                    new SqlParameter("@level",entity.level),
                    new SqlParameter("@id",entity.id),


                };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_DeleteSubAccount", sqlParameters);
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> AddJournalEntry(string data)
        {
            Response response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<JournalEntryHeaderDTO>(data);
                var subEntity = JsonConvert.DeserializeObject<JournalEntryLineDTO>(data);
                SqlParameter[] sqlParameters =
               {
                    new SqlParameter("@Id",entity.id??0),
                    new SqlParameter("@ReferenceNo",entity.referenceNo),
                    new SqlParameter("@Narration",entity.narration),
                    new SqlParameter("@EntryDate",entity.date),
                    new SqlParameter("@Notes",entity.notes),
                    new SqlParameter("@EntityStatus",1),
                    new SqlParameter("@IsActive",true),
                    new SqlParameter("@UserId",entity.userId),


                };
                int result = await uow.ExecuteScalar<int>("AddUpdateJournalEntryHeader", sqlParameters);
                if (result > 0)
                {
                    foreach (var item in subEntity.journalEntry)
                    {
                        SqlParameter[] sqlParametersLine =
                        {
                            new SqlParameter("@Id",item.id??0),
                            new SqlParameter("@JournalEntryHeaderId",result),
                            new SqlParameter("@SubAccountId",item.accountName.id),
                            new SqlParameter("@Description",item.description),
                            new SqlParameter("@Debit",item.debit??0),
                            new SqlParameter("@Credit",item.credit??0),
                            new SqlParameter("@TaxDebit",(item.debit > 0 && item.debit != null) ? item.tax??0 : 0),
                            new SqlParameter("@TaxCredit",(item.credit > 0 && item.credit != null) ? item.tax??0 : 0),
                            new SqlParameter("@EntityStatus",1),
                            new SqlParameter("@IsActive",true),
                            new SqlParameter("@UserId",entity.userId),


                        };
                        var results = uow.ExecuteReaderSingleDS<Response>("AddUpdateJournalEntryLines", sqlParametersLine);
                        response.ResultData = result;
                    }

                    response.Status = ResponseStatus.OK;
                }
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> AddLeadgerTransactionEntry(string data)
        {
            Response response = new Response();

            try
            {
                var entity = JsonConvert.DeserializeObject<LeadgerTransactionEntryDTO>(data);
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@JournalEntryHeaderId",entity.id),
                    new SqlParameter("@UserId",entity.userId),


                };
                var result = uow.ExecuteReaderSingleDS<Response>("PostJournalEntryHeader", sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> AddFiscalPeriod(string data)
        {
            Response response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<FiscalPeriodDTO>(data);
                SqlParameter[] sqlParameters =
               {
                    new SqlParameter("@Id",entity.fiscalPeriodId??0),
                    new SqlParameter("@FiscalYear",entity.fiscalYear),
                    new SqlParameter("@PeriodId",entity.periodId),
                    new SqlParameter("@PeriodName",entity.periodName),
                    new SqlParameter("@Status",entity.status),
                    new SqlParameter("@StartDate",entity.startDate),
                    new SqlParameter("@EndDate",entity.endDate),
                    new SqlParameter("@UserId",entity.userId),


                };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_AddFiscalPeriod", sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetFiscalPeriodsByYear(int fiscalYear)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
               {
                    new SqlParameter("@FiscalYear",fiscalYear),



                };
                response.ResultData = uow.ExecuteReaderSingleDS<FiscalPeriodDTO>("Sp_GetFiscalPeriodsByYear", sqlParameters).ToList();
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetDetailedGLReport(string data)
        {
            Response response = new Response();
            try
            {
                var objParams = JsonConvert.DeserializeObject<GLReportParamsDTO>(data);
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@FiscalPeriod",objParams?.FiscalPeriod),
                    new SqlParameter("@StartDate",objParams?.StartDate),
                    new SqlParameter("@EndDate",objParams?.EndDate),
                    new SqlParameter("@SubAccountId",objParams.SubAccountId),
                };
                response.ResultData = uow.ExecuteReaderSingleDS<DetailedGLReportDTO>("GetDetailedGLReport", sqlParameters).ToList();
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<bool> LeadgerTransectionEntries(long jobQuotationId)
        {
            bool isMarkFinish = false;
            try
            {

                SqlParameter[] sqlParameters = {
                    new SqlParameter("@jobQuotationId",jobQuotationId)
                };
                isMarkFinish = await uow.ExecuteScalar<bool>("Sp_LeadgerTransectionEntries", sqlParameters);
                return isMarkFinish;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return isMarkFinish;
            }
        }

        public Response GetSupplierWallet(long refSupplierId)
        {
            try
            {
                SqlParameter[] sqlParameters =
               {
                new SqlParameter("@refSupplierId",refSupplierId)
                };

                SpTransactionAmountVM sPClass = uow.ExecuteReaderSingleDS<SpTransactionAmountVM>("GetSupplierWalletAmount", sqlParameters)?.FirstOrDefault();
                Response response = new Response
                {
                    Status = ResponseStatus.OK,
                    Message = "Ok.",
                    ResultData = sPClass?.amount ?? 0
                };
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> AddSupplierPaymentWithdrawalRequest(PaymentWithdrawalRequestVM paymentWithdrawalRequestVM)
        {
            try
            {
                Response response = new Response();
                if (paymentWithdrawalRequestVM != null)
                {
                    WithdrawalRequest withdrawalRequest = new WithdrawalRequest()
                    {
                        SupplierId = paymentWithdrawalRequestVM.SupplierId,
                        SupplierName = paymentWithdrawalRequestVM.SupplierName,
                        PhoneNumber = paymentWithdrawalRequestVM.PhoneNumber,
                        Cnic = paymentWithdrawalRequestVM.Cnic,
                        Amount = paymentWithdrawalRequestVM.Amount,
                        CreatedOn = DateTime.Now,
                        CreatedBy = paymentWithdrawalRequestVM.CreatedBy,
                        PaymentStatusId = (int)WithdravalPaymentStatus.Pending,
                        Role = paymentWithdrawalRequestVM.Role,
                    };
                    await uow.Repository<WithdrawalRequest>().AddAsync(withdrawalRequest);
                    await uow.SaveAsync();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data added Successfully Saved !!";
                }
                return response;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> GetWithdrawalListById(long id)
        {
            Response response = new Response();
            List<PaymentWithdrawalRequestVM> paymentWithdrawal = new List<PaymentWithdrawalRequestVM>();
            try
            {
                SqlParameter[] Parameters =
                {
                   new SqlParameter("@Id", id),
                };
                paymentWithdrawal = uow.ExecuteReaderSingleDS<PaymentWithdrawalRequestVM>("SP_GetWithdrawalRequestListById", Parameters).ToList();
                if(paymentWithdrawal != null)
                {
                    response.ResultData = paymentWithdrawal;
                    response.Status = ResponseStatus.OK;
                }
                
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
                Exc.AddErrorLog(ex);
            }
            return response;
        }
    }
}


