using System;
using System.Collections.Generic;

namespace HW.CustomerModels
{
    public partial class CustomerFavoritesTradesman
    {
        public long CustomerFavoritesTradesmanId { get; set; }
        public long CustomerId { get; set; }
        public long TradesmanId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
