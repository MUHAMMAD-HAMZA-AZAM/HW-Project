using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.PackagesAndPaymentsApi.DTO
{
    public class UpdateShippingChargesAndPaymentStatusDTO
    {
        public long? SupplierId { get; set; }
        public long? OrderDetailId { get; set; }
        public long? ItemId { get; set; }
        public long? VariantId { get; set; }
        public decimal ShippingAmount { get; set; }
        public bool PaymentReceivedStatus { get; set; }
        public bool DispatchPaymentStatus { get; set; }
    }
}
