using HW.Job_ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ReportsViewModels
{
   public class JobQuotationDTO
    {
        public long CustomerId { get; set; }
        public long JobQuotationId { get; set; }
        public long JobDetailId { get; set; }
        public long SkillId { get; set; }
        public string SkillName { get; set; }
        public long? SubSkillId { get; set; }
        public string SubSkillName { get; set; }
        public string WorkTitle { get; set; }
        public string WorkDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfileImage { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobileNo { get; set; }
        public DateTime WorkStartDate { get; set; }
        public DateTime SingUpDate { get; set; }
        public decimal? WorkBudget { get; set; }
        public decimal? AgreedBudget { get; set; }
        public decimal? VisitCharges { get; set; }
        public decimal? ServiceCharges { get; set; }
        public decimal? OtherCharges { get; set; }
        public decimal? TradesmanBudget { get; set; }       
        public decimal? EstimatedCommission { get; set; }       
        public decimal? AdditionalCharges { get; set; }       
        public decimal? MaterialCharges { get; set; }       
        public decimal? TotalJobValue { get; set; }       
        public string ChargesDescription { get; set; }       
        public string JobAddress { get; set; }
        public string Area { get; set; }
        public int DesiredBids { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string TradesmanName { get; set; }
        public string CustomerMessage { get; set; }
        public bool SelectiveTradesman { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool isTestUser { get; set; }
        public string City { get; set; }
        public int CSJobStatusId { get; set; }
        public int CSJQJobStatusId { get; set; }
        public List<BidsDTO> BidsList { get; set; }
        public List<JobActivity> jobActivity { get; set; }
        public List<NotificationDTO> notificationDTO { get; set; }
        public List<CSJobRemarksVM> cSJobRemarksVM { get; set; }

    }
    public class NotificationDTO
    {
        public string title { get; set; }
        public string body { get; set; }
        public string senderEntityId { get; set; }
        public string targetActivity { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
