using System;
using System.Collections.Generic;

namespace HW.UserManagmentModels
{
    public partial class ActivePromotion
    {
        public int PromotionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageMobile { get; set; }
        public bool? IsAcitve { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? SkillId { get; set; }
        public bool? IsMain { get; set; }
        public int? UserRoleId { get; set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        public string SubSkillIds { get; set; }
        public int? CampaignTypeId { get; set; }
    }
}
