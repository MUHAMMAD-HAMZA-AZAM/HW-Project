using System;
using System.Collections.Generic;

namespace HW.UserManagmentModels
{
    public partial class Distance
    {
        public long DistanceId { get; set; }
        public long Value { get; set; }
        public string Unit { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
