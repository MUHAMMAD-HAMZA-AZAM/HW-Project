using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class MarkAsFinishJobVM
    {
        public long JobDetailId { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public long CustomerId { get; set; }
        public long? TradesmanId { get; set; }
        public long JobQuotationId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long BidId { get; set; }
        public int BidStatus { get; set; }
        public bool isPaymentDirect { get; set; }
    }
}
