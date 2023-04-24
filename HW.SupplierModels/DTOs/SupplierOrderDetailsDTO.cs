using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
   public class SupplierOrderDetailsDTO
    {
        public long OrderId { get; set; }
        public long OrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string Title { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Commission { get; set; }
        public decimal TotalPayable { get; set; }
        public bool IsPaymentModeConfirm { get; set; }
        public string CustomerName { get; set; }
    }
}
