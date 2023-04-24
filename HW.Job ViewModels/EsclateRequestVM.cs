using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
    public class EsclateRequestVM
    {
        public long Id { get; set; }
        public int? EsclateOptionId { get; set; }
        public string Comment { get; set; }
        public long? CustomerId { get; set; }
        public long? JobQuotationId { get; set; }
        public long? TradesmanId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? Active { get; set; }
        public bool? Approve { get; set; }
        public string EsclateOptionName { get; set; }
        public string CustomerName { get; set; }
        public string TradesmanName { get; set; }
        public string JobTitle { get; set; }
        public string Status { get; set; }
        public string UserRole { get; set; }

    }
}
