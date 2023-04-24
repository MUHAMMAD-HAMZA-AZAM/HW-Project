using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class AdsParameterVM 
    {
        public long CustomerId { get; set; }
        public List<int> ProductCategoryIds { get; set; }
        public long SubCategoryId { get; set; }
        public string SortBy { get; set; }
        public string CatIds { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }
        public bool IsTestUser { get; set; }
    }
}
