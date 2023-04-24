using System;
using System.Collections.Generic;

namespace HW.PaymentModels
{
    public partial class SupplierMembership
    {
        public long SupplierMembershipId { get; set; }
        public long SupplierId { get; set; }
        public decimal Amount { get; set; }
        public long PaymentMethodId { get; set; }
        public DateTime PaymentMonth { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
