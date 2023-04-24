using System;
using System.Collections.Generic;

#nullable disable

namespace HW.CMSModels
{
    public partial class SubCategory
    {
        public long SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public long? CategoryId { get; set; }
        public string Slug { get; set; }
        public string Createdy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
