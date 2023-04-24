using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class RateTradesmanVM
    {
        public long TradesmanFeedbackId { get; set; }
        public long JobQuotationId { get; set; }
        public byte[] TradesmanImage { get; set; }
        public string TradesmanName { get; set; }
        public long TradesmanId { get; set; }
        public long CustomerId { get; set; }
        public string JobTitle { get; set; }
        public DateTime JobStartingDateTime { get; set; }
        public DateTime JobEndingDateTime { get; set; }
        public bool DirectPayment { get; set; }
        public decimal  JobBudget { get; set; }
    }
}
