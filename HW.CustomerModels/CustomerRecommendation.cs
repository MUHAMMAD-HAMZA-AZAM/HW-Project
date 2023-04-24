using System;
using System.Collections.Generic;

namespace HW.CustomerModels
{
    public partial class CustomerRecommendation
    {
        public long CustomerRecommendationId { get; set; }
        public long FromCustomerId { get; set; }
        public long ToCustomerId { get; set; }
        public long? TradesmanId { get; set; }
        public long? SupplierId { get; set; }
        public long? EstateAgentId { get; set; }
        public long Rating { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
