using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class JobContactInfo
    {
        public long JobContactInfoId { get; set; }
        public long JobQuotationId { get; set; }
        public long CustomerId { get; set; }
        public string PropertyRelationship { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long? City { get; set; }
        public long? TownId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
