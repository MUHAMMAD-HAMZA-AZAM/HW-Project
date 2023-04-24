using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ReportsViewModels
{
   public class ProductCategoryVM
    {
        
            public long ProductCategoryId { get; set; }
            public string Code { get; set; }
            public byte[] ProductImage { get; set; }
            public string imagebase64 { get; set; }
            public string Name { get; set; }
            public bool IsActive { get; set; }
        public bool? HasSubItems { get; set; }
        public int? OrderByColumn { get; set; }

            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? ModifiedOn { get; set; }
            public string ModifiedBy { get; set; }
           public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string OgTitle { get; set; }
          public string OgDescription { get; set; }
          public string Canonical { get; set; }

    }
}
