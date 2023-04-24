using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class SavedAdsVM
    {
        public long CustomerId { get; set; }
        public List<long> SupplierAdIds { get; set; }
    }
}
