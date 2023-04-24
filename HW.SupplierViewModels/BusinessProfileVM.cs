using HW.Utility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HW.SupplierViewModels
{
    public class BusinessProfileVM
    {
        public long SupplierId { get; set; }
        public long? CategoryId { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public string PrimaryTrade { get; set; }
        public long? PrimaryTradeId { get; set; }
        public List<IdValueVM> ProductsubCategory { get; set; }
        public long City { get; set; }
        public string CityName { get; set; }
        public string Town { get; set; }
        public string BusinessAddress { get; set; }
        public int? DeliveryRadius { get; set; }
        public List<IdValueVM> selectedSubCategory { get; set; }
        [Display(Name = "Location")]
        public string LocationCoordinates { get; set; }
    }
}
