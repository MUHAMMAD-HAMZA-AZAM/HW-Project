using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class CategoryCommissionSetup
    {
        public int CategoryCommissionSetupId { get; set; }
        public int CategoryId { get; set; }
        public decimal? CommisionAmount { get; set; }
        public decimal? CommissionPercentage { get; set; }
        public string EntityStatus { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? CommissionStartDate { get; set; }
        public DateTime? CommissionEndDate { get; set; }
    }
}
