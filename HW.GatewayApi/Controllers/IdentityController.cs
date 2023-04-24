using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HW.GatewayApi.Controllers
{
    [Produces("text/plain")]
    public class IdentityController : BaseController
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.identityService = identityService;
        }

        public string Start()
        {
            return "Gateway Identity API is started.";
        }

        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Anonymous, UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]

        public async Task<Utility.Response> Login([FromBody] LoginVM model)
        {
            return await identityService.Login(model);
        }

        [HttpPost]
        //[Permission(new string[] { UserRoles.Anonymous, UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<string> CheckEmailandPhoneNumberAvailability([FromBody] UserRegisterVM model)
        {
            return await identityService.CheckEmailandPhoneNumberAvailability(model);
        }

        [HttpPost]
        public async Task<string> RegisterUser([FromBody] UserRegisterVM model)
        {
            return await identityService.RegisterUser(model);
        }

        [HttpPost]
        public async Task<string> ForgotPassword([FromBody] ForgotPasswordVM model)
        {
            return await identityService.ForgotPassword(model);
        }

        [HttpPost]
        public async Task<string> ResetPassword([FromBody] ResetPasswordVM model)
        {
            return await identityService.ResetPassword(model);
        }

        [HttpGet]
        public async Task<string> GetUserIdByEmailOrPhoneNumber([FromQuery] string emailOrPhoneNumber, Role userRoles)
        {
            var res = await identityService.GetUserIdByEmailOrPhoneNumber(emailOrPhoneNumber, userRoles);
            return res;
        }
        [Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<string> IsUserVerified()
        {
            UserRegisterVM userVM = DecodeTokenForUser();
            return await identityService.IsUserVerified(userVM.Id);
        }
        [Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<string> ValidateFacebookToken(string facebookToken)
        {
            return await identityService.ValidateFacebookToken(facebookToken);
        }
        [Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<string> GetFbUserAsync(string facebookToken)
        {
            return await identityService.GetFbUserAsync(facebookToken);
        }
        [Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<string> FindUserFbRecord(string facebookToken)
        {
            return await identityService.FindUserFbRecord(facebookToken);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<bool> GetPhoneNumberVerificationStatus()
        {
            return await identityService.GetPhoneNumberVerificationStatus(DecodeTokenForUser().Id);
        }

        [Produces("application/json")]
        [Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public UserRegisterVM DecodeToken()
        {
            return DecodeTokenForUser();
        }
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<string> GetUserByToken()
        {
            UserRegisterVM userVM = DecodeTokenForUser();
            return await identityService.GetUserByUserId(userVM.Id);
        }
       // [Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<string> GetCitybyToken()
        {
            UserRegisterVM userVM = DecodeTokenForUser();
            return await identityService.GetCitybyToken(userVM.Id);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<Response> UpdateUserFirebaseToken(string firebaseToken)
        {
            UserRegisterVM userVM = DecodeTokenForUser();
            return await identityService.UpdateUserFirebaseToken(userVM?.Id, firebaseToken);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<Response> GetFirebaseIdByUserId()
        {
            UserRegisterVM userVM = DecodeTokenForUser();
            return await identityService.GetFirebaseIdByUserId(userVM.Id);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<bool> ApplicationServerPing()
        {
            UserRegisterVM userVM = DecodeTokenForUser();
            if (!string.IsNullOrEmpty(userVM?.Id))
            {
                return await identityService.ApplicationServerPing(userVM.Id);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] {UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<Response> GetUserPinStatus(string role, string emailOrPhone)
        {
            return await identityService.GetUserPinStatus(role, emailOrPhone);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<Response> GetUserByFacebookId([FromBody] SocialAccountVm socialAccountVm)
        {
            return await identityService.GetUserByFacebookId(socialAccountVm);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<Response> GetUserByGoogleId(string googleId)
        {
            return await identityService.GetUserByGoogleId(googleId);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<bool> GetUserBlockStatus(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = DecodeTokenForUser().Id;
            }
            return await identityService.GetUserBlockStatus(userId);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<Response> GetUserByAppleId(string appleId,Role role)
        {
            return await identityService.GetUserByAppleId(appleId,role);
        }
        public async Task<GetAccountsTypeVM> GetSupplierAcountsType(string userId)
        {
            return await identityService.GetSupplierAcountsType(userId);
        }

    }
}