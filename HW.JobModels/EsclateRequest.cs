using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class EsclateRequest
    {
        public long Id { get; set; }
        public int? EsclateOptionId { get; set; }
        public string Comment { get; set; }
        public long CustomerId { get; set; }
        public long JobQuotationId { get; set; }
        public long TradesmanId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? Active { get; set; }
        public bool? Approve { get; set; }
        public int? UserRole { get; set; }
        public int? Status { get; set; }
    }
}
