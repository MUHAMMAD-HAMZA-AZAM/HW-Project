using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
    public class PromotionsVM
    {
        public long PromotionId { get; set; }
        public long? PromotionIdTypeId { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? CategoryGroupId { get; set; }
        public  string CategoryName { get; set; }
        public string PromotionCode { get; set; }
        public string PromotionName { get; set; }
        public DateTime? PromoStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        //public decimal? DiscountPercentPrice { get; set; }
        //public int? DiscountDays { get; set; }
        //public int? DiscountCategories { get; set; }
        //public int? DiscountJobsApplied { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string PromotionTypeName { get; set; }
        //public bool? PermotionsForAll { get; set; }
        //public bool? PermotionsForOld { get; set; }
        //public bool? /*PermotionsForNew*/ { get; set; }
        public decimal? Amount { get; set; }
        public int? SupplierId { get; set; }
    }
}
