using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
