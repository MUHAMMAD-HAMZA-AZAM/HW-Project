using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class OrderUpdateDTO
    {
        public int OrderId { get; set; }
        public int OrderStatus { get; set; }
        public int SupplierId { get; set; }
        public long? CustomerId { get; set; }
        public bool? isFromWeb { get; set; }
        public string FirebaseClientId { get; set; }

    }
}
