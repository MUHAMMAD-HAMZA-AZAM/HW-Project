using System;
using System.Collections.Generic;
using System.Text;

namespace HW.CMSViewModel
{
    public class PageSeoVM
    {
        public int PageId { get; set; }

        public int? ProjectId { get; set; }
        public string PageName { get; set; }
        public int SitePageId { get; set; }
        public string PageTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Canonical { get; set; }
        public string OgDescription { get; set; }
        public string OgTitle { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string FormAction { get; set; }

    }
}
