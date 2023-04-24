using System;
using System.Collections.Generic;

namespace HW.EstateAgentModels
{
    public partial class EstateAgent
    {
        public long EstateAgentId { get; set; }
        public string AgencyName { get; set; }
        public string OwnerName { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string NoOfEmployees { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
