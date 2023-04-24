using System;
using System.Collections.Generic;

namespace HW.UserManagmentModels
{
    public partial class ApplicationSettingDetail
    {
        public int ApplictaionSettingDetailId { get; set; }
        public int? ApplicationSettingId { get; set; }
        public string SettingKeyName { get; set; }
        public string SettingKeyValue { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
