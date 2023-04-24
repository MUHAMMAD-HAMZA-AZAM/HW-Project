using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ReportsViewModels
{
   public class BidsDTO
    {
        public long BidsId { get; set; }
        public long JobQuotationId { get; set; }
        public string WorkTitle { get; set; }
        public long TradesmanId { get; set; }
        public bool IsOrgnization { get; set; }
        public string TradesmanName { get; set; }
        public decimal WorkBudget { get; set; }
        public string Skill { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public decimal Amount { get; set; }
        public bool IsSelected { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string City { get; set; }
    }
}
