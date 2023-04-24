﻿using System;
using System.Collections.Generic;

#nullable disable

namespace HW.PackagesAndPaymentsModels
{
    public partial class PromotionsType
    {
        public long PromotionTypeId { get; set; }
        public string PromotionTypeCode { get; set; }
        public string PromotionTypeName { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int? PromotionOn { get; set; }
    }
}
