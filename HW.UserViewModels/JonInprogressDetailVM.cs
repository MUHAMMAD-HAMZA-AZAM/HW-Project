using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
   public class InprogressJobDetailVM
    {
        public long JobQuotationId { get; set; }
        public long TradesmanId { get; set; }
        public string TradesmanName { get; set; }
        public string CatagoryName { get; set; }
        public long? CustomerId { get; set; }
        public long? JobDetailId { get; set; }
        public string SubCatagoryName { get; set; }
        public string WorkTitle { get; set; }
        public string WorkDescription { get; set; }
        public decimal? WorkBudget { get; set; }
        public DateTime? WorkStartDate { get; set; }
        public bool DirectPayment { get; set; }
        public string CityName { get; set; }
        public string Town { get; set; }
        public string StreetAddress { get; set; }
        public int DesiredBids { get; set; }
        public bool SelectiveTradesman { get; set; }
        public List<ImageVM> ImageList { get; set; }
        public VideoVM Videos { get; set; }
        public int? PaymentStatus { get; set; }
        public bool? IsFinished { get; set; }
    }
}
