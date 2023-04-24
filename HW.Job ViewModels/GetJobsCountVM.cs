using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
   public class GetJobsCountVM
    {
        public int ActiveBids  { get; set; }
        public int ActiveJobs { get; set; }
        public int CompletedJobs { get; set; }
        public int AcceptedBids { get; set; }
    }
}
