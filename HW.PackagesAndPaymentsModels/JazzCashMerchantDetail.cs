using System;
using System.Collections.Generic;

#nullable disable

namespace HW.PackagesAndPaymentsModels
{
    public partial class JazzCashMerchantDetail
    {
        public int Id { get; set; }
        public Guid? Guid { get; set; }
        public string PpVersion { get; set; }
        public string PpTxnType { get; set; }
        public string PpLanguage { get; set; }
        public string PpMerchantId { get; set; }
        public string PpSubMerchantId { get; set; }
        public string PpPassword { get; set; }
        public string PpBankId { get; set; }
        public string PpProductId { get; set; }
        public string PpTxnCurrency { get; set; }
        public string PpTxnDateTime { get; set; }
        public string PpTxnExpiryDateTime { get; set; }
        public string PpSecureHash { get; set; }
        public string PpTxnRefNo { get; set; }
        public decimal? PpAmount { get; set; }
        public string PpBillReference { get; set; }
        public string PpDescription { get; set; }
        public string PpReturnUrl { get; set; }
        public string Ppmpf1 { get; set; }
        public string Ppmpf2 { get; set; }
        public string Ppmpf3 { get; set; }
        public string Ppmpf4 { get; set; }
        public string Ppmpf5 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
