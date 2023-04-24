using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class CancellationReasonDTO
    {
        public int Id { get; set; }
        public string ReasonName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? UserRole { get; set; }
        public string ReasonFor { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
    }
}
