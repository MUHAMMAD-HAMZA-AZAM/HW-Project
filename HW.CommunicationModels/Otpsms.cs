using System;
using System.Collections.Generic;

namespace HW.CommunicationModels
{
    public partial class Otpsms
    {
        public long SmsId { get; set; }
        public string Message { get; set; }
        public string RecieverMobileNumber { get; set; }
        public bool? IsSent { get; set; }
        public int? Retries { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? SentOn { get; set; }
    }
}
