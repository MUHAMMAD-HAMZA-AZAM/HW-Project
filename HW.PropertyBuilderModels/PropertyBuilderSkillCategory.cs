using System;
using System.Collections.Generic;

namespace HW.PropertyBuilderModels
{
    public partial class PropertyBuilderSkillCategory
    {
        public long PropertyBuilderSkillCategoryId { get; set; }
        public string SkillCategoryName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
