using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class JobQuotation
    {
        public long JobQuotationId { get; set; }
        public long SkillId { get; set; }
        public long? SubSkillId { get; set; }
        public string WorkTitle { get; set; }
        public string WorkDescription { get; set; }
        public long CustomerId { get; set; }
        public DateTime? WorkStartDate { get; set; }
        public decimal? WorkBudget { get; set; }
        public decimal? VisitCharges { get; set; }
        public decimal? ServiceCharges { get; set; }
        public decimal? OtherCharges { get; set; }
        public int? Quantity { get; set; }
        public decimal? EstimatedCommission { get; set; }
        public string JobAddress { get; set; }
        public int? DesiredBids { get; set; }
        public bool? AuthorizeJob { get; set; }
        public int? CsjobStatusId { get; set; }
        public int? StatusId { get; set; }
        public string CustomerMessage { get; set; }
        public bool? SelectiveTradesman { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }  
        public TimeSpan? WorkStartTime { get; set; }
        public string GpsCoordinates { get; set; }
    }
}
