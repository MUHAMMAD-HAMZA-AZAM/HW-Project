using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class FiscalPeriod
    {
        public int FiscalPeriodId { get; set; }
        public int? FiscalPeriodStatusId { get; set; }
        public int? FiscalYear { get; set; }
        public int? Period { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsRestricted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
