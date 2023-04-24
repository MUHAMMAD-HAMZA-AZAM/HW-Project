using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class ProductCategoryIdValueDTO : IdValueDTO
    {
        public bool? HasSubItem { get; set; }
    }
}
