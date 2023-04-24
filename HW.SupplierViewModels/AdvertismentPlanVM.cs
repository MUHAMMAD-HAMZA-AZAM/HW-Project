using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class AdvertismentPlanVM
    {
        public long AdvertisementPlanId { get; set; }
        public decimal Price { get; set; }
        public int Days { get; set; }
        public long AdsStatusId { get; set; }
    }
}
