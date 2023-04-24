using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class MediaImagesVM
    {
        public long BidImageId { get; set; }
        public long JobQuotationId { get; set; }
        public string FileName { get; set; }
        public byte[] Images { get; set; }
        public bool IsMain { get; set; }
    }
}
