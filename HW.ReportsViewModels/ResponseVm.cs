using HW.IdentityViewModels;
using HW.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ReportsViewModels
{
    public class ResponseVm
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public object ResultData { get; set; }
        public List<GetSecurityRoleDetailsVM> getSecurityRoleDetailsVM = new List<GetSecurityRoleDetailsVM>();
    }
}
