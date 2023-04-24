using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
    public class JazzCashPaymentDetailVM
    {
        public int Id { get; set; }
        public string pp_Version { get; set; }
        public string pp_TxnType { get; set; }
        public string pp_Language { get; set; }
        public string pp_MerchantID { get; set; }
        public string pp_SubMerchantID { get; set; }
        public string pp_Password { get; set; }
        public string pp_BankID { get; set; }
        public string pp_ProductID { get; set; }
        public string pp_TxnRefNo { get; set; }
        public decimal pp_Amount { get; set; }
        public string pp_TxnCurrency { get; set; }
        public string pp_TxnDateTime { get; set; }
        public string pp_BillReference { get; set; }
        public string pp_Description { get; set; }
        public string pp_TxnExpiryDateTime { get; set; }
        public string pp_ReturnURL { get; set; }
        public string pp_SecureHash { get; set; }
        public string ppmpf_1 { get; set; }
        public string ppmpf_2 { get; set; }
        public string ppmpf_3 { get; set; }
        public string ppmpf_4 { get; set; }
        public string ppmpf_5 { get; set; }
        public string ppmbf_1 { get; set; }
        public string ppmbf_2 { get; set; }
        public string ppmbf_3 { get; set; }
        public string ppmbf_4 { get; set; }
        public string ppmbf_5 { get; set; }
        public string pp_AuthCode { get; set; }
        public string pp_ResponseCode { get; set; }
        public string pp_ResponseMessage { get; set; }
        public string pp_RetreivalReferenceNo { get; set; }
        public string pp_SettlementExpiry { get; set; }
        public long UserId { get; set; }
    }
}
