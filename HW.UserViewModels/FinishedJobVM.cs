using System;

namespace HW.UserViewModels
{
    public class FinishedJobVM
    {
        public long JobDetailId { get; set; }
        public long TradesmanId { get; set; }
        public long JobQuotationId { get; set; }
        public byte[] JobImage { get; set; }
        public string FileName { get; set; }
        public string JobTitle { get; set; }
        public string TradesmanName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string TradesmanEmail { get; set; }
        public string MobileNumber { get; set; }
        public string Town { get; set; }
        public DateTime JobEndTime { get; set; }
        public int? Rating { get; set; }
        public Decimal Payment { get; set; }
    }
}