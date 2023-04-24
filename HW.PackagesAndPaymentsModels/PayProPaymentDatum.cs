using System;
using System.Collections.Generic;

#nullable disable

namespace HW.PackagesAndPaymentsModels
{
    public partial class PayProPaymentDatum
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderAmount { get; set; }
        public string Description { get; set; }
        public string Click2Pay { get; set; }
        public string ConnectPayId { get; set; }
        public string FetchOrderType { get; set; }
        public string BillUrl { get; set; }
        public string ParityAmount { get; set; }
        public bool? IsParity { get; set; }
        public string PayProId { get; set; }
        public string IsFeeApplied { get; set; }
        public string PayProFee { get; set; }
        public int? OrderExpireAfterSeconds { get; set; }
        public bool? Bafcharge { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
