using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class TradesmanCommissionOverride
    {
        public int TradesmanCommissionOverrideId { get; set; }
        public int CategoryCommissionSetupId { get; set; }
        public decimal? CommissionOverrideAmount { get; set; }
        public decimal? CommissionOverridePercentage { get; set; }
        public int CategoryId { get; set; }
        public long? TradesmanId { get; set; }
        public string EntityStatus { get; set; }
        public string ReferencePerson { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? CommissionStartDate { get; set; }
        public DateTime? CommissionEndDate { get; set; }
    }
}
