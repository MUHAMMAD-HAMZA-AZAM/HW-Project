using System;
using System.Collections.Generic;

#nullable disable

namespace HW.TradesmanModels
{
    public partial class SubSkill
    {
        public long SubSkillId { get; set; }
        public string Name { get; set; }
        public long SkillId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public int? OrderByColumn { get; set; }
        public byte[] SubSkillImage { get; set; }
        public string MetaTags { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string SubSkillTitle { get; set; }
        public decimal? SubSkillPrice { get; set; }
        public string ImagePath { get; set; }
        public decimal? VisitCharges { get; set; }
        public string PriceReview { get; set; }
    
    }
}
