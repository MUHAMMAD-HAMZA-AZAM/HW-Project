using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class DisputeStatus
    {
        public int DisputeStatusId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
