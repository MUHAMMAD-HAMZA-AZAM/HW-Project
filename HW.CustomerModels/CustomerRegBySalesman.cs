using System;
using System.Collections.Generic;

namespace HW.CustomerModels
{
    public partial class CustomerRegBySalesman
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long SalesmanId { get; set; }
        public long? CampaignId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
