using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class MediaImageVM
    {
        public long BidImageId { get; set; }
        public long JobQuotationId { get; set; }
        public string FileName { get; set; }
        public byte[] Images { get; set; }
        public bool IsMain { get; set; }
    }
}
