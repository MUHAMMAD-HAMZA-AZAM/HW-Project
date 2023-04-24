using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class SupplierNicImage
    {
        public long NicImageId { get; set; }
        public long SupplierId { get; set; }
        public byte[] NicImage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
