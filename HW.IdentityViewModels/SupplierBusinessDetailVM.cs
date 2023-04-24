using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HW.IdentityViewModels
{
    public class SupplierBusinessDetailVM
    {
        public SupplierBusinessDetailVM()
        {
            ProductIds = new List<long>();
        }

        public long SupplierId { get; set; }

        [Display(Name = "Trade Name")]

        public string CompanyName { get; set; }

        [Display(Name = "Company Registration Number")]
        public string CompanyRegistrationNo { get; set; }

        [Display(Name = "Primary Trade")]
        public long PrimaryTradeId { get; set; }
        public string PrimaryTrade { get; set; }
        public List<long> ProductIds { get; set; }

        [Display(Name = "Delivery radius")]
        public int DeliveryRadius { get; set; }
        public long CityId { get; set; }

        [Display(Name = "Business Address")]
        public string BusinessAddress { get; set; }
        [Display(Name = "Location")]
        public string LocationCoordinates { get; set; }
        public string Town { get; set; }
        public string EmailAddress { get; set; }
        public bool? FeaturedSupplier { get; set; }

    }
}
