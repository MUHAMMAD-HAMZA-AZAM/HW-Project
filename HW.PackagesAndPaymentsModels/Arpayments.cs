using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class Arpayments
    {
        public long ArpaymentId { get; set; }
        public long? PaymentMethodId { get; set; }
        public decimal? Amount { get; set; }
        public long? AspUserId { get; set; }
        public long? UserRoleId { get; set; }
        public long? OrderId { get; set; }
        public string PaymentStatus { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
