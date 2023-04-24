using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class ProductDetailsDTO
    {
        
        public int? PageNumber { get; set; }
        public int? NoOfRecords { get; set; }
        public int? PageSize { get; set; }
        public long? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string MobileNumber { get; set; }
        public string SateName { get; set; }
        public string AreaName { get; set; }
        public string ShopName { get; set; }
        public string BusinessAddress { get; set; }
        public string RegistrationNumber { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryGroupName { get; set; }
        public long? ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public long? FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool? IsMain { get; set; }
        public bool? IsBlock { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

    }
}
