using System;
using System.Collections.Generic;

namespace HW.ImageModels
{
    public partial class FeaturedSupplierImages
    {
        public int ImageId { get; set; }
        public long? SupplierId { get; set; }
        public byte[] ProfileImage { get; set; }
        public bool? IsActive { get; set; }
    }
}
