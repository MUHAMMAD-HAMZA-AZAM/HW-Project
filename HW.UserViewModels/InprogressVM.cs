using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
   public class InprogressVM
    {
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string TradesmanName { get; set; }
        public long ProfileImageId { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public long? TradesmanId { get; set; }
        public long JobQuotationId { get; set; }
        public long Getcount { get; set; }
        public long BidsId { get; set; }
        public long? JobDetailId { get; set; }
        public decimal? TradesmanOffer { get; set; }
        public byte[] JobImage { get; set; }
        public bool? IsFinished { get; set; }


    }
}
