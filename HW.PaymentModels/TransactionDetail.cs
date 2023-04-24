using System;
using System.Collections.Generic;

namespace HW.PaymentModels
{
    public partial class TransactionDetail
    {
        public long TransactionDetailId { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string ReferenceNo { get; set; }
        public long? BillReference { get; set; }
        public string MerchantId { get; set; }
        public string SubMerchantId { get; set; }
        public string RetreivalReferenceNo { get; set; }
        public string Currency { get; set; }
        public DateTime? InitiateTime { get; set; }
        public string TransactionType { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
