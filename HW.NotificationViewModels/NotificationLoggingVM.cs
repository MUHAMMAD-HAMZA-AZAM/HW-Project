using System;
using System.Collections.Generic;
using System.Text;

namespace HW.NotificationViewModels
{
   public class NotificationLoggingVM
    {
        public long NotificationId { get; set; }
        public string Title { get; set; }
        public string ReciverId { get; set; }
        public string NotificationType { get; set; }
        public string SenderId { get; set; }
        public bool? IsNotificationRecived { get; set; }
        public bool? IsNotificationSent { get; set; }
        public DateTime SuccessfullySentAt { get; set; }
        public string SenderRole { get; set; }
        public string ReciverRole { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
