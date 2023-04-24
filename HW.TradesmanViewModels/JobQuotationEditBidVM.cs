using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class JobQuotationEditBidVM
    {
        public long JobQuotationId { get; set; }
        public long CustomerId { get; set; }
        public decimal BidBudget { get; set; }
        public string Message { get; set; }
        public byte[] Audio { get; set; }
 
    }
}
