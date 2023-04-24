using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
    public class SiteMenuVM
    {
        
        public string UserId { get; set; }
        public int MenuId { get; set; }
        public int SubMenuId { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public string SubMenuItemName { get; set; }
        public string CreatedBy  { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool Active { get; set; }
        public string IconName { get; set; }

    }
}
