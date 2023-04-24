using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
public class OrderVm
    {
        public long OrderId { get; set; }
        public long? PromotionOnPackagesId { get; set; }
        public long? PackageId { get; set; }
        public string PackageName { get; set; }     
        public long? UserRoleId { get; set; }
        public string AspnetUserId { get; set; }
        public string UserName { get; set; }
        public decimal? OriginalSalePrice { get; set; }
        public decimal? DiscountPercentPrice { get; set; }
        public decimal? PriceAfterDiscount { get; set; }
        public int? ValidityDays { get; set; }
        public int? DiscountDays { get; set; }
        public int? DiscountedValidityDays { get; set; }
        public int? TotalApplicableJobs { get; set; }
        public int? DiscountJobsApplied { get; set; }
        public int? DiscountedTotalApplicableJobs { get; set; }
        public int? TotalCategories { get; set; }
        public int? DiscountCategories { get; set; }
        public int? DiscountedTotalCategories { get; set; }
        public decimal? OrderTotal { get; set; }
        public string EntityStatus { get; set; }
        public string SelectedSkills { get; set; }
        public string SkillsIds { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int? noOfPages { get; set; }
        public int? noOfRecoards { get; set; }
        public string OrderByColumn { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
    }
}
