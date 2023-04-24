using HW.CommunicationViewModels;
using HW.CustomerModels;
using HW.GatewayApi.Code;
using HW.Http;
using HW.IdentityViewModels;
using HW.UserViewModels;
using HW.Utility;
using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Response = HW.Utility.Response;

namespace HW.GatewayApi.Services
{
    public interface IIdentityService
    {
        Task<Utility.Response> Login(LoginVM data);
        Task<string> CheckEmailandPhoneNumberAvailability(UserRegisterVM data);
        Task<string> RegisterUser(UserRegisterVM data);
        Task<string> ForgotPassword(ForgotPasswordVM data);
        Task<string> ResetPassword(ResetPasswordVM data);
        Task<string> GetUserIdByEmailOrPhoneNumber(string emailOrPhoneNumber, Role userRoles);
        Task<string> IsUserVerified(string userId);
        Task<string> GetUserByUserId(string userId);
        Task<string> GetCitybyToken(string userId);
        Task<string> ValidateFacebookToken(string facebookToken);
        Task<string> GetFbUserAsync(string facebookToken);
        Task<string> FindUserFbRecord(string facebookToken);
        Task<Response> UpdateUserFirebaseToken(string token, string firebaseToken);
        Task<Response> GetFirebaseIdByUserId(string userId);
        Task<bool> ApplicationServerPing(string _userId);
        Task<bool> GetPhoneNumberVerificationStatus(string id);
        Task<Response> GetUserByFacebookId(SocialAccountVm socialAccountVm);
        Task<bool> GetUserBlockStatus(string userId);
        Task<Response> GetUserByGoogleId(string googleId);
        Task<Response> GetUserPinStatus(string role,string emailOrPhone);
        Task<Response> GetUserByAppleId(string appleId,Role role);
        Task<GetAccountsTypeVM> GetSupplierAcountsType(string userId);
    }

    public class IdentityService : IIdentityService
    {
        HttpClient _httpClient = new HttpClient();
        private readonly IHttpClientService httpClient;
        private readonly ClientCredentials clientCredentials;
        private readonly IUserManagementService userManagementService;
        private readonly ICommunicationService communicationService;
        private readonly ApiConfig _apiConfig;
        private readonly IExceptionService Exc;

        public IdentityService(IHttpClientService httpClient, ClientCredentials clientCredentials, IExceptionService Exc, IUserManagementService userManagementService, ICommunicationService communicationService, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            this.clientCredentials = clientCredentials;
            this.userManagementService = userManagementService;
            this.communicationService = communicationService;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }

        public async Task<Utility.Response> Login(LoginVM model)
        {
            var response = new Utility.Response();
            Response res = new Response();
            UserIdVM userVm = new UserIdVM();
            string userId = string.Empty;
            string EntityId = string.Empty;

            try
            {
                string loginJsonString = string.Empty;
                string EntityIdJson = string.Empty;

                switch (model.Role.ToLower())
                {
                    case "tradesman":
                        model.Role = "1";

                        userVm = JsonConvert.DeserializeObject<UserIdVM>( await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.Login}", model));
                        EntityIdJson = await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetEntityIdByUserId}?userId={userVm.Id}");
                        EntityId = JsonConvert.DeserializeObject<string>(EntityIdJson);
                        model.Role = "Tradesman";
                        break;
                    case "organization":
                        model.Role = "2";
                        userVm = JsonConvert.DeserializeObject<UserIdVM>(await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.Login}", model));
                        EntityIdJson = await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetEntityIdByUserId}?userId={userVm.Id}");
                        EntityId = JsonConvert.DeserializeObject<string>(EntityIdJson);
                        model.Role = "Organization";
                        break;
                    case "customer":
                        model.Role = "3";
                        userVm = JsonConvert.DeserializeObject<UserIdVM>(await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.Login}", model));
                        EntityIdJson = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetEntityIdByUserId}?userId={userVm.Id}");
                        EntityId = JsonConvert.DeserializeObject<string>(EntityIdJson);
                        model.Role = "Customer";
                        break;
                    case "supplier":
                        model.Role = "4";
                        userVm = JsonConvert.DeserializeObject<UserIdVM>(await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.Login}", model));
                        EntityIdJson = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetEntityIdByUserId}?userId={userVm.Id}");
                        EntityId = JsonConvert.DeserializeObject<string>(EntityIdJson);
                        model.Role = "Supplier";
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrWhiteSpace(userVm.Id) && !string.IsNullOrWhiteSpace(EntityId))
                {
                    res = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.UpdateLastActiveLogin}?userId={userId}"));

                   // var disco = await DiscoveryClient.GetAsync(_apiConfig.IdentityServerApiUrl); .net 2.2

                   var disco = await _httpClient.GetDiscoveryDocumentAsync(_apiConfig.IdentityServerApiUrl);

                    if (disco.IsError)
                    {
                        Exc.AddErrorLog(new Exception(disco.Error));
                        response.Message = "LoginFailed";
                        response.ResultData = disco.IsError;
                    }
                    else
                    {
                        //var extra = new Dictionary<string, string> { { "firebaseClientId", model.FirebaseClientId }, { "role", model.Role }, { "entityId", EntityId }, { "userId", userId } };
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
                                { "entityId", EntityId },
                                { "userId", userVm.Id }
                            }
                        });
                        //asif shakir code

                        //var extra = new Dictionary<string, string> { { "firebaseClientId", userVm.FirebaseClientId == null ? "" : userVm.FirebaseClientId }, { "role", model.Role }, { "entityId", EntityId }, { "userId", userVm.Id } };
                        //var tokenClient = new TokenClient(disco.TokenEndpoint, clientCredentials.ClientId, clientCredentials.Secret, null, AuthenticationStyle.PostValues);
                        //var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync($"{model.EmailOrPhoneNumber}", model.Password, "api1", extra);

                        if (!tokenResponse.IsError)
                        {
                            response.ResultData = tokenResponse.AccessToken;
                            response.Status = ResponseStatus.OK; 
                            response.Message = "Login successful.";
                        }
                        else
                        {
                            response.Status = ResponseStatus.Error;
                            response.Message = "Invalid PIN";
                        }
                    }
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Login failed.";
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<string> CheckEmailandPhoneNumberAvailability(UserRegisterVM data)
        {
            try
            {
                return await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.CheckEmailandPhoneNumberAvailability}", data);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<string> RegisterUser(UserRegisterVM data)
        {
            try
            {
                var responseJson = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.RegisterUser}", data);
                var response = JsonConvert.DeserializeObject<Utility.Response>(responseJson);

                if (response.Status == ResponseStatus.OK)
                {
                    var emailVM = new EmailVM()
                    {
                        EmailAddresses = new List<string> { data.Email },
                        Subject = "Welcome to Hoomwork",
                        Body = "Hi !\nThanks so much for signing up with Hoomwork! You have joined an amazing community."
                    };

                    await communicationService.SendContactEmail(emailVM);
                }

                return responseJson;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<string> ForgotPassword(ForgotPasswordVM data)
        {
            try
            {
                return await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.ForgotPassword}", data);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<string> ResetPassword(ResetPasswordVM data)
        {
            return await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.ResetPassword}", data);
        }

        public async Task<string> GetUserIdByEmailOrPhoneNumber(string emailOrPhoneNumber, Role userRoles)
        {
            try
            {
                return await httpClient
                    .GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserIdByEmailOrPhoneNumber}?emailOrPhoneNumber={emailOrPhoneNumber}&userRoles={userRoles}");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<string> IsUserVerified(string userId)
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.IsUserVerified}?userId={userId}", "");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<string> GetUserByUserId(string userId)
        {
            try
            {
                var test = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={userId}", "");
                return test;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }
        public async Task<string> GetCitybyToken(string userId)
        {
            try
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(
                            await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByUserId}?userId={userId}")
                        );
                return customer.CityId.ToString();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<string> ValidateFacebookToken(string facebookToken)
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.ValidateFacebookToken}?facebookToken={facebookToken}");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<string> GetFbUserAsync(string facebookToken)
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetFbUserAsync}?facebookToken={facebookToken}");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<string> FindUserFbRecord(string facebookToken)
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.FindUserFbRecord}?facebookToken={facebookToken}");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<Response> UpdateUserFirebaseToken(string token, string firebaseToken)
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.UpdateUserFirebaseToken}?userId={token}&firebaseToken={firebaseToken}")
                );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response()
                {
                    Message = ex.Message,
                    ResultData = null,
                    Status = ResponseStatus.Error
                };
            }
        }

        public async Task<Response> GetFirebaseIdByUserId(string userId)
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetFirebaseIdByUserId}?userId={userId}")
                );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response()
                {
                    Message = ex.Message,
                    ResultData = null,
                    Status = ResponseStatus.Error
                };
            }
        }

        public async Task<bool> ApplicationServerPing(string _userId)
        {
            return JsonConvert.DeserializeObject<bool>(
                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.ApplicationServerPing}?userId={_userId}"));
        }

        public async Task<bool> GetPhoneNumberVerificationStatus(string id)
        {
            bool isVerified = false;

            try
            {
                isVerified = JsonConvert.DeserializeObject<bool>(
                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetPhoneNumberVerificationStatus}?userId={id}")
                );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return isVerified;
        }

        public async Task<Response> GetUserPinStatus(string role, string emailOrPhone)
        {
            Response response;

            try
            {
                response = JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserPinStatus}?role={role}&emailOrPhone={emailOrPhone}")
                );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                response = new Response()
                {
                    Message = ex.Message,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public async Task<Response> GetUserByFacebookId(SocialAccountVm socialAccountVm)
        {
            try
            {
                Exc.AddErrorLog(socialAccountVm.ToString());
                return JsonConvert.DeserializeObject<Response>(
                    await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByFacebookId}", socialAccountVm)
                );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response()
                {
                    Message = ex.Message,
                    ResultData = null,
                    Status = ResponseStatus.Error
                };
            }
        }

        public async Task<Response> GetUserByGoogleId(string googleId)
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByGoogleId}?googleId={googleId}")
                );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response()
                {
                    Message = ex.Message,
                    ResultData = null,
                    Status = ResponseStatus.Error
                };
            }
        }

        public async Task<bool> GetUserBlockStatus(string userId)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserBlockStatus}?userId={userId}"));
                //return userStatus;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public async Task<Response> GetUserByAppleId(string appleId, Role role)
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>(
                await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByAppleId}?appleId={appleId}&role={role}")
                );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<GetAccountsTypeVM> GetSupplierAcountsType(string userId)
        {
            try
            {
                GetAccountsTypeVM getAccountsTypeVM = new GetAccountsTypeVM();
                var aa= await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetSupplierAcountsType}?userId={userId}");
                getAccountsTypeVM= JsonConvert.DeserializeObject<GetAccountsTypeVM>(aa);
                return getAccountsTypeVM;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new GetAccountsTypeVM();
            }
        }
    }
}
