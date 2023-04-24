using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class FavoriteTradesmanVM
    {
        public long CustomerId { get; set; }
        public long? TradesmanId { get; set; }
        public long? SupplierId { get; set; }
        public long? EstateAgentId { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
