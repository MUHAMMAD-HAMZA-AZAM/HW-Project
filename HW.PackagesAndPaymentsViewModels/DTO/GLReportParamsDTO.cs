using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    public class GLReportParamsDTO
    {
        public DateTime? FiscalPeriod { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SubAccountId { get; set; }
    }
}
