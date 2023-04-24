using System;
using System.Collections.Generic;

#nullable disable

namespace HW.TradesmanModels
{
    public partial class Skill
    {
        public long SkillId { get; set; }
        public string Name { get; set; }
        public string SkillTitle { get; set; }
        public string MetaTags { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public int? OrderByColumn { get; set; }
        public byte[] SkillImage { get; set; }
        public string SkillIconPath { get; set; }
        public string ImagePath { get; set; }
        public string Slug { get; set; }
        public string SeoPageTitle { get; set; }
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
    }
}
