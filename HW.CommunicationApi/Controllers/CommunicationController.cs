using System.Collections.Generic;
using System.Threading.Tasks;
using Hw.EmailViewModel;
using HW.CommunicationApi.Services;
using HW.CommunicationModels;
using HW.CommunicationViewModels;
using HW.EmailViewModel;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;

namespace HW.CommunicationApi.Controllers
{
    public class CommunicationController : BaseController
    {
        private readonly ISmsService smsService;
        private readonly IEmailService emailService;


        public CommunicationController(ISmsService smsService, IEmailService emailService)
        {
            this.smsService = smsService;
            this.emailService = emailService;
        }

        public string Start()
        {
            return "Communication API started.";
        }

        public async Task<bool> SendSms([FromBody] SmsVM smsVM)
        {
            return await smsService.SendSmsAsync(smsVM);
        }
        public async Task<bool> SendRegisterUserSms([FromBody] SmsVM smsVM)
        {
            return await smsService.SendRegisterUserSms(smsVM);
        }

        public bool SendEmail([FromBody] EmailVM emailVM)
        {
            return emailService.SendEmail(emailVM, 0);
        }

        public bool SendEmailJobPost([FromBody] PostJobEmailVM data)
        {
            return emailService.SendEmailJobPost(data);
        }

        public bool SendWellcomeEmail([FromBody] WellcomeEmailVM wellcomeEmailVM)
        {
            return emailService.SendWellcomeEmail(wellcomeEmailVM);
        }

        public bool SendWellcomeEmailTradesman([FromBody] WellcomeEmailVM wellcomeEmailVM)
        {
            return emailService.SendWellcomeEmailTradesman(wellcomeEmailVM);
        }

        public bool SendOtpEmail([FromBody] EmailOTPVM emailOTPVM)
        {
            return emailService.SendOtpEmail(emailOTPVM);
        }

        public bool TradesmanBidEmail([FromBody] TradesmanBidEmail data)
        {
            return emailService.TradesmanBidEmail(data);
        }

        public void SupplierWelcomeEmail([FromBody] PostJobEmailVM data)
        {
            emailService.SupplierWelcomeEmail(data);
        }

        public bool PostAdEmail([FromBody] PostSupplierAdsEmail data)
        {
            return emailService.PostAdEmail(data);
        }

        public bool NewJobPosted([FromBody] NewJobPosted newJobPosted)
        {
            return emailService.NewJobPosted(newJobPosted);
        }
        public bool SendLoginConfirmationEmail([FromBody] ContactEmailVm newJobPosted)
        {
            return emailService.SendLoginConfirmationEmail(newJobPosted);
        }

        public bool SendContactEmail([FromBody] ContactEmailVm contactEmail)
        {
            return emailService.SendContactEmail(contactEmail);
        }

        public bool SendFeedBackEmail([FromBody] List<FeedBackEmailVM> feedBackEmailVM)
        {
          return emailService.SendFeedBackEmail(feedBackEmailVM);
        }
        // Save and update method is not used u can use it for anypurpose
        public long SaveAndUpdateEmail([FromBody] Email email)
        {
            return emailService.SaveEmail(email);
        }

        public bool SaveUpdateInappChat([FromBody] InappChat chat)
        {
            //return emailService.SaveUpdateInappChat(chat);
            return true;
        }

        public int NumberOfRetries([FromQuery] long emailId)
        {
            return emailService.NumberOfRetries(emailId);
        }

        public bool AdminForgotPasswordAuthenticationEmail([FromBody] AdminForgotEmail adminForgotEmail)
        {
            return emailService.AdminForgotPasswordAuthenticationEmail(adminForgotEmail);
        }

        public async Task<Response> PostRequestCallBack([FromBody] CallRequest callRequestModel )
        {
            return await smsService.PostRequestCallBack(callRequestModel);
        } 

        [HttpGet]
        public  List<CallRequestVM> RequestCallBacksList()
        {
            return   emailService.RequestCallBacksList();
        }

        [HttpGet]
        public Response DeleteRequestCaller(long requestCallerId)
        {
            return  emailService.DeleteRequestCaller(requestCallerId);
        }
        [HttpGet]
        public Response SendTestEmail()
        {
            return emailService.SendTestEmail();
        }
    }
}
