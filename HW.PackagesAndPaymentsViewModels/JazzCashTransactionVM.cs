using System;
namespace HW.PackagesAndPaymentsViewModels
{
    public class JazzCashTransactionVM
    {
        public decimal? Amount { get; set; }
        public string AuthCode { get; set; }
        public string BankID { get; set; }
        public string BillReference { get; set; }
        public string Language { get; set; }
        public string MerchantID { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string RetreivalReferenceNo { get; set; }
        public string SecureHash { get; set; }
        public string SettlementExpiry { get; set; }
        public string SubMerchantId { get; set; }
        public string TxnCurrency { get; set; }
        public string TxnDateTime { get; set; }
        public string TxnRefNo { get; set; }
        public string Version { get; set; }
        public string TxnType { get; set; }
        public string ppmbf_1 { get; set; }
        public string ppmbf_2 { get; set; }
        public string ppmbf_3 { get; set; }
        public string ppmbf_4 { get; set; }
        public string ppmbf_5 { get; set; }
        public string ppmpf_1 { get; set; }
        public string ppmpf_2 { get; set; }
        public string ppmpf_3 { get; set; }
        public string ppmpf_4 { get; set; }
        public string ppmpf_5 { get; set; }
    }
}
