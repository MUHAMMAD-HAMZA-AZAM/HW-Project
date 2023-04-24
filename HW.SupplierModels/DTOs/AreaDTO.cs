using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class AreaDTO
    {
        public long AreaId { get; set; }
        public int StateId { get; set; }
        public string AreaName { get; set; }
        public bool? Active { get; set; }
    }
}
