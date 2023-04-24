using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.AuthO
{
    public class PermissionValidatorAttribute : TypeFilterAttribute
    {
        public PermissionValidatorAttribute(string roleItem) : base(typeof(SecurityRoleFilter))
        {
            Arguments = new object[] { new PermissionParam(roleItem) };
        }
    }

    public class SecurityRoleFilter : IAuthorizationFilter
    {
        readonly PermissionParam _permission;

        public SecurityRoleFilter(PermissionParam permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _permission.RoleItem);
            // var someService = context.HttpContext.RequestServices.GetService<IStateManagmentService>();
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
    public class PermissionParam
    {
        public PermissionParam(string roleItem)
        {

        }

        public string RoleItem { get; set; }
    }
}
