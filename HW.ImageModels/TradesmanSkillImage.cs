using System;
using System.Collections.Generic;

namespace HW.ImageModels
{
    public partial class TradesmanSkillImage
    {
        public long TradesmanSkillImageId { get; set; }
        public long SkillId { get; set; }
        public string ImageName { get; set; }
        public byte[] SkillImage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
