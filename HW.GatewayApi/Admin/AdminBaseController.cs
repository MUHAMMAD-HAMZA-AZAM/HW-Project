using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW.GatewayApi.Admin
{
    public class AdminBaseController : ControllerBase
    {
        private readonly IUserManagementService userManagementService;

        public AdminBaseController(IUserManagementService userManagementService)
        {
            this.userManagementService = userManagementService;
        }

        public async Task<long> GetEntityIdByUserId()
        {
            UserRegisterVM userVM = DecodeTokenForUser();
            return userVM.ClientId;
        }

        public UserRegisterVM DecodeTokenForUser()
        {
            UserRegisterVM userRegisterVM = null;

            if (Request.Headers["Authorization"].FirstOrDefault() != null)
            {
                var token = Request.Headers["Authorization"].ToString().Substring(7);
                userRegisterVM = userManagementService.DecodeToken(token);
            }

            return userRegisterVM;
        }
       
    }
}