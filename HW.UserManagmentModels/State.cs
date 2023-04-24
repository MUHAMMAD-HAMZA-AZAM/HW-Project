using System;
using System.Collections.Generic;

namespace HW.UserManagmentModels
{
    public partial class State
    {
        public long StateId { get; set; }
        public long CountryId { get; set; }
        public string Code { get; set; }
        public bool? Active { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
