using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class Packages
    {
        public long PackageId { get; set; }
        public long? UserRoleId { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public decimal? TradePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public int? ValidityDays { get; set; }
        public int? TotalApplicableJobs { get; set; }
        public int? TotalCategories { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public long? PackageTypeId { get; set; }
    }
}
