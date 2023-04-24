using HW.CMSViewModel;
using HW.CommunicationViewModels;
using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.UserViewModels;
using HW.UserWebViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HW.GatewayApi.Controllers
{
    [Produces("application/json")]
    public class UserManagementController : BaseController
    {
        private readonly IUserManagementService userManagementService;

        public UserManagementController(IUserManagementService userManagmentService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.userManagementService = userManagmentService;
        }
        [HttpGet]
        public async Task<PersonalDetailsVM> GetUserDetailsByUserRole(string userRole , string userId)
        {
            return await userManagementService.GetUserDetailsByUserRole(userRole , userId);
        }

        [HttpGet]
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<IdValueVM>> GetAllCities()
        {
            return await userManagementService.GetAllCities();
        }

        [HttpGet]
        public async Task<List<IdValueVM>> GetDistances()
        {
            return await userManagementService.GetDistances();
        }

        [HttpGet]
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<SupplierViewModels.GetCitiesAndDistanceVM> GetDropDownOptionWeb()
        {
            return await userManagementService.GetDropDownOptionWeb();
        }

        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> GetOtp([FromBody]UserRegisterVM userVM)
        {
            return await userManagementService.GetOtp(userVM, DecodeTokenForUser()?.Id);
        }

        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> GetwebOtp([FromBody] UserRegisterVM userVM)
        {
            string userId = "";
            if (!string.IsNullOrEmpty(userVM.Id))
            {
                userId = userVM.Id;
            }
            else
            {
                userId = DecodeTokenForUser()?.Id;
            }
            return await userManagementService.GetwebOtp(userVM, userId);
        }


        public async Task<Response> VerifyOtpAndRegisterUser([FromBody]UserRegisterVM userVM)
        {
            return await userManagementService.VerifyOtpAndRegisterUser(userVM);
        } 
        public async Task<Response> VerifyOtpAndRegisterUserMobile([FromBody]UserRegisterVM userVM)
        {
            Response responce = new Response();
            try
            {
                var result = await userManagementService.VerifyOtpAndRegisterUser(userVM);
                string data = JsonConvert.SerializeObject(result.ResultData);
                responce.ResultData = data;
                responce.Status = result.Status;
                responce.Message = result.Message;
            }
            catch (System.Exception ex)
            {
                
            }
            return responce;
        }


        public async Task<Response> VerifyOtpAndGetPasswordResetToken([FromBody]UserRegisterVM userVM)
        {
            return await userManagementService.VerifyOtpAndGetPasswordResetToken(userVM);
        }

        [HttpPost]
        //[Permission(new string[] { UserRoles.Anonymous, UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<Response> CreateRoleBasedEntity([FromBody]PersonalDetailsVM model)
        {
            return await userManagementService.CreateRoleBasedEntity(model);
        }

        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        [HttpPost]
        public async Task<Response> UpdatePersonalDetails([FromBody]PersonalDetailsVM model)
        {
            string userRole = DecodeTokenForUser().Role;
            model.EntityId = await GetEntityIdByUserId();
            model.UserId = DecodeTokenForUser().Id;
            return await userManagementService.UpdatePersonalDetails(model, userRole);
        }
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Admin, UserRoles.Organization })]
        public async Task<Response> AddEditTradesmanWithSkills([FromBody]TmBusinessDetailVM model)
        {
            model.TradesmanId = await GetEntityIdByUserId();
            return await userManagementService.AddEditTradesmanWithSkills(model);
        }

        //[Permission(new string[] {  UserRoles.Supplier })]
        public async Task<Response> AddSupplierBusinessDetails([FromBody]SupplierBusinessDetailVM model)
        {
            return await userManagementService.AddSupplierBusinessDetails(model, await GetEntityIdByUserId());
        }

        public async Task<Response> WebUserRegistration([FromBody] WebRegistrationBasicVM webRegistrationBasicVM)
        {
            return await userManagementService.WebUserRegistration(webRegistrationBasicVM);
        }


        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> VerifyOtp(string code, string phoneNumber, string email,Role  role= 0)
        {
            string userId = DecodeTokenForUser().Id;
            return await userManagementService.VerifyOtp(userId, code, phoneNumber, role, email);
        }
        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> VerifyOtpWithoutToken(string code, string phoneNumber, string email, string userId)
        {
            Role role = 0;
            return await userManagementService.VerifyOtp(userId, code, phoneNumber, role, email);
        }

        [HttpGet]
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<TownListVM>> GetTownsByCityId(long cityId)
        {
            return await userManagementService.GetTownsByCityId(cityId);
        }
        [HttpGet]

        public async Task<List<ActivePromotionVM>> GetPromotionList()
        {
            return await userManagementService.GetPromotionList();

        }
        [HttpGet]

        public async Task<List<ActivePromotionVM>> GetPromotionListForWeb()
        {
            return await userManagementService.GetPromotionListForWeb();

        }
        [HttpGet]

        public async Task<List<ActivePromotionMobileVM>> GetPromotionListIdValue()
        {
            return await userManagementService.GetPromotionListIdValue();

        }
        [HttpGet]
        public async Task<List<ApplicationSettingVM>> GetSettingList()
        {
            return await userManagementService.GetSettingList();
        }
        [HttpGet]
        public async Task<List<ApplicationSettingVM>> GetSettingDetailsList()
        {
            return await userManagementService.GetSettingDetailsList();
        }
        [HttpGet]
        public async Task<ImageVM> GetPromotionImageById(int promotionId)
        {
            return await userManagementService.GetPromotionImageById(promotionId);
        }
       [HttpPost]
        public async Task<bool> SendMessage([FromBody]SmsVM smsVm)
        {
            return await userManagementService.SendMessage(smsVm);
        }
        [HttpGet]
        public async Task<Response> GetCityListByReleventState(int? stateId)
        {
            return await userManagementService.GetCityListByReleventState(stateId);
        }


        [HttpGet]
        public async Task<string> GetCampaignTypeList(int CampaignTypeId)
        {
            return await userManagementService.GetCampaignTypeList(CampaignTypeId);
        }
    }
}