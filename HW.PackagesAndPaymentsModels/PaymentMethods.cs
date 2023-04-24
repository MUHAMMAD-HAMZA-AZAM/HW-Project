using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class PaymentMethods
    {
        public long PaymentMethodId { get; set; }
        public string PaymentMethodCode { get; set; }
        public string PaymentMethodName { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
