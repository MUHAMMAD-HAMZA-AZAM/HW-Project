using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class GetPaymentDetailVM
    {
        public decimal Amount { get; set; }
        public string To { get; set; }
        public DateTime TransactionDate { get; set; }
        public double RefNo { get; set; }
    }
}
