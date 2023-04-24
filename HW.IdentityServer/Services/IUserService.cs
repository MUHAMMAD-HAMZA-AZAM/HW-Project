
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using HW.IdentityServerModels;
using HW.UserViewModels;

namespace HW.IdentityServer.Services
{
    public interface IUserService
    {
      List<AspnetUserVM> GetUserListByRoleId(int RoleId);
    
    }
}
