using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class ShippingCost
    {
        public int Id { get; set; }
        public decimal? Cost { get; set; }
        public int? CityId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
