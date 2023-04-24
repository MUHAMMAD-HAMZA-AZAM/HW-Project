using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    public class FiscalPeriodDTO
    {
        public int? fiscalPeriodId { get; set; }
        public int? fiscalYear { get; set; }
        public int? periodId { get; set; }
        public string periodName { get; set; }
        public int? status { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string userId { get; set; }
    }
}
