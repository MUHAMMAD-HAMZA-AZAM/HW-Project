using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class SupplierFeedback
    {
        public long SupplierFeedbackId { get; set; }
        public long CustomerId { get; set; }
        public long SupplierId { get; set; }
        public string Comments { get; set; }
        public int? OverallRating { get; set; }
        public int? CommunicationRating { get; set; }
        public int? QualityRating { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
