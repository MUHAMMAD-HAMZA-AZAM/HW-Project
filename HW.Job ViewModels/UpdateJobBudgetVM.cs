using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
   public  class UpdateJobBudgetVM
    {
        public long JobQuotationId { get; set; }
        public decimal? WorkBudget { get; set; }
        public decimal? VisitCharges { get; set; }
        public decimal? OtherCharges { get; set; }
        public decimal? ServiceCharges { get; set; }
        public decimal? AdditionalCharges { get; set; }
        public decimal? MaterialCharges { get; set; }
        public decimal? TotalJobValue { get; set; }
        public int? JobStatus { get; set; }
        public int? Quantity { get; set; }
        public decimal? EstimatedCommission { get; set; }
        public string ChargesDescription { get; set; }
        public int? CsJobStatus { get; set; }
        public int? ChangeStatus { get; set; }
        //Job remarks Properties
        public string RemarksDescription { get; set; }
        public int? CallTries { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}
