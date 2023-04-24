using System;
using System.Collections.Generic;

namespace HW.CustomerModels
{
    public partial class ProductsWishList
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long SupplierId { get; set; }
        public long ProductId { get; set; }
        public bool? IsFavorite { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
