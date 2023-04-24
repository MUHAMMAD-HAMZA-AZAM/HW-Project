using System;
using System.Collections.Generic;

namespace HW.UserManagmentModels
{
    public partial class TooltipFormDetail
    {
        public long FormDetailId { get; set; }
        public long? FormId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
