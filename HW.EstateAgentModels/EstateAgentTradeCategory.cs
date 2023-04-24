using System;
using System.Collections.Generic;

namespace HW.EstateAgentModels
{
    public partial class EstateAgentTradeCategory
    {
        public long EstateAgentTradeCategoryId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
