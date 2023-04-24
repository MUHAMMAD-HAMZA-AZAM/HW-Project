using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class AdViewsVM
    {
        public long UserId { get; set; }
        public long AdViewId { get; set; }
        public long CustomerId { get; set; }
        public long SupplierAdsId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
