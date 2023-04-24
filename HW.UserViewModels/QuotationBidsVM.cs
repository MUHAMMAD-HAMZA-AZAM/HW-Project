using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class QuotationBidsVM
    {
        public long BidId { get; set; }
        public int TotalDoneJob { get; set; }
        public int? Rating { get; set; }
        public string JobQuotationTitle { get; set; }
        public byte[] TradesmanImage { get; set; }
        public DateTime BidOn { get; set; }
        public DateTime JobPostedOn { get; set; }
        public DateTime MemberSince { get; set; }
        public long TradesmanId { get; set; }
        public string BidBy { get; set; }
        public long? BidAmount { get; set; }
        public string TradesmanAddress { get; set; }
        public bool IsSelected { get; set; }
        public decimal? VisitCharges { get; set; }
        public decimal? ServiceCharges { get; set; }
        public decimal? OtherCharges { get; set; }
        public bool? IsOrganization { get; set; }
        public string CompanyName { get; set; }
    }
}
