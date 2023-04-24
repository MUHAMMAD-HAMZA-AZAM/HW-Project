using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class AdsStatus
    {
        public int AdsStatusId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
