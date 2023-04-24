using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class JobAddress
    {
        public long JobAddressId { get; set; }
        public long JobQuotationId { get; set; }
        public long CityId { get; set; }
        public string Area { get; set; }
        public string StreetAddress { get; set; }
        public string AddressLine { get; set; }
        public string GpsCoordinates { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public long? TownId { get; set; }
    }
}
