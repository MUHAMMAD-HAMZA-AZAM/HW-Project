using System;
using System.Collections.Generic;

#nullable disable

namespace HW.PackagesAndPaymentsModels
{
    public partial class Promotion
    {
        public long PromotionId { get; set; }
        public long? PromotionIdTypeId { get; set; }
        public string PromotionCode { get; set; }
        public string PromotionName { get; set; }
        public DateTime? PromoStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        public decimal? DiscountPercentPrice { get; set; }
        public int? DiscountDays { get; set; }
        public int? DiscountCategories { get; set; }
        public long? CategoryId { get; set; }
        public int? DiscountJobsApplied { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool? DiscountInAmount { get; set; }
        public bool? DiscountInPercentage { get; set; }
        public bool? DiscountInDays { get; set; }
        public bool? DiscountInJobs { get; set; }
        public bool? DiscountInCategories { get; set; }
        public bool? PermotionsForAll { get; set; }
        public bool? PermotionsForOld { get; set; }
        public bool? PermotionsForNew { get; set; }
        public decimal? Amount { get; set; }
    }
}
