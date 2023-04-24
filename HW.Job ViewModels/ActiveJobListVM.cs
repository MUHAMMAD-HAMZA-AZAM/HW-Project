using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
   public class ActiveJobListVM
    {
        public long customerId { get; set; }
        public long TradesmanId { get; set; }
        public bool customerStatus { get; set; }
        public long jobId { get; set; }
        public long JobDetailId { get; set; }
        public string StatusId { get; set; }
        public string StreetAddress { get; set; }
        public string jobTitle { get; set; }
        public long NoOfRecoards { get; set; }
        public long RecordNo { get; set; }
        public string CsJobStatusName { get; set; }
        public string city { get; set; }
        public string customerName { get; set; }
        public string Town { get; set; }
        public string mobileNumber { get; set; }
        public string SkillName { get; set; }
        public string SubSkillName { get; set; }
        public string TradesmanName { get; set; }
        public decimal WorkBudget { get; set; }
        public decimal VisitCharges { get; set; }
        public int Quantity { get; set; }
        public long CustomerJobs { get; set; }
        public decimal ServiceCharges { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal AgreedAmount { get; set; }
        public decimal TotalJobAmount { get; set; }
        public decimal EstimatedCommission { get; set; }
        public int recivedBids { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime WorkStartDate { get; set; }
        public bool IsTestUser { get; set; }
        public bool IsAuthorize { get; set; }
        public string Area { get; set; }

    }
}
