using HW.NotificationApi.Services;
using HW.NotificationModels;
using HW.NotificationViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.NotificationApi.Controllers
{
    [Produces("application/json")]
    public class NotificationController : BaseController
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService _notificationService)
        {
            notificationService = _notificationService;
        }

        public string Start()
        {
            return "Notification service is started.";
        }

        [HttpPost]
        public async Task<bool> PostTopicNotification([FromBody]PostNotificationVM postNotificationVM)
        {
            return await notificationService.PostTopicNotification(postNotificationVM);
        }

        [HttpPost]
        public async Task<bool> PostDataNotification([FromBody]PostNotificationVM postNotificationVM)
        {
            return await notificationService.PostDataNotification(postNotificationVM);
        }
        [HttpPost]
        public async Task SaveNotificationDataWeb([FromBody]PostNotificationVM postNotificationVM)
        {
            await notificationService.SaveNotificationDataWeb(postNotificationVM);
        }

        #region Get Notification Data
        
        public IQueryable<NotificationLogging> GetNotifications(string userId, string role, int pageNumber)
        {
            return notificationService.GetNotifications(userId, role, pageNumber);
        }

        public List<NotificationLogging> GetAdminNotifications(int pageSize, int pageNumber,string userId)
        {
          return notificationService.GetAdminNotifications(pageSize, pageNumber, userId);
        }        
        public List<PostNotificationVM> GetNotificationsByUserId(int pageSize, int pageNumber,string userId)
        {
          return notificationService.GetNotificationsByUserId(pageSize, pageNumber, userId);
        }
        public Response MarkNotificationAsRead(int notificationId)
        {
          return notificationService.MarkNotificationAsRead(notificationId);
        }
        public double GetNotificationsCount(string userId, string role)
        {
            return notificationService.GetNotificationsCount(userId, role);
        }
        public double GetHWMallNotificationsCount(string userId, string role)
        {
            return notificationService.GetHWMallNotificationsCount(userId, role);
        }

        public NotificationLogging GetNotificationLogByNotificationId(long _notificationId)
        {
            return notificationService.GetNotificationLogByNotificationId(_notificationId);
        }

        [HttpPost]
        public bool UpdateNotificationLogByNotificationId([FromBody]NotificationLogging _notificationLogging)
        {
            return notificationService.UpdateNotificationLogByNotificationId(_notificationLogging);
        }
        public  List<NotificationLogging> GetNotificationsForOrders(string userId)
        {
            return  notificationService.GetNotificationsForOrders(userId);
        }
        #endregion
    }
}
