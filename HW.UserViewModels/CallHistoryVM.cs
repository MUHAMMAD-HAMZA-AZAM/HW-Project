using System;

namespace HW.UserViewModels
{
    public class CallHistoryVM
    {
        public byte[] CallerImage { get; set; }
        public string CallerName { get; set; }
        public DateTime CallTime { get; set; }
        public int? CallDuration { get; set; }
        public string CallType { get; set; }
        public long CustomerId { get; set; }
        public long TradesmanId { get; set; }
        public long CallLogId { get; set; }
        public bool IsSelected { get; set; }
    }
}
