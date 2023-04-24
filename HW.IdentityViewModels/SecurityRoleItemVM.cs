using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
   public class SecurityRoleItemVM
    {
        public long Id { get; set; }
        public int? SecurityRoleItem { get; set; }
        public int? SecurityRoleItemName { get; set; }
        public int? SubRoleItem { get; set; }
        public bool IsModule { get; set; }
        public string RoutingPath { get; set; }
    }
    public class SecurityRoleVM
    {
        public long SecurityRoleId { get; set; }
        public string SecurityRoleName { get; set; }
    }
  public class GetSecurityRoleDetailsVM
    {
        public long BaseSecurityRoleItemId { get; set; }
        public long Id { get; set; }
        public long SecurityRoleId { get; set; }
        //public string SecurityRoleItems { get; set; }
        public int SecurityRoleItems { get; set; }
        public int SubRoleItem { get; set; }        
        public int MenuId { get; set; }
        public string IconName { get; set; }
        public int SubMenuId { get; set; }
        public Boolean IsModule { get; set; }
        public string RoutingPath { get; set; }
        public string SecurityRoleItemName { get; set; }
        public string SecurityRoleName { get; set; }
        public string UserName { get; set; }
        public Boolean AllowView { get; set; }
        public Boolean AllowEdit { get; set; }
        public Boolean AllowDelete { get; set; }
        public Boolean AllowPrint { get; set; }
        public Boolean spectialrights { get; set; }
        public Boolean AllowExport { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
