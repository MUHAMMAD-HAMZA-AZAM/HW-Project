using System;
using System.Collections.Generic;

namespace HW.ImageModels
{
    public partial class SupplierProductImage
    {
        public long ProductImageId { get; set; }
        public long ProductCategoryId { get; set; }
        public string ImageName { get; set; }
        public byte[] ProductImage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
