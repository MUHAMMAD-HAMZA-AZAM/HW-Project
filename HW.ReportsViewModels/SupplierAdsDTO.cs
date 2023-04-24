using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ReportsViewModels
{
  public  class SupplierAdsDTO
    {
        public long SupplierAdsId { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long? ProductCategoryId { get; set; }
        public string ProductName { get; set; }
        public long ProductSubCategoryId { get; set; }
        public long NoOfRecoards { get; set; }
        public string SubCategoryName { get; set; }
        public int AdsStatusId { get; set; }
        public string AddStatus { get; set; }
        public string AdTitle { get; set; }
        public string AdDescription { get; set; }
        public decimal Price { get; set; }
        public string AdReference { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }
        public int? AdViewCount { get; set; }
        public bool IsDeliverable { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
