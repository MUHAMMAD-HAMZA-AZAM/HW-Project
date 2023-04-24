using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
    public class ReferralVM
    {
        
        public long id { get; set; }
        public string Action { get; set; }
        public int Role { get; set; }
        public DateTime StartingFrom { get; set; }
        public DateTime EndedAt { get; set; }
        public int Limit { get; set; }
        public int Amount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? Active { get; set; }

    }
}
