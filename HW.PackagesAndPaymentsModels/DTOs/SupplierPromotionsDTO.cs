﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsModels.DTOs
{
    public class SupplierPromotionsDTO
    {
        public long PromotionId { get; set; }
        public long SupplierId { get; set; }
        public long? PromotionTypeId { get; set; }
        public string PromotionName { get; set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        public decimal? DiscountPercentPrice { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? CategoryGroupId { get; set; }
        public string EntityStatus { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryGroupName { get; set; }
        public bool? IsActive { get; set; }
        public decimal? Amount { get; set; }
        public int? AmountType { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
