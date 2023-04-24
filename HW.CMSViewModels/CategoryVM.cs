using System;

namespace HW.CMSViewModels
{
    public partial class CategoryVM
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
