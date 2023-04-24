using System;
using System.Collections.Generic;

namespace HW.UserManagmentModels
{
    public partial class City
    {
        public long CityId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long StateId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
