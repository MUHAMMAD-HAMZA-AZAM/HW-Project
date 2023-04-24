using System;
using System.Collections.Generic;
using System.Text;

namespace HW.CMSViewModel
{
   public  class SitePagesVM
    {
        public int PageId { get; set; }

        public int? ProjectId { get; set; }
        public string PageName { get; set; }
          
        public string UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

    }
}
