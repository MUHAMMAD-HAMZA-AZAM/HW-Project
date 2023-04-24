using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ReportsViewModels
{
   public class TownVM
    {
        public long TownId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }

    }
}
