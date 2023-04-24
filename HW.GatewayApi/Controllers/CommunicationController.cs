using System.Collections.Generic;
using System.Threading.Tasks;
using HW.CommunicationViewModels;
using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;

namespace HW.GatewayApi.Controllers
{
    [Produces("application/json")]
    public class CommunicationController : BaseController
    {
        private readonly ICommunicationService communicationService;

        public CommunicationController(ICommunicationService communicationService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.communicationService = communicationService;
        }

        [HttpPost]
        public async Task<bool> SendContactEmail([FromBody] EmailVM emailVM)
        {
            return await communicationService.SendContactEmail(emailVM);
        }

        [HttpGet]
        public async Task<Response> SendCrashSMS()
        {
            UserRegisterVM userVM = DecodeTokenForUser();
            return await communicationService.SendCrashSMS(userVM?.Id);
            //return await communicationService.SendCrashSMS("f5398165-ab10-4e9f-8327-950bf4a4209c");
        }

        [HttpPost]
        public async Task<bool> SaveUpdateInappChat([FromBody] InappChatVM chatVM)
        {
            return await communicationService.SaveUpdateInappChat(chatVM, DecodeTokenForUser().Id);
        }

        [HttpPost]
        public async Task<bool> SendSmsAsync([FromBody] SmsVM smsVM)
        {
            return await communicationService.SendSmsAsync(smsVM);
        }

        [HttpPost]
        public async Task<bool> RegisterUserSms([FromBody] SmsUsersVM smsUsers)
        {
            return await communicationService.RegisterUserSms(smsUsers);
        }

        [HttpPost]
        public async Task<Response> PostRequestCallBack([FromBody] CallRequestVM callRequestVM)
        {
            return await communicationService.PostRequestCallBack(callRequestVM);
        }

        [HttpGet]
        public async Task<List<CallRequestVM>> RequestCallBacksList()
        {
            return await communicationService.RequestCallBacksList();
        }

        [HttpGet]
        public async Task<Response> DeleteRequestCaller( long requestCallerId)
        {
            return await communicationService.DeleteRequestCaller(requestCallerId);
        }
        [HttpGet]
        public async Task<Response> SendTestEmail()
        {
            return await communicationService.SendTestEmail();
        }


    }
}