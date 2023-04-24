using System;
using System.Collections.Generic;

namespace HW.UserManagmentModels
{
    public partial class TooltipForm
    {
        public long FormId { get; set; }
        public string FormName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
