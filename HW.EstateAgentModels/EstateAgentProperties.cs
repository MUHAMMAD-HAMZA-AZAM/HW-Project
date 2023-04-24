using System;
using System.Collections.Generic;

namespace HW.EstateAgentModels
{
    public partial class EstateAgentProperties
    {
        public long EstateAgentPropertiesId { get; set; }
        public long EstateAgentId { get; set; }
        public string AreaAddress { get; set; }
        public string PlotSize { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
