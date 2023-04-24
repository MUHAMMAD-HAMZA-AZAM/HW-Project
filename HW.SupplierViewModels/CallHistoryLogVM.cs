using System;

namespace HW.SupplierViewModels
{
    public class CallHistoryLogVM
    {
        public byte[] CallerImage { get; set; }
        public string CallerName { get; set; }
        public DateTime CallTime { get; set; }
        public int? CallDuration { get; set; }
        public string CallType { get; set; }
        public long CustomerId { get; set; }
        public long SupplierId { get; set; }
        public long SupplierCallLogId { get; set; }
        public bool IsSelected { get; set; }
    }
}
