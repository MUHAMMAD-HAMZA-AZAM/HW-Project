using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
    public class GetPackgesByCategoryVM
    {
        public long PackageId { get; set; }
        public long PackageTypeId { get; set; }
        public string PackageName { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal DiscountPercentPrice { get; set; }
    }
}
