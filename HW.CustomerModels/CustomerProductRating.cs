using System;
using System.Collections.Generic;

namespace HW.CustomerModels
{
    public partial class CustomerProductRating
    {
        public long CustomerProductRatingId { get; set; }
        public long CustomerId { get; set; }
        public long SupplierAdsId { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
