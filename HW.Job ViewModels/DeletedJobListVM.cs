using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
    public class DeletedJobListVM
    {
        public long customerId { get; set; }
        public string customerName { get; set; }
        public bool customerStatus { get; set; }
        public long jobId { get; set; }
        public string StatusId { get; set; }
        public string jobTitle { get; set; }
        public long NoOfRecoards { get; set; }
        public string city { get; set; }    
        public string mobileNumber { get; set; }
        public decimal WorkBudget { get; set; }
        public decimal VisitCharges { get; set; }
        public int Quantity { get; set; }
        public long CustomerJobs { get; set; }
        public decimal ServiceCharges { get; set; }
        public decimal OtherCharges { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime WorkStartDate { get; set; }
        public bool IsTestUser { get; set; }
        public bool IsAuthorize { get; set; }
        public string Town { get; set; }
    }
}
