using System;
using System.Collections.Generic;

#nullable disable

namespace HW.CMSModels
{
    public partial class Menu
    {
        public long MenuId { get; set; }
        public string ManuName { get; set; }
        public string MenuUrl { get; set; }
        public string MenuImage { get; set; }
        public string MenuTarget { get; set; }
        public string MenuDiscription { get; set; }
        public int? MenuOrder { get; set; }
        public int? MenuStatus { get; set; }
        public string Createdy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
