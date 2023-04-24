using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class CampaignVM
    {
        public long CampaignId { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
