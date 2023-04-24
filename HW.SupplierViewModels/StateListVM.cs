using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class StateListVM
    {

        public long? CountryId { get; set; }
        public long StateId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CountryName { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
