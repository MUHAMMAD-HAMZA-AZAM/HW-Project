using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
    public class AssignJobVM
    {
        public decimal Amount { get; set; }
        public string Comments { get; set; }
        public long CustomerId { get; set; }
        public long JobQuotationId { get; set; }
        public long TradesmanId { get; set; }
        public string CreatedBy { get; set; }
    }

}
