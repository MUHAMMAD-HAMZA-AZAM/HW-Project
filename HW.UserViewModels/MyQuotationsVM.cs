using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class MyQuotationsVM
    {
        public string WorkTitle { get; set; }
        public string PostedBy { get; set; }
        public string SkillName { get; set; }
        public string WorkingAddress { get; set; }
        public int TotalRecords { get; set; }
        public string JobStatus { get; set; }
        public DateTime PostedDate { get; set; }
        public int? BidCount { get; set; }
        public int? CallCount { get; set; }
        public long JobQuotationId { get; set; }
        public DateTime? WorkStartDate{ get; set; }
        public byte[] JobImage { get; set; }
        public string TradesmanName { get; set; }
        public decimal TradesmanOffer { get; set; }
        public int? JQStatusId { get; set; }

    }
}
