using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class Area
    {
        public long AreaId { get; set; }
        public int StateId { get; set; }
        public string AreaName { get; set; }
        public long CityId { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
