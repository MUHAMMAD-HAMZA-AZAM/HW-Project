using System;
using System.Collections.Generic;

namespace HW.UserManagmentModels
{
    public partial class ApplicationSetting
    {
        public int ApplicationSettingId { get; set; }
        public string SettingName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
