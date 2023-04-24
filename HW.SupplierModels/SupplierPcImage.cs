using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class SupplierPcImage
    {
        public long PcimageId { get; set; }
        public long ProductCategoryId { get; set; }
        public string ImageName { get; set; }
        public byte[] Pcimage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
