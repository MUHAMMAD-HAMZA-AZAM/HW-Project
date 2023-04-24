using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class PaymentHistoryVM
    {
         
        public decimal Amount { get; set; }
        public long? PaymentMethodId { get; set; }
        public DateTime PaymentMonth { get; set; }
        public bool IsPaid { get; set; }
        public string PaymentMethodCode { get; set; }

    }
}
