using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
  public class InprogressJobList
    {
        public long customerId { get; set; }
        public long jobDetailId { get; set; }
        public long jobId { get; set; }
        public string Name { get; set; }
        public long JobCategoryCount { get; set; }
        public long TotalJobsCount { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string jobTitle { get; set; }
        public long NoOfRecoards { get; set; }
        public long RecordNo { get; set; }
        public string city { get; set; }
        public string customerName { get; set; }
        public DateTime WorkstartDate { get; set; }
        public decimal WorkBudget { get; set; }
        public string Town { get; set; }
        public string mobileNumber { get; set; }
        public string Assignee { get; set; }
        public DateTime createdOn { get; set; }
        public bool? userType { get; set; }
    }
}
