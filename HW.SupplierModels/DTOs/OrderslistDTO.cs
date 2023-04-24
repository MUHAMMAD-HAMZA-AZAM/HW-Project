using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class OrderslistDTO
    {
       
        public long? CustomerId { get; set; }
        public long? OrderId { get; set; }
        public string City { get; set; }
        public string CustomerName { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }

    }
}
