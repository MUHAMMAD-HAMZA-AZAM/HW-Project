using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class JobDetail
    {
        public long JobDetailId { get; set; }
        public long JobQuotationId { get; set; }
        public long CustomerId { get; set; }
        public long TradesmanId { get; set; }
        public long SkillId { get; set; }
        public long? SubSkillId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public decimal? ServiceCharges { get; set; }
        public decimal? OtherCharges { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CustomerMessage { get; set; }
        public decimal? TradesmanBudget { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? PaymentStatus { get; set; }
        public decimal? MaterialCharges { get; set; }
        public decimal? AdditionalCharges { get; set; }
        public decimal? TotalJobValue { get; set; }
        public decimal? EstimatedCommission { get; set; }
        public string ChargesDescription { get; set; }
        public int? CsJobStatus { get; set; }
        public bool? IsFinished { get; set; }
    }
}
