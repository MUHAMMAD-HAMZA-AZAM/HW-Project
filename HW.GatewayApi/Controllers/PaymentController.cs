using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.Job_ViewModels;
using HW.PackagesAndPaymentsModels;
using HW.PackagesAndPaymentsViewModels;
using HW.PaymentModels;
using HW.SupplierViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HW.GatewayApi.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService paymentService;

        JobController jobController;
        private readonly IExceptionService Exc;


        public PaymentController(IPaymentService paymentService, IUserManagementService userManagementService, JobController jobController, IExceptionService exception) : base(userManagementService)
        {
            this.paymentService = paymentService;
            this.jobController = jobController;
            this.Exc = exception;
        }


        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> AddTradesmanJobReceipts(long jobQuotationId, bool directPayment, string paymentMethod, long txnDetailId)
        {
            Response response = new Response();
            response = await paymentService.AddTradesmanJobReceipts(jobQuotationId, directPayment, paymentMethod, txnDetailId);
            return response;
        }

        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> AddTransactionDetail(TransactionDetailVM txnDetail)
        {
            Response response = new Response();
            response = await paymentService.AddTransactionDetail(txnDetail);
            return response;
        }
        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> JazzCashAcknowledgementReceipt([FromBody] JazzCashAcknowledgementReceiptVM txnDetail)
        {
            return await paymentService.JazzCashAcknowledgementReceipt(txnDetail);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<List<AdvertismentPlanVM>> sportLightadvertismentsPlan()
        {
            return await paymentService.sportLightadvertismentsPlan();
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<List<PaymentHistory>> GetPromotionPaymentHistory()
        {
            string userId = DecodeTokenForUser()?.Id;
            return await paymentService.GetPromotionPaymentHistory(userId);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<List<AdvertismentPlanVM>> FeaturedadvertismentsPlan()
        {
            return await paymentService.FeaturedadvertismentsPlan();

        }

        [HttpPost]
        public async Task<ActionResult> JazzCashCallBack([FromBody] JazzCashPaymentDetailVM detailVM)
        {
            try
            {
                //string pp_Amount = formCollection["pp_Amount"];
                //string pp_AuthCode = formCollection["pp_AuthCode"];
                //string pp_BankID = formCollection["pp_BankID"];
                //string pp_BillReference = formCollection["pp_BillReference"];
                //string pp_Language = formCollection["pp_Language"];
                //string pp_MerchantID = formCollection["pp_MerchantID"];
                //string pp_ResponseCode = formCollection["pp_ResponseCode"];
                //string pp_ResponseMessage = formCollection["pp_ResponseMessage"];
                //string pp_RetreivalReferenceNo = formCollection["pp_RetreivalReferenceNo"];
                //string pp_SecureHash = formCollection["pp_SecureHash"];
                //string pp_SettlementExpiry = formCollection["pp_SettlementExpiry"];
                //string pp_SubMerchantId = formCollection["pp_SubMerchantId"];
                //string pp_TxnCurrency = formCollection["pp_TxnCurrency"];
                //string pp_TxnDateTime = formCollection["pp_TxnDateTime"];
                //string pp_TxnRefNo = formCollection["pp_TxnRefNo"];
                //string pp_Version = formCollection["pp_Version"];
                //string pp_TxnType = formCollection["pp_TxnType"];
                //string ppmbf_1 = formCollection["ppmbf_1"];
                //string ppmbf_2 = formCollection["ppmbf_2"];
                //string ppmbf_3 = formCollection["ppmbf_3"];
                //string ppmbf_4 = formCollection["ppmbf_4"];
                //string ppmbf_5 = formCollection["ppmbf_5"];
                //string ppmpf_1 = formCollection["ppmpf_1"];
                //string ppmpf_2 = formCollection["ppmpf_2"];
                //string ppmpf_3 = formCollection["ppmpf_3"];
                //string ppmpf_4 = formCollection["ppmpf_4"];
                //string ppmpf_5 = formCollection["ppmpf_5"];

                // long BidId = Convert.ToInt64(pp_BillReference);
                JazzCashAcknowledgementReceiptVM jazzCashAcknowledgementReceiptVM = new JazzCashAcknowledgementReceiptVM()
                {
                    Amount = Convert.ToInt64(detailVM.pp_Amount),
                    AuthCode = detailVM.pp_AuthCode,
                    BankID = detailVM.pp_BankID,
                    BillReference = detailVM.pp_BillReference,
                    Language = detailVM.pp_Language,
                    MerchantID = detailVM.pp_MerchantID,
                    ResponseCode = detailVM.pp_ResponseCode,
                    ResponseMessage = detailVM.pp_ResponseMessage,
                    RetreivalReferenceNo = detailVM.pp_RetreivalReferenceNo,
                    SecureHash = detailVM.pp_SecureHash,
                    SettlementExpiry = detailVM.pp_SettlementExpiry,
                    SubMerchantId = detailVM.pp_SubMerchantID,
                    TxnCurrency = detailVM.pp_TxnCurrency,
                    TxnDateTime = detailVM.pp_TxnDateTime,
                    TxnRefNo = detailVM.pp_TxnRefNo,
                    Version = detailVM.pp_Version,
                    TxnType = detailVM.pp_TxnType,
                    //ppmbf_1 = detailVM.ppmbf_1,
                    //ppmbf_2 = detailVM.ppmbf_2,
                    //ppmbf_3 = detailVM.ppmbf_3,
                    //ppmbf_4 = detailVM.ppmbf_4,
                    //ppmbf_5 = detailVM.ppmbf_5,
                    ppmpf_1 = detailVM.ppmpf_1,
                    ppmpf_2 = detailVM.ppmpf_2,
                    ppmpf_3 = detailVM.ppmpf_3,
                    ppmpf_4 = detailVM.ppmpf_4,
                    ppmpf_5 = detailVM.ppmpf_5,
                    CreatedOn = DateTime.Now,
                    CreatedBy = "Admin"
                };
                Exc.AddErrorLog(new Exception(JsonConvert.SerializeObject(jazzCashAcknowledgementReceiptVM)));
                Response txnRespponse = await JazzCashAcknowledgementReceipt(jazzCashAcknowledgementReceiptVM);
                Exc.AddErrorLog(new Exception(JsonConvert.SerializeObject(txnRespponse)));

                if (txnRespponse.Status == ResponseStatus.OK && detailVM.pp_ResponseCode == "000")
                {
                    Response response = new Response();
                    response =  await paymentService.AddLeadgerTransaction(jazzCashAcknowledgementReceiptVM.BillReference,jazzCashAcknowledgementReceiptVM.TxnRefNo, (jazzCashAcknowledgementReceiptVM.Amount/100));
                    Exc.AddErrorLog(new Exception(JsonConvert.SerializeObject(response)));

                }
                // TransactionDetailVM transactionDetail = new TransactionDetailVM()
                //{
                //    UserId = pp_MerchantID,
                //    ResponseCode = pp_ResponseCode,
                //    ResponseMessage = pp_ResponseMessage,
                //    ReferenceNo = pp_TxnRefNo,
                //    MerchantId = pp_MerchantID,
                //    SubMerchantId = pp_SubMerchantId,
                //    Currency = pp_TxnCurrency,
                //    InitiateTime = pp_TxnDateTime,
                //    TransactionType = pp_TxnType,
                //    RetreivalReferenceNo = pp_RetreivalReferenceNo
                //};

                //Exc.AddErrorLog(new Exception(JsonConvert.SerializeObject(transactionDetail)));



                //  Response txnRespponse = await AddTransactionDetail(transactionDetail);

                //if (pp_ResponseCode == "000")
                //{
                //    Response response = await jobController.AddJobDetails(BidId, false, (int)BidStatus.Accepted);
                //    Response receiptRespponse = await AddTradesmanJobReceipts(Convert.ToInt64(response.ResultData), false, pp_TxnType, Convert.ToInt64(txnRespponse.ResultData));
                //}

                // return Redirect($"http://localhost:4200/Message/SuccessMessage?pp_ResponseCode={detailVM.pp_ResponseCode}&pp_BillReference={pp_BillReference}");
                //return Redirect($"https://www.hoomwork.com/Message/SuccessMessage?pp_ResponseCode={detailVM.pp_ResponseCode}&pp_BillReference={detailVM.pp_BillReference}&pp_ResponseMessage={detailVM.pp_ResponseMessage}");
                //  return Redirect($"http://test2.hoomwork.com/Message/SuccessMessage?pp_ResponseCode={detailVM.pp_ResponseCode}&pp_BillReference={pp_BillReference}&pp_ResponseMessage={pp_ResponseMessage}");
                return Ok();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return NotFound();
            }
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<PaymentHistoryVM>> GetpaymentRecord()
        {
            return await paymentService.GetpaymentRecord(await GetEntityIdByUserId());
        }
        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Payment_BillingplanVM> GetPaymentBill()
        {
            return await paymentService.GetPaymentBill(await GetEntityIdByUserId());
        }

        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> ProceedToJazzCash([FromBody] JazzCashPaymentDetailVM txnDetail)
        {
            //long userId = await GetEntityIdByUserId();
            txnDetail.UserId = 0;
            return await paymentService.ProceedToJazzCash(txnDetail);
        }
        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<string> GetJazzCashMerchantDetails(string key)
        {
            return await paymentService.GetJazzCashMerchantDetails(key);
        }

    }
}