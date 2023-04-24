using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class CityAndStateVM
    {
        public long CityId { get; set; }
        public string Code { get; set; }
        public string CityName { get; set; }
        public long? StateId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string StateName { get; set; }

    }
}
