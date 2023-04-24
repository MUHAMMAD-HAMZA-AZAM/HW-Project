using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class ApplicationSettingVM
    {
        public int ApplicationSettingId { get; set; }
        public string SettingName { get; set; }
        public string Action{ get; set; }
        public string UserId{ get; set; }
        public int ApplictaionSettingDetailId { get; set; }
        public string SettingKeyName { get; set; }
        public string SettingKeyValue { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
