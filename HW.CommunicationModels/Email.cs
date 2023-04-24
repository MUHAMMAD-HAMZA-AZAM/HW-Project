using System;
using System.Collections.Generic;

namespace HW.CommunicationModels
{
    public partial class Email
    {
        public long EmailId { get; set; }
        public string RecieverEmailId { get; set; }
        public string SenderEmailId { get; set; }
        public bool IsSend { get; set; }
        public DateTime? SentOn { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public int? Retries { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? RetriedDate { get; set; }
        public DateTime? NextRetriedDate { get; set; }
        public string CcEmails { get; set; }
        public string BccEmail { get; set; }
        public int? SentFor { get; set; }
    }
}
