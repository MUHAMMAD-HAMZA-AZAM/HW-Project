using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class RsaNicImage
    {
        public long NicImageId { get; set; }
        public long EstateAgentId { get; set; }
        public byte[] NicImage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
