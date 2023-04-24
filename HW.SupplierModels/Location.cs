using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class Location
    {
        public long LocationId { get; set; }
        public long AreaId { get; set; }
        public string LocationName { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
