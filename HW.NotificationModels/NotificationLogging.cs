using System;
using System.Collections.Generic;

namespace HW.NotificationModels
{
    public partial class NotificationLogging
    {
        public long NotificationLoggingId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public byte Type { get; set; }
        public string PayLoad { get; set; }
        public bool? IsRecived { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public bool? IsAborted { get; set; }
        public bool? IsSent { get; set; }
        public string ReasonToAbort { get; set; }
        public int? Retries { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
  }
}
