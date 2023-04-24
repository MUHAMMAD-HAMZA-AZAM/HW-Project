using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class AreaListVM
    {
        public long AreaId { get; set; }

        public long CityId { get; set; }
        public string CityName { get; set; }
        public string UserId { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string AreaName { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
