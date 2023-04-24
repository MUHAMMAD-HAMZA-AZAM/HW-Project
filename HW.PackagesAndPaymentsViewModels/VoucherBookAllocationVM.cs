using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class VoucherBookAllocationVM
    {
        public int VoucherBookAllocationId { get; set; }
        public int VoucherBookId { get; set; }
        public string VoucherBookName { get; set; }
        public string AssigneeFirstName { get; set; }
        public string AssigneeLastName { get; set; }
        public string AssigneeName { get; set; }
        public string ContactNo { get; set; }
        public bool? IsInternalPerson { get; set; }
        public string EmployDesignation { get; set; }
        public string ExternalPersonOccupation { get; set; }
        public string ExternalDesignation { get; set; }
        public string Company { get; set; }
        public int? NopagesAssigned { get; set; }
        public int? NoOfLeavesAssigned { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
