using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class VendorProfileDTO
    {
        public long? SupplierId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int? DiscountInPercentage { get; set; }
        public byte[] ProfileImage { get; set; }
        public int NoOfRecords { get; set; }
        public byte[] ShopCoverImage { get; set; }
        public string ShopName { get; set; }
        public string BusinessDescription { get; set; }
        public string Slug { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long ProductId { get; set; }
        public int Year { get; set; }
    }
}
