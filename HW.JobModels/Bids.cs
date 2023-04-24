using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class Bids
    {
        public long BidsId { get; set; }
        public long JobQuotationId { get; set; }
        public long TradesmanId { get; set; }
        public long CustomerId { get; set; }
        public string Comments { get; set; }
        public decimal Amount { get; set; }
        public bool IsSelected { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string CustomerName { get; set; }
        public string StatusName { get; set; }
        public string TradesmanName { get; set; }
        public bool? IsStarted { get; set; }
    }
}
