using System;
using System.Collections.Generic;

namespace HW.PurchaseModels
{
    public partial class PurchaseDetail
    {
        public long PurchaseDetailId { get; set; }
        public string Title { get; set; }
        public string ProductionDescription { get; set; }
        public long CustomerId { get; set; }
        public long SupplierId { get; set; }
        public decimal Price { get; set; }
        public string ProductionName { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
