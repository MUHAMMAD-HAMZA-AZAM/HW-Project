using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class EsclateOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? UserRole { get; set; }
    }
}
