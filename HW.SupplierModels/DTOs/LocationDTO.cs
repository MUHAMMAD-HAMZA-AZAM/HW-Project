using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class LocationDTO
    {
        public long LocationId { get; set; }
        public long AreaId { get; set; }
        public string LocationName { get; set; }
        public bool? Active { get; set; }
    }
}
