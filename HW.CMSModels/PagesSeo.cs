using System;
using System.Collections.Generic;

#nullable disable

namespace HW.CMSModels
{
    public partial class PagesSeo
    {
        public int PageId { get; set; }
        public int? SitePageId { get; set; }
        public int? ProjectId { get; set; }
        public string PageTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
        public string Canonical { get; set; }
    }
}
