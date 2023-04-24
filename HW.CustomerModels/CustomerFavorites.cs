using System;
using System.Collections.Generic;

namespace HW.CustomerModels
{
    public partial class CustomerFavorites
    {
        public long CustomerFavoritesId { get; set; }
        public long CustomerId { get; set; }
        public long? TradesmanId { get; set; }
        public long? SupplierId { get; set; }
        public long? EstateAgentId { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
