﻿using System;
using System.Collections.Generic;

#nullable disable

namespace HW.PackagesAndPaymentsModels
{
    public partial class Coupon
    {
        public long CouponId { get; set; }
        public Guid? CouponIdGuid { get; set; }
        public long? CouponTypeId { get; set; }
        public string CouponCode { get; set; }
        public string CouponeName { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int? DiscountDays { get; set; }
        public int? DiscountCategories { get; set; }
        public int? DiscountJobsApplied { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
