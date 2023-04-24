using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class LocationListVM
    {
        public long LocationId { get; set; }
        public long AreaId { get; set; }
        public string LocationName { get; set; }
        public string UserId { get; set; }
        public string AreaName { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
