using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
   public class PromotionsTypeVM
    {
        public long PromotionTypeId { get; set; }
        public string PromotionTypeName { get; set; }
        public string PromotionTypeCode { get; set; }
        public decimal? Amount { get; set; }
        public long? CategoryId { get; set; }


    }
}

