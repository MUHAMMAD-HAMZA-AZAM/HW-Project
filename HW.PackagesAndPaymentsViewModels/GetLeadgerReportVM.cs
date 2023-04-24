using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class GetLeadgerReportVM
    {
        public long? TradesmanJobReceiptsId { get; set; }
        public long? CustomerId { get; set; }
        public int? PaymentStatus { get; set; }
        public long? TradesmanId { get; set; }
        public long? JobQuotationId { get; set; }
        public string CustomerName { get; set; }
        public long? JobDetailId { get; set; }
        public string JobTitle { get; set; }
        public  decimal? JobAmount { get; set; }
        public decimal? ServiceCharges { get; set; }
        public decimal? AdditionalCharges { get; set; }
        public decimal? AgreedAmount { get; set; }
        public decimal? Commission { get; set; }
        public decimal? CustomerPayable { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? DiscountedAmount { get; set; }
        public string DiscountMethod { get; set; }
        public string TradesmanName { get; set; }
        public decimal? HoomworkPayableToTradesman { get; set; }
        public decimal? TradesmanPayableToHoomwork { get; set; }
        public decimal? PaidByWallet { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalDiscountedAmount { get; set; }
        public decimal? TotalCustomerPayable { get; set; }
        public decimal? TotalHoomworkPayable { get; set; }
        public decimal? TotalTradesmanPayable { get; set; }
        public string Section { get; set; }
        public decimal? CustomerWalletAmount { get; set; }
        public decimal? TradesmanWalletAmount { get; set; }
        public decimal? ReferralAmount { get; set; }
        public decimal? CustomerTopup { get; set; }
        public decimal? TradesmanTopup { get; set; }
    }
}
