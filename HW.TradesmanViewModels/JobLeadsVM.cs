using System;

namespace HW.TradesmanViewModels
{
    public class JobLeadsVM
    {
        //JobQuotesId
        //JobTitle    
        //    Budget 
        //    PostedOn    
        //    BidCount 
        //    FileName    
        //    Address 
        //    City

        public string JobTitle { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? WorkStartDate { get; set; }
        public string WorkStartTime { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Budget { get; set; }
        //public byte[] JobImage { get; set; }
        public string FileName { get; set; }
        public int BidCount { get; set; }
        public long JobQuotesId { get; set; }
    }
    public class JobLeadsWebVM
    {
        //JobQuotesId
        //JobTitle    
        //    Budget 
        //    PostedOn    
        //    BidCount 
        //    FileName    
        //    Address 
        //    City

        public string JobTitle { get; set; }
        public DateTime PostedOn { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Budget { get; set; }
        //public byte[] JobImage { get; set; }
        public Byte[] BidImage { get; set; }
        public int BidCount { get; set; }
        public long JobQuotesId { get; set; }
    }
}