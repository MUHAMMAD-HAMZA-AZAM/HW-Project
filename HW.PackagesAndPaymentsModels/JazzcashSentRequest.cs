using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class JazzcashSentRequest
    {
        public long JazzcashSentRequestId { get; set; }
        public decimal? Amount { get; set; }
        public string AuthCode { get; set; }
        public string BankId { get; set; }
        public string BillReference { get; set; }
        public string Language { get; set; }
        public string MerchantId { get; set; }
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
        public string Ppmbf1 { get; set; }
        public string Ppmbf2 { get; set; }
        public string Ppmbf3 { get; set; }
        public string Ppmbf4 { get; set; }
        public string Ppmbf5 { get; set; }
        public string Ppmpf1 { get; set; }
        public string Ppmpf2 { get; set; }
        public string Ppmpf3 { get; set; }
        public string Ppmpf4 { get; set; }
        public string Ppmpf5 { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
