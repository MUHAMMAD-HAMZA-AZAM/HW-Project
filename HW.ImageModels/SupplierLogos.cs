using System;
using System.Collections.Generic;

namespace HW.ImageModels
{
    public partial class SupplierLogos
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public long SupplierId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
