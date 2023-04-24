using System;
using System.Collections.Generic;

#nullable disable

namespace HW.CMSModels
{
    public partial class SitePage
    {
        public int PageId { get; set; }
        public int? ProjectId { get; set; }

        public string PageName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
