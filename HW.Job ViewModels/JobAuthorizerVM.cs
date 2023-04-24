using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
    public class JobAuthorizerVM
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string phoneNumber { get; set; }
        public bool isActive { get; set; }
        public string action { get; set; }
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        public DateTime? modifiedOn { get; set; }
        public DateTime? createdOn { get; set; }
    }
}
