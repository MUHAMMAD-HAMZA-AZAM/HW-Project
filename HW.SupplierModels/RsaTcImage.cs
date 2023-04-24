using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class RsaTcImage
    {
        public long TcImageId { get; set; }
        public long EstateAgentTradeCategoryId { get; set; }
        public byte[] Tcimage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
