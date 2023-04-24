using HW.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace HW.NotificationViewModels
{
    public class PostNotificationVM
    {
        public long NotificationId { get; set; }
        public string Title { get; set; }
        public bool? isFromWeb { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string SenderEntityId { get; set; }
        public string TargetActivity { get; set; }
        public string  SenderUserId { get; set; }
        public string TragetUserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public TargetDatabase TargetDatabase { get; set; }
        public long? JobReferenceId { get; set; }
        public int? JobRetries { get; set; }
        public int? unreadNotifictionsRecord { get; set; }
        public string JobAbortReason{ get; set; }
        public bool? IsAborted { get; set; }
        public bool? IsRead { get; set; }
        public byte[] ProfileImage { get; set; }
        public List<string> TradesmenList { get; set; }
    }
}
