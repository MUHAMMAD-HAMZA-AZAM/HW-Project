using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class FeaturedSuppliers
    {
        public int SupplierId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Cnic { get; set; }
        public byte? Gender { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public string State { get; set; }
        public long? CityId { get; set; }
        public string BusinessAddress { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string UserId { get; set; }
    }
}
