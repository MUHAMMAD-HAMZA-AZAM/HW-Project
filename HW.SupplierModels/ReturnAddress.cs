using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class ReturnAddress
    {
        public long Id { get; set; }
        public bool? IsWarehouseAddress { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public long SupplierId { get; set; }
        public string Email { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public long? CityId { get; set; }
        public long? AreaId { get; set; }
        public string Location { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
