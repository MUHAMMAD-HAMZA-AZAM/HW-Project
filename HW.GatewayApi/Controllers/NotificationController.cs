using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.NotificationModels;
using HW.NotificationViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HW.GatewayApi.Controllers
{
    [Produces("application/json")]
    public class NotificationController : BaseController
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.notificationService = notificationService;
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Customer, UserRoles.Organization })]
        public async Task<int> NotificationCallRequest(long jobQuotationId, long customerId)
        {
            return await notificationService.NotificationCallRequest(await GetEntityIdByUserId(), DecodeTokenForUser(), jobQuotationId, customerId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<bool> PostBidAcceptanceNotification(long tradesmanId, long bidId, string jobTitle)
        {
            return await notificationService.PostBidAcceptanceNotification(await GetEntityIdByUserId(), tradesmanId, bidId, jobTitle);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<bool> RequestFeedbackNotification(long jobDetailId)
        {
            return await notificationService.RequestFeedbackNotification(jobDetailId);
        }

        [HttpGet]
        [Permission(new string[] { UserRoles.Supplier })]
        public async Task<bool> PromoteAdNotification(string adTitle)
        {
            return await notificationService.PromoteAdNotification(await GetEntityIdByUserId(), adTitle);
        }

        [HttpGet]
        [Permission(new string[] { UserRoles.Customer})]
        public async Task<bool> NotificationRateSupplier(long supplierId, int overallRating)
        {
            return await notificationService.NotificationRateSupplier(await GetEntityIdByUserId(), supplierId, overallRating);
        }

        [HttpGet]
       // [Permission(new string[] { UserRoles.Customer })]
        public async Task<bool> NotificationRatingTradesman(long tradesmanId, int overallRating, long jobDetailId)
        {
            return await notificationService.NotificationRatingTradesman(await GetEntityIdByUserId(), tradesmanId, overallRating, jobDetailId);
        }

        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<PostNotificationVM>> GetNotifications(int pageNumber)
        {
            UserRegisterVM user = DecodeTokenForUser();
            return await notificationService.GetNotifications(user.Id, user.Role, pageNumber);
        }

        public async Task<Response> GetNotificationsCount()
        {
            UserRegisterVM user = DecodeTokenForUser();
            return await notificationService.GetNotificationsCount(user.Id, user.Role);
        }
        public async Task<Response> GetHWMallNotificationsCount()
        {
            UserRegisterVM user = DecodeTokenForUser();
            return await notificationService.GetHWMallNotificationsCount(user.Id, user.Role);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<bool> NotificationJobCostUpdate(long jobDetailId)
        {
            return await notificationService.NotificationJobCostUpdate(jobDetailId);
        }

        [HttpGet]
       // [Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> NotificationBidDecline(long bidId, string jobTitle)
        {
            UserRegisterVM user = DecodeTokenForUser();
            return await notificationService.NotificationBidDecline(bidId,jobTitle,await GetEntityIdByUserId(), user.Id);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> NotificationJobUpdate(long jobQuotationId)
        {
            return await notificationService.NotificationJobUpdate(jobQuotationId, await GetEntityIdByUserId());
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer, UserRoles.Supplier, UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<NotificationLogging> GetNotificationLogByNotificationId(long _notificationId)
        {
            return await notificationService.GetNotificationLogByNotificationId(_notificationId);
        }

        [HttpPost]
        //[Permission(new string[] { UserRoles.Customer, UserRoles.Supplier, UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<bool> UpdateNotificationLogByNotificationId([FromBody]NotificationLogging _notificationLogging)
        {
            return await notificationService.UpdateNotificationLogByNotificationId(_notificationLogging);
        }
        public async Task<List<PostNotificationVM>> GetNotificationsForOrders(string userId)
        {
            return await notificationService.GetNotificationsForOrders(userId);
        }
    }
}