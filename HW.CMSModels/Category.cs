using System;
using System.Collections.Generic;

#nullable disable

namespace HW.CMSModels
{
    public partial class Category
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public string Createdy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
