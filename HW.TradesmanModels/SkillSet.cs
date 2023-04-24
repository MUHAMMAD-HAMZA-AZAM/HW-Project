using System;
using System.Collections.Generic;

#nullable disable

namespace HW.TradesmanModels
{
    public partial class SkillSet
    {
        public long SkillSetId { get; set; }
        public long SkillId { get; set; }
        public long TradesmanId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
