using HW.CustomerModels;
using HW.GatewayApi.Code;
using HW.Http;
using HW.Job_ViewModels;
using HW.JobModels;
using HW.PackagesAndPaymentsModels;
using HW.PackagesAndPaymentsViewModels;
using HW.PaymentModels;
using HW.SupplierViewModels;
using HW.TradesmanModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PaymentMethod = HW.PaymentModels.PaymentMethod;
using PaymentMethods = HW.Utility.PaymentMethods;
using TradesmanJobReceipts = HW.PaymentModels.TradesmanJobReceipts;
using TransactionDetail = HW.PaymentModels.TransactionDetail;

namespace HW.GatewayApi.Services
{
    public interface IPaymentService
    {
        Task<Response> AddTradesmanJobReceipts(long jobQuotationId, bool directPayment, string paymentMethod, long txnDetailId);
        Task<List<AdvertismentPlanVM>> sportLightadvertismentsPlan();
        Task<List<PaymentHistory>> GetPromotionPaymentHistory(string userID);
        Task<List<AdvertismentPlanVM>> FeaturedadvertismentsPlan();
        Task<List<PaymentHistoryVM>> GetpaymentRecord(long supplierId);
        Task<Payment_BillingplanVM> GetPaymentBill(long roleId);
        Task<Response> AddTransactionDetail(TransactionDetailVM txnDetail);
        Task<Response> JazzCashAcknowledgementReceipt(JazzCashAcknowledgementReceiptVM txnDetail);
        Task<Response> AddLeadgerTransaction(string billReference, string txnRefNo, decimal? amount);
        Task<Response> ProceedToJazzCash(JazzCashPaymentDetailVM txnDetail);
        Task<string> GetJazzCashMerchantDetails(string key);

    }

    public class PaymentService : IPaymentService
    {

        private readonly IHttpClientService httpClient;
        private readonly ClientCredentials clientCred;
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;

        public PaymentService(IHttpClientService httpClient, ClientCredentials clientCred, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.clientCred = clientCred;
            this.httpClient = httpClient;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }

        public async Task<Response> AddTradesmanJobReceipts(long jobQuotationId, bool directPayment, string paymentMethod, long txnDetailId)
        {
            try
            {
                Response response = new Response();

                JobDetail jobDetail = JsonConvert.DeserializeObject<JobDetail>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                TradesmanJobReceipts tradesmanJobReceipts = new TradesmanJobReceipts()
                {
                    TradesmanId = jobDetail.TradesmanId,
                    PaymentDate = DateTime.Now.Date,
                    JobDetailId = jobDetail.JobDetailId,
                    Amount = jobDetail.Budget,
                    DirectPayment = directPayment,
                    PaymentMethodId = (paymentMethod == "MIGS" || paymentMethod == "MWALLET") ?
                (int)PaymentMethods.JazzCash : (int)PaymentMethods.DirectPayment,
                    TransactionDetailId = txnDetailId,
                    CreatedOn = jobDetail.CreatedOn,
                    CreatedBy = jobDetail.CreatedBy
                };

                response = JsonConvert.DeserializeObject<Response>
                (await httpClient.PostAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.AddEditTradesmanJobReceipts}", tradesmanJobReceipts));

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> AddTransactionDetail(TransactionDetailVM txnDetail)
        {
            Response response = new Response();

            try
            {

                TransactionDetail transactionDetail = new TransactionDetail()
                {
                    ResponseCode = txnDetail.ResponseCode,
                    ResponseMessage = txnDetail.ResponseMessage,
                    BillReference = txnDetail.BidId,
                    ReferenceNo = txnDetail.ReferenceNo,
                    MerchantId = txnDetail.MerchantId,
                    SubMerchantId = txnDetail.SubMerchantId,
                    RetreivalReferenceNo = txnDetail.RetreivalReferenceNo,
                    Currency = txnDetail.Currency,
                    InitiateTime = DateTime.ParseExact(txnDetail.InitiateTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                    TransactionType = txnDetail.TransactionType,
                    CreatedOn = DateTime.Now,
                    CreatedBy = txnDetail.UserId
                };

                response = JsonConvert.DeserializeObject<Response>
                (await httpClient.PostAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.AddEditTransationDetail}", transactionDetail));

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }



        public async Task<List<AdvertismentPlanVM>> sportLightadvertismentsPlan()
        {

            try
            {
                List<AdvertismentPlanVM> sportLight = new List<AdvertismentPlanVM>();
                List<AdvertismentPlan> advertismentPlan = JsonConvert.DeserializeObject<List<AdvertismentPlan>>(await httpClient.GetAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.GetadvertismentPlans}", "")).ToList();

                sportLight = advertismentPlan.Where(x => x.AdsStatusId == 3).Select(x => new AdvertismentPlanVM { AdvertisementPlanId = x.AdvertisementPlanId, Days = x.Days, AdsStatusId = x.AdsStatusId, Price = x.Price }).ToList();
                return sportLight;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<AdvertismentPlanVM>();
            }
        }

        public async Task<List<PaymentHistory>> GetPromotionPaymentHistory(string userId)
        {
            List<PaymentHistory> paymentHistories = new List<PaymentHistory>();
            try
            {
                paymentHistories = JsonConvert.DeserializeObject<List<PaymentHistory>>(await httpClient.GetAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.GetPromotionPaymentHistory}?userId={userId}", ""));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return paymentHistories;
        }

        public async Task<List<AdvertismentPlanVM>> FeaturedadvertismentsPlan()
        {
            List<AdvertismentPlanVM> featured = new List<AdvertismentPlanVM>();

            try
            {
                List<AdvertismentPlan> advertismentPlan = JsonConvert.DeserializeObject<List<AdvertismentPlan>>(await httpClient.GetAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.GetadvertismentPlans}", "")).ToList();

                featured = advertismentPlan.Where(x => x.AdsStatusId == 2).Select(x => new AdvertismentPlanVM { AdvertisementPlanId = x.AdvertisementPlanId, Days = x.Days, AdsStatusId = x.AdsStatusId, Price = x.Price }).ToList();


            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return featured;
        }

        public async Task<List<PaymentHistoryVM>> GetpaymentRecord(long supplierId)
        {
            List<PaymentHistoryVM> historyVMs = new List<PaymentHistoryVM>();
            try
            {
                List<SupplierMembership> memberships = new List<SupplierMembership>();
                List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
                memberships = JsonConvert.DeserializeObject<List<SupplierMembership>>(await httpClient.GetAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.GetPaymentsHistory}?supplierId={supplierId}", ""));
                List<long> PaymentMethodId = memberships.Select(x => x.PaymentMethodId).ToList();
                // paymentMethods = JsonConvert.DeserializeObject<List<PaymentMethod>>(await httpClient.PutAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.GetMethodCodeById}",PaymentMethodId));


                historyVMs = memberships.Where(x => x.IsPaid == true).Select(x => new PaymentHistoryVM
                {


                    Amount = x.Amount,
                    IsPaid = x.IsPaid,
                    PaymentMonth = DateTime.Now,
                    PaymentMethodId = x.PaymentMethodId,
                    // PaymentMethodCode = paymentMethods.FirstOrDefault(y => y.PaymentMethodId == x.PaymentMethodId)?.Code


                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return historyVMs;



        }

        public async Task<Payment_BillingplanVM> GetPaymentBill(long roleId)
        {

            Payment_BillingplanVM payment_BillingplanVM = new Payment_BillingplanVM();
            try
            {
                PaymentPlan paymentPlan = JsonConvert.DeserializeObject<PaymentPlan>(await httpClient.GetAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.GetPaymentsPlan}?rolesId={roleId}", ""));


                payment_BillingplanVM.Name = paymentPlan.Name;
                payment_BillingplanVM.Price = paymentPlan.Price;
                payment_BillingplanVM.CreatedOn = DateTime.Now;

                int day = payment_BillingplanVM.CreatedOn.Date.Day;
                int month = payment_BillingplanVM.CreatedOn.Date.Month;
                int t = day + 30;

                if (t > 30)
                {
                    int i = t - 30;
                    month += 1;
                    payment_BillingplanVM.NextPaymentDate = "2019-" + month.ToString() + "-" + i.ToString();

                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }




            return payment_BillingplanVM;
        }

        public async Task<Response> JazzCashAcknowledgementReceipt(JazzCashAcknowledgementReceiptVM txnDetail)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>
                (await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.JazzCashAcknowledgementReceipt}", txnDetail));

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> AddLeadgerTransaction(string billReference, string txnRefNo, decimal? amount)
        {
            try
            {
                Exc.AddErrorLog(new Exception($"line 271 = {txnRefNo}"));
                Exc.AddErrorLog(new Exception($"line 271 = {billReference}"));
                string role = txnRefNo.Substring(0, 2);
                Exc.AddErrorLog(new Exception($"line 273 = {role}"));
                JobQuotation jobQuotation = new JobQuotation();
                Response response = new Response();
                HW.SupplierModels.Orders orders = new HW.SupplierModels.Orders();
                Customer customer = new Customer();
                Tradesman tradesman = new Tradesman();

                Response transactionresponse = new Response();
                SubAccount subAccount = new SubAccount();

                subAccount = JsonConvert.DeserializeObject<SubAccount>(
                        await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetHoomWorkJZSubAccount}", ""));


                jobQuotation = JsonConvert.DeserializeObject<JobQuotation>
                        (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationByJobQuotationId}?jobQuotationId={Convert.ToInt64(billReference)}", ""));
                Response orderresponse = new Response();
                var aa = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetOrderByOrderId}?orderId={Convert.ToInt64(billReference)}"));
                var result = JsonConvert.DeserializeObject<List<HW.SupplierModels.Orders>>(aa.ResultData.ToString());
                customer = JsonConvert.DeserializeObject<Customer>(
                      await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={Convert.ToInt64(billReference)}", "")
                      );

                tradesman = JsonConvert.DeserializeObject<Tradesman>(
                    await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={Convert.ToInt64(billReference)}", ""));
                Exc.AddErrorLog(new Exception($"line 302 =  {JsonConvert.SerializeObject(tradesman)}"));
                if (jobQuotation != null && role == "JP")
                {

                    /////////////// Hoomwork Transaction //////////////////
                    ///


                    LeadgerTransection HoomworkleadgerTransction = new LeadgerTransection()
                    {
                        AccountId = subAccount.AccountId,
                        SubAccountId = subAccount.SubAccountId,
                        Debit = amount,
                        Credit = 0,
                        Active = true,
                        RefCustomerSubAccountId = jobQuotation.CustomerId,
                        ReffrenceDocumentNo = jobQuotation.JobQuotationId,
                        ReffrenceDocumentType = "JazzCash Job",
                        CreatedOn = DateTime.Now,
                        CreatedBy = jobQuotation.CustomerId.ToString()
                    };

                    transactionresponse = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", HoomworkleadgerTransction)
                    );

                    return transactionresponse;
                }
                else if (customer != null && role == "CT")
                {
                    SubAccount subAccountCustomer = new SubAccount();
                    subAccountCustomer = JsonConvert.DeserializeObject<HW.PackagesAndPaymentsModels.SubAccount>(
                    await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccount}?customerId={customer.CustomerId}", ""));

                    LeadgerTransection SelfleadgerTransction = new LeadgerTransection()
                    {
                        AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountReceiveables),
                        SubAccountId = subAccountCustomer.SubAccountId,
                        Debit = 0,
                        Credit = amount,
                        Active = true,
                        RefCustomerSubAccountId = customer.CustomerId,
                        ReffrenceDocumentType = "Self Deposit",
                        CreatedOn = DateTime.Now,
                        CreatedBy = customer.CustomerId.ToString()
                    };

                    transactionresponse = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", SelfleadgerTransction)
                    );

                    /////////////// Hoomwork Transaction //////////////////




                    //LeadgerTransection HoomworkleadgerTransction = new LeadgerTransection()
                    //{
                    //    AccountId = Convert.ToInt64(HW.Utility.AccountType.Assets),
                    //    SubAccountId = subAccount.SubAccountId,
                    //    Debit = amount,
                    //    Credit = 0,
                    //    Active = true,
                    //    RefCustomerSubAccountId = customer.CustomerId,
                    //    ReffrenceDocumentType = "JazzCash Deposit",
                    //    CreatedOn = DateTime.Now,
                    //    CreatedBy = customer.CustomerId.ToString()
                    //};

                    //transactionresponse = JsonConvert.DeserializeObject<Response>(
                    //    await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", HoomworkleadgerTransction)
                    //);

                    return transactionresponse;
                }
                else if (tradesman != null && role == "TT")
                {
                    SubAccount subAccountTradesman = new SubAccount();

                    subAccountTradesman = JsonConvert.DeserializeObject<HW.PackagesAndPaymentsModels.SubAccount>(
                        await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccountByTradesmanId}?tradesmanId={tradesman.TradesmanId}&role=Tradesman", ""));
                    Exc.AddErrorLog(new Exception($"line 383 =  {JsonConvert.SerializeObject(subAccountTradesman)}"));


                    LeadgerTransection SelfleadgerTransction = new LeadgerTransection()
                    {
                        AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountPayables),
                        SubAccountId = subAccountTradesman.SubAccountId,
                        Debit = 0,
                        Credit = amount,
                        Active = true,
                        RefTradesmanSubAccountId = tradesman.TradesmanId,
                        ReffrenceDocumentType = "Self Deposit",
                        CreatedOn = DateTime.Now,
                        CreatedBy = tradesman.TradesmanId.ToString()
                    };

                    transactionresponse = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", SelfleadgerTransction)
                    );

                    Exc.AddErrorLog(new Exception($"line 403 =  {JsonConvert.SerializeObject(transactionresponse)}"));

                    /////////////// Hoomwork Transaction //////////////////




                    //LeadgerTransection HoomworkleadgerTransction = new LeadgerTransection()
                    //{
                    //    AccountId = Convert.ToInt64(HW.Utility.AccountType.Assets),
                    //    SubAccountId = subAccount.SubAccountId,
                    //    Debit = amount,
                    //    Credit = 0,
                    //    Active = true,
                    //    RefTradesmanSubAccountId = tradesman.TradesmanId,
                    //    ReffrenceDocumentType = "JazzCash Deposit",
                    //    CreatedOn = DateTime.Now,
                    //    CreatedBy = tradesman.TradesmanId.ToString()
                    //};

                    //transactionresponse = JsonConvert.DeserializeObject<Response>(
                    //    await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", HoomworkleadgerTransction)
                    //);

                    return transactionresponse;
                }
                else if (result != null && role == "OP")
                {
                    LeadgerTransection HoomworkleadgerTransction = new LeadgerTransection()
                    {
                        AccountId = Convert.ToInt64(HW.Utility.AccountType.Assets),
                        SubAccountId = subAccount.SubAccountId,
                        Debit = amount,
                        Credit = 0,
                        Active = true,
                        RefCustomerSubAccountId = result[0].CustomerId,
                        ReffrenceDocumentNo = result[0].Id,
                        ReffrenceDocumentType = "JazzCash Deposit",
                        CreatedOn = DateTime.Now,
                        CreatedBy = result[0].CustomerId.ToString()
                    };

                    transactionresponse = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", HoomworkleadgerTransction)
                    );

                    return transactionresponse;
                }
                return transactionresponse;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> ProceedToJazzCash(JazzCashPaymentDetailVM txnDetail)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>
                (await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.ProceedToJazzCash}", txnDetail));

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<string> GetJazzCashMerchantDetails(string key)
        {
            return await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetJazzCashMerchantDetails}?key=" + key, "");

            //Response response = new Response();
            //try
            //{
            //    response = JsonConvert.DeserializeObject<Response>
            //    (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetJazzCashMerchantDetails}?key="+key, ""));

            //    return response;
            //}
            //catch (Exception ex)
            //{
            //    Exc.AddErrorLog(ex);
            //    return new Response();
            //}
        }

    }
}