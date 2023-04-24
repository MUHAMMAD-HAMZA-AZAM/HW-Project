using System;
using System.Collections.Generic;

namespace HW.UserManagmentModels
{
    public partial class Town
    {
        public long TownId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public long CityId { get; set; }
    }
}
