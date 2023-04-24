using HW.CommunicationModels;
using HW.CommunicationViewModels;
using HW.EmailViewModel;
using HW.Http;
using HW.IdentityViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HW.Utility.ClientsConstants;

namespace HW.GatewayApi.Services
{
    public interface ICommunicationService
    {
        Task<bool> SendContactEmail(EmailVM emailVM);
        Task<bool> SendSmsAsync(SmsVM smsVM);
        Task<bool> RegisterUserSms(SmsUsersVM smsUsers);
        Task<bool> SendWellcomeEmail(WellcomeEmailVM wellcomeEmailVM);
        Task<bool> SendOtpEmail(EmailOTPVM emailOTPVM);
        Task<bool> SendWellcomeEmailTradesman(WellcomeEmailVM wellcomeEmailVM);
        Task<Response> SendCrashSMS(string id);
        Task<bool> SaveUpdateInappChat(InappChatVM chatVM, string userId);
        Task<Response> PostRequestCallBack(CallRequestVM callRequestVM);
        Task<List<CallRequestVM>> RequestCallBacksList();
        Task<Response> DeleteRequestCaller(long requestCallerId);
        Task<Response> SendTestEmail();
    }

    public class CommunicationService : ICommunicationService
    {
        private readonly IHttpClientService httpClient;
        private readonly ApiConfig _apiConfig;
        private readonly IExceptionService Exc;

        public CommunicationService(IHttpClientService httpClient, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }

        public async Task<Response> PostRequestCallBack(CallRequestVM callRequestVM)
        {
            try
            {
                CallRequest callRequest = new CallRequest()
                {
                    Name = callRequestVM.Name,
                    PhoneNumber = callRequestVM.PhoneNumber,
                    CreatedOn = DateTime.Now,
                    IsActive= callRequestVM.IsActive
                };
                Response responce = JsonConvert.DeserializeObject<Response>
                       (await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.PostRequestCallBack}", callRequest));
                return responce;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }


        }

        public async  Task<List<CallRequestVM>> RequestCallBacksList()
        {
            try
            {
                List<CallRequestVM> requestCallBackList = JsonConvert.DeserializeObject<List<CallRequestVM>>(await httpClient.GetAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.RequestCallBacksList}",""));
                return requestCallBackList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<CallRequestVM>();
            }
        }

        public async Task<Response> DeleteRequestCaller(long requestCallerId) 
        {
            try
            {
                Response responce = JsonConvert.DeserializeObject<Response>
                      (await httpClient.GetAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.DeleteRequestCaller}?requestCallerId={requestCallerId}"));
                return responce;
            }

            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<bool> SaveUpdateInappChat(InappChatVM chatVM, string userId)
        {
            try
            {
                InappChat inappChat = new InappChat() 
                {
                    ChatKey = chatVM.ChatKey,
                    ChatRoom = chatVM.ChatRoom,
                    Message = chatVM.Message,
                    StatusId = chatVM.StatusId,
                    UserRoleId = chatVM.UserRoleId,
                    DateSent = chatVM.DateSent,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now
                };


                bool responce = JsonConvert.DeserializeObject<bool>
                    (await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SaveUpdateInappChat}", inappChat));

                return responce;
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public async Task<Response> SendCrashSMS(string id)
        {
            Response response = JsonConvert.DeserializeObject<Response>(
                await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={id}")
            );

            if (response.Status != ResponseStatus.Error)
            {
                UserRegisterVM userRegisterVM = JsonConvert.DeserializeObject<UserRegisterVM>(response?.ResultData.ToString());

                SmsVM smsVM = new SmsVM()
                {
                    Message = Loadingmessages.AppCrashMsg,
                    MobileNumber = userRegisterVM.PhoneNumber
                };

                bool result = JsonConvert.DeserializeObject<bool>(
                    await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendSms}", smsVM)
                );
            }

            return response;
        }

        public async Task<bool> SendContactEmail(EmailVM emailVM)
        {
            try
            {
                ContactEmailVm contactEmailVm = new ContactEmailVm()
                {
                    Subject = emailVM.Subject,
                    FullName = emailVM.Name,
                    EmailAddress = emailVM.EmailAddresses[0],
                    Message = emailVM.Body,
                    PhoneNumber = emailVM.Phone,
                    Email = new Email
                    {
                        CreatedBy = "Send To Help Desk.",
                        Retries = 0,
                        IsSend = false
                    }

                };
                bool response = JsonConvert.DeserializeObject<bool>(
                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendContactEmail}", contactEmailVm)
                    );
                return response;
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }
        }

        public async Task<bool> SendOtpEmail(EmailOTPVM emailOTPVM)
        {
            try
            {
                var responce = JsonConvert.DeserializeObject<bool>(
                       await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendOtpEmail}", emailOTPVM)
                   );
                return responce;
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }
        }

        public async Task<bool> SendSmsAsync(SmsVM smsVM)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<bool>(
                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendSms}", smsVM)
                    );
                return response;
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }
        }

        public async Task<bool> RegisterUserSms(SmsUsersVM smsUsers)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<bool>(
                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendRegisterUserSms}", smsUsers)
                    );
                return response;
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }
        }

        public async Task<bool> SendWellcomeEmail(WellcomeEmailVM wellcomeEmailVM)
        {
            try
            {
                var responce = JsonConvert.DeserializeObject<bool>(
                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendWellcomeEmail}", wellcomeEmailVM)
                    );
                return responce;
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }
        }

        public async Task<bool> SendWellcomeEmailTradesman(WellcomeEmailVM wellcomeEmailVM)
        {
            try
            {
                var responce = JsonConvert.DeserializeObject<bool>(
                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendWellcomeEmailTradesman}", wellcomeEmailVM)
                    );
                return responce;
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }
        }

        public async Task<Response> SendTestEmail()
        {
            try
            {
                Response responce = JsonConvert.DeserializeObject<Response>
                      (await httpClient.GetAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendTestEmail}"));
                return responce;
            }

            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
    }
}
