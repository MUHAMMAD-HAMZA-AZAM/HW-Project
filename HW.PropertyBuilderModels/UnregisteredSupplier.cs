using System;
using System.Collections.Generic;

namespace HW.PropertyBuilderModels
{
    public partial class UnregisteredSupplier
    {
        public long UnregisteredSupplierId { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string MobileNumber { get; set; }
        public long PropertyBuilderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
