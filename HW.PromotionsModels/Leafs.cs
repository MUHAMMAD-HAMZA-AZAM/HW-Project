using System;
using System.Collections.Generic;

namespace HW.PromotionsModels
{
    public partial class Leafs
    {
        public int LeafId { get; set; }
        public long LeafCode { get; set; }
        public long BookId { get; set; }
        public DateTime? RedeemedOn { get; set; }
        public string RedeemedBy { get; set; }
        public decimal Discount { get; set; }
        public int DiscountType { get; set; }
        public bool? IsActive { get; set; }
    }
}
