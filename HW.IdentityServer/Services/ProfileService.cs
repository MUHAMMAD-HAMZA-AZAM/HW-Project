using HW.CallModels;
using HW.IdentityViewModels;
using HW.Utility;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HW.IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        //services
        private IUserRepository _userRepository;
        private readonly IUnitOfWork uow;
        IExceptionService Exc;
        public ProfileService(IUserRepository userRepository, IUnitOfWork unitOfWork, IExceptionService Exc_)
        {
            _userRepository = userRepository; //DI
            uow = unitOfWork;
            Exc = Exc_;
        }
        //Get user profile date in terms of claims when calling /connect/userinfo
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                

                //depending on the scope accessing the user data.
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    
                }
                else
                {
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");
                    if (!string.IsNullOrEmpty(userId?.Value))
                    {
                        var claims = TokenCustomClaims();
                        context.IssuedClaims = claims;
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }
        //Getting Claims
        public List<Claim> TokenCustomClaims()
        {
            var role = staticResources.Role;

            var claimList = new List<Claim>();
            try
            {
                claimList.Add(new Claim("Id", staticResources.EntityId));
                claimList.Add(new Claim("Role", staticResources.Role));
                claimList.Add(new Claim("UserId", staticResources.UserId));
                claimList.Add(new Claim("FirebaseClientId", staticResources.FirebaseClientId));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                claimList = null;
            }
            return claimList;
        }
        //check if user account is active.
        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                context.IsActive = true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }
    }
}
