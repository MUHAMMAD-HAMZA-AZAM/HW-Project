using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class InventoryDTO
    {
        public int SupplierId { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }
      }
}
