using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
    public class TransectionReportVM
    {
        public long? LeadgerTransectionId { get; set; }
        public string TransectionType { get; set; }
        public decimal? Amount { get; set; }
        public long? UserId { get; set; }
        public int? UserTypeId { get; set; }
        public string Name { get;set;}
        public DateTime? CreatedOn { get; set; }

    }
}
