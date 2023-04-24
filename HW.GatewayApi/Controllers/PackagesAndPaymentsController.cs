using HW.GatewayApi.Services;
using HW.PackagesAndPaymentsModels;
using HW.PackagesAndPaymentsViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.Controllers
{
    public class PackagesAndPaymentsController : BaseController
    {
        private readonly IPackageAndPaymentService packageAndPaymentService;

        public PackagesAndPaymentsController(IPackageAndPaymentService promotiontService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.packageAndPaymentService = promotiontService;
        }

        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> AddEditPromotionReferalCode([FromBody] ReferalCode referalCode)
        {
            Response response = new Response();
            referalCode.ReferredUser = DecodeTokenForUser().Id;
            referalCode.CreatedBy = DecodeTokenForUser().Id;
            response = await packageAndPaymentService.AddEditPromotionReferalCode(referalCode);
            return response;
        }

        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> getRedemptionRecord([FromBody] PromotionRedemptions proRedemption)
        {
            Response response = new Response();
            proRedemption.RedeemBy = DecodeTokenForUser().Id;
            proRedemption.CustomerId = await GetEntityIdByUserId();
            response = await packageAndPaymentService.getRedemptionRecord(proRedemption);
            return response;
        }
        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> AddPromotionRedemptions([FromBody] PromotionRedemptions proRedemption)
        {
            Response response = new Response();
            proRedemption.RedeemBy = DecodeTokenForUser().Id;
            proRedemption.CustomerId = await GetEntityIdByUserId();
            response = await packageAndPaymentService.AddPromotionRedemptions(proRedemption);
            return response;
        }

        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> AddEditRedemptions([FromBody] Redemptions redemptions)
        {
            Response response = new Response();
            redemptions.RedeemBy = DecodeTokenForUser().Id;
            response = await packageAndPaymentService.AddEditRedemptions(redemptions);
            return response;
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<ReferalCode>> GetReferalRecordByreferalCode(string referalCode)
        {
            return await packageAndPaymentService.GetReferalRecordByreferalCode(referalCode);
        }

        [HttpGet]
        public async Task<Response> AddUpdateAccountReceivable(long orderId,int paymentMethodId,string userName,bool isPaymentRecived)
        {
             //string userName = DecodeTokenForUser().UserName;
             return await packageAndPaymentService.AddUpdateAccountReceivable(orderId,paymentMethodId, userName, isPaymentRecived);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<SubAccount> GetSubAccount(long customerId)
        {
            return await packageAndPaymentService.GetSubAccount(customerId);
        }

        public async Task<Response> GetLedgerTransaction()
        {
            long id = await GetEntityIdByUserId();
            return await packageAndPaymentService.GetLedgerTransaction(id);
        }
        public async Task<Response> GetTradesmanWallet(long tradesmanId)
        {
            return await packageAndPaymentService.GetLedgerTransaction(tradesmanId);
        }
        public async Task<Response> GetLedgerTransactionCustomer(long refercustomerId)
        {
            return await packageAndPaymentService.GetLedgerTransactionCustomer(refercustomerId);
        }
        [HttpGet]
        public async Task<Response> GetSupplierWallet(long refSupplierId)
         {
            return await packageAndPaymentService.GetSupplierWallet(refSupplierId);
        }

        [HttpPost]
        public async Task<Response> AddSubAccount()
        {
            string userId = DecodeTokenForUser().Id;
            string role = DecodeTokenForUser().Role;
            return await packageAndPaymentService.AddSubAccount(userId, role);
        }
        [HttpPost]
        public async Task<Response> AddSubAccountWithoutToken(string userId,string role)
        {
            return await packageAndPaymentService.AddSubAccount(userId, role);
        }

        [HttpPost]
        public async Task<Response> AddLeadgerTransection(LeadgerTransection leadgerTransection)
        {
            return await packageAndPaymentService.AddLeadgerTransection(leadgerTransection);
        }

        [HttpGet]
        public async Task<string> GetRedemptionById(string userId)
        {
            return await packageAndPaymentService.GetRedemptionById(userId);
        }

        [HttpGet]
        public async Task<List<VoucherBookLeaves>> GetVoucherList()
        {
            return await packageAndPaymentService.GetVoucherList();
        }

        [HttpGet]
        public async Task<PaymentWithdrawalRequestVM> GetPaymentWithdrawalRequestByTradesmanId()
        {
            long tradesmanId=await GetEntityIdByUserId();
            string role = DecodeTokenForUser().Role;
            return await packageAndPaymentService.GetPaymentWithdrawalRequestByTradesmanId(tradesmanId, role);
        }

        [HttpGet]
        public async Task<List<WalletHistoryVM>> GetWalletHistory()
        {
            long id = await GetEntityIdByUserId();
            string role = DecodeTokenForUser().Role;
            return await packageAndPaymentService.GetWalletHistory(id, role);
        }

        [HttpPost]
        public Response CreateJazzCashHash(JazzCashTransactionVM jazzCashVM)
        {
            return packageAndPaymentService.CreateJazzCashHash(jazzCashVM);
        }
        public async Task<Response> getSubAccountRecord()
        {
           long id = await GetEntityIdByUserId();
            return await packageAndPaymentService.getSubAccountRecord(id);
        }
        public async Task<Response> getSubAccountRecordWithoutToken(long id)
        {
            return await packageAndPaymentService.getSubAccountRecord(id);
        }
        [HttpPost]
        public async Task<Response> AddPaymentWithdrawalRequest([FromBody] PaymentWithdrawalRequestVM paymentWithdrawalRequestVM)
        {
            
            if (paymentWithdrawalRequestVM.TradesmanId > 0)
            {
                if (!string.IsNullOrWhiteSpace(paymentWithdrawalRequestVM.CreatedBy))
                {
                    return await packageAndPaymentService.AddPaymentWithdrawalRequest(paymentWithdrawalRequestVM);
                }
                else
                {
                    paymentWithdrawalRequestVM.CreatedBy = DecodeTokenForUser().Id;
                    return await packageAndPaymentService.AddPaymentWithdrawalRequest(paymentWithdrawalRequestVM);
                }
            }
            else
            {
                paymentWithdrawalRequestVM.TradesmanId = await GetEntityIdByUserId();
                paymentWithdrawalRequestVM.Role = DecodeTokenForUser().Role;
                return await packageAndPaymentService.AddPaymentWithdrawalRequest(paymentWithdrawalRequestVM);
            }
           
        }
        [HttpPost]
        public async Task<Response> AddSupplierPaymentWithdrawalRequest([FromBody] PaymentWithdrawalRequestVM paymentWithdrawalRequestVM)
        {
            paymentWithdrawalRequestVM.CreatedBy = DecodeTokenForUser().Id;
            if (paymentWithdrawalRequestVM.SupplierId > 0)
            {
                return await packageAndPaymentService.AddSupplierPaymentWithdrawalRequest(paymentWithdrawalRequestVM);
            }
            else
            {
                paymentWithdrawalRequestVM.SupplierId = await GetEntityIdByUserId();
                return await packageAndPaymentService.AddSupplierPaymentWithdrawalRequest(paymentWithdrawalRequestVM);
            }

        }
        [HttpGet]
        public async Task<Response> AddLeaderTransactionForCreditUser(decimal walletValue, long jobqoutationId, long jobbidsId)
        {
            long id = await GetEntityIdByUserId();
            string userId = DecodeTokenForUser().Id;
            return await packageAndPaymentService.AddLeaderTransactionForCreditUser(walletValue, jobqoutationId, id, userId,jobbidsId);
        }
        [HttpGet]
        public async Task<List<PromotionsTypeVM>> GetPromotionTypes()
        {
            return await packageAndPaymentService.GetPromotionTypes();
        }
        
        [HttpPost]
        public async Task<Response> SupplierLeadgerTransaction(long? supplierId)
        {
            return await packageAndPaymentService.SupplierLeadgerTransaction(supplierId);
        }
        [HttpPost]
        public async Task<Response> InsertChartOfAccounts ([FromBody] string data)
        {
            return await packageAndPaymentService.InsertChartOfAccounts(data);
        }
        [HttpPost]
        public async Task<Response> DeleteChartOfAccounts([FromBody] string data)
        {
            return await packageAndPaymentService.DeleteChartOfAccounts(data);
        }
        [HttpPost]
        public async Task<Response> AddJournalEntry([FromBody] string data)
        {
            return await packageAndPaymentService.AddJournalEntry(data);
        }
        [HttpPost]
        public async Task<Response> AddFiscalPeriod([FromBody] string data)
        {
            return await packageAndPaymentService.AddFiscalPeriod(data);
        }
        [HttpPost]
        public async Task<Response> AddLeadgerTransactionEntry([FromBody] string data)
        {
            return await packageAndPaymentService.AddLeadgerTransactionEntry(data);
        }
        [HttpGet]
        public async Task<Response> GetFiscalPeriodsByYear(int fiscalYear)
        {
            return await packageAndPaymentService.GetFiscalPeriodsByYear(fiscalYear);
        }
        [HttpGet]
        public async Task<string> GetPromotionsBySuplierId(long supplierId)
        {
            return await packageAndPaymentService.GetPromotionsBySuplierId(supplierId);
        }
    }
}
