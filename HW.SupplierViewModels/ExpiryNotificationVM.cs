using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public partial class ExpiryNotificationVM
    {
        public long SupplierAdsId { get; set; }
        public long SupplierId { get; set; }
        public int AdsStatusId { get; set; }
        public string AdTitle { get; set; }
        public string WorkTitle { get; set; }
        public decimal Price { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string FirebaseClientId { get; set; }
    }
}
