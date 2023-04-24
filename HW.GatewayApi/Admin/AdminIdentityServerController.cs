using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.GatewayApi.Admin;
using HW.GatewayApi.AdminServices;
using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.ReportsViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW.GatewayApi.AdminControllers
{
    [Produces("application/json")]
    public class AdminIdentityServerController : AdminBaseController
    {
        private readonly IAdminIdentityServer adminIdentityServer;
        public AdminIdentityServerController(IAdminIdentityServer adnIdentityServer_, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.adminIdentityServer = adnIdentityServer_;
        }

        [HttpPost]
        public async Task<ReportsViewModels.ResponseVm> Login([FromBody]LoginVM model)
        {
            return await adminIdentityServer.Login(model);
        }

        [HttpPost]

        public async Task<Response> AdminResetPassword([FromBody] AdminResetPasswordVm adminResetPasswordVm)
        {
            return await adminIdentityServer.AdminResetPassword(adminResetPasswordVm);
        }

        [HttpPost]

        public async Task<Response> ChangeAdminUserPassword([FromBody] ChangePasswordVM changePasswordVM)
        {
            return await adminIdentityServer.ChangeAdminUserPassword(changePasswordVM);
        }

        [HttpPost]
        public async Task<Response> CheckEmailandPhoneNumberAvailability([FromBody]UserRegisterVM model)
        {
            return await adminIdentityServer.CheckEmailandPhoneNumberAvailability(model);
        }

        [HttpPost]
        public async Task<string> CreateAdminUser([FromBody] UserRegisterVM userRegister)
        {
            return await adminIdentityServer.CreateAdminUser(userRegister);
        }

        public async Task<Response> CHangeUserType(string userid)
        {
            return await adminIdentityServer.CHangeUserType(userid);
        }

        [HttpPost]

        public async Task<Response> DeleteUserInfo([FromBody] DeleteUserInfoVM deleteUserInfoVM)
        {
            return await adminIdentityServer.DeleteUserInfo(deleteUserInfoVM);
        }

        [HttpPost]
        public async Task<List<CustomersDTO>> GetDeleteUserInfo([FromBody] DeleteUserInfoVM deleteUserInfoVM)
        {
            return await adminIdentityServer.GetDeleteUserInfo(deleteUserInfoVM);
        }        
        [HttpPost]
        public async Task<Response> AddUpdateMenuItem([FromBody] SiteMenuVM siteMenuVM)
        {
            return await adminIdentityServer.AddUpdateMenuItem(siteMenuVM);
        }
        [HttpGet]
        public async Task<List<SiteMenuVM>> GetMenuItemsList()
        {
            return await adminIdentityServer.GetMenuItemsList();
        }
        [HttpPost]
        public async Task<Response> AddUpdateSubMenuItem([FromBody] SiteMenuVM siteMenuVM)
        {
            return await adminIdentityServer.AddUpdateSubMenuItem(siteMenuVM);
        }
        [HttpGet]
        public async Task<List<SiteMenuVM>> GetSubMenuItemsList()
        {
            return await adminIdentityServer.GetSubMenuItemsList();
        }        
        //[HttpGet]
        //public async Task<List<GetSecurityRoleDetailsVM>> GetSecurityRoleDetails(int roleId = 0, string userId = "")
        //{
        //    return await adminIdentityServer.GetSecurityRoleDetails(roleId,userId);
        //}

    }
}
