using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class SkillAndSubSkillVM
    {
        public long SubSkillId { get; set; }
        public string SubSkillName { get; set; }
        public string SubSkillTitle { get; set; }     
        public long SkillId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string SkillName { get; set; }
        public bool IsActive { get; set; }
        public int? OrderByColumn { get; set; }
        public byte[] SkillImage { get; set; }
        public byte[] SubSkillImage { get; set; }
        public string Base64Image { get; set; }
        public string MetaTags { get; set; }
        public string SkillTitle { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string ImagePath { get; set; }
        public decimal? SubSkillPrice { get; set; } 
        public string SkillIconPath { get; set; }
        public decimal? VisitCharges { get; set; }
        public string SeoPageTitle { get; set; }
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
        public string PriceReview { get; set; }

    }
}
