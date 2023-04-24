using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class UserFavoriteTradesmenVM 
    {
        public long TradesmanId { get; set; }
        public string TradesmanName { get; set; }
        public string AddressLine { get; set; }
        public int ReviewsCount { get; set; }
        public string Rating { get; set; }
    }
}
