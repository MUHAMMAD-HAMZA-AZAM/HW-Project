using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
    public class JobsVm
    {
        public long JobsCreated { get; set; }
        public long JobsInProgress { get; set; }
        public long JobsCompleted { get; set; }
        public long ActiveJobs { get; set; }
        public long JobsApprove { get; set; }
        public long DeclinedJobs { get; set; }
        public long DeletedJobs { get; set; }
    }
}
