using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class ProductSubCategoryDTO
    {
        public long ProductCategoryId { get; set; }
        public long ProductSubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public bool IsActive { get; set; }
        public bool? HasSubItems { get; set; }
    }
}
