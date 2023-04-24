using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class JobDetailVM
    {
        public long JobDetailId { get; set; }
        public long BidId { get; set; }
       // public byte[] JobImage { get; set; }
        public string FileName { get; set; }
        public string WorkTitle { get; set; }
        public long JobQuotationId { get; set; }
        public string WorkAddress { get; set; }
        public string JobStartedDate { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public long CustomerId { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string LastName { get; set; }
        public int Rating { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string StreetAddress { get; set; }
        public bool? IsFinished { get; set; }
    }
    public class JobDetailWebVM
    {
        public long JobDetailId { get; set; }
        public Byte[] BidImage { get; set; }
       // public string FileName { get; set; }
        public string WorkTitle { get; set; }
        public long JobQuotationId { get; set; }
        public string WorkAddress { get; set; }
        public string JobStartedDate { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string LastName { get; set; }
        public int Rating { get; set; }
        public string JobCreater { get; set; }
        public decimal? WorkBudget { get; set; }
        public long? CustomerId { get; set; }

    }
}
