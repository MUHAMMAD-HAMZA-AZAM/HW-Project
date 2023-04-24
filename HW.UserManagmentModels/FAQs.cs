using System;
using System.Collections.Generic;

namespace HW.UserManagmentModels
{
    public partial class Faqs
    {
        public long FaqId { get; set; }
        public string Header { get; set; }
        public string FaqsText { get; set; }
        public int? UserType { get; set; }
        public int? OrderByColumn { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
