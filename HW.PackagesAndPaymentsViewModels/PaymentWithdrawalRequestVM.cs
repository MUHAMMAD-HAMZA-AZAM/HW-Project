using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class PaymentWithdrawalRequestVM
    {
        public long WithdrawalRequestId { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long? TradesmanId { get; set; }
        public string TradesmanName { get; set; }
        public long? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string Cnic { get; set; }
        public decimal? Amount { get; set; }
        public int? PaymentStatusId { get; set; }
        public string PaymentStatusName { get; set; }
        public DateTime? CreatedOn  { get; set; }
        public string CreatedBy  { get; set; }
        public DateTime? ModifiedOn  { get; set; }
        public string ModifiedBy  { get; set; }
        public byte[] IdfrontImage { get; set; }
        public byte[] IdbackImage { get; set; }
        public decimal? TotalWithDrawAmount { get; set; }
    }
}
