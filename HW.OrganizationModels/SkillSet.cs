using System;
using System.Collections.Generic;

namespace HW.OrganizationModels
{
    public partial class SkillSet
    {
        public long SkillSetId { get; set; }
        public long OrganizationId { get; set; }
        public long SkillId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
