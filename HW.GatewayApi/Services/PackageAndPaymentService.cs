using HW.CustomerModels;
using HW.GatewayApi.Code;
using HW.GatewayApi.Helpers;
using HW.Http;
using HW.IdentityViewModels;
using HW.PackagesAndPaymentsModels;
using HW.PackagesAndPaymentsViewModels;
using HW.SupplierModels;
using HW.TradesmanModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HW.GatewayApi.Services
{
    public interface IPackageAndPaymentService
    {
        Task<Response> AddEditPromotionReferalCode(ReferalCode referalCode);
        Task<Response> AddPromotionRedemptions(PromotionRedemptions proRedemption);
        Task<Response> AddLeadgerTransection(LeadgerTransection leadgerTransection);
        Task<Response> AddUpdateAccountReceivable(long orderId,int paymentMethodId,string userName,bool isPaymentRecived);
        Task<Response> AddEditRedemptions(Redemptions redemptions);
        Task<List<ReferalCode>> GetReferalRecordByreferalCode(string referalCode);
        Task<string> GetRedemptionById(string userId);
        Task<Response> AddSubAccount(string userId, string Role);
        Task<SubAccount> GetSubAccount(long customerId);
        Task<PaymentWithdrawalRequestVM> GetPaymentWithdrawalRequestByTradesmanId(long tradesmanId,string role);
        Task<Response> GetLedgerTransaction(long reftradesmanId);
        Task<Response> GetLedgerTransactionCustomer(long refcustomerId);
        Task<List<VoucherBookLeaves>> GetVoucherList();
        Task<List<WalletHistoryVM>> GetWalletHistory(long id, string role);

        Task<Response> getRedemptionRecord(PromotionRedemptions proRedemption);

        Response CreateJazzCashHash(JazzCashTransactionVM jazzCashVM);
        Task<Response> getSubAccountRecord(long id);
        Task<Response> AddLeaderTransactionForCreditUser(decimal walletValue, long jobqoutationId, long id, string userId, long jobbidsId);
        Task<Response> AddPaymentWithdrawalRequest(PaymentWithdrawalRequestVM paymentWithdrawalRequestVM);
        Task<List<PromotionsTypeVM>> GetPromotionTypes();
        Task<Response> SupplierLeadgerTransaction(long? supplierId);
        Task<Response> InsertChartOfAccounts(string data);
        Task<Response> DeleteChartOfAccounts(string data);
        Task<Response> AddJournalEntry(string data);
        Task<Response> AddFiscalPeriod(string data);
        Task<Response> AddLeadgerTransactionEntry(string data);
        Task<Response> GetFiscalPeriodsByYear(int fiscalYear);
        Task<Response> GetSupplierWallet(long refSupplierId);
        Task<Response> AddSupplierPaymentWithdrawalRequest(PaymentWithdrawalRequestVM paymentWithdrawalRequestVM);
        Task<string> GetPromotionsBySuplierId(long supplierId);
    }
    public class PackageAndPaymentService : IPackageAndPaymentService
    {
        private readonly IHttpClientService httpClient;
        private readonly ClientCredentials clientCred;
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;

        public PackageAndPaymentService(IHttpClientService httpClient, ClientCredentials clientCred, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.clientCred = clientCred;
            this.httpClient = httpClient;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }

        public async Task<Response> AddEditPromotionReferalCode(ReferalCode referalCode)
        {
            Response response = new Response();

            try
            {

                string sharedRefferalUserID = JsonConvert.DeserializeObject<string>
                    (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByPublicId}?publicID={referalCode.ReferralCode}"));

                if (!string.IsNullOrEmpty(sharedRefferalUserID) && sharedRefferalUserID != referalCode.ReferredUser)
                {
                    Customer customer = JsonConvert.DeserializeObject<Customer>(
                        await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByUserId}?userId={sharedRefferalUserID}"));

                    List<ReferalCode> referallist = new List<ReferalCode>();
                    referallist = JsonConvert.DeserializeObject<List<ReferalCode>>
                                    (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetReferalRecordByreferalCode}?referalCode={referalCode.ReferralCode}"));
                    if (referallist.Count < 5)
                    {
                        ReferalCode getRecord = new ReferalCode();

                        getRecord = JsonConvert.DeserializeObject<ReferalCode>
                                    (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetReferalRecordByreferalUserID}?referalUserID={referalCode.ReferredUser}"));
                        if (getRecord == null)
                        {
                            ReferalCode referalCodeVM = new ReferalCode()
                            {
                                ReferralCode = referalCode.ReferralCode,
                                ReferredUser = referalCode.ReferredUser,
                                CreatedOn = DateTime.Now,
                                CreatedBy = referalCode.CreatedBy,
                                JobQuotationId = referalCode.JobQuotationId,
                                IsJobComplete = referalCode.IsJobComplete,
                                UserId = sharedRefferalUserID,
                                RefferalAmount = referalCode.RefferalAmount
                            };

                            response = JsonConvert.DeserializeObject<Response>
                                (await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddEditPromotionReferalCode}", referalCodeVM));


                        }
                        else
                        {
                            response.Status = ResponseStatus.Restrected;
                            response.Message = "You are already used reffral code.";
                        }

                    }
                    else
                    {
                        response.Message = "Reward Completed";
                    }

                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Invalid Refferal";
                }



                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }
        public async Task<Response> AddPromotionRedemptions(PromotionRedemptions proRedemption)
        {
            Response response = new Response();

            try
            {
                PromotionRedemptions getRecord = new PromotionRedemptions();

                getRecord = JsonConvert.DeserializeObject<PromotionRedemptions>
                            (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPromotionRedeemRecordByRedeemUserID}?redeemByUserID={proRedemption.RedeemBy}&promotionTypeID={proRedemption.PromotionId}&voucherId={proRedemption.VoucherBookLeavesId}"));
                if (getRecord == null)
                {
                    PromotionRedemptions proRedemptionVM = new PromotionRedemptions()
                    {
                        PromotionId = proRedemption.PromotionId,
                        RedeemBy = proRedemption.RedeemBy,
                        RedeemOn = DateTime.Now,
                        TotalDiscount = proRedemption.TotalDiscount,
                        JobQuotationId = proRedemption.JobQuotationId,
                        CustomerId = proRedemption.CustomerId,
                        IsVoucher = proRedemption.IsVoucher,
                        VoucherBookLeavesId = proRedemption.VoucherBookLeavesId,
                        CategoryId = proRedemption.CategoryId
                        
                    };
                    if (proRedemption.VoucherBookLeavesId != null)
                    {
                        VoucherBookLeaves voucherBookLeaves = new VoucherBookLeaves()
                        {
                            VoucherBookLeavesId = Convert.ToInt32(proRedemption.VoucherBookLeavesId),
                            IsUsed = true,
                            ModifiedBy = proRedemption.RedeemBy
                        };
                        response = JsonConvert.DeserializeObject<Response>
                            (await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.UpdateVoucherBookLeaves}", voucherBookLeaves));
                    }
                    response = JsonConvert.DeserializeObject<Response>
                            (await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddPromotionRedemptions}", proRedemptionVM));
                }
                else
                {
                    response.Status = ResponseStatus.Restrected;
                    response.Message = "You are already used promotional code.";
                }


                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }
        public async Task<Response> AddEditRedemptions(Redemptions redemptions)
        {
            Response response = new Response();

            try
            {
                Redemptions getRecord = new Redemptions();

                getRecord = JsonConvert.DeserializeObject<Redemptions>
                            (await httpClient.GetAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.GetRedeemRecordByRedeemUserID}?redeemByUserID={redemptions.RedeemBy}&voucherID={redemptions.VoucherId}"));
                if (getRecord == null)
                {
                    Redemptions redemptionVM = new Redemptions()
                    {
                        VoucherId = redemptions.VoucherId,
                        RedeemBy = redemptions.RedeemBy,
                        RedeemOn = DateTime.Now,
                        TotalDiscount = redemptions.TotalDiscount,
                        JobQuotationId = redemptions.JobQuotationId
                    };

                    response = JsonConvert.DeserializeObject<Response>
                            (await httpClient.PostAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.AddEditRedemptions}", redemptionVM));
                }
                else
                {
                    response.Status = ResponseStatus.Restrected;
                    response.Message = "You are already used promotional code.";
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }

        public async Task<Response> AddSubAccount(string userId, string role)
        {
            Response response = new Response();

            try
            {
                UserProfileVM userProfileVM = new UserProfileVM();

                if (Utility.UserRoles.Customer == role)
                {
                    Customer customer = JsonConvert.DeserializeObject<Customer>(
                        await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByUserId}?userId={userId}")
                    );
                    if (customer != null)
                    {
                        SubAccount subAccount = new SubAccount()
                        {
                            SubAccountName = $"{customer.FirstName} {customer.LastName}",
                            CustomerId = customer.CustomerId,
                            SubAccountNo = Convert.ToString(customer.CustomerId),
                            CustomerName = $"{customer.FirstName} {customer.LastName}",
                            AccountId = Convert.ToInt64(Utility.AccountType.AccountReceiveables),
                            Active = true,
                            CreatedBy = userId,
                            
                            CreatedOn = DateTime.Now
                        };

                        response = JsonConvert.DeserializeObject<Response>
                                (await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddSubAccount}", subAccount));
                    }
                }
                else if (Utility.UserRoles.Tradesman == role || Utility.UserRoles.Organization == role)
                {
                        Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(
                        await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanByUserId}?userId={userId}")
                        );

                    if (tradesman != null)
                    {
                        userProfileVM = new UserProfileVM()
                        {
                            EntityId = tradesman.TradesmanId,
                            UserName = $"{tradesman.FirstName} {tradesman.LastName}",
                            City = tradesman.City,
                            UserId = userId,
                            tradesmanId = tradesman.TradesmanId,
                            PublicId = tradesman.PublicId,
                            Email = tradesman.EmailAddress
                        };

                        SubAccount subAccount = new SubAccount()
                        {
                            SubAccountName = userProfileVM.UserName,
                            TradesmanId = userProfileVM.tradesmanId,
                            SubAccountNo = Convert.ToString(userProfileVM.tradesmanId),
                            TradesmanName = userProfileVM.UserName,
                            AccountId = Convert.ToInt64(Utility.AccountType.AccountPayables),
                            Active = true,
                            CreatedBy = userId,
                            ModifiedOn = DateTime.Now,
                            ModifiedBy = userId,
                            CreatedOn = DateTime.Now
                        };

                        response = JsonConvert.DeserializeObject<Response>
                                (await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddSubAccount}", subAccount));
                    }
                }
                else if (Utility.UserRoles.Supplier == role)
                {
                    Supplier supplier = JsonConvert.DeserializeObject<Supplier>(
                        await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierByUserId}?userId={userId}")
                    );

                    if (supplier != null)
                    {
                        SubAccount subAccount = new SubAccount()
                        {
                            SubAccountName = supplier.FirstName + " " + supplier.LastName,
                            SupplierId = supplier.SupplierId,
                            SubAccountNo = Convert.ToString(supplier.SupplierId),
                            SupplierName = supplier.FirstName + " " + supplier.LastName,
                            AccountId = Convert.ToInt64(Utility.AccountType.AccountReceiveables),
                            Active = true,
                            CreatedBy = userId,
                            ModifiedOn = DateTime.Now,
                            ModifiedBy = userId,
                            CreatedOn = DateTime.Now
                        };

                        response = JsonConvert.DeserializeObject<Response>
                                (await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddSubAccount}", subAccount));
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }
        public async Task<Response> AddUpdateAccountReceivable(long orderId,int paymentMethodId,string userName,bool isPaymentRecived)
        {
          Response response = new Response();

          try
          {
            response.ResultData = JsonConvert.DeserializeObject<Response>
                      (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddUpdateAccountReceivable}?orderId={orderId}&paymentMethodId={paymentMethodId}&userName={userName}&isPaymentRecived={isPaymentRecived}"));
            response.Status = ResponseStatus.OK;
          }
          catch (Exception ex)
          {
            Exc.AddErrorLog(ex);
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
          }
          return response;
        }
        public async Task<Response> AddLeadgerTransection(LeadgerTransection leadgerTransection)
        {
            Response response = new Response();

            try
            {
                if (leadgerTransection != null)
                {
                    response = JsonConvert.DeserializeObject<Response>
                            (await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransection));
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }
        public async Task<List<ReferalCode>> GetReferalRecordByreferalCode(string referalCode)
        {
            List<ReferalCode> referallist = new List<ReferalCode>();
            try
            {
                referallist = JsonConvert.DeserializeObject<List<ReferalCode>>
                    (await httpClient.GetAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.GetReferalRecordByreferalCode}?referalCode={referalCode}"));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return null;
            }
            return referallist;
        }

        public async Task<string> GetRedemptionById(string userid)
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.GetRedemptionById}?userId={userid}", "");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return ex.Message;
            }
        }

        public async Task<SubAccount> GetSubAccount(long customerId)
        {
            try
            {
                SubAccount subAccount = JsonConvert.DeserializeObject<SubAccount>(await httpClient.GetAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.PackagesAndPayments.GetSubAccount}?customerId={customerId}", ""));
                return subAccount;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new SubAccount();
            }
        }

        public async Task<PaymentWithdrawalRequestVM> GetPaymentWithdrawalRequestByTradesmanId(long tradesmanId,string role)
        {
            try
            {
                PaymentWithdrawalRequestVM addPaymentWithdrawalRequestVM = JsonConvert.DeserializeObject<PaymentWithdrawalRequestVM>(await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPaymentWithdrawalRequestByTradesmanId}?tradesmanId={tradesmanId}&role={role}", ""));
                return addPaymentWithdrawalRequestVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new PaymentWithdrawalRequestVM();
            }
        }
        public async Task<Response> GetLedgerTransaction(long reftradesmanId)
        {
            try
            {
                Response subAccount = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetLedgerTransaction}?reftradesmanId={reftradesmanId}", ""));
                return subAccount;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new Response();
            }
        }

        public async Task<Response> GetLedgerTransactionCustomer(long refcustomerId)
        {
            try
            {
                Response subAccount = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetLedgerTransactionCustomer}?refcustomerId={refcustomerId}", ""));
                return subAccount;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new Response();
            }
        }

        public async Task<List<VoucherBookLeaves>> GetVoucherList()
        {
            List<VoucherBookLeaves> getVoucher = new List<VoucherBookLeaves>();
            var getVouchersList = await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetVoucherList}");
            return getVoucher = JsonConvert.DeserializeObject<List<VoucherBookLeaves>>(getVouchersList);
        }

        public async Task<List<WalletHistoryVM>> GetWalletHistory(long id, string role)
        {
            SubAccount subAccount = new SubAccount();

            List<WalletHistoryVM> wallet = new List<WalletHistoryVM>();

            if (role == "Customer")
            {
                subAccount = JsonConvert.DeserializeObject<SubAccount>(
                   await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccount}?customerId={id}", ""));
            }
            else
            {
                subAccount = JsonConvert.DeserializeObject<HW.PackagesAndPaymentsModels.SubAccount>(
                await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccountByTradesmanId}?tradesmanId={id}", ""));

            }

            var List = await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetWalletHistory}?subAccountId={subAccount.SubAccountId}");

            List<LeadgerTransection> getTransactionHistory = new List<LeadgerTransection>();
            getTransactionHistory = JsonConvert.DeserializeObject<List<LeadgerTransection>>(List);
            wallet = getTransactionHistory.Select(s => new WalletHistoryVM
            {
                TransactionId = s.LeadgerTransectionId,
                Debit = s.Debit,
                Credit = s.Credit,
                CreatedOn = s.CreatedOn,
                ReffrenceDocumentType = s.ReffrenceDocumentType

            }).ToList();

            return wallet;
        }

        public Response CreateJazzCashHash(JazzCashTransactionVM jazzCashVM)
        {
            Response response;

            string values = string.Empty, hashedValue = string.Empty;
            foreach (PropertyInfo prop in jazzCashVM.GetType().GetProperties())
            {
                values += $"{prop.GetValue(jazzCashVM)}&";
            }

            hashedValue = Hasher.Base64Encode(values);

            response = new Response()
            {
                Status = ResponseStatus.OK,
                ResultData = hashedValue
            };
            return response;
        }

        public async Task<Response> getRedemptionRecord([FromBody] PromotionRedemptions proRedemption)
        {
            Response response = new Response();

            try
            {
                PromotionRedemptions getRecord = new PromotionRedemptions();

                getRecord = JsonConvert.DeserializeObject<PromotionRedemptions>
                            (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPromotionRedeemRecordByRedeemUserID}?redeemByUserID={proRedemption.RedeemBy}&promotionTypeID={proRedemption.PromotionId}&voucherId={proRedemption.VoucherBookLeavesId}"));
                if(getRecord == null)
                {
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Status = ResponseStatus.Restrected;
                }
              
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
            return response;
        }

        public async Task<Response> getSubAccountRecord(long id)
        {
            Response response = new Response();
            var jsonResponse = await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.getSubAccountRecord}?id={id}");
            response = JsonConvert.DeserializeObject<Response>(jsonResponse);
            return response;
        }

        public async Task<Response> AddLeaderTransactionForCreditUser(decimal walletValue, long jobqoutationId, long id, string userId, long jobbidsId)
        {
            SubAccount subAccountCustomer = new SubAccount();
            Response response = new Response();
            
            
            ///// Customer Deduction Entry ///////////////
            
            subAccountCustomer = JsonConvert.DeserializeObject<HW.PackagesAndPaymentsModels.SubAccount>(
                    await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccount}?customerId={id}", ""));
            PackagesAndPaymentsModels.LeadgerTransection SelfleadgerTransction = new PackagesAndPaymentsModels.LeadgerTransection()
            {
                AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountReceiveables),
                SubAccountId = subAccountCustomer.SubAccountId,
                Debit = walletValue,
                Credit = 0,
                Active = true,
                RefCustomerSubAccountId = id,
                ReffrenceDocumentNo = jobqoutationId,
                ReffrenceDocumentId = jobbidsId,
                ReffrenceDocumentType = "Paid By Wallet",
                CreatedOn = DateTime.Now,
                CreatedBy = userId
            };

            response = JsonConvert.DeserializeObject<Response>(
                await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", SelfleadgerTransction)
            );

            ///////// Hoomwork debited entry ////////////////

            //SubAccount sub = JsonConvert.DeserializeObject<SubAccount>(
            //            await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetHoomWorkSubAccount}", "")
            //        );

            //LeadgerTransection leadgerTransctn = new LeadgerTransection()
            //{
            //    AccountId = Convert.ToInt64(HW.Utility.AccountType.Assets),
            //    SubAccountId = sub.SubAccountId,
            //    Debit = walletValue,
            //    Credit =0,
            //    Active = true,
            //    RefCustomerSubAccountId = id,
            //    ReffrenceDocumentNo = jobqoutationId,
            //    ReffrenceDocumentId = jobbidsId,
            //    ReffrenceDocumentType = "Paid By Wallet",
            //    CreatedOn = DateTime.Now,
            //    CreatedBy = userId
            //};

            //response = JsonConvert.DeserializeObject<Response>(
            //    await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransctn)
            //);


            return response;
        }

        public async Task<Response> AddPaymentWithdrawalRequest(PaymentWithdrawalRequestVM paymentWithdrawalRequestVM)
        {
            Response response = new Response();
            var jsonResponse = await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddPaymentWithdrawalRequest}",paymentWithdrawalRequestVM);
            response = JsonConvert.DeserializeObject<Response>(jsonResponse);
            return response;
        }
        public async Task<List<PromotionsTypeVM>> GetPromotionTypes()
        {
            List<PromotionsTypeVM> getPromotionsType = new List<PromotionsTypeVM>();
            var getPromotions = await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPromotionData}");
            return getPromotionsType = JsonConvert.DeserializeObject<List<PromotionsTypeVM>>(getPromotions);

        }

        public async Task<Response> InsertChartOfAccounts(string data)
        {
            Response response = new Response();
            var jsonResponce = await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.InsertChartOfAccounts}", data);
            response = JsonConvert.DeserializeObject<Response>(jsonResponce);
            return response;
        }

        public async Task<Response> DeleteChartOfAccounts(string data)
        {
            Response response = new Response();
            var jsonResponce = await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.DeleteChartOfAccounts}", data);
            response = JsonConvert.DeserializeObject<Response>(jsonResponce);
            return response;
        }

        public async Task<Response> AddJournalEntry(string data)
        {
            Response response = new Response();
            var jsonResponce = await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddJournalEntry}", data);
            response = JsonConvert.DeserializeObject<Response>(jsonResponce);
            return response;
        }
        public async Task<Response> AddFiscalPeriod(string data)
        {
            Response response = new Response();
            var jsonResponce = await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddFiscalPeriod}", data);
            response = JsonConvert.DeserializeObject<Response>(jsonResponce);
            return response;
        }

        public async Task<Response> AddLeadgerTransactionEntry(string data)
        {
            Response response = new Response();
            var jsonResponce = await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransactionEntry}", data);
            response = JsonConvert.DeserializeObject<Response>(jsonResponce);
            return response;
        }

        public async Task<Response> SupplierLeadgerTransaction(long? supplierId)
        {
            Response response = new Response();
            var jsonResponse = await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.SupplierLeadgerTransaction}?supplierId={supplierId}","");
            return response = JsonConvert.DeserializeObject<Response>(jsonResponse);
        }

        public async Task<Response> GetFiscalPeriodsByYear(int fiscalYear)
        {
            Response response = new Response();
            var jsonResponse = await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetFiscalPeriodsByYear}?fiscalYear={fiscalYear}","");
            response = JsonConvert.DeserializeObject<Response>(jsonResponse);
            return response;
        }

        public async Task<Response> GetSupplierWallet(long refSupplierId)
        {
            try
            {
                Response subAccount = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSupplierWallet}?refSupplierId={refSupplierId}", ""));
                return subAccount;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<Response> AddSupplierPaymentWithdrawalRequest(PaymentWithdrawalRequestVM paymentWithdrawalRequestVM)
        {
            Response response = new Response();
            var jsonResponse = await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddSupplierPaymentWithdrawalRequest}", paymentWithdrawalRequestVM);
            response = JsonConvert.DeserializeObject<Response>(jsonResponse);
            return response;
        }
        public async Task<string> GetPromotionsBySuplierId(long supplierId)
        {
            return await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetPromotionsBySuplierId}?supplierId={supplierId}");
        }
    }
}
