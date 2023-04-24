using System;
using System.Collections.Generic;
using System.Text;

namespace HW.JobModels
{
    public partial class FacebookLeads
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? Budget { get; set; }
        public long? SkillId { get; set; }
        public long? SubSkillId { get; set; }
        public DateTime? StartedDate { get; set; }
    }
}
