using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels
{
  public partial class CustomerFeedBack
  {
        public int Id { get; set; }
        public int FiveStars { get; set; }
        public int FoureStars { get; set; }
        public int ThreeStars { get; set; }
        public int TwoStars { get; set; }
        public int OneStars { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public long? CustomerId { get; set; }
        public long? ProductId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? TotalReviews { get; set; }
        public double AverageRating { get; set; }
    }
}
