using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
    public class EscalateOptionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? UserRole { get; set; }
        public string EscalateOptionFor { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
    }

}
