using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class FavoriteTradesman
    {
        public long FavoriteTradesmanId { get; set; }
        public long JobQuotationId { get; set; }
        public long CustomerFavoritesId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyOn { get; set; }
    }
}
