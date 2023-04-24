using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
    public class TransactionDetailVM
    {
        public string UserId { get; set; }
        public long BidId { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string ReferenceNo { get; set; }
        public string MerchantId  { get; set; }
        public string SubMerchantId { get; set; }
        public string RetreivalReferenceNo { get; set; }
        public string Currency { get; set; }
        public string InitiateTime { get; set; }
        public string TransactionType { get; set; }
    }
}
