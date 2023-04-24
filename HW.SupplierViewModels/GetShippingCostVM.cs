using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class GetShippingCostVM
    {
        public int Id { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string CityName { get; set; }
        public string UserId { get; set; }
        public int CityId { get; set; }
        public string ModifiedBy { get; set; }

    }
}
