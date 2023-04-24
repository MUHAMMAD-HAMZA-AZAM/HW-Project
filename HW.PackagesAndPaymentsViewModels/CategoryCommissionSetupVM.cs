using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class CategoryCommissionSetupVM
    {
        
        public long id { get; set; }
        public string Action { get; set; }
        public int CategoryCommissionSetupId { get; set; }
        public int CategoryId { get; set; }
        public long TradesmanId { get; set; }
        public string ReferencePerson { get; set; }
        public bool? Active { get; set; }
        public string CategoryName { get; set; }
        public decimal? CommisionAmount { get; set; }
        public decimal? CommissionPercentage { get; set; }
        public string EntityStatus { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? CommissionStartDate { get; set; }
        public DateTime? CommissionEndDate { get; set; }
    }
}
