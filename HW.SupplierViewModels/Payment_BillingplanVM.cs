using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class Payment_BillingplanVM
    {



        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string NextPaymentDate { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}