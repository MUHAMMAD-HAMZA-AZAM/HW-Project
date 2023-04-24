using HW.GatewayApi.Code;
using HW.Http;
using HW.IdentityViewModels;
using HW.Utility;
using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using HW.CommunicationViewModels;
using static HW.Utility.ClientsConstants;
using HW.EmailViewModel;
using HW.ReportsViewModels;

namespace HW.GatewayApi.AdminServices
{
    public interface IAdminIdentityServer
    {
        Task<ReportsViewModels.ResponseVm> Login(LoginVM data);
        Task<Utility.Response> AdminResetPassword(AdminResetPasswordVm adminResetPasswordVm);
        Task<Utility.Response> ChangeAdminUserPassword(ChangePasswordVM adminResetPasswordVm);
        Task<Utility.Response> CheckEmailandPhoneNumberAvailability(UserRegisterVM model);
        Task<string> CreateAdminUser(UserRegisterVM userRegister);
        Task<Utility.Response> CHangeUserType(string userid);
        Task<Utility.Response> DeleteUserInfo(DeleteUserInfoVM deleteUserInfoVM);
        Task<List<CustomersDTO>> GetDeleteUserInfo(DeleteUserInfoVM deleteUserInfoVM);
        Task<Utility.Response> AddUpdateMenuItem(SiteMenuVM siteMenuVM);
        Task<List<SiteMenuVM>> GetMenuItemsList();
        Task<Utility.Response> AddUpdateSubMenuItem(SiteMenuVM siteMenuVM);
        Task<List<SiteMenuVM>> GetSubMenuItemsList();
        //Task<List<GetSecurityRoleDetailsVM>> GetSecurityRoleDetails(int roleId , string userId);
    }
    public class AdminIdentityServer : IAdminIdentityServer
    {
        private readonly ApiConfig _apiConfig;
        private readonly IHttpClientService httpClient;
        private readonly IExceptionService Exc;
        private readonly ClientCredentials clientCredentials;
        public AdminIdentityServer(IHttpClientService httpClient, IExceptionService Exc, ApiConfig apiConfig, ClientCredentials clientCredentials)
        {
            this.httpClient = httpClient;
            this.clientCredentials = clientCredentials;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }

        public async Task<Utility.Response> AdminResetPassword(AdminResetPasswordVm adminResetPasswordVm)
        {
            Utility.Response response = new Utility.Response();
            string resetPasswordJsonResponse = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.AdminResetPassword}", adminResetPasswordVm);
            response = JsonConvert.DeserializeObject<Utility.Response>(resetPasswordJsonResponse);

            return response;
        }
        public async Task<Utility.Response> ChangeAdminUserPassword(ChangePasswordVM changePasswordVM)
        {
            Utility.Response response = new Utility.Response();
            string resetPasswordJsonResponse = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.ChangeAdminUserPassword}", changePasswordVM);
            response = JsonConvert.DeserializeObject<Utility.Response>(resetPasswordJsonResponse);
            return response;
        }
        //public async Task<List<GetSecurityRoleDetailsVM>> GetSecurityRoleDetails(int roleId,string userId)
        //{
        //    var pageRoles = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetSecurityRoleDetails}?roleId={0}&UserId={userId}", "");
        //    List<GetSecurityRoleDetailsVM> securityRoleItemVMs = JsonConvert.DeserializeObject<List<GetSecurityRoleDetailsVM>>(pageRoles);
        //    return securityRoleItemVMs;
        //}
        public async Task<ReportsViewModels.ResponseVm> Login(LoginVM model)
        {
            System.Net.Http.HttpClient _httpClient = new System.Net.Http.HttpClient();

            //ContactEmailVm contactEmail = new ContactEmailVm();
            //contactEmail.EmailAddress = model.EmailOrPhoneNumber;
            //await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendLoginConfirmationEmail}", contactEmail);

            var response = new ReportsViewModels.ResponseVm();
            string servermessege = string.Empty;
            string userId = string.Empty;
            UserIdVM userVm = new UserIdVM();
            string entityId = string.Empty;

            try
            {
                model.Role = "5";
                userVm = JsonConvert.DeserializeObject<UserIdVM>(await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.Login}", model));
                if (!string.IsNullOrWhiteSpace(userVm.Id))
                {
                    //var disco = await DiscoveryClient.GetAsync(_apiConfig.IdentityServerApiUrl);   only supports .net core 2.2

                    var disco = await _httpClient.GetDiscoveryDocumentAsync(_apiConfig.IdentityServerApiUrl); // supports .net core 5.0

                    if (disco.IsError)
                    {
                        model.RequestType = "3";
                        // model.UserId = UserId;
                        servermessege = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.AdminUserLoging}", model);
                        Exc.AddErrorLog(new Exception(disco.Error));
                        response.Message = "LoginFailed";
                        response.ResultData = disco.IsError;
                    }
                    else
                    {
                        var pageRoles = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetSecurityRoleDetails}?roleId={0}&UserId={userVm.Id}", "");
                        List<GetSecurityRoleDetailsVM> securityRoleItemVMs = JsonConvert.DeserializeObject<List<GetSecurityRoleDetailsVM>>(pageRoles);

                        model.Role = "Admin";

                        //var extra = new Dictionary<string, string> { { "role", model.Role }, { "entityId", userId }, { "userId", userId } };
                        //var tokenClient = new TokenClient(disco.TokenEndpoint, clientCredentials.ClientId, clientCredentials.Secret, null, AuthenticationStyle.PostValues);
                        //var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync($"{model.EmailOrPhoneNumber}", model.Password, "api1", extra);

                        var tokenResponse = await HttpClientTokenRequestExtensions.RequestPasswordTokenAsync(_httpClient, new PasswordTokenRequest
                        {
                            Scope = "api1",
                            Address = disco.TokenEndpoint,
                            ClientSecret = clientCredentials.Secret,
                            ClientId = clientCredentials.ClientId,
                            UserName = model.EmailOrPhoneNumber,
                            Password = model.Password,
                            Parameters =
                            {
                                { "firebaseClientId", model.FirebaseClientId },
                                { "role", model.Role },
                                { "entityId", userVm.Id },
                                { "userId", userVm.Id }
                            }
                        });

                        if (!tokenResponse.IsError)
                        {
                            model.RequestType = "1";
                            model.UserId = userVm.Id;
                            servermessege =  await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.AdminUserLoging}", model);
                            if (servermessege == "Blocked")
                            {
                                response.Status = ResponseStatus.Error;
                                response.Message = "Blocked";
                                //response.ResultData = tokenResponse.AccessToken;
                            }
                            else
                            {
                                //response.ResultData = tokenResponse.AccessToken;
                                response.Status = ResponseStatus.OK;
                                response.Message = "Login successful.";
                                //response.getSecurityRoleDetailsVM = securityRoleItemVMs;

                                response.ResultData = new { 
                                                AccessToken = tokenResponse.AccessToken,
                                                GetSecurityRoleDetails = securityRoleItemVMs
                                };
                            }
                        }
                        else
                        {
                            model.RequestType = "2";
                            model.UserId = userId;
                            //servermessege = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.AdminUserLoging}", model);
                            response.Status = ResponseStatus.Error;
                            response.Message = "Login failed.";
                            //response.ResultData = tokenResponse.AccessToken;
                            response.ResultData = new
                            {
                                AccessToken = tokenResponse.AccessToken
                            };

                            //response.Message = ;
                            // response.getSecurityRoleDetailsVM = securityRoleItemVMs;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        //public async Task<Utility.Response> Login(LoginVM model)
        //{
        //    var response = new Utility.Response();
        //    try
        //    {
        //        var disco = await DiscoveryClient.GetAsync(_apiConfig.IdentityServerApiUrl);
        //        if (disco.IsError)
        //        {
        //            Exc.AddErrorLog(new Exception(disco.Error));
        //            response.Message = "LoginFailed";
        //            response.ResultData = disco.IsError;
        //        }
        //        else
        //        {
        //            var extra = new Dictionary<string, string> { { "firebaseClientId", model.FirebaseClientId } };
        //            var tokenClient = new TokenClient(disco.TokenEndpoint, clientCredentials.ClientId, clientCredentials.Secret, null, AuthenticationStyle.PostValues);
        //            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync($"{model.EmailOrPhoneNumber}", model.Password, "api1", extra);
        //            if (!tokenResponse.IsError)
        //            {
        //                response.ResultData = tokenResponse.AccessToken;
        //                response.Status = ResponseStatus.OK;
        //                response.Message = "Login successful.";
        //            }
        //            else
        //            {
        //                response.Status = ResponseStatus.Error;
        //                response.Message = "Login failed.";
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Exc.AddErrorLog(ex);
        //    }

        //    return response;

        //}
        public async Task<Utility.Response> CheckEmailandPhoneNumberAvailability(UserRegisterVM data)
        {
            Utility.Response response = new Utility.Response();
            try
            {
                var aa = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.CheckEmailandPhoneNumberAvailability}", data);
                response = JsonConvert.DeserializeObject<Utility.Response>(aa);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return response;
        }
        public async Task<string> CreateAdminUser(UserRegisterVM userRegister)
        {
            var responseJson = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.RegisterUser}", userRegister);
            var response = JsonConvert.DeserializeObject<Utility.Response>(responseJson);

            return responseJson;
        }
        public async Task<Utility.Response> CHangeUserType(string userid)
        {
            var responseJson = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.Changeusertype}?userid={userid}", "");
            return JsonConvert.DeserializeObject<Utility.Response>(responseJson);
        }
        public async Task<Utility.Response> DeleteUserInfo(DeleteUserInfoVM deleteUserInfoVM)
        {
            var responseJson = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.DeleteUserInfo}", deleteUserInfoVM);
            return JsonConvert.DeserializeObject<Utility.Response>(responseJson);
        }
        public async Task<List<CustomersDTO>> GetDeleteUserInfo(DeleteUserInfoVM deleteUserInfoVM)
        {
            var responseJson = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetDeleteUserInfo}", deleteUserInfoVM);
            return JsonConvert.DeserializeObject<List<CustomersDTO>>(responseJson);
        }
        public async Task<Utility.Response> AddUpdateMenuItem(SiteMenuVM siteMenuVM)
        {
            var responseJson = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.AddUpdateMenuItem}", siteMenuVM);
            return JsonConvert.DeserializeObject<Utility.Response>(responseJson);
        }
        public async Task<List<SiteMenuVM>> GetMenuItemsList()
        {
            var responseJson = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetMenuItemsList}", "");
            return JsonConvert.DeserializeObject<List<SiteMenuVM>>(responseJson);
        }
        public async Task<Utility.Response> AddUpdateSubMenuItem(SiteMenuVM siteMenuVM)
        {
            var responseJson = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.AddUpdateSubMenuItem}", siteMenuVM);
            return JsonConvert.DeserializeObject<Utility.Response>(responseJson);
        }
        public async Task<List<SiteMenuVM>> GetSubMenuItemsList()
        {
            var responseJson = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetSubMenuItemsList}", "");
            return JsonConvert.DeserializeObject<List<SiteMenuVM>>(responseJson);
        }
    }
}
