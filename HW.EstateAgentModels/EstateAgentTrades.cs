using System;
using System.Collections.Generic;

namespace HW.EstateAgentModels
{
    public partial class EstateAgentTrades
    {
        public long EstateAgentTradesId { get; set; }
        public long EstateAgentTradeCategoryId { get; set; }
        public long EstateAgentId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
