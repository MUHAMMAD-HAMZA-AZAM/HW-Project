using System;
using System.Collections.Generic;

namespace HW.TradesmanViewModels
{
    public class BidDetailVM
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string UserId { get; set; }
        public string  CustomerPhoneNumber { get; set; }
        public string CustomerProfileImage { get; set; }
        public long JobDetailId { get; set; }
        public long JobQuotationId { get; set; }
        public string CustomerEmail { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public List<ImageVM> JobImages { get; set; }
        public string LatLong { get; set; }
        public VideoVM Video { get; set; }
        public decimal Budget { get; set; }
        public decimal? TradesmanBid { get; set; }
        public string ExpectedJobStartDate { get; set; }
        public string ExpectedJobStartTime { get; set; }
        public string TradesmanMessage { get; set; }
        public AudioVM AudioMessage { get; set; }
        public string JobLocation { get; set; }
        public string JobAddress { get; set; }
        public string JobAddressLine { get; set; }
        public DateTime PostedOn { get; set; }
        public string CustomerAddress { get; set; }
        public string UserAddress { get; set; }
        public long BidId { get; set; }
        public int StatusId { get; set; }
        public bool? IsFinished { get; set; }
        public string FirebaseClientId { get; set; }
    }
}
