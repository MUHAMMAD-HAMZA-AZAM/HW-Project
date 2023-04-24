using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class TooltipVM
    {
        public long FormDetailId { get; set; }
        public long FormId { get; set; }
        public string FormName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Action { get; set; }
        public Boolean IsActive { get; set; }

    }
}
