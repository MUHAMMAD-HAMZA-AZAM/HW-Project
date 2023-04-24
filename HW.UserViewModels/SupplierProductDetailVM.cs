using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class SupplierProductDetailVM
    {
        public List<byte[]> ProductImages { get; set; }
        public List<string> ImageNames { get; set; }
        public List<long> ImageIds { get; set; }
        public long VideoId { get; set; }
        public byte[] ProductVideo { get; set; }
        public string VideoName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal? FinalPrice { get; set; }
        public string ProductName { get; set; }
        public long SupplierAdId{ get; set; }
        public string SupplierAdReference{ get; set; }
        public string ProductBy { get; set; }
        public string ProductDescription { get; set; }
        public bool DeleiveryAvailable { get; set; }
        public bool CollectionAvailable { get; set; }
        public string SupplierEmail { get; set; }
        public string MobileNumber { get; set; }
        public int AdViews { get; set; }
        public long ProductId { get; set; }
        public long SupplierId { get; set; }
        public bool IsSaved { get; set; }
        public bool IsLiked { get; set; }
        public int Rating { get; set; }
        public string Discount { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
