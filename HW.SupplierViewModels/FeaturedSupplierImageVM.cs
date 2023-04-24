using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class FeaturedSupplierImageVM
    {
       // public List<byte[]> profileImage { get; set; }
       public byte[] profileImage { get; set; }
       // public bool FImageStatus { get; set; }
        public int ImageId { get; set; }
        public long? SupplierId { get; set; }
        public bool? IsActive { get; set; }

    }
}
