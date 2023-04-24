using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class ActivePromotionVM
    {
        public int PromotionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageMobile { get; set; }
        public string Base64Image { get; set; }
        public string Base64Mobile { get; set; }
        public bool? IsAcitve { get; set; }
        public bool? IsMain { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Action { get; set; }
        public string SkillName { get; set; }
        public int SkillId { get; set; }
        public string SubSkillIds { get; set; }
        public int? UserRoleId { get; set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        public string PromotionFor { get; set; }
        public string CampaignFor { get; set; }
        public int? CampaignTypeId { get; set; }
        public string CampaignTypeName { get; set; }
    }
    public class ActivePromotionMobileVM
    {
        public int PromotionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SkillName { get; set; }
        public int SkillId { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageWeb { get; set; }
        public bool IsMain { get; set; }
    }
}
