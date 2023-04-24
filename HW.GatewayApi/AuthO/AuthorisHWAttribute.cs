using HW.GatewayApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.AuthO
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _roleItem;

        public PermissionAttribute(string[] roleItem)
        {
            _roleItem = roleItem;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Headers["Authorization"].FirstOrDefault() != null)
            {
                string token = context.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (!string.IsNullOrWhiteSpace(token))
                {
                    IUserManagementService securityroleService = (IUserManagementService)context.HttpContext.RequestServices.GetService(typeof(IUserManagementService));
                    IdentityViewModels.UserRegisterVM userRegisterVM = securityroleService.DecodeToken(token);



                    if (!_roleItem.Any(x => x == userRegisterVM?.Role))
                    {
                        context.Result = new CustomUnauthorizedResult("You have not sufficient permission to access requested resource.");
                        return;
                    }
                }
                else
                {
                    context.Result = new CustomUnauthorizedResult("You have not sufficient permission to access requested resource.");
                    return;
                }
            }
            else
            {
                context.Result = new CustomUnauthorizedResult("You have not sufficient permission to access requested resource.");
                return;
            }
        }
    }

    public class CustomUnauthorizedResult : JsonResult
    {
        public CustomUnauthorizedResult(string message, ErrorCode errorCode = ErrorCode.Unauthorized) : base(new CustomError(message, errorCode))
        {
            StatusCode = StatusCodes.Status401Unauthorized;
        }

        public CustomUnauthorizedResult(int statusCode, string message, ErrorCode errorCode = ErrorCode.Unauthorized) : base(new CustomError(message, errorCode))
        {
            StatusCode = StatusCode;
        }

    }
}